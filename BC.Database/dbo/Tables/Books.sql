CREATE TABLE [dbo].[Books] (
    [BookId]        INT            NOT NULL IDENTITY,
    [DatePublished] DATE           NOT NULL,
    [PagesCount]    INT            NOT NULL,
    [Name]          NVARCHAR (255) NOT NULL,
    [Rating] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED ([BookId] ASC)
);

