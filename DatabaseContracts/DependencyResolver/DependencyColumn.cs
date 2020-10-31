using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codenesium.DatabaseContracts.DependencyResolver
{
    public class DependencyTable
    {
        public List<DependencyColumn> Columns = new List<DependencyColumn>();

        public string Name { get; set; }

        public  string Schema { get; set; }
        public DependencyTable(string name, string schema)
        {
            this.Name = name;
            this.Schema = schema;
        }
        public DependencyTable(string name, List<DependencyColumn> columns)
        {
            this.Name = name;
            this.Columns = columns;
        }
    }

    public class DependencyColumn : Column
    {
        public DependencyTable Table { get; set; }

        public string Schema { get; set; }

        public int Depth { get; set; }

        public DependencyColumn RefersTo { get; set; }


        public void SetDepth(int depth)
        {
            this.Depth = depth;
        }

        public void SetRefersTo(DependencyColumn from)
        {
            if (this.RefersTo != null)
            {

            }
            this.RefersTo = from;
        }

        public DependencyColumn(Column column, DependencyTable table)
        {
            this.DatabaseGenerated = column.DatabaseGenerated;
            this.DataType = column.DataType;
            this.IsNullable = column.IsNullable;
            this.MaxLength = column.MaxLength;
            this.Name = column.Name;
            this.NumericPrecision = column.NumericPrecision;
            this.Table = table;
            this.Depth = 0;
        }
    }
}
