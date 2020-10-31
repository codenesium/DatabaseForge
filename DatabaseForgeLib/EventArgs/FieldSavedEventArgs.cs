using System;

using Codenesium.DatabaseContracts;

namespace Codenesium.DatabaseForgeLib
{
    public class ColumnSavedEventArgs : EventArgs
    {
        public Column Column { get; set; }

        public string OriginalName { get; set; }

        public ColumnSavedEventArgs(Column column, string originalName = "")
        {
            this.OriginalName = originalName;
            this.Column = column;
        }
    }

    public class ConstraintSavedEventArgs : EventArgs
    {
        public Constraint Constraint { get; set; }

        public ConstraintSavedEventArgs(Constraint constraint)
        {
            this.Constraint = constraint;
        }
    }

    public class ForeignKeySavedEventArgs : EventArgs
    {
        public ForeignKey Key { get; set; }

        public Constraint Constraint { get; set; }


        public ForeignKeySavedEventArgs(ForeignKey key, Constraint constraint)
        {
            this.Constraint = constraint;
            this.Key = key;
        }
    }
}
