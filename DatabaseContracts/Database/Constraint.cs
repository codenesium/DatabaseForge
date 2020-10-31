using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codenesium.DatabaseContracts
{
    [Serializable]
    public class Constraint
    {
        public string Name { get; set; }
        public string TableName { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string ConstraintType { get; set; }
        public List<ConstraintColumn> Columns { get; set; } = new List<ConstraintColumn>();
        public string SchemaName { get; set; }
        public bool IsUnique { get; set; }


        public virtual Constraint Clone()
        {
            Constraint constraint = new Constraint();
            constraint.Name = this.Name;
            constraint.TableName = this.TableName;
            constraint.IsUnique = this.IsUnique;
            constraint.IsPrimaryKey = this.IsPrimaryKey;
            constraint.ConstraintType = this.ConstraintType;
            constraint.SchemaName = this.SchemaName;
            this.Columns.ForEach(x =>
           {
               constraint.Columns.Add(x.Clone());
           });
           return constraint;
        }

    }
}