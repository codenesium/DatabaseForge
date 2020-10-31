using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codenesium.DatabaseContracts.DependencyResolver
{
    public class Resolver
    {

        public List<DependencyTable> ConvertToDependencyTables(DatabaseContainer database)
        {
            List<DependencyTable> tables = new List<DependencyTable>();
            List<ForeignKey> foreignKeys = new List<ForeignKey>();
            database.Schemas.ForEach(s =>
            {
                foreignKeys.AddRange(s.ForeignKeys);
                s.Tables.ForEach(t =>
                {
                    DependencyTable table = new DependencyTable(t.Name,s.Name);
                    t.Columns.ForEach(c =>
                   {
                       DependencyColumn column = new DependencyColumn(c, table);
                       table.Columns.Add(column);
                   });
                    tables.Add(table);
                });
            });

            return tables;
        }

        public void BuildDependsOnList(List<ForeignKey> foreignKeys, List<DependencyTable> tables, DependencyTable tableToBuildFor)
        {
            Action<DependencyColumn> buildDependencies = null;

            int depth = -1;
            buildDependencies = (column) =>
            {
                // Find tables this column references
                ForeignKey associatedForeignKey = foreignKeys.FirstOrDefault(x =>
                            x.Columns.Any(cn =>
                                cn.ForeignKeyColumnName == column.Name &&
                                cn.ForeignKeyTableName == column.Table.Name &&
                                cn.ForeignKeySchemaName == column.Table.Schema
                                )
                            );

                depth++;

                column.Depth = depth;


                if (associatedForeignKey != null)
                {

                    // find all fields on the table that we reference
                    var foreignKeyReferenceTable = tables.Where(x => x.Name == associatedForeignKey.Columns.First().PrimaryKeyTableName
                        && x.Schema == associatedForeignKey.Columns.First().PrimaryKeySchemaName).First();

                    var primaryKey = foreignKeyReferenceTable.Columns.First(x => x.Name == associatedForeignKey.Columns.First().PrimaryKeyColumnName);

                    column.SetRefersTo(primaryKey);

                    //iterate the column on the referenced table looking for foreign keys that that table depends on
                    for (int i = 0; i < foreignKeyReferenceTable.Columns.Count; i++)
                    {
                        if (foreignKeyReferenceTable.Columns[i].Table != column.Table)
                        {
                            buildDependencies(foreignKeyReferenceTable.Columns[i]);
                        }
                    }
                }

                depth--;
            };

           tableToBuildFor.Columns.ForEach(x =>
           {
               buildDependencies(x);
           });

        }

        public void ClearValues(DependencyTable table)
        {
            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].Depth = 0;
                table.Columns[i].RefersTo = null;
            }
        }
    }
}
