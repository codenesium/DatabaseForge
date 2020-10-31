using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codenesium.DatabaseContracts;
using Codenesium.DatabaseContracts.DependencyResolver;

namespace Codenesium.DatabaseContracts
{
    public class MssqlGenerator : AbstractGenerator, ISQLGenerator
    {

        public string GenerateCreateDatabase(DatabaseContainer database)
        {
            StringBuilder sb = new StringBuilder();
                
            database.Schemas.ForEach(schema =>
            {
                sb.AppendLine(GenerateCreateSchema(schema));
            });


            database.Schemas.ForEach(schema =>
            {
                sb.AppendLine(GenerateDisableForeignKeyContraints(schema));
            });

            database.Schemas.ForEach(schema =>
            {
                sb.AppendLine(GenerateDropAllTables(schema));
            });


            database.Schemas.ForEach(schema =>
            {
                schema.Tables.ForEach(table =>
                {
                    sb.AppendLine(GenerateCreateTableStatement(schema.Name, table, schema.ForeignKeys));
                });
            });


            List<Constraint> constraints = new List<Constraint>();
            database.Schemas.ForEach(schema =>
            {
                schema.Tables.ForEach(table =>
                {
                    constraints.AddRange(table.Constraints);
                });
            });
            sb.AppendLine(GenerateIndexes(constraints));

            List<DefaultConstraint> defaultConstraints = new List<DefaultConstraint>();
            database.Schemas.ForEach(schema =>
            {
                schema.Tables.ForEach(table =>
                {
                    defaultConstraints.AddRange(table.DefaultConstraints);
                });
            });
            sb.AppendLine(GenerateCreateDefaultValueConstraints(defaultConstraints));


            database.Schemas.ForEach(schema =>
            {
                sb.AppendLine(GenerateForeignKeyConstraints(schema));
            });

            return sb.ToString();
        }

        public string GenerateCreateSchema(Schema schema)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"IF NOT EXISTS(SELECT *");
            sb.AppendLine($"FROM sys.schemas");
            sb.AppendLine($"WHERE name = N'{schema.Name}')");
            sb.AppendLine($"EXEC('CREATE SCHEMA [{schema.Name}] AUTHORIZATION [dbo]');");
            sb.AppendLine($"GO");
            return sb.ToString();
        }

        public string GenerateDisableForeignKeyContraints(Schema schema)
        {
            StringBuilder sb = new StringBuilder();
            schema.ForeignKeys.ForEach(x =>
            {
                sb.AppendLine($"--IF (OBJECT_ID('{x.Columns.First().ForeignKeySchemaName}.{x.ForeignKeyName}', 'F') IS NOT NULL)");
                sb.AppendLine($"--BEGIN");
                sb.AppendLine($"--ALTER TABLE [{x.Columns.First().ForeignKeySchemaName}].[{x.Columns.First().ForeignKeyTableName}] DROP CONSTRAINT [{x.ForeignKeyName}]");
                sb.AppendLine($"--END");
                sb.AppendLine($"--GO");
            });
            return sb.ToString();
        }

        public string GenerateForeignKeyConstraints(Schema schema)
        {
            StringBuilder sb = new StringBuilder();
            schema.ForeignKeys.ForEach(key =>
            {

                string foreignKeyColumns = string.Empty;
                string primaryKeyColumns = string.Empty;

                List<ForeignKeyColumn> orderedColumns = key.Columns.OrderBy(x => x.Order).ToList();

                orderedColumns.ForEach(c =>
                {
                    foreignKeyColumns += $"[{c.ForeignKeyColumnName}]";
                    if(c != orderedColumns.Last())
                    {
                        foreignKeyColumns += ",";
                    }
                    primaryKeyColumns += $"[{c.PrimaryKeyColumnName}]";
                    if (c != orderedColumns.Last())
                    {
                        primaryKeyColumns += ",";
                    }
                });
                 
                sb.AppendLine($"ALTER TABLE[{key.Columns.First().ForeignKeySchemaName}].[{key.Columns.First().ForeignKeyTableName}]  WITH CHECK ADD  CONSTRAINT[{key.ForeignKeyName}] FOREIGN KEY({foreignKeyColumns})");
                sb.AppendLine($"REFERENCES[{key.Columns.First().PrimaryKeySchemaName}].[{key.Columns.First().PrimaryKeyTableName}]({primaryKeyColumns}) on delete no action on update no action");
                sb.AppendLine($"GO");
                sb.AppendLine($"ALTER TABLE[{key.Columns.First().ForeignKeySchemaName}].[{key.Columns.First().ForeignKeyTableName}] CHECK CONSTRAINT[{key.ForeignKeyName}]");
                sb.AppendLine($"GO");
            });
            return sb.ToString();
        }

        public string GenerateDropAllTables(Schema schema)
        {
            StringBuilder sb = new StringBuilder();
            schema.Tables.ForEach(x =>
            {
                sb.AppendLine($"--IF OBJECT_ID('{schema.Name}.{x.Name}', 'U') IS NOT NULL ");
                sb.AppendLine($"--BEGIN");
                sb.AppendLine($"--DROP TABLE [{schema.Name}].[{x.Name}]");
                sb.AppendLine($"--END");
                sb.AppendLine($"--GO");
            });
            return sb.ToString();
        }

        public string GenerateCreateTableStatement(string schema, Table table, List<ForeignKey> foreignKeys)
        {
            StringBuilder sb = new StringBuilder();
            List<Column> columnList = table.Columns;
            Constraint primaryKey = table.Constraints.FirstOrDefault(x => x.IsPrimaryKey && x.TableName == table.Name);
            if(primaryKey!= null)
            {
                Column primaryKeyColumn = columnList.First(x => x.Name == primaryKey.Columns.First().Name);
                columnList.Remove(primaryKeyColumn);
                columnList.Insert(0, primaryKeyColumn);
            }

            sb.AppendLine($"CREATE TABLE [{schema}].[{table.Name}](");
            columnList.ForEach(column =>
            {
                bool isRowGuidColumn = table.DefaultConstraints.Any(x => x.IsRowGuid && x.ColumnName.ToUpper() == column.Name.ToUpper());
                string maxLengthString = String.Empty;

                if (column.DataType.ToUpper() == "HIERARCHYID")
                {
                    maxLengthString = "";
                }
                else if(column.MaxLength > 8000)
                {
                    maxLengthString = "";
                }
                else
                {
                    maxLengthString = column.MaxLength > 0 ? $"({ column.MaxLength.ToString()})" : "";
                }

                string rowGuid = String.Empty;

                if (isRowGuidColumn)
                {
                    rowGuid = "ROWGUIDCOL";
                }

                DatabaseContracts.Constraint identityConstraint = table.Constraints.FirstOrDefault(x =>
                x.SchemaName == schema &&
                x.TableName == table.Name &&
                x.Columns.FirstOrDefault(c => c.Name.ToUpper() != "TENANTID" && c.IsIdentity)?.Name == column.Name);

                string identityString = String.Empty;

                if (identityConstraint != null && column.Name.ToUpper() != "TENANTID")
                {
                    if(column.DataType.ToUpper() == "INT" || column.DataType.ToUpper() == "BIGINT")
                    {
                        identityString = "IDENTITY(1,1)";
                    }
                }
                
                string nullString = column.IsNullable ? "NULL" : "NOT NULL";
                sb.AppendLine($"[{column.Name}] [{column.DataType}]  {maxLengthString} {identityString} {rowGuid} {nullString},");
            });
            sb.AppendLine($") ON[PRIMARY]");
            sb.AppendLine($"GO");

            return sb.ToString();
        }


        public string GenerateIndex(Constraint index)
        {
            if(index.ConstraintType.ToUpper() == "XML")
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"CREATE {(index.IsUnique ? "UNIQUE" : "")} {index.ConstraintType} INDEX[{index.Name}] ON[{index.SchemaName}].[{index.TableName}]");
            sb.AppendLine($"(");

            string columns = string.Empty;
            index.Columns.ForEach(x =>
            {
                if (!x.IsIncludedColumn)
                {
                    columns += $"[{x.Name}] {(x.Descending ? "DESC" : "ASC")},";
                    columns += Environment.NewLine;
                }
            });

            sb.Append(columns.Remove(columns.LastIndexOf("," + Environment.NewLine)));
            sb.AppendLine($")");

            string includedColumns = string.Empty;
            if (index.Columns.Any(x => x.IsIncludedColumn))
            {
                sb.AppendLine($"INCLUDE(");
                index.Columns.ForEach(x =>
                {
                    if (x.IsIncludedColumn)
                    {
                        includedColumns += $"[{x.Name}],";
                        includedColumns += Environment.NewLine;
                    }
                });
                sb.Append(includedColumns.Remove(includedColumns.LastIndexOf("," + Environment.NewLine)));
                sb.AppendLine($")");
            }



            sb.AppendLine($"WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)");
            sb.AppendLine($"GO");
            return sb.ToString();
        }
        public string GeneratePrimaryKeyConstraint(Constraint constraint)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"ALTER TABLE[{constraint.SchemaName}].[{constraint.TableName}]");
            sb.AppendLine($"ADD CONSTRAINT[{constraint.Name}] PRIMARY KEY {constraint.ConstraintType}");
            sb.AppendLine($"(");
            constraint.Columns.ForEach(x =>
            {
                sb.AppendLine($"[{x.Name}] {(x.Descending ? "DESC" : "ASC")}");
                if(x != constraint.Columns.Last())
                {
                    sb.Append(",");
                }
            });
            sb.AppendLine($")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF,  ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)");
            sb.AppendLine($"GO");
            return sb.ToString();
        }

        public string GenerateCreateDefaultValueConstraint(DefaultConstraint constraint)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"ALTER TABLE[{constraint.SchemaName}].[{constraint.TableName}]");
            sb.AppendLine($"ADD CONSTRAINT[{constraint.Name}]  DEFAULT{constraint.ColumnDefault} FOR[{constraint.ColumnName}]");
            sb.AppendLine($"GO");
            return sb.ToString();
        }

        public string GenerateCreateDefaultValueConstraints(List<DefaultConstraint> constraints)
        {
            StringBuilder sb = new StringBuilder();
            constraints.ForEach(constraint =>
            {
                sb.AppendLine(this.GenerateCreateDefaultValueConstraint(constraint));
            });
           
            return sb.ToString();
        }

        public string GenerateDropDefaultValueConstraint(DefaultConstraint constraint)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"ALTER TABLE [{constraint.SchemaName}].[{constraint.TableName}] DROP CONSTRAINT [{constraint.Name}]");
            return sb.ToString();
        }


        public string GenerateIndexes(List<Constraint> indexes)
        {
            StringBuilder sb = new StringBuilder();
            indexes.ForEach(index =>
            {
                if (index.IsPrimaryKey)
                {
                    sb.Append(GeneratePrimaryKeyConstraint(index));
                }
                else
                {
                    sb.Append(GenerateIndex(index));
                }
            });
            return sb.ToString();
        }
    }
}
