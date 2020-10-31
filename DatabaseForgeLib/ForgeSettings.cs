using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codenesium.DatabaseForgeLib
{
    public class ForgeSettings
    {
        public bool CodenesiumMode { get; set; }
        public bool CreatePrimaryKeyFieldForNewTable { get; set; } = true;
    }
}
