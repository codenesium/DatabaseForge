using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codenesium.DatabaseContracts
{
    public class ConstraintColumn
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public bool Descending { get; set; }
        public bool IsIncludedColumn { get; set; }
        public bool IsIdentity { get; set; }

        public virtual ConstraintColumn Clone()
        {
            ConstraintColumn column = new ConstraintColumn();
            column.Name = this.Name;
            column.Order = this.Order;
            column.Descending = this.Descending;
            column.IsIncludedColumn = this.IsIncludedColumn;
            column.IsIdentity = this.IsIdentity;
            return column;
        }
    }
}