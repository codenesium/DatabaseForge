create database ForgeDB
go

use ForgeDB
go

IF NOT EXISTS(SELECT *
FROM sys.schemas
WHERE name = N'fge2')
EXEC('CREATE SCHEMA [fge2] AUTHORIZATION [dbo]');
GO

--IF (OBJECT_ID('fge2.fk_tableb_tableaid_tablea_id', 'F') IS NOT NULL)
--BEGIN
--ALTER TABLE [fge2].[tableB] DROP CONSTRAINT [fk_tableb_tableaid_tablea_id]
--END
--GO

--IF OBJECT_ID('fge2.tableA', 'U') IS NOT NULL 
--BEGIN
--DROP TABLE [fge2].[tableA]
--END
--GO
--IF OBJECT_ID('fge2.tableB', 'U') IS NOT NULL 
--BEGIN
--DROP TABLE [fge2].[tableB]
--END
--GO

CREATE TABLE [fge2].[tableA](
[Id] [int]   IDENTITY(1,1)  NOT NULL,
[Description] [varchar]  (250)   NOT NULL,
) ON[PRIMARY]
GO

CREATE TABLE [fge2].[tableB](
[Id] [int]   IDENTITY(1,1)  NOT NULL,
[Description] [varchar]  (250)   NOT NULL,
[tableAId] [int]     NOT NULL,
) ON[PRIMARY]
GO

ALTER TABLE[fge2].[tableA]
ADD CONSTRAINT[PK_tableA] PRIMARY KEY 
(
[Id] ASC
)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF,  ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
ALTER TABLE[fge2].[tableB]
ADD CONSTRAINT[PK_tableB] PRIMARY KEY 
(
[Id] ASC
)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF,  ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE  NONCLUSTERED INDEX[IX_tableB_tableAId] ON[fge2].[tableB]
(
[tableAId] ASC)
WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


ALTER TABLE[fge2].[tableB]  WITH CHECK ADD  CONSTRAINT[fk_tableb_tableaid_tablea_id] FOREIGN KEY([tableAId])
REFERENCES[fge2].[tableA]([Id]) on delete no action on update no action
GO
ALTER TABLE[fge2].[tableB] CHECK CONSTRAINT[fk_tableb_tableaid_tablea_id]
GO



IF NOT EXISTS(SELECT *
FROM sys.schemas
WHERE name = N'fge2')
EXEC('CREATE SCHEMA [fge2] AUTHORIZATION [dbo]');
GO


--IF OBJECT_ID('fge2.ypesDatabase', 'U') IS NOT NULL 
--BEGIN
--DROP TABLE [fge2].[typesDatabase]
--END
--GO

CREATE TABLE [fge2].[typesDatabase](
[bigIntType] [bigint]     NOT NULL,
[binaryType] [binary]     NOT NULL,
[bitType] [bit]     NOT NULL,
[charType] [char]     NOT NULL,
[dateType] [date]     NOT NULL,
[datetimeType] [datetime]     NOT NULL,
[datetime2Type] [datetime2]     NOT NULL,
[datetimeoffsetType] [datetimeoffset]     NOT NULL,
[decimalType] [decimal]     NOT NULL,
[floatType] [float]     NOT NULL,
[geographyType] [geography]     NOT NULL,
[geometryType] [geometry]     NOT NULL,
[hierarchyidType] [hierarchyid]     NOT NULL,
[imageType] [image]     NOT NULL,
[intType] [int]     NOT NULL,
[moneyType] [money]     NOT NULL,
[ncharType] [nchar]     NOT NULL,
[ntextType] [ntext]     NOT NULL,
[numericType] [numeric]     NOT NULL,
[nvarcharType] [nvarchar]     NOT NULL,
[realType] [real]     NOT NULL,
[smalldatetimeType] [smalldatetime]     NOT NULL,
[smallintType] [smallint]     NOT NULL,
[smallmoneyType] [smallmoney]     NOT NULL,
[sql_variantType] [sql_variant]     NOT NULL,
[sysnameType] [sysname]     NOT NULL,
[textType] [text]     NOT NULL,
[timeType] [time]     NOT NULL,
[timestampType] [timestamp]     NOT NULL,
[tinyintType] [tinyint]     NOT NULL,
[uniqueidentifierType] [uniqueidentifier]     NOT NULL,
[varbinaryType] [varbinary]     NOT NULL,
[varcharType] [varchar]     NOT NULL,
[xmlType] [xml]     NOT NULL,

) ON[PRIMARY]
GO



IF NOT EXISTS(SELECT *
FROM sys.schemas
WHERE name = N'fge2')
EXEC('CREATE SCHEMA [fge2] AUTHORIZATION [dbo]');
GO

--IF (OBJECT_ID('fge2.fk_users_usersid_users_id', 'F') IS NOT NULL)
--BEGIN
--ALTER TABLE [fge2].[users] DROP CONSTRAINT [fk_users_usersid_users_id]
--END
--GO

--IF OBJECT_ID('fge2.users', 'U') IS NOT NULL 
--BEGIN
--DROP TABLE [fge2].[users]
--END
--GO

CREATE TABLE [fge2].[users](
[Id] [int]   IDENTITY(1,1)  NOT NULL,
[Name] [varchar]  (128)   NOT NULL,
[Lastname] [varchar]     NOT NULL,
[usersId] [int]     NOT NULL,
) ON[PRIMARY]
GO

ALTER TABLE[fge2].[users]
ADD CONSTRAINT[PK_users] PRIMARY KEY 
(
[Id] ASC
)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF,  ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE  NONCLUSTERED INDEX[IX_users_usersId] ON[fge2].[users]
(
[usersId] ASC)
WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


ALTER TABLE[fge2].[users]  WITH CHECK ADD  CONSTRAINT[fk_users_usersid_users_id] FOREIGN KEY([usersId])
REFERENCES[fge2].[users]([Id]) on delete no action on update no action
GO
ALTER TABLE[fge2].[users] CHECK CONSTRAINT[fk_users_usersid_users_id]
GO






-------------- USEFUL
select * FROM sys.types order by name


use [master]
declare @sql varchar(8000)='';
select @sql = @sql + 'kill '+CONVERT(varchar(5), session_id)+';'
from sys.dm_exec_sessions
where database_id= DB_ID('ForgeDB')
exec(@sql);


