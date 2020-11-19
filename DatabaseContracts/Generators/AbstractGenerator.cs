using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codenesium.DatabaseContracts.DependencyResolver;

namespace Codenesium.DatabaseContracts
{
    public abstract class AbstractGenerator
    {
        public string GenerateInsertStatmentsForDatabase(DatabaseContainer database)
        {
            TestDataGenerator generator = new TestDataGenerator(new TestDataRepository());
            Resolver resolver = new Resolver();

            /* Convert the tables to a dependency table list is really just our database with dependency tables instead of table */
            List<DependencyTable> convertedTables = resolver.ConvertToDependencyTables(database);
            string output = string.Empty;

            //This must be defined like this for the recursive call to work inside the action. It's a .NET quirk. 
            Action<DependencyTable> recursiveGenerateInsert = null;

            /*
             * primaryKeys tracks what the auto incremnted primary key is for each table while we're inserting
             *
             */
            Dictionary<Tuple<string, string>, int> primaryKeys = new Dictionary<Tuple<string, string>, int>(); //key is schema:table

            /*
             * fieldTypeIndex tracks the index in the test generator for unique indexes. 
             * So we have a list of unique guids in our test data array and we are inserting into a guid
             * column that is unique. We can't insert the same quid twice. So we use this index which is keyed by schema:table:column
             * to track which entry in the test data array we're on.
             * 
             */
            Dictionary<Tuple<string, string, string>, int> fieldTypeIndex = new Dictionary<Tuple<string, string, string>, int>(); // keys is schema:tablecolumn


            // this iterated the table columns and walks the tree using the RefersTo field on the table to find the next node.
            // When it gets to the bottom leaf of the tree it generates the insert SQL and then returns 
            recursiveGenerateInsert = (t) =>
            {
                Dictionary<string, int> explicitForeignKeys = new Dictionary<string, int>();

                t.Columns.ForEach(c =>
                {
                    if (c.RefersTo != null) // if there is a table we reference
                    {
                        if (c.RefersTo.Table != t)
                        {
                            recursiveGenerateInsert(c.RefersTo.Table); // iterate all of the columns 
                        }
                        else
                        {
                            // this is a hack. When we insert into a table with a foreign key to itself we need a value so
                            // we're just hard coding it to one for now
                            primaryKeys[Tuple.Create<string, string>(c.RefersTo.Table.Schema, c.RefersTo.Table.Name)] = 1;
                        }


                        /* Set the value for the column that is a foreign key explicit by looking for the current primaryKey for the referenced table
                         * 
                         */
                        explicitForeignKeys[c.Name] = primaryKeys[Tuple.Create<string, string>(c.RefersTo.Table.Schema, c.RefersTo.Table.Name)];
                    }
                });


                Schema schema = database.Schemas.First(x => x.Name.ToUpper() == t.Schema.ToUpper());
                Table table = schema.Tables.First(x => x.Name.ToUpper() == t.Name.ToUpper());
                int primaryKey = 0;

                if (primaryKeys.Keys.Any(k => k.Item1 == schema.Name && k.Item2 == table.Name)) // a primary key for this table exists
                {
                    primaryKey = primaryKeys[Tuple.Create<string, string>(schema.Name, table.Name)] + 1; // increment it and return the new value
                    primaryKeys[Tuple.Create<string, string>(schema.Name, table.Name)] = primaryKey;
                }
                else
                {
                    primaryKey = 1;// this table doesn't exist. Create a dictionary entry and set it to 1
                    primaryKeys[Tuple.Create<string, string>(schema.Name, table.Name)] = primaryKey;
                }

                output += generator.GeneratInsert(schema, table, primaryKey, fieldTypeIndex, explicitForeignKeys);

            };

            convertedTables.ForEach(t =>
            {
                resolver.ClearValues(t); // clear the RefersTo for all of the tables.

                // Build the dependency tree for this table
                resolver.BuildDependsOnList(database.GetForeignKeys(), convertedTables, t);

                // Iterate this tables columns and any tables those columns reference 
                recursiveGenerateInsert(t);
            });

            return output;
        }

    }
}
