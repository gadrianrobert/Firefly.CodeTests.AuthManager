CREATE PROCEDURE [dbo].[spFindUser]
	@UserName nvarchar(150)
AS
BEGIN 
	SELECT UserName, Password FROM Users WHERE UserName = @UserName
END
