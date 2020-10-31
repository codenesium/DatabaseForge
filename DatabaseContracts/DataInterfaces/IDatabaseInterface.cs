using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codenesium.DatabaseContracts
{
    public interface IDatabaseInterface
    {
        void SetConnectionString(string connectionString);

		string CreateConnectionStringUsingWindowsAuthentication(string instance, string database);

		bool TestConnection();

        Task<bool> TestConnectionAsync();

        List<ForeignKey> GetForeignKeysForDatabase();

        List<Constraint> GetIndexesForDatabase();

        DatabaseContainer GetDatabaseStructure();

        List<String> GetTableList(string schema);

        List<String> GetSchemaList();

        List<String> GetDatabaseList();

        string CreateConnectionString(string instance, string database, string username, string password);

        List<String> ColumnTypes { get; set; }
    }

    public enum Providers
    {
        MSSQL,
        PostgreSQL
    }
}