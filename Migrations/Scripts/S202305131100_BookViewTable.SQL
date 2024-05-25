USE [$DatabaseName$]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW  [dbo].[BookView] 
WITH SCHEMABINDING
AS 
SELECT [BookId],
JSON_VALUE(BookInfo, '$.BookTitle') AS Title,
JSON_VALUE(BookInfo, '$.Author') AS Author,
JSON_VALUE(BookInfo, '$.BookDescription') AS [Description],
JSON_VALUE(BookInfo, '$.PublishDate') AS PublishDate,
[LastModified],
SUBSTRING(
        BookInfo, 
        CHARINDEX('"CoverBase64":"', BookInfo) + LEN('"CoverBase64":"'),
        CHARINDEX('"', BookInfo, CHARINDEX('"CoverBase64":"', BookInfo) + LEN('"CoverBase64":"')) - CHARINDEX('"CoverBase64":"', BookInfo) - LEN('"CoverBase64":"')
    ) AS CoverBase64

FROM [dbo].[Book] 
GO

CREATE UNIQUE CLUSTERED INDEX IX_BookView_BookId ON dbo.BookView (BookId);
CREATE NONCLUSTERED INDEX IX_BookView_Title ON BookView(Title);
CREATE NONCLUSTERED INDEX IX_BookView_Author ON BookView(Author);
CREATE NONCLUSTERED INDEX IX_BookView_Description ON BookView([Description]);
CREATE NONCLUSTERED INDEX IX_BookView_PublishDate ON BookView(PublishDate); 
GO
