using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codenesium.DatabaseContracts;

namespace Codenesium.DatabaseForgeLib
{
    public class SchemaSavedEventArgs : EventArgs
    {
        public Schema Schema { get; set; }

        public string OriginalName { get; set; }
        public SchemaSavedEventArgs(Schema schema, string originalName = "")
        {
            this.OriginalName = originalName;
            this.Schema = schema;
        }
    }
}
