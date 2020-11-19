using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codenesium.DataConversionExtensions;
using Npgsql;

namespace Codenesium.DatabaseContracts
{
    public class PostgresqlInterface : IDatabaseInterface
    {
        private string _connectionString;

        public void SetConnectionString(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public string CreateConnectionString(string instance, string database, string username, string password)
        {
            return $"User ID={username};Password={password};Server={instance};Database={database};";
        }

		public string CreateConnectionStringUsingWindowsAuthentication(string instance, string database)
		{
			throw new NotImplementedException("Windows auth is not supoprted on Postgres.");
		}
		public List<string> GetDatabaseList()
        {
            List<string> response = new List<string>();
            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(@"SELECT datname FROM pg_database WHERE datistemplate = false;", conn);

                command.CommandTimeout = 300;

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    response.Add(reader["datname"].ToString());
                }
            }
            return response;
        }

        public DatabaseContainer GetDatabaseStructure()
        {
            Microsoft.Data.SqlClient.SqlConnectionStringBuilder builder =
              new Microsoft.Data.SqlClient.SqlConnectionStringBuilder(this._connectionString);

            DatabaseContainer container = new DatabaseContainer(builder.InitialCatalog, DatabaseContainer.DatabaseTypePostgreSQL);

            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                List<Constraint> databaseConstraints = this.GetIndexesForDatabase();
                //List<DefaultConstraint> databaseDefaultConstraints = GetDefaultConstraintsForDatabase();
                List<ForeignKey> databaseForeignKeys = this.GetForeignKeysForDatabase();

                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(@"select  
                ist.TABLE_NAME,
                COLUMN_NAME,
                DATA_TYPE,
                NUMERIC_PRECISION,
                CHARACTER_MAXIMUM_LENGTH,
                IS_NULLABLE,
                IS_IDENTITY,
                ist.table_schema as Schema,
                ist.TABLE_TYPE
                FROM  INFORMATION_SCHEMA.Columns isc
                LEFT JOIN INFORMATION_SCHEMA.Tables ist on ist.table_name = isc.table_name AND
                ist.TABLE_SCHEMA = isc.TABLE_SCHEMA
                WHERE 
                ist.table_schema NOT IN ('pg_catalog','information_schema') AND 
                ist.TABLE_NAME <> '__EFMigrationsHistory' AND
                (ist.TABLE_TYPE = 'BASE TABLE' OR ist.TABLE_TYPE = 'VIEW')", conn);

                command.CommandTimeout = 300;
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string table = reader["TABLE_NAME"].ToString();
                    string schema = reader["SCHEMA"].ToString();

                    Column column = new Column();
                    column.Name = reader["COLUMN_NAME"].ToString();
                    column.DataType = reader["DATA_TYPE"].ToString();
                    column.NumericPrecision = String.IsNullOrEmpty(reader["NUMERIC_PRECISION"].ToString()) ? 0 : Convert.ToInt32(reader["NUMERIC_PRECISION"]);
                    column.MaxLength = String.IsNullOrEmpty(reader["CHARACTER_MAXIMUM_LENGTH"].ToString()) ? 0 : Convert.ToInt32(reader["CHARACTER_MAXIMUM_LENGTH"]);
                    column.IsNullable = reader["IS_NULLABLE"].ToString() == "YES" ? true : false;
                    bool isComputed = false; ///Convert.ToBoolean(reader["IS_COMPUTED"].ToString());
                    bool isRowGuidColumn = false;// Convert.ToBoolean(reader["is_rowguidcol"].ToString());
                    bool isIdentity = reader["is_identity"].ToString().ToBoolean();

                    //if (isComputed || isRowGuidColumn || isIdentity || column.DataType.ToUpper() == "TIMESTAMP")
                    //{
                    //    column.DatabaseGenerated = true;
                    //}

                    Schema existingSchema = container.Schemas.FirstOrDefault(x => x.Name == schema);

                    if (existingSchema == null)
                    {
                        Schema newSchema = new Schema
                        {
                            Name = schema,
                            ForeignKeys = databaseForeignKeys.Where(f => f.Columns.Any(c => c.ForeignKeySchemaName == schema)).ToList()
                        };
                        Table existingTable = newSchema.Tables.FirstOrDefault(x => x.Name == table);
                        if (existingTable == null)
                        {
                            Table newTable = new Table();
                            newTable.Name = table;
                            string tableType = reader["TABLE_TYPE"].ToString();
                            if (tableType.ToUpper() == "VIEW")
                            {
                                newTable.IsView = true;
                            }
                            newTable.Constraints = databaseConstraints.Where(c => c.SchemaName == schema && c.TableName == table).ToList();
                            //newTable.DefaultConstraints = databaseDefaultConstraints.Where(c => c.SchemaName == schema && c.TableName == table).ToList();
                            newTable.Columns.Add(column);
                            newSchema.Tables.Add(newTable);
                        }
                        else
                        {
                            existingTable.Columns.Add(column);
                        }
                        container.Schemas.Add(newSchema);
                    }
                    else
                    {
                        Table existingTable = existingSchema.Tables.FirstOrDefault(x => x.Name == table);
                        if (existingTable == null)
                        {
                            Table newTable = new Table();
                            newTable.Name = table;
                            string tableType = reader["TABLE_TYPE"].ToString();
                            if (tableType.ToUpper() == "VIEW")
                            {
                                newTable.IsView = true;
                            }
                            newTable.Columns.Add(column);
                            newTable.Constraints = databaseConstraints.Where(c => c.SchemaName == schema && c.TableName == table).ToList();
                            //newTable.DefaultConstraints = databaseDefaultConstraints.Where(c => c.SchemaName == schema && c.TableName == table).ToList();
                            existingSchema.Tables.Add(newTable);
                        }
                        else
                        {
                            existingTable.Columns.Add(column);
                        }
                    }
                }
            }
            return container;
        }

        public List<ForeignKey> GetForeignKeysForDatabase()
        {
            //TODO:Add filtering by schema
            List<ForeignKey> response = new List<ForeignKey>();
            string query = @"SELECT
            FK.TABLE_NAME as FK_Table,
            CU.COLUMN_NAME as FK_Column,
            PK.TABLE_NAME as PK_Table,
            PT.COLUMN_NAME as PK_Column,
            C.CONSTRAINT_NAME as Constraint_Name,
            FK.TABLE_SCHEMA as ForeignKeySchema,
 	        PK.TABLE_SCHEMA as PrimaryKeySchema,
	        CU.ORDINAL_POSITION
        FROM
            INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS C
        INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS FK
            ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
        INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS PK
            ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS CU
            ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
        INNER JOIN (
                    SELECT
                        i1.TABLE_NAME,
				        i1.CONSTRAINT_SCHEMA,
                        i2.COLUMN_NAME,
		     	        i2.ORDINAL_POSITION
                    FROM
                        INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS i1
                    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS i2
                        ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
                    WHERE
                        i1.CONSTRAINT_TYPE = 'PRIMARY KEY'
                   ) PT
            ON PT.TABLE_NAME = PK.TABLE_NAME
	        AND PT.CONSTRAINT_SCHEMA = PK.CONSTRAINT_SCHEMA
	        AND PT.ORDINAL_POSITION =  CU.ORDINAL_POSITION
            AND PT.TABLE_NAME <> '__EFMigrationsHistory' 
	        ORDER BY  FK.TABLE_SCHEMA,FK.TABLE_NAME,CU.ORDINAL_POSITION";

            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, conn);

                command.CommandTimeout = 300;

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ForeignKeyColumn column = new ForeignKeyColumn
                    {
                        ForeignKeyTableName = reader["FK_Table"].ToString(),
                        ForeignKeyColumnName = reader["FK_Column"].ToString(),
                        PrimaryKeyTableName = reader["PK_Table"].ToString(),
                        PrimaryKeyColumnName = reader["PK_Column"].ToString(),
                        ForeignKeySchemaName = reader["ForeignKeySchema"].ToString(),
                        PrimaryKeySchemaName = reader["PrimaryKeySchema"].ToString(),
                        Order = reader["ORDINAL_POSITION"].ToString().ToInt()
                    };

                    ForeignKey key = new ForeignKey();
                    key.Columns.Add(column);
                    key.ForeignKeyName = reader["Constraint_Name"].ToString();

                    // we already have a key with this name. Add the column to its collection
                    if (response.Any(x => x.ForeignKeyName == key.ForeignKeyName))
                    {
                        response.First(x => x.ForeignKeyName == key.ForeignKeyName).Columns.Add(column);
                    }
                    else
                    {
                        response.Add(key);
                    }
                }
            }
            return response;
        }

        public List<Constraint> GetIndexesForDatabase()
        {
            List<Constraint> response = new List<Constraint>();
            // this query is incomplete. amcanorder was removed from pg_catalog.pg_am in the latest versions of PostGres
            // And I don't know where to get the index sort direction
            string query = @"SELECT tnsp.nspname AS SchemaName,
        ct.relname AS TableName, 
        replace(pg_catalog.pg_get_indexdef(ci.oid, (i.keys).n, false),'""','') AS ColumnName, 
        i.indisunique As IsUnique,
        i.indisclustered As IsClustered,
        i.indisprimary As IsPrimaryKey,
       ci.relname AS IndexName, 
       (i.keys).n AS ColumnOrder, 
       am.amname as IndexType,
--       CASE am.amcanorder 
--         WHEN true THEN CASE i.indoption[(i.keys).n - 1] & 1 
--           WHEN 1 THEN 'DESC' 
--           ELSE 'ASC' 
--         END 
--         ELSE NULL 
--       END AS ASC_OR_DESC,
      pg_catalog.pg_get_expr(i.indpred, i.indrelid) AS FILTER_CONDITION 
FROM pg_catalog.pg_class ct 
  JOIN pg_catalog.pg_namespace n ON (ct.relnamespace = n.oid) 
  JOIN (SELECT i.indexrelid, i.indrelid, i.indoption, 
          i.indisunique, i.indisclustered, i.indpred, 
          i.indisprimary,
          i.indexprs, 
          information_schema._pg_expandarray(i.indkey) AS keys 
        FROM pg_catalog.pg_index i) i 
    ON (ct.oid = i.indrelid) 
  JOIN pg_catalog.pg_class ci ON (ci.oid = i.indexrelid) 
  JOIN pg_catalog.pg_am am ON (ci.relam = am.oid) 
  JOIN pg_class AS trel ON trel.oid = i.indrelid
  JOIN pg_namespace AS tnsp ON trel.relnamespace = tnsp.oid
ORDER BY ct.relname,  (i.keys).n";

            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, conn);
                command.CommandTimeout = 300;
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Constraint index = new Constraint
                    {
                        Name = reader["IndexName"].ToString(),
                        ConstraintType = reader["IsClustered"].ToString().ToBoolean() ? "CLUSTERED" : "NONCLUSTERED",
                        IsPrimaryKey = Convert.ToBoolean(reader["IsPrimaryKey"].ToString()),
                        SchemaName = reader["SchemaName"].ToString(),
                        TableName = reader["TableName"].ToString(),
                        IsUnique = Convert.ToBoolean(reader["IsUnique"].ToString())
                    };

                    ConstraintColumn column = new ConstraintColumn
                    {
                        Name = reader["ColumnName"].ToString(),
                        Order = Convert.ToInt32(reader["ColumnOrder"].ToString()),
                        Descending = false, // Convert.ToBoolean(reader["IsDescending"].ToString()),
                        IsIncludedColumn = false,// Convert.ToBoolean(reader["IsIncludedColumn"].ToString())
                        IsIdentity = Convert.ToBoolean(reader["IsPrimaryKey"].ToString())
                    };

                    if (response.Any(x => x.Name == index.Name && x.TableName == index.TableName && x.SchemaName == index.SchemaName))
                    {
                        response.FirstOrDefault(x => x.Name == index.Name && x.TableName == index.TableName && x.SchemaName == index.SchemaName).Columns.Add(column);
                    }
                    else
                    {
                        index.Columns.Add(column);
                        response.Add(index);
                    }
                }
            }
            return response;
        }


        public List<string> GetSchemaList()
        {
            List<string> response = new List<string>();
            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(@"cSELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA where SCHEMA_OWNER='dbo' AND SCHEMA_NAME NOT IN ('pg_catalog','information_schema') ORDER BY SCHEMA_NAME", conn);

                command.CommandTimeout = 300;

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    response.Add(reader["SCHEMA_NAME"].ToString());
                }
            }
            return response;
        }


        public List<string> GetTableList(string schema)
        {

            List<string> tables = new List<string>();
            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(@"SELECT TABLE_NAME FROM information_schema.tables
                WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA=@schema ORDER BY TABLE_NAME", conn);
                command.Parameters.AddWithValue("schema", schema);

                command.CommandTimeout = 300;

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tables.Add((string)reader["TABLE_NAME"]);
                }
            }
            return tables;
        }


        public bool TestConnection()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> TestConnectionAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
                    {
                        conn.Open();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
        public List<string> ColumnTypes
        {
            get
            {
                return new List<string>()
                {
                    "bigint",
                    "bigserial",
                    "bit",
                    "bit varying",
                    "bool",
                    "boolean",
                    "box",
                    "bytea",
                    "char",
                    "character",
                    "character varying",
                    "cidr",
                    "circle",
                    "date",
                    "decimal",
                    "double precision",
                    "float4",
                    "float8",
                    "inet",
                    "int",
                    "int2",
                    "int4",
                    "int8",
                    "integer",
                    "interval",
                    "json",
                    "jsonb",
                    "line",
                    "lseg",
                    "macaddr",
                    "money",
                    "numeric",
                    "path",
                    "pg_lsn",
                    "point",
                    "polygon",
                    "real",
                    "smallint",
                    "smallserial",
                    "serial",
                    "serial2",
                    "serial4",
                    "serial8",
                    "text",
                    "timetz",
                    "time with time zone",
                    "time without time zone",
                    "timstamptz",
                    "timestamp with time zone",
                    "timestamp without time zone",
                    "tsquery",
                    "tsvector",
                    "txid_snapshot",
                    "uuid",
                    "varchar",
                    "xml"
                };
            }
            set
            {

            }
        }
    }
}
