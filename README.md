# DatabaseForge

DatabaseForge is windows app and libraries to load and manipulate SQL Server databases. Database Contracts is a library that can be used as the foundation for code generation tools that are built on SQL Server.

To use the DatabaseForge run the DatabaseForgeApp project in the solution.

To use the database contracts nuget package in your project.
`IDatabaseInterface sqlInterface = DatabaseInterfaceFactory.Factory("MSSQL");
sqlInterface.SetConnectionString(connectToDatabase.ConnectionString);
DatabaseContainer databaseContainer = sqlInterface.GetDatabaseStructure();
`

[![NuGet Badge](https://buildstats.info/nuget/Codenesium.DatabaseContracts)](https://www.nuget.org/packages/Codenesium.DatabaseContracts/)
