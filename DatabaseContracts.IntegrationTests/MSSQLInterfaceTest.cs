using System;
using System.Collections.Generic;
using System.Linq;
using Codenesium.DatabaseContracts;
using FluentAssertions;

using Xunit;

namespace DatabaseContracts.IntegrationTests
{
    public class MSSQLInterfaceTest
    {
        [Theory]
        [InlineData(@"Data Source=CADU-PC\sql2012;Initial Catalog=ForgeDB;Integrated Security=True")]
        public void GetDatabaseStructure_SqlServerTypes_ContainsAllCorrectly(string connection)
        {
            // Given
            Codenesium.DatabaseContracts.MSSQLInterface dbInterface = new Codenesium.DatabaseContracts.MSSQLInterface();
            dbInterface.SetConnectionString(connection);
           
            // When 
            DatabaseContainer container = dbInterface.GetDatabaseStructure();

            // Then
            container.Schemas.Count.Should().Be(1);
            container.Schemas[0].Tables[2].Name.ToLower().Should().Be("typesdatabase");
            container.Schemas[0].Tables[2].Columns.Count.Should().Be(34);
            List<Column> tableColumns = container.Schemas[0].Tables[2].Columns.ToList();

            tableColumns.Should().Contain(x => x.Name.ToLower() == "biginttype" &&
                                                x.DataType.ToLower()== "bigint");

            tableColumns.Should().Contain(x => x.Name.ToLower() == "binarytype" &&
                                               x.DataType.ToLower() == "binary");

            tableColumns.Should().Contain(x => x.Name.ToLower() == "bittype" &&
                                               x.DataType.ToLower() == "bit");

            tableColumns.Should().Contain(x => x.Name.ToLower() == "chartype" &&
                                                x.DataType.ToLower() == "char");
           
            tableColumns.Should().Contain(x => x.Name.ToLower() == "datetype" &&
                                                x.DataType.ToLower() == "date");
            
            tableColumns.Should().Contain(x => x.Name.ToLower() == "datetimetype" &&
                                                x.DataType.ToLower() == "datetime");
            
            tableColumns.Should().Contain(x => x.Name.ToLower() == "datetime2type" &&
                                                x.DataType.ToLower() == "datetime2");
            
            tableColumns.Should().Contain(x => x.Name.ToLower() == "datetimeoffsettype" &&
                                                x.DataType.ToLower() == "datetimeoffset");
           
            tableColumns.Should().Contain(x => x.Name.ToLower() == "decimaltype" &&
                                                x.DataType.ToLower() == "decimal");
           
            tableColumns.Should().Contain(x => x.Name.ToLower() == "floattype" &&
                                                x.DataType.ToLower() == "float");
            
            tableColumns.Should().Contain(x => x.Name.ToLower() == "geographytype" &&
                                                x.DataType.ToLower() == "geography");
            
            tableColumns.Should().Contain(x => x.Name.ToLower() == "geometrytype" &&
                                                x.DataType.ToLower() == "geometry");
            
            tableColumns.Should().Contain(x => x.Name.ToLower() == "hierarchyidtype" &&
                                                x.DataType.ToLower() == "hierarchyid");
           
            tableColumns.Should().Contain(x => x.Name.ToLower() == "imagetype" &&
                                                x.DataType.ToLower() == "image");
            
            tableColumns.Should().Contain(x => x.Name.ToLower() == "inttype" &&
                                                x.DataType.ToLower() == "int");
           
            tableColumns.Should().Contain(x => x.Name.ToLower() == "moneytype" &&
                                                x.DataType.ToLower() == "money");
            
            tableColumns.Should().Contain(x => x.Name.ToLower() == "nchartype" &&
                                                x.DataType.ToLower() == "nchar");
           
            tableColumns.Should().Contain(x => x.Name.ToLower() == "ntexttype" &&
                                                x.DataType.ToLower() == "ntext");
            
            tableColumns.Should().Contain(x => x.Name.ToLower() == "numerictype" &&
                                                x.DataType.ToLower() == "numeric");
         
            tableColumns.Should().Contain(x => x.Name.ToLower() == "nvarchartype" &&
                                                x.DataType.ToLower() == "nvarchar");
          
            tableColumns.Should().Contain(x => x.Name.ToLower() == "realtype" &&
                                                x.DataType.ToLower() == "real");
       
            tableColumns.Should().Contain(x => x.Name.ToLower() == "smalldatetimetype" &&
                                                x.DataType.ToLower() == "smalldatetime");
           
            tableColumns.Should().Contain(x => x.Name.ToLower() == "smallinttype" &&
                                                x.DataType.ToLower() == "smallint");
         
            tableColumns.Should().Contain(x => x.Name.ToLower() == "smallmoneytype" &&
                                                x.DataType.ToLower() == "smallmoney");
          
            tableColumns.Should().Contain(x => x.Name.ToLower() == "sql_varianttype" &&
                                                x.DataType.ToLower() == "sql_variant");

            //https://docs.microsoft.com/en-us/previous-versions/sql/sql-server-2008-r2/ms191240(v=sql.105)?redirectedfrom=MSDN#sysname
            tableColumns.Should().Contain(x => x.Name.ToLower() == "sysnametype" &&
                                                x.DataType.ToLower() == "nvarchar");
        
            tableColumns.Should().Contain(x => x.Name.ToLower() == "texttype" &&
                                                x.DataType.ToLower() == "text");
          
            tableColumns.Should().Contain(x => x.Name.ToLower() == "timetype" &&
                                                x.DataType.ToLower() == "time");
           
            tableColumns.Should().Contain(x => x.Name.ToLower() == "timestamptype" &&
                                                x.DataType.ToLower() == "timestamp");
           
            tableColumns.Should().Contain(x => x.Name.ToLower() == "tinyinttype" &&
                                                x.DataType.ToLower() == "tinyint");
           
            tableColumns.Should().Contain(x => x.Name.ToLower() == "uniqueidentifiertype" &&
                                                x.DataType.ToLower() == "uniqueidentifier");
            
            tableColumns.Should().Contain(x => x.Name.ToLower() == "varbinarytype" &&
                                                x.DataType.ToLower() == "varbinary");
           
            tableColumns.Should().Contain(x => x.Name.ToLower() == "varchartype" &&
                                                x.DataType.ToLower() == "varchar");
           
            tableColumns.Should().Contain(x => x.Name.ToLower() == "xmltype" &&
                                                x.DataType.ToLower() == "xml");
          
        }

        [Theory]
        [InlineData(@"Data Source=CADU-PC\sql2012;Initial Catalog=ForgeDB;Integrated Security=True")]
        public void GetDatabaseStructure_ForeignKeysBetweenTwoTables_ResponseIsCorrect(string connection)
        {
            // Given
            Codenesium.DatabaseContracts.MSSQLInterface dbInterface = new Codenesium.DatabaseContracts.MSSQLInterface();
            dbInterface.SetConnectionString(connection);

            // When 
            DatabaseContainer container = dbInterface.GetDatabaseStructure();

            // Then
            container.Schemas.Count.Should().Be(1);
            container.Schemas[0].Tables[0].Name.ToLower().Should().Be("tablea");
            container.Schemas[0].Tables[1].Name.ToLower().Should().Be("tableb");
            container.Schemas[0].ForeignKeys[0].ForeignKeyName.ToLower().Should().Be("fk_tableb_tableaid_tablea_id");
        }


        [Theory]
        [InlineData(@"Data Source=CADU-PC\sql2012;Initial Catalog=ForgeDB;Integrated Security=True")]
        public void GetDatabaseStructure_ForeignKeyToItself_ResponseIsCorrect(string connection)
        {
            // Given
            Codenesium.DatabaseContracts.MSSQLInterface dbInterface = new Codenesium.DatabaseContracts.MSSQLInterface();
            dbInterface.SetConnectionString(connection);

            // When 
            DatabaseContainer container = dbInterface.GetDatabaseStructure();

            // Then
            container.Schemas.Count.Should().Be(1);
            container.Schemas[0].Tables.Should().Contain(t=>t.Name.ToLower()=="users");
            container.Schemas[0].ForeignKeys.Should().Contain(f=>f.ForeignKeyName.ToLower()== "fk_users_usersid_users_id");
        }


    }
}
