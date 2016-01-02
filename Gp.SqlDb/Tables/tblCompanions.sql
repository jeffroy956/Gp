CREATE TABLE [dbo].[tblCompanions]
(
    [FamilyId] INT NOT NULL, 
	[RelatedFamilyId] INT NOT NULL,
    [LastModified] DATETIMEOFFSET NOT NULL DEFAULT getutcdate(),
	CONSTRAINT pk_tblCompanions PRIMARY KEY (FamilyId,[RelatedFamilyId])
)
