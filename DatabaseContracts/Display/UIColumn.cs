using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codenesium.DatabaseContracts.Display
{

    public static class Extensions
    {
        public static List<UIColumn> ToUIColumns(this List<Column> columns, Table table, Schema schema)
        {
            List<UIColumn> response = new List<UIColumn>();
            columns.ForEach(x =>
            {
                bool isPrimaryKey = table.Constraints.Any(c => c.IsPrimaryKey && c.Columns.Any(a => a.Name.ToUpper() == x.Name.ToUpper()));
                bool isForeignKey = schema.ForeignKeys.Any(k => k.Columns.Any(c => c.ForeignKeyColumnName.ToUpper() == x.Name.ToUpper() &&
                c.ForeignKeyTableName.ToUpper() == table.Name.ToUpper() &&
                c.ForeignKeySchemaName.ToUpper() == schema.Name.ToUpper()
                ));
                bool isUnique = table.Constraints.Any(c => c.IsUnique && c.Columns.Any(a => a.Name.ToUpper() == x.Name.ToUpper()));

                bool isNonUnique = table.Constraints.Any(c => !c.IsUnique && c.Columns.Any(a => a.Name.ToUpper() == x.Name.ToUpper()));

                response.Add(new UIColumn(x,isPrimaryKey, isUnique, isNonUnique, isForeignKey));
            });
            return response;
        }
    }

    public class UIColumn : Column
    {
        public bool IsPrimaryKey { get; private set; }
        public bool IsForeignKey { get; private set; }
        public bool IsUnique { get; private set; }
        public bool IsNonUnique { get; private set; }

        public UIColumn(Column column, bool isPrimaryKey, bool isUnique, bool isNonUnique, bool isForeignKey)
        {
            this.IsForeignKey = isForeignKey;
            this.IsPrimaryKey = isPrimaryKey;
            this.IsUnique = isUnique;
            this.IsNonUnique = isNonUnique;
            this.DatabaseGenerated = column.DatabaseGenerated;
            this.DataType = column.DataType;
            this.IsNullable = column.IsNullable;
            this.MaxLength = column.MaxLength;
            this.Name = column.Name;
            this.NumericPrecision = column.NumericPrecision;
        }
        public string DisplayName
        {
            get
            {
                string response = this.Name;

                if (this.IsPrimaryKey)
                {
                    response += " P*";
                }

                if (this.IsUnique)
                {
                    response += " U*";
                }

                if (this.IsNonUnique)
                {
                    response += " I*";
                }

                if (this.IsForeignKey)
                {
                    response += " F*";
                }
                

                return response;

            }

        }
    }
}
