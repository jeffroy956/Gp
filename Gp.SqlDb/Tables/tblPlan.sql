CREATE TABLE [dbo].[tblPlan]
(
	[PlanId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY NONCLUSTERED, 
    [EventDescription] NVARCHAR(255) NOT NULL, 
	[Variety] NVARCHAR(50) NULL,
	[CalendarYear] INT NOT NULL,
    [PlanDate] DATETIME NULL, 
    [ActualDate] DATETIME NULL, 
    [LastModified] DATETIMEOFFSET NOT NULL DEFAULT getutcdate()
)
