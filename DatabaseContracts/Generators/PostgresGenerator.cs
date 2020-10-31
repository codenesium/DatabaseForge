using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codenesium.DatabaseContracts;
using Codenesium.DatabaseContracts.DependencyResolver;

namespace Codenesium.DatabaseContracts
{
    public class PostgresGenerator : AbstractGenerator, ISQLGenerator
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
            sb.AppendLine($@"CREATE SCHEMA IF NOT EXISTS ""{schema.Name}"";");
            return sb.ToString();
        }

        public string GenerateDisableForeignKeyContraints(Schema schema)
        {
            StringBuilder sb = new StringBuilder();
            schema.ForeignKeys.ForEach(x =>
            {
                sb.AppendLine($@"--ALTER TABLE ""{x.Columns.First().ForeignKeySchemaName}"".""{x.Columns.First().ForeignKeyTableName}"" DISABLE TRIGGER ALL;");
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
                    foreignKeyColumns += $@"""{c.ForeignKeyColumnName}""";
                    if(c != orderedColumns.Last())
                    {
                        foreignKeyColumns += ",";
                    }
                    primaryKeyColumns += $@"""{c.PrimaryKeyColumnName}""";
                    if (c != orderedColumns.Last())
                    {
                        primaryKeyColumns += ",";
                    }
                });
                 
                sb.AppendLine($@"ALTER TABLE ""{key.Columns.First().ForeignKeySchemaName}"".""{key.Columns.First().ForeignKeyTableName}"" ADD CONSTRAINT ""{key.ForeignKeyName}"" FOREIGN KEY({foreignKeyColumns})");
                sb.AppendLine($@"REFERENCES ""{key.Columns.First().PrimaryKeySchemaName}"".""{key.Columns.First().PrimaryKeyTableName}"" ({primaryKeyColumns});");
            });
            return sb.ToString();
        }

        public string GenerateDropAllTables(Schema schema)
        {
            StringBuilder sb = new StringBuilder();
            schema.Tables.ForEach(x =>
            {
                sb.AppendLine($@"--DROP TABLE IF EXISTS ""{schema.Name}"".""{x.Name}"";");
            });
            return sb.ToString();
        }

        public string GenerateCreateTableStatement(string schema, Table table, List<ForeignKey> foreignKeys)
        {
            StringBuilder sb = new StringBuilder();
            List<Column> columnList = table.Columns;
            Constraint primaryKey = table.Constraints.FirstOrDefault(x => x.IsPrimaryKey && x.TableName == table.Name);

            if (primaryKey != null)
            {
                Column primaryKeyColumn = columnList.First(x => x.Name == primaryKey.Columns.First().Name);
                columnList.Remove(primaryKeyColumn);
                columnList.Insert(0, primaryKeyColumn);
            }



            sb.AppendLine($@"CREATE TABLE ""{schema}"".""{table.Name}""(");
            string statement = string.Empty;
            columnList.ForEach(column =>
            {
                bool isRowGuidColumn = table.DefaultConstraints.Any(x => x.IsRowGuid && x.ColumnName.ToUpper() == column.Name.ToUpper());
                string maxLengthString = String.Empty;

                maxLengthString = column.MaxLength > 0 ? $"({ column.MaxLength.ToString()})" : "";

                DatabaseContracts.Constraint identityConstraint = table.Constraints.FirstOrDefault(x =>
                x.SchemaName == schema &&
                x.TableName == table.Name &&
                x.Columns.FirstOrDefault(c => c.Name.ToUpper() != "TENANTID" && c.IsIdentity)?.Name == column.Name);

                string identityString = String.Empty;

                if (identityConstraint != null && column.Name.ToUpper() != "TENANTID")
                {
                    if (column.DataType.ToUpper() == "SMALLINT" || column.DataType.ToUpper() == "INT2" || column.DataType.ToUpper() == "SMALLSERIAL" || column.DataType.ToUpper() == "SERIAL2")
                    {
                        identityString = "SMALLSERIAL";
                    }
                    else if (column.DataType.ToUpper() == "INTEGER" || column.DataType.ToUpper() == "INT4" || column.DataType.ToUpper() == "SERIAL" || column.DataType.ToUpper() == "SERIAL4" || column.DataType.ToUpper() == "INT")
                    {
                        identityString = "SERIAL";
                    }
                    else if (column.DataType.ToUpper() == "BIGINT" || column.DataType.ToUpper() == "BIGSERIAL" || column.DataType.ToUpper() == "SERIAL8" || column.DataType.ToUpper() == "INT8")
                    {
                        identityString = "BIGSERIAL";
                    }

                }

                if(string.IsNullOrWhiteSpace(identityString))
                {
                    string nullString = column.IsNullable ? "NULL" : "NOT NULL";
                    statement += $@"""{column.Name}"" {column.DataType}  {maxLengthString} {identityString} {nullString},";
                    statement += Environment.NewLine;
                }
                else
                {
                    statement += $@"""{column.Name}""  {identityString} ,";
                    statement += Environment.NewLine;
                }

            });

            sb.Append(statement.Remove(statement.LastIndexOf("," + Environment.NewLine)));
            sb.AppendLine($");");

            return sb.ToString();
        }


        public string GenerateIndex(Constraint index)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"CREATE {(index.IsUnique ? "UNIQUE" : "")} INDEX ""{index.Name}"" ON ""{index.SchemaName}"".""{index.TableName}""");
            sb.AppendLine($"(");

            string columns = string.Empty;
            index.Columns.ForEach(x =>
            {
                columns += $@"""{x.Name}"" {(x.Descending ? "DESC" : "ASC")},";
                columns += Environment.NewLine;
            });

            sb.Append(columns.Remove(columns.LastIndexOf("," + Environment.NewLine)));
            sb.AppendLine($");");

            return sb.ToString();
        }
        public string GeneratePrimaryKeyConstraint(Constraint constraint)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"ALTER TABLE ""{constraint.SchemaName}"".""{constraint.TableName}""");
            sb.AppendLine($@"ADD CONSTRAINT ""{constraint.Name}""");
            sb.AppendLine($@"PRIMARY KEY");
            sb.AppendLine($"(");
            constraint.Columns.ForEach(x =>
            {
                sb.AppendLine($@"""{x.Name}""");
                if(x != constraint.Columns.Last())
                {
                    sb.Append(",");
                }
            });
            sb.AppendLine($");");
            return sb.ToString();
        }

        public string GenerateCreateDefaultValueConstraint(DefaultConstraint constraint)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"ALTER TABLE ""{constraint.SchemaName}"".""{constraint.TableName}""");
            sb.AppendLine($@"ADD CONSTRAINT ""{constraint.Name}""  DEFAULT {constraint.ColumnDefault} FOR ""{constraint.ColumnName}"";");
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
            sb.AppendLine($@"ALTER TABLE ""{constraint.SchemaName}"".""{constraint.TableName}"" DROP CONSTRAINT ""{constraint.Name}""");
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
