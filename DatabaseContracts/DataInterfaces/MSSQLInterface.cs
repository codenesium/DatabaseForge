using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Codenesium.DataConversionExtensions;


namespace Codenesium.DatabaseContracts
{
    public class MSSQLInterface : IDatabaseInterface
    {
        private string _connectionString;

        public void SetConnectionString(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public string CreateConnectionString(string instance, string database, string username, string password)
        {
            return $"Server={instance};Persist Security Info=False;User ID={username};Password={password};Initial Catalog={database};MultipleActiveResultSets=True;Connection Timeout=10;";
        }

		public string CreateConnectionStringUsingWindowsAuthentication(string instance, string database)
		{
			return $"Server={instance};Persist Security Info=False;;Integrated Security=SSPI;persist security info=True;Initial Catalog={database};MultipleActiveResultSets=True;Connection Timeout=10;";
		}

		public DatabaseContainer GetDatabaseStructure()
        {
            Microsoft.Data.SqlClient.SqlConnectionStringBuilder builder =
                new Microsoft.Data.SqlClient.SqlConnectionStringBuilder(this._connectionString);

            DatabaseContainer container = new DatabaseContainer(builder.InitialCatalog, DatabaseContainer.DatabaseTypeMSSQL);

            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                List<Constraint> databaseConstraints = GetIndexesForDatabase();
                List<DefaultConstraint> databaseDefaultConstraints = GetDefaultConstraintsForDatabase();
                List<ForeignKey> databaseForeignKeys = GetForeignKeysForDatabase();

                conn.Open();
                SqlCommand command = new SqlCommand(@"SELECT
                INFORMATION_SCHEMA.TABLES.TABLE_NAME,
                COLUMN_NAME,
                DATA_TYPE,
                NUMERIC_PRECISION,
                CHARACTER_MAXIMUM_LENGTH,
                columns.IS_NULLABLE,
                columns.TABLE_SCHEMA as [SCHEMA],
                sc.is_computed,
                sc.is_rowguidcol,
                sc.is_identity,
                TABLES.TABLE_TYPE as TABLE_TYPE
                FROM INFORMATION_SCHEMA.COLUMNS AS columns
                LEFT JOIN  INFORMATION_SCHEMA.TABLES on INFORMATION_SCHEMA.TABLES.TABLE_NAME = columns.TABLE_NAME
	                AND INFORMATION_SCHEMA.TABLES.TABLE_SCHEMA = columns.table_schema 
                LEFT JOIN sys.columns sc on object_name(sc.object_id) = columns.TABLE_NAME
	                AND  OBJECT_SCHEMA_NAME(sc.object_id) = columns.table_schema 
	                AND sc.name = COLUMN_NAME
                WHERE
                    (TABLES.TABLE_TYPE = 'BASE TABLE' OR TABLES.TABLE_TYPE = 'VIEW') 
                        AND columns.TABLE_SCHEMA <> 'SYS'
                        AND INFORMATION_SCHEMA.TABLES.TABLE_NAME <> '__EFMigrationsHistory'
                ORDER BY [SCHEMA],columns.TABLE_NAME,columns.COLUMN_NAME", conn);

                command.CommandTimeout = 300;
                SqlDataReader reader = command.ExecuteReader();

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
                    bool isComputed = Convert.ToBoolean(reader["IS_COMPUTED"].ToString());
                    bool isRowGuidColumn = Convert.ToBoolean(reader["is_rowguidcol"].ToString());
                    bool isIdentity = Convert.ToBoolean(reader["is_identity"].ToString());

                    if (isComputed || isRowGuidColumn || isIdentity || column.DataType.ToUpper() == "TIMESTAMP")
                    {
                        column.DatabaseGenerated = true;
                    }
                    var existingSchema = container.Schemas.FirstOrDefault(x => x.Name == schema);

                    if (existingSchema == null)
                    {
                        Schema newSchema = new Schema
                        {
                            Name = schema,
                            ForeignKeys = databaseForeignKeys.Where(f => f.Columns.Any(c => c.ForeignKeySchemaName == schema)).ToList()
                        };
                        var existingTable = newSchema.Tables.FirstOrDefault(x => x.Name == table);
                        if (existingTable == null)
                        {
                            Table newTable = new Table();

                            var tableType = reader["TABLE_TYPE"].ToString();
                            if (tableType.ToUpper() == "VIEW")
                            {
                                newTable.IsView = true;
                            }

                            newTable.Name = table;
                            newTable.Constraints = databaseConstraints.Where(c => c.SchemaName == schema && c.TableName == table).ToList();
                            newTable.DefaultConstraints = databaseDefaultConstraints.Where(c => c.SchemaName == schema && c.TableName == table).ToList();
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
                        var existingTable = existingSchema.Tables.FirstOrDefault(x => x.Name == table);
                        if (existingTable == null)
                        {
                            Table newTable = new Table();

                            var tableType = reader["TABLE_TYPE"].ToString();
                            if (tableType.ToUpper() == "VIEW")
                            {
                                newTable.IsView = true;
                            }

                            newTable.Name = table;
                            newTable.Columns.Add(column);
                            newTable.Constraints = databaseConstraints.Where(c => c.SchemaName == schema && c.TableName == table).ToList();
                            newTable.DefaultConstraints = databaseDefaultConstraints.Where(c => c.SchemaName == schema && c.TableName == table).ToList();
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
    FK_Table = FK.TABLE_NAME,
    FK_Column = CU.COLUMN_NAME,
    PK_Table = PK.TABLE_NAME,
    PK_Column = PT.COLUMN_NAME,
    Constraint_Name = C.CONSTRAINT_NAME,
    FK.TABLE_SCHEMA as 'ForeignKeySchema',
	PK.TABLE_SCHEMA as 'PrimaryKeySchema',
	CU.ORDINAL_POSITION
FROM
    INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C
INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK
    ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK
    ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU
    ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
INNER JOIN (
            SELECT
                i1.TABLE_NAME,
				i1.CONSTRAINT_SCHEMA,
                i2.COLUMN_NAME,
		     	i2.ORDINAL_POSITION
            FROM
                INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2
                ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
            WHERE
                i1.CONSTRAINT_TYPE = 'PRIMARY KEY'
           ) PT
    ON PT.TABLE_NAME = PK.TABLE_NAME
	AND PT.CONSTRAINT_SCHEMA = PK.CONSTRAINT_SCHEMA
	AND PT.ORDINAL_POSITION =  CU.ORDINAL_POSITION
    AND PK.TABLE_SCHEMA <> 'SYS'
    AND PK.TABLE_NAME <> '__EFMigrationsHistory'
	ORDER BY ForeignKeySchema,FK_Table,ORDINAL_POSITION";

            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);

                command.CommandTimeout = 300;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var column = new ForeignKeyColumn
                    {
                        ForeignKeyTableName = reader["FK_Table"].ToString(),
                        ForeignKeyColumnName = reader["FK_Column"].ToString(),
                        PrimaryKeyTableName = reader["PK_Table"].ToString(),
                        PrimaryKeyColumnName = reader["PK_Column"].ToString(),
                        ForeignKeySchemaName = reader["ForeignKeySchema"].ToString(),
                        PrimaryKeySchemaName = reader["PrimaryKeySchema"].ToString(),
                        Order =  reader["ORDINAL_POSITION"].ToString().ToInt()
                    };

                    var key = new ForeignKey();
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

        public List<DefaultConstraint> GetDefaultConstraintsForDatabase()
        {
            List<DefaultConstraint> response = new List<DefaultConstraint>();
            string query = $@"SELECT
             OBJECT_SCHEMA_NAME(C.object_id) AS SchemaName,
             OBJECT_NAME(C.object_id) as TableName,
             COL_NAME(C.object_id, column_id) ColumnName,
             D.Name ConstraintName,
             D.[definition] ColumnDefault,
             C.is_rowGuidCol IsRowGuidColumn
             FROM sys.columns AS C
             JOIN sys.default_constraints AS D
             ON C.column_id = D.parent_column_id
             WHERE  
                D.parent_object_id = C.object_id AND
                OBJECT_SCHEMA_NAME(C.object_id) <> 'SYS' AND
                OBJECT_NAME(C.object_id)  <> '__EFMigrationsHistory'
             Order by SchemaName, TableName, ColumnName, ConstraintName";

            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandTimeout = 300;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var defaultConstraint = new DefaultConstraint()
                    {
                        ColumnDefault = reader["ColumnDefault"].ToString(),
                        ColumnName = reader["ColumnName"].ToString(),
                        Name = reader["ConstraintName"].ToString(),
                        SchemaName = reader["SchemaName"].ToString(),
                        TableName = reader["TableName"].ToString(),
                        IsRowGuid = Convert.ToBoolean(reader["IsRowGuidColumn"].ToString())
                    };
                    response.Add(defaultConstraint);
                }
            }
            return response;
        }


        public List<Constraint> GetIndexesForDatabase()
        {
            // https://dba.stackexchange.com/questions/63185/listing-indexes-and-constraints
            // http://sqldbpros.com/2013/02/sql-query-list-all-indexes-and-their-columns/
            List<Constraint> response = new List<Constraint>();

            // This union selects indexes for tables and then unions on a select for views
            string query = @"
	           SELECT  OBJECT_SCHEMA_NAME(ind.object_id) AS SchemaName
      , OBJECT_NAME(ind.object_id) AS TableName
      , ind.name AS IndexName
      , ind.is_primary_key AS IsPrimaryKey
      , ind.is_unique AS IsUnique
      , col.name AS ColumnName
      , ic.is_included_column AS IsIncludedColumn
      , ic.key_ordinal AS ColumnOrder
      , ind.type_desc as IndexType
      , ic.is_descending_key IsDescending
      , col.is_identity IsIdentity
FROM    sys.indexes ind
        INNER JOIN sys.index_columns ic
            ON ind.object_id = ic.object_id
               AND ind.index_id = ic.index_id
        INNER JOIN sys.columns col
            ON ic.object_id = col.object_id
               AND ic.column_id = col.column_id
        INNER JOIN sys.tables t
            ON ind.object_id = t.object_id
WHERE   t.is_ms_shipped = 0

UNION

    SELECT  OBJECT_SCHEMA_NAME(ind.object_id) AS SchemaName
      , OBJECT_NAME(ind.object_id) AS TableName
      , ind.name AS IndexName
      , ind.is_primary_key AS IsPrimaryKey
      , ind.is_unique AS IsUnique
      , col.name AS ColumnName
      , ic.is_included_column AS IsIncludedColumn
      , ic.key_ordinal AS ColumnOrder
      , ind.type_desc as IndexType
      , ic.is_descending_key IsDescending
      , col.is_identity IsIdentity
FROM    sys.indexes ind
        INNER JOIN sys.index_columns ic
            ON ind.object_id = ic.object_id
               AND ind.index_id = ic.index_id
        INNER JOIN sys.columns col
            ON ic.object_id = col.object_id
               AND ic.column_id = col.column_id
        INNER JOIN sys.views t -- THIS IS WHAT IS DIFFERENT IN THIS BLOCK
            ON ind.object_id = t.object_id
WHERE   t.is_ms_shipped = 0
ORDER BY OBJECT_SCHEMA_NAME(ind.object_id) --SchemaName
      , OBJECT_NAME(ind.object_id) --ObjectName
      , ind.is_primary_key DESC
      , ind.is_unique DESC
      , ind.name --IndexName
      , ic.key_ordinal

";

            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandTimeout = 300;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var index = new Constraint
                    {
                        Name = reader["IndexName"].ToString(),
                        ConstraintType = reader["IndexType"].ToString(),
                        IsPrimaryKey = Convert.ToBoolean(reader["IsPrimaryKey"].ToString()),
                        SchemaName = reader["SchemaName"].ToString(),
                        TableName = reader["TableName"].ToString(),
                        IsUnique = Convert.ToBoolean(reader["IsUnique"].ToString())
                    };

                    //This is to handle the unique index on views
                    if(index.IsUnique && index.ConstraintType.ToUpper() == "CLUSTERED")
                    {
                        index.IsPrimaryKey = true;
                    }


                    var column = new ConstraintColumn
                    {
                        Name = reader["ColumnName"].ToString(),
                        Order = Convert.ToInt32(reader["ColumnOrder"].ToString()),
                        Descending = Convert.ToBoolean(reader["IsDescending"].ToString()),
                        IsIncludedColumn = Convert.ToBoolean(reader["IsIncludedColumn"].ToString()),
                        IsIdentity = Convert.ToBoolean(reader["IsIdentity"].ToString())
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


        public bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this._connectionString))
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
                    using (SqlConnection conn = new SqlConnection(this._connectionString))
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

        public List<string> GetTableList(string schema)
        {

            List<string> tables = new List<string>();
            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(@"SELECT TABLE_NAME FROM information_schema.tables
                WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA=@schema ORDER BY TABLE_NAME", conn);
                command.Parameters.AddWithValue("schema", schema);

                command.CommandTimeout = 300;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tables.Add((string)reader["TABLE_NAME"]);
                }
            }
            return tables;
        }

        public List<string> GetSchemaList()
        {
            List<string> response = new List<string>();
            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(@"SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA where SCHEMA_OWNER='dbo' ORDER BY SCHEMA_NAME", conn);

                command.CommandTimeout = 300;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    response.Add(reader["SCHEMA_NAME"].ToString());
                }
            }
            return response;
        }


        public List<string> GetDatabaseList()
        {
            List<string> response = new List<string>();
            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(@"select name from sys.sysdatabases order by name asc", conn);

                command.CommandTimeout = 300;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    response.Add(reader["name"].ToString());
                }
            }
            return response;
        }

        public List<string> ColumnTypes
        {
            get
            {
                return new List<string>()
                {
                    "bigint",
                    "binary",
                    "bit",
                    "char",
                    "date",
                    "datetime",
                    "datetime2",
                    "datetimeoffset",
                    "decimal",
                    "float",
                    "geography",
                    "geometry",
                    "hierarchyid",
                    "image",
                    "int",
                    "money",
                    "nchar",
                    "ntext",
                    "numeric",
                    "nvarchar",
                    "real",
                    "smalldatetime",
                    "smallint",
                    "smallmoney",
                    "sql_variant",
                    "sysname",
                    "text",
                    "time",
                    "timestamp",
                    "tinyint",
                    "uniqueidentifier",
                    "varbinary",
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