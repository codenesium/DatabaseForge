import * as React from 'react'
import * as ReactDOM  from 'react-dom';
import * as BS from 'react-bootstrap';
import * as Models from './Models'
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faKey,faServer } from '@fortawesome/free-solid-svg-icons';

library.add(faKey);
library.add(faServer);

let databaseJson = `{"Name":"testCase1","Schemas":[{"Name":"dbo","Tables":[{"Name":"ColumnSameAsFKTable","Columns":[{"Name":"id","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":true},{"Name":"Person","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"PersonId","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false}],"Constraints":[{"Name":"PK_ColumnSameAsFKTable","TableName":"ColumnSameAsFKTable","IsPrimaryKey":true,"IsUnique":true,"IsIdentity":true,"ConstraintType":"CLUSTERED","Columns":[{"Name":"id","Order":1,"Descending":false,"IsIncludedColumn":false}],"SchemaName":"dbo"}],"DefaultConstraints":[]},{"Name":"compositePrimaryKey","Columns":[{"Name":"id","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"id2","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false}],"Constraints":[{"Name":"PK_compositePrimaryKey","TableName":"compositePrimaryKey","IsPrimaryKey":true,"IsUnique":true,"IsIdentity":false,"ConstraintType":"CLUSTERED","Columns":[{"Name":"id","Order":1,"Descending":false,"IsIncludedColumn":false},{"Name":"id2","Order":2,"Descending":false,"IsIncludedColumn":false}],"SchemaName":"dbo"}],"DefaultConstraints":[]},{"Name":"PERSON","Columns":[{"Name":"PERSON_ID","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":true},{"Name":"PERSON_NAME","DataType":"varchar","NumericPrecision":0,"MaxLength":50,"IsNullable":false,"DatabaseGenerated":false}],"Constraints":[{"Name":"PK_PERSON_2","TableName":"PERSON","IsPrimaryKey":true,"IsUnique":true,"IsIdentity":true,"ConstraintType":"CLUSTERED","Columns":[{"Name":"PERSON_ID","Order":1,"Descending":false,"IsIncludedColumn":false}],"SchemaName":"dbo"}],"DefaultConstraints":[]},{"Name":"RowVersionCheck","Columns":[{"Name":"id","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":true},{"Name":"name","DataType":"varchar","NumericPrecision":0,"MaxLength":50,"IsNullable":false,"DatabaseGenerated":false},{"Name":"rowVersion","DataType":"uniqueidentifier","NumericPrecision":0,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":true}],"Constraints":[{"Name":"PK_RowVersionCheck","TableName":"RowVersionCheck","IsPrimaryKey":true,"IsUnique":true,"IsIdentity":true,"ConstraintType":"CLUSTERED","Columns":[{"Name":"id","Order":1,"Descending":false,"IsIncludedColumn":false}],"SchemaName":"dbo"}],"DefaultConstraints":[{"Name":"DF_RowVersionCheck_rowVersion","ColumnName":"rowVersion","TableName":"RowVersionCheck","SchemaName":"dbo","ColumnDefault":"(newid())","IsRowGuid":true}]},{"Name":"selfReference","Columns":[{"Name":"id","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":true},{"Name":"selfReferenceId","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"selfReferenceId2","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false}],"Constraints":[{"Name":"PK_selfReference","TableName":"selfReference","IsPrimaryKey":true,"IsUnique":true,"IsIdentity":true,"ConstraintType":"CLUSTERED","Columns":[{"Name":"id","Order":1,"Descending":false,"IsIncludedColumn":false}],"SchemaName":"dbo"}],"DefaultConstraints":[]},{"Name":"Tables","Columns":[{"Name":"id","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":true},{"Name":"name","DataType":"varchar","NumericPrecision":0,"MaxLength":50,"IsNullable":false,"DatabaseGenerated":false}],"Constraints":[{"Name":"PK_Tables","TableName":"Tables","IsPrimaryKey":true,"IsUnique":true,"IsIdentity":true,"ConstraintType":"CLUSTERED","Columns":[{"Name":"id","Order":1,"Descending":false,"IsIncludedColumn":false}],"SchemaName":"dbo"}],"DefaultConstraints":[]},{"Name":"TestAllFieldTypes","Columns":[{"Name":"fieldBigInt","DataType":"bigint","NumericPrecision":19,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldBinary","DataType":"binary","NumericPrecision":0,"MaxLength":50,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldBit","DataType":"bit","NumericPrecision":0,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldChar","DataType":"char","NumericPrecision":0,"MaxLength":10,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldDate","DataType":"date","NumericPrecision":0,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldDateTime","DataType":"datetime","NumericPrecision":0,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldDateTime2","DataType":"datetime2","NumericPrecision":0,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldDateTimeOffset","DataType":"datetimeoffset","NumericPrecision":0,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldDecimal","DataType":"decimal","NumericPrecision":18,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldFloat","DataType":"float","NumericPrecision":53,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldGeography","DataType":"geography","NumericPrecision":0,"MaxLength":-1,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldGeometry","DataType":"geometry","NumericPrecision":0,"MaxLength":-1,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldHierarchyId","DataType":"hierarchyid","NumericPrecision":0,"MaxLength":892,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldImage","DataType":"image","NumericPrecision":0,"MaxLength":2147483647,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldMoney","DataType":"money","NumericPrecision":19,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldNChar","DataType":"nchar","NumericPrecision":0,"MaxLength":10,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldNText","DataType":"ntext","NumericPrecision":0,"MaxLength":1073741823,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldNumeric","DataType":"numeric","NumericPrecision":18,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldNVarchar","DataType":"nvarchar","NumericPrecision":0,"MaxLength":50,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldReal","DataType":"real","NumericPrecision":24,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldSmallDateTime","DataType":"smalldatetime","NumericPrecision":0,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldSmallInt","DataType":"smallint","NumericPrecision":5,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldSmallMoney","DataType":"smallmoney","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldText","DataType":"text","NumericPrecision":0,"MaxLength":2147483647,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldTime","DataType":"time","NumericPrecision":0,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldTimestamp","DataType":"timestamp","NumericPrecision":0,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":true},{"Name":"fieldTinyInt","DataType":"tinyint","NumericPrecision":3,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldUniqueIdentifier","DataType":"uniqueidentifier","NumericPrecision":0,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldVarBinary","DataType":"varbinary","NumericPrecision":0,"MaxLength":50,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldVarchar","DataType":"varchar","NumericPrecision":0,"MaxLength":50,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldVariant","DataType":"sql_variant","NumericPrecision":0,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"fieldXML","DataType":"xml","NumericPrecision":0,"MaxLength":-1,"IsNullable":false,"DatabaseGenerated":false},{"Name":"id","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":true}],"Constraints":[{"Name":"PK_TestAllFieldTypes","TableName":"TestAllFieldTypes","IsPrimaryKey":true,"IsUnique":true,"IsIdentity":true,"ConstraintType":"CLUSTERED","Columns":[{"Name":"id","Order":1,"Descending":false,"IsIncludedColumn":false}],"SchemaName":"dbo"}],"DefaultConstraints":[]},{"Name":"TestAllFieldTypesNullable","Columns":[{"Name":"fieldBigInt","DataType":"bigint","NumericPrecision":19,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldBinary","DataType":"binary","NumericPrecision":0,"MaxLength":50,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldBit","DataType":"bit","NumericPrecision":0,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldChar","DataType":"char","NumericPrecision":0,"MaxLength":10,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldDate","DataType":"date","NumericPrecision":0,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldDateTime","DataType":"datetime","NumericPrecision":0,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldDateTime2","DataType":"datetime2","NumericPrecision":0,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldDateTimeOffset","DataType":"datetimeoffset","NumericPrecision":0,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldDecimal","DataType":"decimal","NumericPrecision":18,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldFloat","DataType":"float","NumericPrecision":53,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldGeography","DataType":"geography","NumericPrecision":0,"MaxLength":-1,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldGeometry","DataType":"geometry","NumericPrecision":0,"MaxLength":-1,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldHierarchyId","DataType":"hierarchyid","NumericPrecision":0,"MaxLength":892,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldImage","DataType":"image","NumericPrecision":0,"MaxLength":2147483647,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldMoney","DataType":"money","NumericPrecision":19,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldNChar","DataType":"nchar","NumericPrecision":0,"MaxLength":10,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldNText","DataType":"ntext","NumericPrecision":0,"MaxLength":1073741823,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldNumeric","DataType":"numeric","NumericPrecision":18,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldNVarchar","DataType":"nvarchar","NumericPrecision":0,"MaxLength":50,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldReal","DataType":"real","NumericPrecision":24,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldSmallDateTime","DataType":"smalldatetime","NumericPrecision":0,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldSmallInt","DataType":"smallint","NumericPrecision":5,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldSmallMoney","DataType":"smallmoney","NumericPrecision":10,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldText","DataType":"text","NumericPrecision":0,"MaxLength":2147483647,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldTime","DataType":"time","NumericPrecision":0,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldTimestamp","DataType":"timestamp","NumericPrecision":0,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":true},{"Name":"fieldTinyInt","DataType":"tinyint","NumericPrecision":3,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldUniqueIdentifier","DataType":"uniqueidentifier","NumericPrecision":0,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldVarBinary","DataType":"varbinary","NumericPrecision":0,"MaxLength":50,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldVarchar","DataType":"varchar","NumericPrecision":0,"MaxLength":50,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldVariant","DataType":"sql_variant","NumericPrecision":0,"MaxLength":0,"IsNullable":true,"DatabaseGenerated":false},{"Name":"fieldXML","DataType":"xml","NumericPrecision":0,"MaxLength":-1,"IsNullable":true,"DatabaseGenerated":false},{"Name":"id","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":true}],"Constraints":[{"Name":"PK_TestAllFieldTypesNullable","TableName":"TestAllFieldTypesNullable","IsPrimaryKey":true,"IsUnique":true,"IsIdentity":true,"ConstraintType":"CLUSTERED","Columns":[{"Name":"id","Order":1,"Descending":false,"IsIncludedColumn":false}],"SchemaName":"dbo"}],"DefaultConstraints":[]},{"Name":"TimestampCheck","Columns":[{"Name":"id","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":true},{"Name":"name","DataType":"varchar","NumericPrecision":0,"MaxLength":50,"IsNullable":true,"DatabaseGenerated":false},{"Name":"timestamp","DataType":"timestamp","NumericPrecision":0,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":true}],"Constraints":[{"Name":"PK_TimestampCheck","TableName":"TimestampCheck","IsPrimaryKey":true,"IsUnique":true,"IsIdentity":true,"ConstraintType":"CLUSTERED","Columns":[{"Name":"id","Order":1,"Descending":false,"IsIncludedColumn":false}],"SchemaName":"dbo"}],"DefaultConstraints":[]}],"ForeignKeys":[{"Columns":[{"ForeignKeyTableName":"ColumnSameAsFKTable","ForeignKeyColumnName":"Person","PrimaryKeyTableName":"PERSON","PrimaryKeyColumnName":"PERSON_ID","PrimaryKeySchemaName":"dbo","ForeignKeySchemaName":"dbo","Order":1}],"ForeignKeyName":"FK_ColumnSameAsFKTable_PERSON"},{"Columns":[{"ForeignKeyTableName":"ColumnSameAsFKTable","ForeignKeyColumnName":"PersonId","PrimaryKeyTableName":"PERSON","PrimaryKeyColumnName":"PERSON_ID","PrimaryKeySchemaName":"dbo","ForeignKeySchemaName":"dbo","Order":1}],"ForeignKeyName":"FK_ColumnSameAsFKTable_PERSONId"},{"Columns":[{"ForeignKeyTableName":"selfReference","ForeignKeyColumnName":"selfReferenceId","PrimaryKeyTableName":"selfReference","PrimaryKeyColumnName":"id","PrimaryKeySchemaName":"dbo","ForeignKeySchemaName":"dbo","Order":1}],"ForeignKeyName":"FK_selfReference_selfReference"},{"Columns":[{"ForeignKeyTableName":"selfReference","ForeignKeyColumnName":"selfReferenceId2","PrimaryKeyTableName":"selfReference","PrimaryKeyColumnName":"id","PrimaryKeySchemaName":"dbo","ForeignKeySchemaName":"dbo","Order":1}],"ForeignKeyName":"FK_selfReference_selfReference2"}]},{"Name":"SchemaA","Tables":[{"Name":"Person","Columns":[{"Name":"id","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":true},{"Name":"name","DataType":"varchar","NumericPrecision":0,"MaxLength":50,"IsNullable":false,"DatabaseGenerated":false}],"Constraints":[{"Name":"PK_Person","TableName":"Person","IsPrimaryKey":true,"IsUnique":true,"IsIdentity":true,"ConstraintType":"CLUSTERED","Columns":[{"Name":"id","Order":1,"Descending":false,"IsIncludedColumn":false}],"SchemaName":"SchemaA"}],"DefaultConstraints":[]}],"ForeignKeys":[]},{"Name":"SchemaB","Tables":[{"Name":"Person","Columns":[{"Name":"id","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":true},{"Name":"name","DataType":"varchar","NumericPrecision":0,"MaxLength":50,"IsNullable":false,"DatabaseGenerated":false}],"Constraints":[{"Name":"PK_Person_1","TableName":"Person","IsPrimaryKey":true,"IsUnique":true,"IsIdentity":true,"ConstraintType":"CLUSTERED","Columns":[{"Name":"id","Order":1,"Descending":false,"IsIncludedColumn":false}],"SchemaName":"SchemaB"}],"DefaultConstraints":[]},{"Name":"PersonRef","Columns":[{"Name":"id","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":true},{"Name":"personAId","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false},{"Name":"personBId","DataType":"int","NumericPrecision":10,"MaxLength":0,"IsNullable":false,"DatabaseGenerated":false}],"Constraints":[{"Name":"PK_PersonRef","TableName":"PersonRef","IsPrimaryKey":true,"IsUnique":true,"IsIdentity":true,"ConstraintType":"CLUSTERED","Columns":[{"Name":"id","Order":1,"Descending":false,"IsIncludedColumn":false}],"SchemaName":"SchemaB"}],"DefaultConstraints":[]}],"ForeignKeys":[{"Columns":[{"ForeignKeyTableName":"PersonRef","ForeignKeyColumnName":"personAId","PrimaryKeyTableName":"Person","PrimaryKeyColumnName":"id","PrimaryKeySchemaName":"SchemaA","ForeignKeySchemaName":"SchemaB","Order":1}],"ForeignKeyName":"FK_PersonRef_PersonA"},{"Columns":[{"ForeignKeyTableName":"PersonRef","ForeignKeyColumnName":"personBId","PrimaryKeyTableName":"Person","PrimaryKeyColumnName":"id","PrimaryKeySchemaName":"SchemaB","ForeignKeySchemaName":"SchemaB","Order":1}],"ForeignKeyName":"FK_PersonRef_PersonB"}]}],"Version":"1.0","DatabaseType":"MSSQL"}`;



interface IPrimaryKeyIconProps {
  primaryKey: Models.ConstraintColumn
  foreignKeys: Array<Models.ForeignKeyModel>;
  context:any;
}

interface IPrimaryKeyIconState{
  showPopup:boolean;
  target:HTMLSpanElement;
}

class PrimaryKeyIcon extends React.Component<IPrimaryKeyIconProps, IPrimaryKeyIconState>
{
  public context:any;
  public refs:any;
  public state:IPrimaryKeyIconState = {showPopup:false, target:null};

  constructor(props:IPrimaryKeyIconProps, state:IPrimaryKeyIconState ) {
    super(props, state);
  }
  private buttonSpan: HTMLSpanElement;

  render() {

    let fkColumns: JSX.Element [] = [];

    this.props.foreignKeys.forEach((foreignKey) =>
    {
      foreignKey.Columns.forEach((foreignKeyColumn) => 
      {
        fkColumns.push(<p key={foreignKeyColumn.ForeignKeyColumnName}>{foreignKeyColumn.ForeignKeySchemaName}.{foreignKeyColumn.ForeignKeyTableName}.{foreignKeyColumn.ForeignKeyColumnName}</p>);
      });
    });

    let popOver = (<BS.Popover id={this.props.primaryKey.Name} title={this.props.primaryKey.Name}>
        {fkColumns}
    </BS.Popover>);

    let button = <span 
      onClick={(e) => {this.setState({showPopup:!this.state.showPopup, target:this.state.target})}}
      ref = {(ref) => this.buttonSpan = ref}>
      <FontAwesomeIcon icon="key" className={"primary-key-icon"} />
      </span>;

    return  <span className="primary-key-container">
              {button}

              <BS.Overlay
                show={this.state.showPopup}
                container={this}
                placement="bottom"
                target={() => ReactDOM.findDOMNode(this.buttonSpan)}>
                {popOver}
              </BS.Overlay>
          </span>;
  }
}


interface IForeignKeyIconProps {
  foreignKey: Models.ForeignKeyModel
  context:any;
}

interface IForeignKeyIconState {
  showPopup:boolean;
  target:HTMLSpanElement;
}

class ForeignKeyIcon extends React.Component<IForeignKeyIconProps, IForeignKeyIconState>
{
  public context:any;
  public refs:any;
  public state:IForeignKeyIconState = {showPopup:false, target:null};

  constructor(props:IForeignKeyIconProps, state:IForeignKeyIconState ) {
    super(props, state);
  }
  private buttonSpan: HTMLSpanElement;

  render() {

    let fkColumns: JSX.Element [] = [];

    this.props.foreignKey.Columns.forEach((foreignKeyColumn) =>
    {
      fkColumns.push(<p key={foreignKeyColumn.ForeignKeyColumnName}>{foreignKeyColumn.PrimaryKeySchemaName}.{foreignKeyColumn.PrimaryKeyTableName}.{foreignKeyColumn.PrimaryKeyColumnName}</p>);
    });

    let popOver = (<BS.Popover id={this.props.foreignKey.ForeignKeyName} title={this.props.foreignKey.ForeignKeyName}>
        {fkColumns}
    </BS.Popover>);

    let button = <span 
      onClick={(e) => {this.setState({showPopup:!this.state.showPopup, target:this.state.target})}}
      ref = {(ref) => this.buttonSpan = ref}>
      <FontAwesomeIcon icon="key" className={"foreign-key-icon"} />
      </span>;

    return  <span className="foreign-key-container">
              {button}

              <BS.Overlay
                show={this.state.showPopup}
                container={this}
                placement="bottom"
                target={() => ReactDOM.findDOMNode(this.buttonSpan)}>
                {popOver}
              </BS.Overlay>
          </span>;
  }
}

class ColumnIcon extends React.Component
{
  public context:any;
  public refs:any;

  render() {

    return <FontAwesomeIcon icon="server" className={"column-icon"} />;
  }
}


interface IConstraintComponentProps {
  constraint: Models.ConstraintModel;
  context:any;
}

interface IConstraintComponentState{
}

class ConstraintComponent extends React.Component<IConstraintComponentProps, IColumnColumnComponentState>
{
  public context:any;
  public refs:any;

  render() {
      let columns: JSX.Element[] = [];

      this.props.constraint.Columns.forEach((constraintColumn:Models.ConstraintColumn) => 
      {
          columns.push(<p key={constraintColumn.Name}>{constraintColumn.Name} {constraintColumn.Descending ? " (desc)" : null} {constraintColumn.IsIncludedColumn ? " (included column)" : null}</p>)  
      });

    return <div>
              <div>
              <strong>{this.props.constraint.Name}</strong> {this.props.constraint.IsIdentity ? " (identity)" : null} {this.props.constraint.IsUnique ? " (unique)" : null} {this.props.constraint.IsPrimaryKey ? " (primary key)" : null} 
              </div>
        <div>
        {columns}
       </div>
       <br />
    </div>;
  }
}


interface IColumnComponentProps {
  column: Models.ColumnModel;
  primaryKey:Models.ConstraintColumn;
  foreignKeysToPrimaryKey:Array<Models.ForeignKeyModel>;
  foreignKey:Models.ForeignKeyModel;
  context:any;
}

interface IColumnColumnComponentState{
}

class ColumnComponent extends React.Component<IColumnComponentProps, IColumnColumnComponentState>
{
  public context:any;
  public refs:any;
  

  render() {
    let icon: JSX.Element = null;

    if (this.props.primaryKey != null) {
      icon = <PrimaryKeyIcon primaryKey={this.props.primaryKey} foreignKeys={this.props.foreignKeysToPrimaryKey} context={null} />;
    }
    else if (this.props.foreignKey != null) {
      icon = <ForeignKeyIcon foreignKey={this.props.foreignKey} context={null} />;
    }
    else  {
      icon = <ColumnIcon />;
    }

    return <div>{icon} {this.props.column.Name} ({this.props.column.DataType}{this.props.column.MaxLength > 0 ? " (" + this.props.column.MaxLength + ")" : null}, {this.props.column.IsNullable ? "null" : "not null"})</div>;
  }
}

interface IFormEditorProps {
    database: Models.DatabaseParameterModel;
    context:any;
}

interface IFormEditorState {

}

class FormDatabaseEditor extends React.Component<IFormEditorProps,IFormEditorState>
{
    constructor(props:IFormEditorProps) {
        super(props);
    }

    public props:IFormEditorProps ;
    public state:IFormEditorState ;
    public context:any;
    public refs:any;

    setState() {

    }

    forceUpdate(){

    }
    render() {

        let tables: JSX.Element[] = [];
        this.props.database.Schemas.forEach((schema:Models.SchemaModel) => {
            schema.Tables.forEach((table:Models.TableModel) => {

                let columns: JSX.Element[] = [];
                let foreignKeyPopups: JSX.Element[] = [];
                let constraints: JSX.Element[] = [];
                let primaryKey = Models.GetPrimaryKeyConstraint(table);

                if(primaryKey == null)
                {
                  return;
                }
                let foreignKeysToPrimaryKey = Models.GetForeignKeysForColumn(this.props.database,schema.Name,table.Name,primaryKey.Columns[0].Name);
             
                let sortedColumns = Array<Models.ColumnModel>();
                
                //put the primary key columns at the top of the list
                primaryKey.Columns.forEach(keyColumn => {
                  let column = table.Columns.find(x => x.Name == keyColumn.Name);
                  if(column != null)
                  {
                    sortedColumns.push(column);
                  }
                });

                // add the non primary key columns in order
                table.Columns.forEach((column:Models.ColumnModel) => {
                    if(!primaryKey.Columns.find(x => x.Name == column.Name)){
                      sortedColumns.push(column);
                    }
                });

                sortedColumns.forEach((column:Models.ColumnModel) => {
                  let foreignKey = Models.GetColumnForeignKey(this.props.database,schema.Name,table.Name,column.Name);

                  let primaryKeyForColumn:Models.ConstraintColumn = null;

                  primaryKey.Columns.forEach(key => {
                    if(key.Name == column.Name)
                    {
                       primaryKeyForColumn = key;
                    }
                  });

                  let columnItem = <ColumnComponent key={column.Name} 
                  column={column} 
                  context={null} 
                  primaryKey={primaryKeyForColumn} 
                  foreignKeysToPrimaryKey={column.Name == primaryKey.Columns[0].Name ? foreignKeysToPrimaryKey : []} 
                  foreignKey={foreignKey} />; 
                  columns.push(columnItem);
                });
                
                table.Constraints.forEach((constraint:Models.ConstraintModel) =>
                {
                  let constraintItem = <ConstraintComponent key={constraint.Name} constraint={constraint} context={null} />
                  constraints.push(constraintItem);
                });

                let foreignKeysForTable = Models.GetForeignKeysForTable(this.props.database,schema.Name,table.Name);

                tables.push(<BS.Panel bsStyle="primary" className="main-panel" key={schema.Name + table.Name}>
                    <BS.Panel.Heading>
                        <BS.Panel.Title componentClass="h3">{table.Name}</BS.Panel.Title>
                    </BS.Panel.Heading>
                    <BS.Panel.Body>
                        {columns}
                        <br/>
                        {constraints}
                    </BS.Panel.Body>
                </BS.Panel>);
            });
        }); 

        return <div className="row col-md-8 col-md-offset-2">
            {tables}
            </div>;


    }
}


  let jsonDatabase =  JSON.parse(databaseJson); 
  let originalDatabaseModel = jsonDatabase as Models.DatabaseParameterModel;
  let formDatabaseEditor = <FormDatabaseEditor database={originalDatabaseModel} context={null} />;
  
  ReactDOM.render(
    formDatabaseEditor,
    document.getElementById("app")
  );

