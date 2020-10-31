using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codenesium.DatabaseContracts.DataInterfaces;

namespace Codenesium.DatabaseContracts
{
    [Serializable]
    public class DatabaseContainer
    {
        public const string DatabaseTypeMSSQL = "MSSQL";
        public const string DatabaseTypePostgreSQL = "POSTGRESQL";
        public const string DatabaseTypeUnknown = "UNKNOWN";

        public string Name { get; private set; }

        public List<Schema> Schemas { get; set; } = new List<Schema>();

        public string Version { get; protected set; }

        public string DatabaseType { get; protected set; }

        public DatabaseContainer(string name, string databaseType, string version = "1.0")
        {
            this.Name = name;
            this.Version = version;
            this.DatabaseType = databaseType;
        }

        public virtual List<ForeignKey> GetForeignKeys()
        {
            List<ForeignKey> response = new List<ForeignKey>();
            this.Schemas.ForEach(x =>
            {
                response.AddRange(x.ForeignKeys);
            });
            return response;
        }

        public void TransformToMssql()
        {
            if (this.DatabaseType == DatabaseTypePostgreSQL)
            {
                this.DatabaseType = DatabaseTypeMSSQL;
                DatabaseMapperPostgresMssql databaseFieldMapper = new DatabaseMapperPostgresMssql();
                databaseFieldMapper.MapFromPostgresToMssql(this);
            }

        }

        public void TransformToPostgres()
        {
            if (this.DatabaseType == DatabaseTypeMSSQL)
            {
                this.DatabaseType = DatabaseTypePostgreSQL;
                DatabaseMapperPostgresMssql databaseFieldMapper = new DatabaseMapperPostgresMssql();
                databaseFieldMapper.MapFromMssqlToPostgres(this);
            }
        }

        public virtual void AddIsDeleted()
        {
            string isDeletedColumnName = this.IsUpperCaseColumnsNames() ? "IsDeleted" : "isDeleted";

            for (int i = 0; i < this.Schemas.Count; i++)
            {
                for (int t = 0; t < this.Schemas[i].Tables.Count; t++)
                {
                    if (this.Schemas[i].Tables[t].Name.ToUpper() != "TENANT")
                    {
                        if (!this.Schemas[i].Tables[t].Columns.Any(c => c.Name.ToUpper() == isDeletedColumnName.ToUpper()))
                        {
                            string columnType = string.Empty;

                            if(this.DatabaseType == DatabaseTypeMSSQL)
                            {
                                columnType = "bit";
                            }
                            else if (this.DatabaseType == DatabaseTypePostgreSQL)
                            {
                                columnType = "bool";
                            }
                            else
                            {
                                columnType = $"unknown_database_type_{this.DatabaseType}";
                            }

                            this.Schemas[i].Tables[t].Columns.Add(new Column()
                            {
                                DatabaseGenerated = false,
                                DataType = columnType,
                                IsNullable = false,
                                MaxLength = 0,
                                Name = isDeletedColumnName,
                                NumericPrecision = 0
                            });
                        }
                    }
                }
            }
        }

        public virtual void RemoveMultiTenancy()
        {
            this.RemoveColumnFromAllTables("tenantId");
            this.Schemas.ForEach(s =>
            {
                s.Tables.RemoveAll(t => t.Name.ToUpper() == "TENANT");
            });
        }

        public virtual void RemoveIsDeleted()
        {
            this.RemoveColumnFromAllTables("isDeleted");
        }

        public virtual void RemoveColumnFromAllTables(string name)
        {
            for (int i = 0; i < this.Schemas.Count; i++)
            {
                this.Schemas[i].Tables.ForEach(t =>
                {
                    t.Constraints.ForEach(c =>
                    {
                        c.Columns.RemoveAll(col => col.Name.ToUpper() == name.ToUpper());
                    });

                    t.DefaultConstraints.RemoveAll(col => col.Name.ToUpper() == name.ToUpper());

                    t.Columns.RemoveAll(col => col.Name.ToUpper() == name.ToUpper());
                });

                this.Schemas[i].ForeignKeys.ForEach(f =>
                {
                    f.Columns.RemoveAll(col => col.ForeignKeyColumnName.ToUpper() == name.ToUpper());
                });
                this.Schemas[i].ForeignKeys.RemoveAll(f => f.Columns.Count == 0);

            }
        }

        /// <summary>
        /// Is the user's database using upper case first letter 
        /// </summary>
        /// <returns></returns>
        private bool IsUpperCaseTableNames()
        {
            if (this.Schemas.Count > 0 && this.Schemas.First().Tables.Count > 0)
            {
                return char.IsUpper(this.Schemas.First().Tables.First().Name[0]);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Is the user's database using upper case first letter 
        /// </summary>
        /// <returns></returns>
        private bool IsUpperCaseColumnsNames()
        {
            if (this.Schemas.Count > 0 && this.Schemas.First().Tables.Count > 0 && this.Schemas.First().Tables.First().Columns.Count > 0)
            {
                return char.IsUpper(this.Schemas.First().Tables.First().Columns.First().Name[0]);
            }
            else
            {
                return false;
            }
        }
        public virtual void AddMultiTenancy()
        {
            string tenantTableName = this.IsUpperCaseTableNames() ? "Tenant" : "tenant";
            string tenantColumnName = this.IsUpperCaseColumnsNames() ? "TenantId" : "tenantId";

            for (int i = 0; i < this.Schemas.Count; i++)
            {
                // add the tenant table
                if (!this.Schemas[i].Tables.Any(t => t.Name.ToUpper() == tenantTableName.ToUpper()))
                {
                    Table tenantTable = new Table();
                    tenantTable.SetProperties(tenantTableName,
                        false,
                        new List<Column>()
                        {
                            new Column()
                            {
                                DatabaseGenerated = true,
                                DataType = "int",
                                IsNullable = false,
                                MaxLength = 0,
                                Name = "id",
                                NumericPrecision = 0
                            },
                            new Column()
                            {
                                DatabaseGenerated = false,
                                DataType = "varchar",
                                IsNullable = false,
                                MaxLength = 128,
                                Name = "name",
                                NumericPrecision = 0,
                            }
                        },
                        new List<Constraint>()
                        {
                            new Constraint()
                            {
                               ConstraintType = "CLUSTERED",
                               IsPrimaryKey = true,
                               IsUnique = true,
                               SchemaName = this.Schemas[i].Name,
                               TableName = tenantTableName,
                               Name = "PK_Tenant",
                               Columns = new List<ConstraintColumn>()
                               {
                                   new ConstraintColumn()
                                   {
                                       IsIdentity = true,
                                       IsIncludedColumn = false,
                                       Descending = false,
                                       Order = 0,
                                       Name = "id"
                                   }
                               }
                            }
                        },
                        new List<DefaultConstraint>());

                    this.Schemas[i].Tables.Add(tenantTable);

                }
            }


            // add the tenantId column to all tables except tenant table
            for (int i = 0; i < this.Schemas.Count; i++)
            {
                for (int t = 0; t < this.Schemas[i].Tables.Count; t++)
                {
                    if (this.Schemas[i].Tables[t].Name.ToUpper() != tenantTableName.ToUpper())
                    {
                        if (!this.Schemas[i].Tables[t].Columns.Any(c => c.Name.ToUpper() == tenantColumnName.ToUpper()))
                        {
                            this.Schemas[i].Tables[t].Columns.Add(new Column()
                            {
                                DatabaseGenerated = false,
                                DataType = "int",
                                IsNullable = false,
                                MaxLength = 0,
                                Name = tenantColumnName,
                                NumericPrecision = 0
                            });
                        }
                    }
                }
            }


            // add tenantId to the first position of all primary keys that are not table tenant
            for (int i = 0; i < this.Schemas.Count; i++)
            {
                for (int t = 0; t < this.Schemas[i].Tables.Count; t++)
                {
                    if (this.Schemas[i].Tables[t].Name.ToUpper() != tenantTableName.ToUpper())
                    {
                        var primaryKey = this.Schemas[i].Tables[t].Constraints.FirstOrDefault(x => x.IsPrimaryKey);

                        if (primaryKey != null)
                        {
                            primaryKey.Columns.RemoveAll(c => c.Name.ToUpper() == tenantColumnName.ToUpper());

                            primaryKey.Columns.Insert(0, new ConstraintColumn()
                            {
                                Descending = false,
                                IsIdentity = false,
                                IsIncludedColumn = false,
                                Order = 0,
                                Name = tenantColumnName
                            });
                        }
                    }
                }
            }


            // Add foreign keys from every table to tenant
            for (int i = 0; i < this.Schemas.Count; i++)
            {

                this.Schemas[i].ForeignKeys.RemoveAll(
                    f => f.Columns.Any(c => c.PrimaryKeyColumnName.ToUpper() == "TENANTID" &&
                   c.PrimaryKeySchemaName.ToUpper() == this.Schemas[i].Name &&
                   c.PrimaryKeyTableName.ToUpper() == "TENANT"));

                for (int t = 0; t < this.Schemas[i].Tables.Count; t++)
                {
                    if (this.Schemas[i].Tables[t].Name.ToUpper() != "TENANT")
                    {
                        this.Schemas[i].ForeignKeys.Add(new ForeignKey
                        {
                            ForeignKeyName = $"fk_{this.Schemas[i].Tables[t].Name}_tenantId_tenant_id",
                            Columns = new List<ForeignKeyColumn>()
                        {
                            new ForeignKeyColumn()
                            {
                                ForeignKeyColumnName = "tenantId",
                                ForeignKeySchemaName = this.Schemas[i].Name,
                                ForeignKeyTableName = this.Schemas[i].Tables[t].Name,
                                PrimaryKeyColumnName = "id",
                                PrimaryKeySchemaName = this.Schemas[i].Name,
                                PrimaryKeyTableName = "Tenant",
                                Order = 0
                            }
                        }
                        });
                    }
                }
            }

            // fix existing foreign keys
            for (int i = 0; i < this.Schemas.Count; i++)
            {
                for (int fk = 0; fk < this.Schemas[i].ForeignKeys.Count; fk++)
                {
                    if (!this.Schemas[i].ForeignKeys[fk].Columns.Any(c => c.ForeignKeyColumnName.ToUpper() == "TENANTID"))
                    {
                        this.Schemas[i].ForeignKeys[fk].Columns.Insert(0, new ForeignKeyColumn()
                        {
                            ForeignKeyColumnName = "tenantId",
                            ForeignKeySchemaName = this.Schemas[i].Name,
                            ForeignKeyTableName = this.Schemas[i].ForeignKeys[fk].Columns.First().ForeignKeyTableName,
                            PrimaryKeyColumnName = "tenantId",
                            PrimaryKeySchemaName = this.Schemas[i].Name,
                            PrimaryKeyTableName = this.Schemas[i].ForeignKeys[fk].Columns.First().PrimaryKeyTableName,
                            Order = 0
                        });
                    }
                }
            }
        }


        public virtual void DeletePrimaryKey(string schemaName, string tableName)
        {
            var schema = this.Schemas.First(x => x.Name.ToUpper() == schemaName.ToUpper());
            var table = schema.Tables.First(x => x.Name.ToUpper() == tableName.ToUpper());
            var primaryKey = table.Constraints.FirstOrDefault(x => x.IsPrimaryKey);

            if(primaryKey != null)
            {
                this.RemoveForeignKeyReferencesToTable(schemaName, tableName);
                table.Constraints.RemoveAll(c => c.IsPrimaryKey);
            }
        }

        private void RemoveForeignKeyReferencesToTable(string schemaName, string tableName)
        {
            var schema = this.Schemas.First(x => x.Name.ToUpper() == schemaName.ToUpper());
            var table = schema.Tables.First(x => x.Name.ToUpper() == tableName.ToUpper());
            for (int i = 0; i < this.Schemas.Count; i++)
            {
                this.Schemas[i].ForeignKeys.RemoveAll(x => x.Columns.Any(c =>
                    c.PrimaryKeyTableName.ToUpper() == table.Name.ToUpper() &&
                    c.PrimaryKeySchemaName.ToUpper() == schema.Name.ToUpper()
                    ));

                this.Schemas[i].ForeignKeys.RemoveAll(x => x.Columns.Any(c =>
                  c.ForeignKeyTableName.ToUpper() == table.Name.ToUpper() &&
                  c.ForeignKeySchemaName.ToUpper() == schema.Name.ToUpper()
                  ));
            }
        }

        public virtual void DeleteTable(string schemaName, string tableName)
        {
            this.RemoveForeignKeyReferencesToTable(schemaName, tableName);
            var schema = this.Schemas.First(x => x.Name.ToUpper() == schemaName.ToUpper());
            var table = schema.Tables.First(x => x.Name.ToUpper() == tableName.ToUpper());
            schema.Tables.Remove(table);
        }

        public virtual void DeleteSchema(string schemaName)
        {
            var schema = this.Schemas.First(x => x.Name.ToUpper() == schemaName.ToUpper());
            this.Schemas.Remove(schema);
        }

        public virtual void AddSchema(Schema schema, string originalName = "")
        {
            if(string.IsNullOrWhiteSpace(originalName))
            {
                this.Schemas.Add(schema);
            }
            else
            {
                for (int i = 0; i < this.Schemas.Count; i++)
                {
                   var matchingByPrimaryKeys = this.Schemas[i].ForeignKeys.Where(x => x.Columns.Any(c =>
                        c.PrimaryKeySchemaName.ToUpper() == originalName.ToUpper()
                        )).ToList();

                    var matchingByForeignKeys  = this.Schemas[i].ForeignKeys.Where(x => x.Columns.Any(c =>
                      c.ForeignKeySchemaName.ToUpper() == originalName.ToUpper()
                      )).ToList();

                    matchingByPrimaryKeys.ForEach(x =>
                    {
                        x.Columns.ForEach(c =>
                        {
                            c.PrimaryKeySchemaName = schema.Name;
                        });
                    });

                    matchingByForeignKeys.ForEach(x =>
                    {
                        x.Columns.ForEach(c =>
                        {
                            c.ForeignKeySchemaName = schema.Name;
                        });
                    });

                }
            }
        }

        public virtual DatabaseContainer Clone()
        {
            DatabaseContainer container = new DatabaseContainer(this.Name, this.DatabaseType, this.Version);

            this.Schemas.ForEach(x =>
            {
                container.Schemas.Add(x.Clone());
            });
            return container;
        }
    }
}