CREATE TABLE [dbo].[tblFamilies]
(
	[FamilyID] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [LastModified] DATETIMEOFFSET NOT NULL DEFAULT getutcdate()
)
