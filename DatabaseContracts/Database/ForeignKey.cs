using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codenesium.DatabaseContracts
{
    [Serializable]
    public class ForeignKey
    {

        public List<ForeignKeyColumn> Columns = new List<ForeignKeyColumn>();
        public string ForeignKeyName { get; set; }

        public virtual ForeignKey Clone()
        {
            ForeignKey key = new ForeignKey();
            this.Columns.ForEach(x =>
            {
               key.Columns.Add(x.Clone());
            });
            key.ForeignKeyName = ForeignKeyName;
            return key;
        }
    }
}