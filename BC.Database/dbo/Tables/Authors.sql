CREATE TABLE [dbo].[Authors]
(
	[AuthorId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(MAX) NOT NULL, 
    [LastName] NVARCHAR(MAX) NOT NULL, 
    [YearBorn] SMALLINT NULL, 
    [YearDied] INT NULL, 
    [Country] NVARCHAR(MAX) NULL, 
    [Quote] NVARCHAR(MAX) NULL, 
    [Rating] SMALLINT NULL, 
    [ExtraInfo] NVARCHAR(MAX) NULL
)
