using System;
using Codenesium.DatabaseContracts;

namespace Codenesium.DatabaseForgeLib
{
    public class TableSavedEventArgs : EventArgs
    {
        public Table Table { get; set; }

        public String OriginalName { get; set; } = string.Empty;
        public TableSavedEventArgs(Table table, string originalName)
        {
            this.OriginalName = originalName;
            this.Table = table;
        }
    }
}
