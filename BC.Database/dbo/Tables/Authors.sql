CREATE TABLE [dbo].[Authors]
(
	[AuthorId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NCHAR(100) NOT NULL, 
    [LastName] NCHAR(100) NOT NULL
)
