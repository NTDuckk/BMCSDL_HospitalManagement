USE QLBenhVien_ACCOUNT;
GO

CREATE OR ALTER PROCEDURE sp_Login_CheckAccount
    @username VARCHAR(50),
    @passwordHash VARBINARY(MAX)  
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM ACCOUNT
        WHERE username = @username AND passwordHash = @passwordHash
    )
    BEGIN
        SELECT 
            A.username,
            A.typeID,
            A.maNhanVien,        
            P.permissionID
        FROM ACCOUNT A
        LEFT JOIN ACCOUNT_PERMISSION AP ON A.username = AP.username
        LEFT JOIN PERMISSION P ON AP.permissionID = P.permissionID
        WHERE A.username = @username AND A.passwordHash = @passwordHash;
    END
    ELSE
    BEGIN
        SELECT 
            NULL AS username, 
            NULL AS typeID, 
            NULL AS maNhanVien,   
            NULL AS permissionID;
    END
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].sp_Login_CheckAccount TO userAccountBenhVien
    AS [dbo];
GO
--drop proc sp_Login_CheckAccount