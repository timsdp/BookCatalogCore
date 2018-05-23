CREATE TABLE [dbo].[Books] (
    [BookId]        INT            NOT NULL IDENTITY,
    [DatePublished] DATE           NOT NULL,
    [PagesCount]    INT            NOT NULL,
    [Name]          NVARCHAR (255) NOT NULL,
    [Language] NVARCHAR(255) NOT NULL, 
    CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED ([BookId] ASC)
);

