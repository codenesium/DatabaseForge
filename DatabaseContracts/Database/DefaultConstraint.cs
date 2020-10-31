using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codenesium.DatabaseContracts
{
    [Serializable]
    public class DefaultConstraint
    {
        public string Name { get; set; }
        public string ColumnName { get; set; }
        public string TableName { get; set; }
        public string SchemaName { get; set; }
        public string ColumnDefault { get; set; }
        public bool IsRowGuid { get; set; }

        public virtual DefaultConstraint Clone()
        {
            DefaultConstraint constraint = new DefaultConstraint();
            constraint.Name = this.Name;
            constraint.TableName = this.TableName;
            constraint.ColumnName = this.TableName;
            constraint.ColumnDefault = this.TableName;
            constraint.SchemaName = this.SchemaName;
            constraint.IsRowGuid = this.IsRowGuid;
            return constraint;
        }

    }
}