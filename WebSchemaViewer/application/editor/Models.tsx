export class PrimaryKeyModel {
    ColumnName: string;
    TableName: string;
    SchemaName: string;
    KeyName: string;
    constructor() {
        this.ColumnName = "";
        this.TableName = "";
        this.SchemaName = "";
        this.KeyName = "";
    }
}

export class ForeignKeyModel {
    ForeignKeyName: string;
    Columns: Array<ForeignKeyColumnModel>;
    constructor() {
        this.ForeignKeyName = "";
        this.Columns = new Array<ForeignKeyColumnModel>();
    }
}

export class ForeignKeyColumnModel {
    ForeignKeyTableName: string;
    ForeignKeyColumnName: string;
    ForeignKeySchemaName: string;
    PrimaryKeyTableName: string;
    PrimaryKeyColumnName: string;
    PrimaryKeySchemaName: string;
    constructor() {
        this.ForeignKeyTableName = "";
        this.ForeignKeyColumnName = "";
        this.ForeignKeySchemaName = "";
        this.PrimaryKeyTableName = "";
        this.PrimaryKeyColumnName = "";
        this.PrimaryKeySchemaName = "";
    }
}

export class ConstraintColumn {
    Name: string;
    Order: number;
    Descending: boolean;
    IsIncludedColumn: boolean;
    constructor() {
        this.Name = "";
        this.Order = 0;
        this.Descending = false;
        this.IsIncludedColumn = false;
    }
}

export class ConstraintModel {
    Name: string;
    IsPrimaryKey: boolean;
    IsUnique: boolean;
    IndexType: string;
    IsIdentity: boolean;
    Columns: Array<ConstraintColumn>;
    SchemaName: string;
    TableName: string;
    constructor() {
        this.Name = "";
        this.IsPrimaryKey = false;
        this.IsIdentity = false;
        this.IsUnique = false;
        this.IndexType = "";
        this.SchemaName = "";
        this.TableName = "";
        this.Columns = new Array<ConstraintColumn>();
    }
}

export class ColumnModel {
    Name: string;
    DataType: string;
    NumericPrecision: number;
    MaxLength: number;
    IsNullable: boolean;
    DatabaseGenerated: boolean;
    constructor() {
        this.Name = "";
        this.DataType = "";
        this.NumericPrecision = -1;
        this.MaxLength = -1;
        this.IsNullable = false;
        this.DatabaseGenerated = false;
    }
}


export class TableModel {
    Name: string;
    Columns: Array<ColumnModel>;
    Constraints: Array<ConstraintModel>;
    constructor() {
        this.Name = "";
        this.Columns = new Array<ColumnModel>();
        this.Constraints = new Array<ConstraintModel>();
    }
}

 

export class SchemaModel {
    Name: string;
    Tables: Array<TableModel>;
    ForeignKeys: Array<ForeignKeyModel>;
    constructor() {
        this.Name = "";
        this.Tables = new Array<TableModel>();
        this.ForeignKeys = new Array<ForeignKeyModel>();
    }
}




export class DatabaseParameterModel {
    Name: boolean;
    Schemas: Array<SchemaModel>;
    constructor() {
    }
}

export function GetPrimaryKeyConstraint(table:TableModel) {
    return table.Constraints.find(x => x.IsPrimaryKey == true);
}

export function GetForeignKeysForColumn(database:DatabaseParameterModel, schemaName:string, tableName:string, columnName:string) {
    let foreignKeys : Array<ForeignKeyModel> = [];

    for (var s = 0; s < database.Schemas.length; s++) {
        for (var k = 0; k < database.Schemas[s].ForeignKeys.length; k++) {
            if(database.Schemas[s].ForeignKeys[k].Columns[0].PrimaryKeySchemaName == schemaName
                && database.Schemas[s].ForeignKeys[k].Columns[0].PrimaryKeyTableName == tableName
                && database.Schemas[s].ForeignKeys[k].Columns[0].PrimaryKeyColumnName == columnName)
                {
                    foreignKeys.push(database.Schemas[s].ForeignKeys[k]);
                }
        }
    }

    return foreignKeys;
}

export function GetColumnForeignKey(database:DatabaseParameterModel, schemaName:string, tableName:string, columnName:string) {
    let foreignKey : ForeignKeyModel = null;
    
    for (var s = 0; s < database.Schemas.length; s++) {
        for (var k = 0; k < database.Schemas[s].ForeignKeys.length; k++) {
            if(database.Schemas[s].ForeignKeys[k].Columns[0].ForeignKeySchemaName == schemaName
                && database.Schemas[s].ForeignKeys[k].Columns[0].ForeignKeyTableName == tableName
                && database.Schemas[s].ForeignKeys[k].Columns[0].ForeignKeyColumnName == columnName)
                {
                    foreignKey = database.Schemas[s].ForeignKeys[k];
                    break;
                }
        }
    }

    return foreignKey;
}


export function GetForeignKeysForTable(database:DatabaseParameterModel, schemaName:string, tableName:string)
{
    let keys = new Array<ForeignKeyModel>();

    for (var s = 0; s < database.Schemas.length; s++) {
        for (var k = 0; k < database.Schemas[s].ForeignKeys.length; k++) {
            if(database.Schemas[s].ForeignKeys[k].Columns[0].ForeignKeySchemaName == schemaName
                && database.Schemas[s].ForeignKeys[k].Columns[0].ForeignKeyTableName == tableName)
                {
                    keys.push(database.Schemas[s].ForeignKeys[k]);
                }
        }
    }

    return keys;
}
