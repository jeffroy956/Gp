CREATE TABLE [dbo].[Varieties]
(
	[VarietyId] uniqueidentifier NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
	[CreateDate] DATETIMEOFFSET NOT NULL
)
