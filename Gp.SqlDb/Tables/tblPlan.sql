CREATE TABLE [dbo].[tblPlan]
(
	[EventId] INT NOT NULL PRIMARY KEY, 
    [Description] VARCHAR(255) NOT NULL, 
    [VarietyId] INT NULL, 
    [PlanDate] DATETIMEOFFSET NULL, 
    [ActualDate] DATETIMEOFFSET NULL, 
    [Notes] VARCHAR(MAX) NULL
)
