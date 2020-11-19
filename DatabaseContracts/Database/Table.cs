using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Codenesium.DatabaseContracts
{
    [Serializable]
    public class Table
    {
        public string Name { get; set; }

        public List<Column> Columns { get; set; } = new List<Column>();

        public List<Constraint> Constraints { get; set; } = new List<Constraint>();

        public List<DefaultConstraint> DefaultConstraints { get; set; } = new List<DefaultConstraint>();

        public bool IsView { get; set; }
        public void SetProperties(string name,bool isView, List<Column> columns, List<Constraint> constraints, List<DefaultConstraint> defaultConstraints)
        {
            this.Name = name;
            this.IsView = this.IsView;
            this.Columns = columns;
            this.Constraints = constraints;
            this.DefaultConstraints = defaultConstraints;
        }
        public virtual void AddConstraint(Constraint constraint)
        {
            this.Constraints.RemoveAll(x => x.Name == constraint.Name);
            this.Constraints.Add(constraint);
        }

        public virtual void AddDefaultConstraint(DefaultConstraint constraint)
        {
            this.DefaultConstraints.RemoveAll(x => x.Name == constraint.Name);
            this.DefaultConstraints.Add(constraint);
        }

        public virtual void DeletePrimaryKey(string table)
        {
            this.Constraints.RemoveAll(x => x.TableName == table && x.IsPrimaryKey);
        }

        public virtual Table Clone()
        {
            Table table = new Table();
            table.Name = this.Name;
            table.IsView = this.IsView;
            this.Columns.ForEach(x => {
                table.Columns.Add(x.Clone());
            });

            this.Constraints.ForEach(x => {
                table.Constraints.Add(x.Clone());
            });
            return table;
        }
    }
}