using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codenesium.DatabaseContracts
{
    [Serializable]
    public class Column
    {
        public string Name { get; set; } //name of the field in the database
        public string DataType { get; set; }
        public int NumericPrecision { get; set; }
        public int MaxLength { get; set; }
        public bool IsNullable { get; set; }
        public bool DatabaseGenerated { get; set; }

        public virtual Column Clone()
        {
            Column column = new Column();
            column.DatabaseGenerated = this.DatabaseGenerated;
            column.DataType = this.DataType;
            column.IsNullable = this.IsNullable;
            column.MaxLength = this.MaxLength;
            column.Name = this.Name;
            column.NumericPrecision = this.NumericPrecision;
            return column;
        }
    }

}