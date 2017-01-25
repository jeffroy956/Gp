Declare @TableName VarChar(50)
Declare @SetAlias VarChar(50)
Declare @ReaderAlias VarChar(50)
Declare	@CmdAlias VarChar(50)

Set @TableName = 'tblPlan'
Set @SetAlias = 'plan'
Set @ReaderAlias = 'r'
Set @CmdAlias = 'cmd'

Select	sc.COLUMN_NAME + ',' As SelectColumn
,		 @CmdAlias + '.AddWithValue(@' + sc.COLUMN_NAME + ', ' + @SetAlias + '.' + sc.COLUMN_NAME + ')' As AddWithValue
,		'private int idx' + sc.COLUMN_NAME + ';' As DeclareOrdinalIndex
,		'int idx' + sc.COLUMN_NAME + ' = ' + @ReaderAlias + '.GetOrdinal(' + sc.COLUMN_NAME + ');' As SetOrdinalIndex
,		sc.COLUMN_NAME + ' = ' + @ReaderAlias + '.' +
		Case When sc.IS_NULLABLE = 'YES' Then 'GetSafe' Else 'Get' End +
		Case 
			When sc.DATA_TYPE like '%char%' Then 'String'
			When sc.DATA_TYPE = 'int' Then 'Int32'
			When sc.DATA_TYPE = 'datetime' Then 'DateTime'
			When sc.DATA_TYPE = 'datetimeoffset' Then 'DateTimeOffset'
			When sc.DATA_TYPE = 'uniqueidentifier' Then 'Guid'
			When sc.DATA_TYPE = 'decimal' then 'Decimal'
			Else 'UnmappedTypeSoAddIt'
		End + 
		'(idx' + sc.COLUMN_NAME + ');' As ReaderMapText
,		'@' + sc.COLUMN_NAME + ' ' + sc.DATA_TYPE +
			Case When sc.Character_Maximum_Length Is Not Null Then '(' + ltrim(str(sc.Character_Maximum_Length)) + ')' Else '' End + ',' As DeclareVariable
,		'@' + sc.COLUMN_NAME + ',' As DeclareVariableNoType
From INFORMATION_SCHEMA.COLUMNS AS sc Where TABLE_NAME = @TableName


Select *
From INFORMATION_SCHEMA.COLUMNS AS sc Where TABLE_NAME = @TableName
