using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codenesium.DatabaseContracts
{
    [Serializable]
    public class ForeignKeyColumn
    {
        public string ForeignKeyTableName { get; set; }
        public string ForeignKeyColumnName { get; set; }
        public string PrimaryKeyTableName { get; set; }
        public string PrimaryKeyColumnName { get; set; }
        public string PrimaryKeySchemaName { get; set; }
        public string ForeignKeySchemaName { get; set; }
        public int Order { get; set; }


        public virtual ForeignKeyColumn Clone()
        {
            ForeignKeyColumn key = new ForeignKeyColumn();
            key.ForeignKeyTableName = this.ForeignKeyTableName;
            key.ForeignKeyColumnName = this.ForeignKeyColumnName;
            key.PrimaryKeyTableName = this.PrimaryKeyTableName;
            key.PrimaryKeyColumnName = this.PrimaryKeyColumnName;
            key.PrimaryKeySchemaName = this.PrimaryKeySchemaName;
            key.ForeignKeyColumnName= this.ForeignKeyColumnName;
            key.ForeignKeySchemaName = this.ForeignKeySchemaName;
            key.Order = this.Order;
            return key;
        }
    }
}