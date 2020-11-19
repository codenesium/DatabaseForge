using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codenesium.DatabaseContracts.DependencyResolver
{
    public class TestDataGenerator
    {
        TestDataRepository _repository;
        public TestDataGenerator(TestDataRepository repository)
        {
            this._repository = repository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="primaryKeyValue">The explicit value to use for the column that is the primary key</param>
        /// <param name="fieldTypeIndex">A dictionary that tracks where our indexex are in the TestData array for a type</param>
        /// <param name="explicitForeignKeys">Explicit values to use for foreign keys instead of generating a value that would not be a real foreign key</param>
        /// <returns></returns>
        public string GeneratInsert(Schema schema, Table table, object primaryKeyValue, Dictionary<Tuple<string,string,string>, int> fieldTypeIndex, Dictionary <string,int> explicitForeignKeys)
        {

            string columnClause = String.Empty;
            string valueClause = String.Empty;

            foreach(Column column in table.Columns)
            {
               if(column.DatabaseGenerated == true)
               {
                   continue;
               }

                columnClause += $"[{column.Name}]";
                if(column != table.Columns.Last())
                {
                    columnClause += ",";
                }

                Tuple<string, string, string> fieldTypeIndexKey = Tuple.Create<string, string, string>(schema.Name, table.Name, column.Name);

                // We have not inserted a record for the schema:table:column. Create one. 
                if (!fieldTypeIndex.Keys.Any(x => x.Item1 == fieldTypeIndexKey.Item1 && x.Item2 == fieldTypeIndexKey.Item2 && x.Item3 == fieldTypeIndexKey.Item3))
                {
                    fieldTypeIndex[fieldTypeIndexKey] = 0;
                }

                List<Constraint> constraints = table.Constraints.Where(x => x.IsUnique && !x.IsPrimaryKey && x.Columns.Any(c => c.Name == column.Name)).ToList();

                // We have a uniuqe constraint
                if (constraints.Count > 0)
                {
                    fieldTypeIndex[fieldTypeIndexKey] = fieldTypeIndex[fieldTypeIndexKey] + 1;
                }
                
                int dataIndex = fieldTypeIndex[fieldTypeIndexKey];


                if (table.Constraints.FirstOrDefault(x => x.IsPrimaryKey) != null && table.Constraints.FirstOrDefault(x => x.IsPrimaryKey).Columns.Any(x => x.Name == column.Name))
                {
                    valueClause += $"{primaryKeyValue}";
                }
                else if(explicitForeignKeys.Keys.Any(k => k == column.Name))
                {
                    valueClause += $"{explicitForeignKeys[column.Name]}";
                }
                else
                {
                    valueClause += $"{this._repository.GetTestValue(column, dataIndex)}";
                }

                if (column != table.Columns.Last())
                {
                    valueClause += ",";
                }
            }

            if (!string.IsNullOrWhiteSpace(columnClause) && columnClause.Last() == ',')
            {
                columnClause = columnClause.Substring(0, columnClause.Length - 1);
            }
            if (!string.IsNullOrWhiteSpace(valueClause) && valueClause.Last() == ',')
            {
                valueClause = valueClause.Substring(0, valueClause.Length - 1);
            }

            return $@"SET IDENTITY_INSERT {schema.Name}.{table.Name} ON
                      INSERT INTO [{schema.Name}].[{table.Name}] ({columnClause}) VALUES ({valueClause})
                      SET IDENTITY_INSERT {schema.Name}.{table.Name} OFF" + Environment.NewLine;
        }


    }
}
