using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codenesium.DatabaseContracts
{
    public class Schema
    {
        public string Name { get; set; }
        public List<Table> Tables { get; set; } = new List<Table>();
        public List<ForeignKey> ForeignKeys { get; set; } = new List<ForeignKey>();

        public virtual void AddForeignKey(ForeignKey key)
        {
            this.ForeignKeys.RemoveAll(x => x.ForeignKeyName == key.ForeignKeyName);
            this.ForeignKeys.Add(key);
        }

        public virtual void AddTable(Table table, string originalName = "")
        {
            if(string.IsNullOrWhiteSpace(originalName))
            {
                this.Tables.Add(table);
            }
            else
            {
                foreach (var key in this.ForeignKeys)
                {
                    var matchinForeignColumns = key.Columns.Where(x => x.ForeignKeySchemaName == this.Name && x.ForeignKeyTableName == originalName).ToList();
                    matchinForeignColumns.ForEach(fk =>
                    {
                        fk.ForeignKeyTableName = table.Name;
                    });

                    var matchinPrimaryColumns = key.Columns.Where(x => x.PrimaryKeySchemaName == this.Name && x.PrimaryKeyTableName == originalName).ToList();
                    matchinPrimaryColumns.ForEach(fk =>
                    {
                        fk.PrimaryKeyTableName = table.Name;
                    });
                }

            }
        }

        public virtual void AddColumn(string tableName, Column column, string originalName = "")
        {
            var table = this.Tables.First(x => x.Name.ToUpper() == tableName.ToUpper());

            if (string.IsNullOrWhiteSpace(originalName))
            {
                table.Columns.Add(column);
            }
            else
            {

                var existingColumn = table.Columns.First(x => x.Name.ToUpper() == originalName.ToUpper());


                foreach (var key in this.ForeignKeys)
                {
                    var matchinForeignColumns = key.Columns.Where(x => x.ForeignKeySchemaName == this.Name &&                 
                    x.ForeignKeyTableName == tableName &&
                    x.ForeignKeyColumnName == originalName).ToList();

                    matchinForeignColumns.ForEach(fk =>
                    {
                        fk.ForeignKeyColumnName = column.Name;
                    });

                    var matchinPrimaryColumns = key.Columns.Where(x => x.PrimaryKeySchemaName == this.Name && 
                    x.PrimaryKeyTableName == tableName &&
                    x.PrimaryKeyColumnName == originalName).ToList();
                    matchinPrimaryColumns.ForEach(fk =>
                    {
                        fk.PrimaryKeyColumnName = column.Name;
                    });


                }

                table.Columns[table.Columns.IndexOf(existingColumn)] = column.Clone();

                table.Constraints.ForEach(c =>
                {
                    c.Columns.ForEach(col =>
                    {
                        if (col.Name.ToUpper() == originalName.ToUpper())
                        {
                            col.Name = column.Name;
                        }
                    });
                });
            }
        }

        public virtual void DeletePrimaryKey(string tableName)
        {
            var table = this.Tables.First(x => x.Name == tableName);
            var primaryKey = table.Constraints.FirstOrDefault(x => x.IsPrimaryKey);
            if (primaryKey != null)
            {
                // remove all foreign key references to this table
                this.ForeignKeys.RemoveAll(x => x.Columns.Any(c => c.ForeignKeyColumnName.ToUpper() == primaryKey.Columns.First().Name.ToUpper() &&
                 c.ForeignKeyTableName.ToUpper() == table.Name.ToUpper() &&
                 c.ForeignKeySchemaName.ToUpper() == this.Name.ToUpper()
                ));
                table.DeletePrimaryKey(tableName);
            }
        }

        public virtual void DeleteForeignKey(string fromTableName,string fromColumnName)
        {
            this.ForeignKeys.RemoveAll(x => x.Columns.Any(c => c.ForeignKeyColumnName.ToUpper() == fromColumnName.ToUpper() && c.ForeignKeyTableName.ToUpper() == fromTableName.ToUpper()));
        }

        public virtual void DeleteColumn(string tableName, string columnName)
        {
            var table = this.Tables.First(x => x.Name == tableName);

            table.Columns.RemoveAll(x => x.Name == columnName);

            table.Constraints.RemoveAll(x => x.Columns.Any(c => c.Name == columnName));

            this.ForeignKeys.RemoveAll(x => x.Columns.Any(
                fk => fk.ForeignKeyTableName == tableName && fk.ForeignKeyColumnName == columnName 
            || fk.PrimaryKeyTableName == tableName && fk.PrimaryKeyColumnName == columnName));
        }

        public virtual Schema Clone()
        {
            Schema schema = new Schema();
            schema.Name = this.Name;


            this.Tables.ForEach(x => {
                schema.Tables.Add(x.Clone());
            });

            this.ForeignKeys.ForEach(x => {
                schema.ForeignKeys.Add(x.Clone());
            });



            return schema;
        }
    }
}