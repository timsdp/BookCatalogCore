CREATE TABLE [dbo].[BooksAuthors]
(
	[BookId] INT NOT NULL , 
    [AuthorId] INT NOT NULL, 
    CONSTRAINT [FK_BooksAuthors_BookId] FOREIGN KEY ([BookId]) REFERENCES [Books]([BookId]), 
    CONSTRAINT [FK_BooksAuthors_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Authors]([AuthorId]), 
    PRIMARY KEY ([AuthorId], [BookId]), 
)
