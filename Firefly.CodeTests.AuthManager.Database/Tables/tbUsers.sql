CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(150) NOT NULL, 
    [Password] NVARCHAR(150) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_Users_UserName] ON [dbo].[Users] ([UserName])
