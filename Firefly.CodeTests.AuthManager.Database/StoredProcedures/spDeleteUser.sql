CREATE PROCEDURE [dbo].[spDeleteUser]
	@UserName nvarchar(150)
AS
BEGIN 
	DELETE FROM Users WHERE UserName = @UserName
END
