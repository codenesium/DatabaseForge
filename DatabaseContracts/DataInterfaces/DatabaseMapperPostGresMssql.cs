using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codenesium.DatabaseContracts.DataInterfaces
{
    public class DatabaseMapperPostgresMssql
    {

        public void MapFromMssqlToPostgres(DatabaseContainer container)
        {
            container.Schemas.ForEach(s =>
            {
                s.Tables.ForEach(t =>
                {
                    t.Columns.ForEach(c =>
                    {
                        c.DataType = this.MapMssqlTypeToPostgres(c.DataType);
                    });
                });
            });
        }

        public void MapFromPostgresToMssql(DatabaseContainer container)
        {
            container.Schemas.ForEach(s =>
            {

                s.Tables.ForEach(t =>
                {
                    t.Columns.ForEach(c =>
                    {
                        c.DataType = this.MapPostgresTypeToMssql(c.DataType);
                    });
                });
            });
        }

        public string MapPostgresTypeToMssql(string type)
        {
            switch (type.ToUpper())
            {
                case "BYTEA":
                    {
                        return "binary";
                    }
                case "BOOLEAN":
                    {
                        return "bit";
                    }
                case "CHARACTER":
                    {
                        return "char";
                    }
                case "CHARACTER VARYING":
                    {
                        return "varchar";
                    }
                case "INT2":
                    {
                        return "smallint";
                    }
                case "INT4":
                    {
                        return "integer";
                    }
                case "INT8":
                    {
                        return "bigint";
                    }
                case "TIMESTAMP":
                    {
                        return "datetime";
                    }
                case "TIMESTAMP WITH TIME ZONE":
                    {
                        return "datetimeoffset";
                    }
                case "TIMESTAMP WITHOUT TIME ZONE":
                    {
                        return "datetimeoffset";
                    }
                case "TIME WITH TIME ZONE":
                    {
                        return "time";
                    }
                case "TIME WITHOUT TIME ZONE":
                    {
                        return "time";
                    }
                case "SMALLINT":
                    {
                        return "smallint";
                    }
                case "UUID":
                    {
                        return "uniqueidentifier";
                    }
                default:
                    {
                        return type;
                    }
            }
        }

        public string MapMssqlTypeToPostgres(string type)
        {
            switch (type.ToUpper())
            {
                case "BINARY":
                    {
                        return "bytea";
                    }
                case "BIT":
                    {
                        return "boolean";
                    }
                case "DATETIME":
                    {
                        return "timestamp";
                    }
                case "DATETIME2":
                    {
                        return "timestamp";
                    }
                case "DATETIMEOFFSET":
                    {
                        return "timestamp with time zone";
                    }
                case "FLOAT":
                    {
                        return "double precision";
                    }
                case "IMAGE":
                    {
                        return "bytea";
                    }
                case "NCHAR":
                    {
                        return "char";
                    }
                case "NTEXT":
                    {
                        return "text";
                    }
                case "NVARCHAR":
                    {
                        return "varchar";
                    }
                case "ROWVERSION":
                    {
                        return "bytea";
                    }
                case "SMALLDATETIME":
                    {
                        return "timestamp";
                    }
                case "SMALLMONEY":
                    {
                        return "money";
                    }
                case "TIMESTAMP":
                    {
                        return "bytea";
                    }
                case "TINYINT":
                    {
                        return "smallint";
                    }
                case "UNIQUEIDENTIFIER":
                    {
                        return "uuid";
                    }
                case "VARBINARY":
                    {
                        return "bytea";
                    }
                default:
                    {
                        return type;
                    }
            }
        }
    }
}
