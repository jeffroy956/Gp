CREATE TABLE [dbo].[tblEnemies]
(
    [FamilyId] INT NOT NULL, 
	[RelatedFamilyId] INT NOT NULL,
    [LastModified] DATETIMEOFFSET NOT NULL DEFAULT getutcdate(),
	CONSTRAINT pk_tblEnemies PRIMARY KEY (FamilyId,[RelatedFamilyId])
)

