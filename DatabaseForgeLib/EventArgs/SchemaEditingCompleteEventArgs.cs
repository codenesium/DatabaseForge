using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codenesium.DatabaseContracts;

namespace Codenesium.DatabaseForgeLib
{
    public class SchemaEditingCompleteEventArgs : EventArgs
    {
        public DatabaseContainer Database { get; set; }

        public SchemaEditingCompleteEventArgs(DatabaseContainer database)
        {
            this.Database = database;
        }
    }
}
