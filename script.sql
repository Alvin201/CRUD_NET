USE [ImageSystem]
GO
/****** Object:  Database [ImageSystem]    Script Date: 07/07/2020 21:04:11 ******/
ALTER DATABASE [ImageSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ImageSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS_1\MSSQL\DATA\ImageSystem.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ImageSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ImageSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ImageSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ImageSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ImageSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ImageSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ImageSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ImageSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ImageSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [ImageSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ImageSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ImageSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ImageSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ImageSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ImageSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ImageSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ImageSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ImageSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ImageSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ImageSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ImageSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ImageSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ImageSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ImageSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ImageSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ImageSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ImageSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ImageSystem] SET  MULTI_USER 
GO
ALTER DATABASE [ImageSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ImageSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ImageSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ImageSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ImageSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ImageSystem] SET QUERY_STORE = OFF
GO
USE [ImageSystem]
GO
/****** Object:  Table [dbo].[DocumentUpload]    Script Date: 07/07/2020 21:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentUpload](
	[ID] [varchar](255) NOT NULL,
	[FileName] [varchar](255) NULL,
	[CurrentStatus] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ELMAH_Error]    Script Date: 07/07/2020 21:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELMAH_Error](
	[ErrorId] [uniqueidentifier] NOT NULL,
	[Application] [nvarchar](60) NOT NULL,
	[Host] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](60) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[StatusCode] [int] NOT NULL,
	[TimeUtc] [datetime] NOT NULL,
	[Sequence] [int] IDENTITY(1,1) NOT NULL,
	[AllXml] [ntext] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ErrorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogDocumentUpload]    Script Date: 07/07/2020 21:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogDocumentUpload](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](255) NULL,
	[LogMessage] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogUserSetup]    Script Date: 07/07/2020 21:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogUserSetup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](255) NOT NULL,
	[LogMessage] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 07/07/2020 21:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[IDMenu] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NULL,
	[URL] [varchar](max) NULL,
	[Fa_Awesome] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IDMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionSetup]    Script Date: 07/07/2020 21:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionSetup](
	[PermissionID] [int] IDENTITY(1,1) NOT NULL,
	[PermissDescription] [varchar](50) NULL,
	[IDmenu] [int] NULL,
 CONSTRAINT [PK_PermissionSetup] PRIMARY KEY CLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleMenuSetup]    Script Date: 07/07/2020 21:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMenuSetup](
	[IDRoleMenu] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NULL,
	[MenuID] [int] NULL,
 CONSTRAINT [PK_RoleMenuSetup] PRIMARY KEY CLUSTERED 
(
	[IDRoleMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleSetup]    Script Date: 07/07/2020 21:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleSetup](
	[ID_Role] [int] IDENTITY(1,1) NOT NULL,
	[Role_Desc] [nvarchar](50) NULL,
 CONSTRAINT [PK_RoleSetup] PRIMARY KEY CLUSTERED 
(
	[ID_Role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSetup]    Script Date: 07/07/2020 21:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSetup](
	[NIK] [int] NULL,
	[Username] [varchar](255) NULL,
	[Password] [varchar](255) NULL,
	[CurrentStatus] [varchar](50) NULL,
	[AttemptsLeft] [int] NULL,
	[LastLoginDate] [datetime] NULL,
	[LastPasswordChangedDate] [datetime] NULL,
	[Role_ID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[ChangePassword]    Script Date: 07/07/2020 21:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChangePassword]
@NIK int,
@Password varchar(max)

AS
BEGIN
	Update dbo.UserSetup SET Password = @Password  WHERE NIK = @NIK
END
GO
/****** Object:  StoredProcedure [dbo].[ChangePassword_day]    Script Date: 07/07/2020 21:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChangePassword_day]
@NIK varchar(255),
@Password varchar(max)

AS
BEGIN
	Update dbo.UserSetup SET Password = @Password,LastPasswordChangedDate= Dateadd(day, +30, LastPasswordChangedDate)  WHERE NIK = @NIK
END
GO
/****** Object:  StoredProcedure [dbo].[CheckLogin]    Script Date: 07/07/2020 21:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckLogin]  
    @nik nvarchar(255),  
    @password nvarchar(255)  
AS  
BEGIN  
  
    SET NOCOUNT ON;  
  
    DECLARE @Userpassword nvarchar(255) = NULL  
    DECLARE @maxAttempts int  = 5  
    DECLARE @attemptsLeft int = 5  
    DECLARE @currentStatus nvarchar(50)
	DECLARE @LastPasswordChangedDate datetime
	DECLARE @logstatus nvarchar(50)
	
    /*  
        RETURN CODES:  
        0 = Authorized  
        1 = InvalidCredentials  
        2 = Suspended 
    */  
--SELECT TOP(1) @Userpassword = [Password], @attemptsLeft = [AttemptsLeft], @currentStatus = [CurrentStatus], @LastPasswordChangedDate = [LastPasswordChangedDate] FROM [UserSetup] WHERE NIK = @nik  
 
-- SELECT TOP (1) CAST(LastLoginDate as date), DATEDIFF(day,LastLoginDate, GETDATE()) AS day_to_exp, [Password] = @Userpassword,[AttemptsLeft]=@attemptsLeft, [CurrentStatus] = @currentStatus,[LastPasswordChangedDate] =@LastPasswordChangedDate,
--	CASE WHEN  DATEDIFF(day,LastLoginDate, GETDATE()) > 6 THEN 'Expired'
--            ELSE NULL END AS status_exp
--FROM UserSetup WHERE NIK = @nik  
 
--	IF @logstatus = 'Expired'
--		BEGIN  
--            SELECT 1 as [Result], @maxAttempts as [AttemptsRemaining]  
--            RETURN  
--        END  
 
  SELECT TOP (1) @Userpassword = us.Password,@attemptsLeft = us.AttemptsLeft, @currentStatus = us.CurrentStatus, @LastPasswordChangedDate = us.LastPasswordChangedDate FROM UserSetup us 
  
   INNER JOIN RoleSetup rol ON us.Role_ID  = rol.ID_Role
  INNER JOIN RoleMenuSetup rol_m ON us.Role_ID = rol_m.RoleID 
  INNER JOIN Menu men ON men.IDMenu = rol_m.MenuID
  
 WHERE us.NIK = @nik

    IF @Userpassword IS NULL  
        BEGIN  
            SELECT 1 as [Result], @maxAttempts as [AttemptsRemaining]  
            RETURN  
        END  
  
    --account suspended.. Return a suspended result  
    If @currentStatus = 'Suspended'  
        BEGIN  
            SELECT 2 as [Result], 0 as [AttemptsRemaining]  
            RETURN  
        END  
  
    --passwords dont match (note this is case insensitive on default collation)  
    If @password IS NULL OR @password <> @Userpassword  
        BEGIN  
            --decrease attempts  
            SET @attemptsLeft = @attemptsLeft - 1  
  
            --if the attempts left are greater than 0 then set the account active and decrease the attempts remaining  
            IF @attemptsLeft > 0  
                BEGIN  
                    UPDATE [UserSetup] SET [CurrentStatus] = 'Active', AttemptsLeft = @attemptsLeft WHERE NIK = @nik  
                    SELECT 1 as [Result], @attemptsLeft as [AttemptsRemaining]  
        RETURN  
                END  
            --else the attempts left are less than or equal to zero therefore they should be suspended and attempts left set to zero (dont want negative attempts)  
            ELSE  
                BEGIN  
                    UPDATE [UserSetup] SET [CurrentStatus] = 'Suspended', AttemptsLeft = 0 WHERE NIK = @nik  
                    SELECT 2 as [Result], 0 as [AttemptsRemaining]  
                    RETURN  
      
    END  
        END  
    --if we get here then all is good and we can just reset the account status and max attempts for the next login attempt  
    UPDATE [UserSetup] SET [CurrentStatus] = 'Active', AttemptsLeft = @maxAttempts,LastLoginDate = GETDATE() WHERE NIK = @nik  
	INSERT INTO [LogUserSetup] SELECT @nik,'Succesfully Login'  
	SELECT 0 as [Result], @maxAttempts AS [AttemptsRemaining]  
  
END  
GO
/****** Object:  StoredProcedure [dbo].[ELMAH_GetErrorsXml]    Script Date: 07/07/2020 21:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    Create PROCEDURE[dbo].[ELMAH_GetErrorsXml]  
      
    (  
        @Application NVARCHAR(60),  
        @PageIndex INT = 0,  
        @PageSize INT = 15,  
        @TotalCount INT OUTPUT  
      
    )  
      
    AS  
      
    SET NOCOUNT ON  
      
    DECLARE @FirstTimeUTC DATETIME  
    DECLARE @FirstSequence INT  
    DECLARE @StartRow INT  
    DECLARE @StartRowIndex INT  
    SELECT  
      
    @TotalCount = COUNT(1)  
      
    FROM  
      
        [ELMAH_Error]  
      
    WHERE  
      
        [Application] = @Application  
    SET @StartRowIndex = @PageIndex * @PageSize + 1  
    IF @StartRowIndex <= @TotalCount  
      
    BEGIN  
      
    SET ROWCOUNT @StartRowIndex  
      
    SELECT  
      
    @FirstTimeUTC = [TimeUtc],  
      
        @FirstSequence = [Sequence]  
      
    FROM  
      
        [ELMAH_Error]  
      
    WHERE  
      
        [Application] = @Application  
      
    ORDER BY  
      
        [TimeUtc] DESC,  
        [Sequence] DESC  
      
    END  
      
    ELSE  
      
    BEGIN  
      
    SET @PageSize = 0  
      
    END  
      
    SET ROWCOUNT @PageSize  
      
    SELECT  
      
    errorId = [ErrorId],  
      
        application = [Application],  
        host = [Host],  
        type = [Type],  
        source = [Source],  
        message = [Message],  
        [user] = [User],  
        statusCode = [StatusCode],  
        time = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'  
      
    FROM  
      
        [ELMAH_Error] error  
      
    WHERE  
      
        [Application] = @Application  
      
    AND  
      
        [TimeUtc] <= @FirstTimeUTC  
      
    AND  
      
        [Sequence] <= @FirstSequence  
      
    ORDER BY  
      
        [TimeUtc] DESC,  
      
        [Sequence] DESC  
      
    FOR  
      
    XML AUTO  
GO
/****** Object:  StoredProcedure [dbo].[ELMAH_GetErrorXml]    Script Date: 07/07/2020 21:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    Create PROCEDURE[dbo].[ELMAH_GetErrorXml]  
      
    (  
      
        @Application NVARCHAR(60),  
        @ErrorId UNIQUEIDENTIFIER  
      
    )  
      
    AS  
      
    SET NOCOUNT ON  
    SELECT  
      
        [AllXml]  
    FROM  
      
        [ELMAH_Error]  
    WHERE  
      
        [ErrorId] = @ErrorId  
    AND  
        [Application] = @Application
GO
/****** Object:  StoredProcedure [dbo].[ELMAH_LogError]    Script Date: 07/07/2020 21:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    Create PROCEDURE[dbo].[ELMAH_LogError]  
      
    (  
      
        @ErrorId UNIQUEIDENTIFIER,    
        @Application NVARCHAR(60),    
        @Host NVARCHAR(30),    
        @Type NVARCHAR(100),  
        @Source NVARCHAR(60),    
        @Message NVARCHAR(500),  
        @User NVARCHAR(50),   
        @AllXml NTEXT,    
        @StatusCode INT,   
        @TimeUtc DATETIME  
      
    )  
      
    AS  
      
    SET NOCOUNT ON  
      
    INSERT  
      
    INTO  
      
        [ELMAH_Error]
    (  
      
        [ErrorId],   
        [Application],   
        [Host],  
        [Type],  
        [Source],  
        [Message],    
        [User],    
        [AllXml],    
        [StatusCode],    
        [TimeUtc]  
      
    )  
      
    VALUES  
      
        (  
      
        @ErrorId,  
        @Application,    
        @Host,    
        @Type,    
        @Source,   
        @Message,    
        @User,   
        @AllXml,   
        @StatusCode,   
        @TimeUtc  
      
    )
GO
/****** Object:  StoredProcedure [dbo].[InsertDocument]    Script Date: 07/07/2020 21:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertDocument]  
(
@FileName VARCHAR(255),  
@CurrentStatus int,  
@Action nvarchar(20) = '',
@NIK VARCHAR(255)
)  
AS  
			DECLARE 
			@awalan varchar(255),
			@hari int,
			@bulan varchar(255),
			@tahun int,
			@number_unique varchar(255),
			@urut int,
			@gabung varchar(255)
			
			SELECT @urut = CONVERT(INT, isnull(max(right(ID,4)),0)) from DocumentUpload
		    
			--PRINT @ID

		   SET @awalan = 'DOC'
		   SET @hari = CONVERT(int,RIGHT(CAST(day(getdate()) as varchar),2))
		   SET @bulan = CONVERT(varchar,UPPER(LEFT (CAST(format(getdate(),'MM') as varchar),2)))
		   SET @tahun = CONVERT(int,RIGHT(CAST(year(getdate()) as varchar),2))
		   SET @number_unique = CONVERT(varchar,RIGHT(CAST(year(getdate()) as varchar),2) + RIGHT('0000' + CAST(isnull((@urut + 1), 1) as nvarchar), 4))

		   SET @gabung =  CONVERT(varchar,CONCAT(@awalan,@hari,@bulan,@tahun,@number_unique))

BEGIN  
	IF @Action = 'INSERT'  
	BEGIN  
	INSERT INTO DocumentUpload (ID,FileName,CurrentStatus) VALUES (@gabung,@FileName,@CurrentStatus) 
	--insert log
	INSERT INTO [LogDocumentUpload] SELECT @NIK, 'New document has been inserted'		
	END  

--Mengembalikan nilai @gabung
RETURN @number_unique
END

 
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 07/07/2020 21:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUser]  
(  
@Username VARCHAR(255), 
@Password VARCHAR(255),
@Action  VARCHAR(255)
)  
AS  
BEGIN  


	DECLARE 
	@NIKAutoNumber int,
	@urut int,
	@number_unique varchar(255),
	@month varchar(255),
	@year varchar(255)

	SELECT @urut = CONVERT(INT, isnull(max(right(NIK,4)),0)) from UserSetup

	SET @year  = right(cast(year(getdate()) as varchar),2) 
	SET @month = right('00' + cast(month(getdate()) as varchar), 2)
	SET @number_unique = CONVERT(varchar,RIGHT(CAST(year(getdate()) as varchar),2) + RIGHT('0000' + CAST(isnull((@urut + 1), 1) as nvarchar), 4))
	
	SET @NIKAutoNumber =  CONVERT(int,CONCAT(@year,@month,@number_unique))

	IF @Action = 'INSERT'  
			BEGIN  
			INSERT into dbo.UserSetup
			(
				NIK,
				Username,
				Password,
				CurrentStatus,
				AttemptsLeft
			)
			VALUES
			(
				 --GENERATE NUMBER NIK	
				@NIKAutoNumber,
				@Username, -- Username - varchar
				@Password, -- Password - varchar
				'Active', -- CurrentStatus - varchar
				5 -- AttemptsLeft - int
			)END  

--Mengembalikan nilai @gabung
RETURN @number_unique
END

 
GO
/****** Object:  StoredProcedure [dbo].[Menu_Session]    Script Date: 07/07/2020 21:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
  CREATE PROCEDURE [dbo].[Menu_Session] (
  @NIK varchar(255)

  ) AS
  BEGIN

  SELECT us.NIK, * FROM UserSetup us 
  INNER JOIN RoleMenuSetup rol_m ON us.NIK = rol_m.NIK 
  INNER JOIN RoleSetup rol ON us.Role_ID  = rol.ID_Role
  INNER JOIN Menu men ON men.IDMenu = rol_m.MenuID
  WHERE us.NIK = @NIK
  END
GO
/****** Object:  StoredProcedure [dbo].[UpdateDocument]    Script Date: 07/07/2020 21:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateDocument] (
	@ID VARCHAR(255),
	@FileName  VARCHAR(255),
	@CurrentStatus int,
	@NIK VARCHAR(255),
	@Action varchar(255) = ''
)

AS
BEGIN

	IF @Action = 'UPDATE'  
	BEGIN  
    
	UPDATE DocumentUpload SET FileName = @FileName, CurrentStatus = @CurrentStatus WHERE ID = @ID 
	
	--insert log
	INSERT INTO [LogDocumentUpload] SELECT  @NIK,'Document ID :' + CONVERT(varchar,@ID) +' has been updated'
	END
	
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 07/07/2020 21:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateUser] (
	@NIK int,
	@Password  VARCHAR(255),
	@Username VARCHAR(255),
	@Action varchar(255) = ''
)

AS
BEGIN

		IF @Action = 'UPDATE'
	
		BEGIN
			UPDATE UserSetup SET Username = @Username,Password=@Password,CurrentStatus='Active' WHERE NIK = @NIK  
		END


	
END
GO
/****** Object:  StoredProcedure [dbo].[UploadMasterDocumentUpd]    Script Date: 07/07/2020 21:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UploadMasterDocumentUpd]
	@ID varchar(255) = '',
	@FileName varchar(max) = '',
	@CurrentStatus varchar(20) = ''

AS
BEGIN
 IF EXISTS(SELECT ID FROM DocumentUpload WHERE ID = @ID)
	BEGIN
		Update dbo.DocumentUpload
		SET
			dbo.DocumentUpload.FileName = @FileName, -- varchar
			dbo.DocumentUpload.CurrentStatus = @CurrentStatus -- int
		where ID = @ID
	END  
	ELSE
	BEGIN  
	INSERT into DocumentUpload (ID,FileName,CurrentStatus) values(@ID,@FileName, @CurrentStatus)  
	END  
END
GO
USE [master]
GO
ALTER DATABASE [ImageSystem] SET  READ_WRITE 
GO
