CREATE TABLE [dbo].[tblVarieties]
(
	[VarietyId] INT NOT NULL PRIMARY KEY, 
    [FamilyId] INT NULL, 
    [Name] VARCHAR(50) NULL, 
    [LastModified] DATETIMEOFFSET NOT NULL DEFAULT getutcdate()
)
