CREATE TABLE [dbo].[tblPlan]
(
	[PlanId] INT NOT NULL PRIMARY KEY NONCLUSTERED, 
	[CalendarId] INT NOT NULL,
	[RecurrenceId] INT NULL,
    [EventDescription] VARCHAR(255) NOT NULL, 
    [VarietyId] INT NULL, 
    [PlanDate] DATETIME NULL, 
    [ActualDate] DATETIME NULL, 
    [Notes] VARCHAR(MAX) NULL, 
    [LastModified] DATETIMEOFFSET NOT NULL DEFAULT getutcdate()
)
