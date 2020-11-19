using System;
using Codenesium.DatabaseContracts;
using FluentAssertions;
using Xunit;

namespace DatabaseContracts.IntegrationTests
{
    public class UnitTest1
    {
        [Fact]
        public void GetDatabaseStructure_ForeignKeysBetweenTwoTables_ResponseIsCorrect()
        {
            // Given
            MSSQLInterface dbInterface = new MSSQLInterface();
            dbInterface.SetConnectionString("");

            // When 
            DatabaseContainer container = dbInterface.GetDatabaseStructure();

            // Then
            container.Schemas.Count.Should().Be(1);
            container.Schemas[0].Tables[0].Name.Should().Be("tableA");
            container.Schemas[0].ForeignKeys[0].ForeignKeyName.Should().Be("");
        }
    }
}
