CREATE PROCEDURE [dbo].[spAddUser]
	@UserName nvarchar(150),
	@Password nvarchar(150)
AS
BEGIN 
	INSERT INTO Users(UserName, Password)
	VALUES(@UserName, @Password)
END
