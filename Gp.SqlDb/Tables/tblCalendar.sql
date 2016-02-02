CREATE TABLE [dbo].[tblCalendar]
(
	[CalendarId] INT NOT NULL PRIMARY KEY, 
    [Description] VARCHAR(50) NOT NULL, 
	[Year] INT NOT NULL,
    [LastModified] DATETIMEOFFSET NOT NULL DEFAULT getutcdate()
)
