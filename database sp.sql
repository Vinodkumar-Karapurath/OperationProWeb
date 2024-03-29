USE [master]
GO
/****** Object:  Database [OperationPro]    Script Date: 11/5/2023 11:14:49 PM ******/
CREATE DATABASE [OperationPro]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OperationPro', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQL2\MSSQL\DATA\OperationPro.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OperationPro_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQL2\MSSQL\DATA\OperationPro_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [OperationPro] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OperationPro].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OperationPro] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OperationPro] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OperationPro] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OperationPro] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OperationPro] SET ARITHABORT OFF 
GO
ALTER DATABASE [OperationPro] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OperationPro] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OperationPro] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OperationPro] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OperationPro] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OperationPro] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OperationPro] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OperationPro] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OperationPro] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OperationPro] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OperationPro] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OperationPro] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OperationPro] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OperationPro] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OperationPro] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OperationPro] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OperationPro] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OperationPro] SET RECOVERY FULL 
GO
ALTER DATABASE [OperationPro] SET  MULTI_USER 
GO
ALTER DATABASE [OperationPro] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OperationPro] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OperationPro] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OperationPro] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OperationPro] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OperationPro] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'OperationPro', N'ON'
GO
ALTER DATABASE [OperationPro] SET QUERY_STORE = OFF
GO
USE [OperationPro]
GO
/****** Object:  UserDefinedFunction [dbo].[SortString]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[SortString]
(
  @s nvarchar(64)
)
RETURNS TABLE WITH SCHEMABINDING
AS
  RETURN 
  ( -- get a sequence number for every character in @s
    WITH n AS 
    (
      SELECT n = 1 UNION ALL SELECT n + 1 FROM n WHERE n < LEN(@s)
    ),
    s AS 
    ( -- break out each character, apply sequence number, test numeric
      SELECT n, s = SUBSTRING(@s, n, 1), isn = ISNUMERIC(SUBSTRING(@s, n, 1))
      FROM n
    ),
    s2 AS
    ( -- apply reverse match pointers, but only for strings
      SELECT n,s, 
        rn1 = CASE WHEN isn = 0 THEN ROW_NUMBER() OVER 
              (PARTITION BY isn ORDER BY n ASC) END,
        rn2 = CASE WHEN isn = 0 THEN ROW_NUMBER() OVER 
              (PARTITION BY isn ORDER BY n DESC) END
      FROM s
    )
    SELECT s2.n, New = COALESCE(s3.s, s2.s), Original = s2.s
        FROM s2 LEFT OUTER JOIN s2 AS s3
        ON s2.rn2 = s3.rn1
  );
GO
/****** Object:  Table [dbo].[TBL_Company]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Company](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Company] [nvarchar](50) NULL,
 CONSTRAINT [PK_TBL_Company] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_Department]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Department](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Department] [nvarchar](50) NULL,
 CONSTRAINT [PK_TBL_Department] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_Document]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Document](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Documents] [nvarchar](50) NULL,
 CONSTRAINT [PK_TBL_Document] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_JOB]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_JOB](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[JobTitle] [nvarchar](50) NULL,
 CONSTRAINT [PK_TBL_JOB] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_Location]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Location](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Location] [nvarchar](50) NULL,
 CONSTRAINT [PK_TBL_Location] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_MainMaster]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_MainMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmpName] [nvarchar](100) NULL,
	[EMPID] [nvarchar](20) NULL,
	[TGID] [nvarchar](20) NULL,
	[CompanyID] [int] NULL,
	[JobTitle] [int] NULL,
	[Depart] [int] NULL,
	[Location] [int] NULL,
	[Mobile] [nvarchar](20) NULL,
	[Email] [nvarchar](30) NULL,
	[IqamaID] [nvarchar](30) NULL,
	[IqamaExpDate] [datetime] NULL,
	[SARID] [nvarchar](30) NULL,
	[SARExpDate] [datetime] NULL,
	[Passport] [nvarchar](30) NULL,
	[PassportExpDate] [datetime] NULL,
	[Photo] [varbinary](max) NULL,
 CONSTRAINT [PK_TBL_MainMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_PdfFile]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_PdfFile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DOCID] [int] NULL,
	[MasterID] [int] NULL,
	[PDFFile] [varbinary](max) NULL,
	[ContentType] [varchar](200) NULL,
 CONSTRAINT [PK_TBL_PdfFile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_Type]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Type](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_TBL_Type] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_UserMaster]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_UserMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Role] [int] NULL,
 CONSTRAINT [PK_TBL_UserMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_Vehicle]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Vehicle](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Vehicle] [nvarchar](30) NULL,
	[VehicleExpDate] [datetime] NULL,
	[InsuranceExpDate] [datetime] NULL,
	[AuthorizationExpDate] [datetime] NULL,
	[Model] [nvarchar](30) NULL,
	[Type] [int] NULL,
	[plateNo] [nvarchar](30) NULL,
	[AuthorizationID] [int] NULL,
	[PDFFile] [varbinary](max) NULL,
	[ContentType] [varchar](200) NULL,
 CONSTRAINT [PK_TBL_Vehicle] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_WarningDays]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_WarningDays](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IqamaExpWarnDay] [int] NULL,
	[SarExpWarnDay] [int] NULL,
	[PassportExpWarnDay] [int] NULL,
	[VehicleWarnDay] [int] NULL,
	[InsuranceWarnDay] [int] NULL,
	[AuthorizationWarnDay] [int] NULL,
 CONSTRAINT [PK_TBL_WarningDays] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TBL_MainMaster]  WITH CHECK ADD  CONSTRAINT [FK_TBL_MainMaster_TBL_Company] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[TBL_Company] ([ID])
GO
ALTER TABLE [dbo].[TBL_MainMaster] CHECK CONSTRAINT [FK_TBL_MainMaster_TBL_Company]
GO
ALTER TABLE [dbo].[TBL_MainMaster]  WITH CHECK ADD  CONSTRAINT [FK_TBL_MainMaster_TBL_Department] FOREIGN KEY([Depart])
REFERENCES [dbo].[TBL_Department] ([ID])
GO
ALTER TABLE [dbo].[TBL_MainMaster] CHECK CONSTRAINT [FK_TBL_MainMaster_TBL_Department]
GO
ALTER TABLE [dbo].[TBL_MainMaster]  WITH CHECK ADD  CONSTRAINT [FK_TBL_MainMaster_TBL_JOB] FOREIGN KEY([JobTitle])
REFERENCES [dbo].[TBL_JOB] ([ID])
GO
ALTER TABLE [dbo].[TBL_MainMaster] CHECK CONSTRAINT [FK_TBL_MainMaster_TBL_JOB]
GO
ALTER TABLE [dbo].[TBL_MainMaster]  WITH CHECK ADD  CONSTRAINT [FK_TBL_MainMaster_TBL_Location] FOREIGN KEY([Location])
REFERENCES [dbo].[TBL_Location] ([ID])
GO
ALTER TABLE [dbo].[TBL_MainMaster] CHECK CONSTRAINT [FK_TBL_MainMaster_TBL_Location]
GO
ALTER TABLE [dbo].[TBL_MainMaster]  WITH CHECK ADD  CONSTRAINT [FK_TBL_MainMaster_TBL_MainMaster] FOREIGN KEY([ID])
REFERENCES [dbo].[TBL_MainMaster] ([ID])
GO
ALTER TABLE [dbo].[TBL_MainMaster] CHECK CONSTRAINT [FK_TBL_MainMaster_TBL_MainMaster]
GO
ALTER TABLE [dbo].[TBL_PdfFile]  WITH CHECK ADD  CONSTRAINT [FK_TBL_PdfFile_TBL_Document] FOREIGN KEY([DOCID])
REFERENCES [dbo].[TBL_Document] ([ID])
GO
ALTER TABLE [dbo].[TBL_PdfFile] CHECK CONSTRAINT [FK_TBL_PdfFile_TBL_Document]
GO
ALTER TABLE [dbo].[TBL_PdfFile]  WITH CHECK ADD  CONSTRAINT [FK_TBL_PdfFile_TBL_MainMaster] FOREIGN KEY([MasterID])
REFERENCES [dbo].[TBL_MainMaster] ([ID])
GO
ALTER TABLE [dbo].[TBL_PdfFile] CHECK CONSTRAINT [FK_TBL_PdfFile_TBL_MainMaster]
GO
ALTER TABLE [dbo].[TBL_PdfFile]  WITH CHECK ADD  CONSTRAINT [FK_TBL_PdfFile_TBL_PdfFile] FOREIGN KEY([ID])
REFERENCES [dbo].[TBL_PdfFile] ([ID])
GO
ALTER TABLE [dbo].[TBL_PdfFile] CHECK CONSTRAINT [FK_TBL_PdfFile_TBL_PdfFile]
GO
ALTER TABLE [dbo].[TBL_Vehicle]  WITH NOCHECK ADD  CONSTRAINT [FK_TBL_Vehicle_TBL_MainMaster] FOREIGN KEY([AuthorizationID])
REFERENCES [dbo].[TBL_MainMaster] ([ID])
GO
ALTER TABLE [dbo].[TBL_Vehicle] NOCHECK CONSTRAINT [FK_TBL_Vehicle_TBL_MainMaster]
GO
ALTER TABLE [dbo].[TBL_Vehicle]  WITH CHECK ADD  CONSTRAINT [FK_TBL_Vehicle_TBL_Type] FOREIGN KEY([Type])
REFERENCES [dbo].[TBL_Type] ([ID])
GO
ALTER TABLE [dbo].[TBL_Vehicle] CHECK CONSTRAINT [FK_TBL_Vehicle_TBL_Type]
GO
/****** Object:  StoredProcedure [dbo].[SP_GETALLStaff]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE    PROCEDURE [dbo].[SP_GETALLStaff]
	@type int = 0,
	@condtion varchar(30) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if @type = 0 -- all
    -- Insert statements for procedure here
		SELECT st.ID, st.EmpName,
		st.EMPID, st.TGID,cm.Company, jb.JobTitle,dp.Department, lc.[Location],
		st.Mobile,st.Email,st.IqamaID,st.IqamaExpDate, st.SARID, st.SARExpDate,
		st.Passport, st.PassportExpDate
		from TBL_MainMaster st inner join TBL_Company CM  on st.CompanyID = cm.ID
		inner join TBL_Department DP  on st.Depart = DP.ID
		inner join TBL_JOB JB  on st.JobTitle = JB.ID
		inner join TBL_Location LC  on st.Location = LC.ID
else  if @type = 1 -- EmpName
	SELECT st.ID, st.EmpName,
		st.EMPID, st.TGID,cm.Company, jb.JobTitle,dp.Department, lc.[Location],
		st.Mobile,st.Email,st.IqamaID,st.IqamaExpDate, st.SARID, st.SARExpDate,
		st.Passport, st.PassportExpDate
		from TBL_MainMaster st inner join TBL_Company CM  on st.CompanyID = cm.ID
		inner join TBL_Department DP  on st.Depart = DP.ID
		inner join TBL_JOB JB  on st.JobTitle = JB.ID
		inner join TBL_Location LC  on st.Location = LC.ID where EmpName like '%' + @condtion + '%'
else  if @type = 2 -- EMPID
	SELECT st.ID, st.EmpName,
		st.EMPID, st.TGID,cm.Company, jb.JobTitle,dp.Department, lc.[Location],
		st.Mobile,st.Email,st.IqamaID,st.IqamaExpDate, st.SARID, st.SARExpDate,
		st.Passport, st.PassportExpDate
		from TBL_MainMaster st inner join TBL_Company CM  on st.CompanyID = cm.ID
		inner join TBL_Department DP  on st.Depart = DP.ID
		inner join TBL_JOB JB  on st.JobTitle = JB.ID
		inner join TBL_Location LC  on st.Location = LC.ID where EMPID like '%' + @condtion + '%'

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GETALLVehicle]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE    PROCEDURE [dbo].[SP_GETALLVehicle]
	@type int = 0,
	@condtion varchar(30) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if @type = 0 -- all
    -- Insert statements for procedure here
		SELECT st.ID, st.Vehicle,
		st.VehicleExpDate, st.InsuranceExpDate,st.AuthorizationExpDate, st.Model,
		st.plateNo, st.PDFFile, st.ContentType, ty.[Type], MM.EmpName,MM.EMPID
		from TBL_Vehicle st 
		left join [TBL_Type] ty 		
		on st.Type = ty.ID
		left join [TBL_MainMaster] MM
		on st.AuthorizationID = MM.ID
else  if @type = 1 -- plateNo
	SELECT st.ID, st.Vehicle,
		st.VehicleExpDate, st.InsuranceExpDate,st.AuthorizationExpDate, st.Model,
		st.plateNo, st.PDFFile, st.ContentType, ty.[Type], MM.EmpName,MM.EMPID
		from TBL_Vehicle st 
		left join [TBL_Type] ty 		
		on st.Type = ty.ID
		left join [TBL_MainMaster] MM
		on st.AuthorizationID = MM.ID where plateNo like '%' + @condtion + '%'
else  if @type = 2 -- EmpName
	SELECT st.ID, st.Vehicle,
		st.VehicleExpDate, st.InsuranceExpDate,st.AuthorizationExpDate, st.Model,
		st.plateNo, st.PDFFile, st.ContentType, ty.[Type], MM.EmpName,MM.EMPID
		from TBL_Vehicle st 
		left join [TBL_Type] ty 		
		on st.Type = ty.ID
		left join [TBL_MainMaster] MM
		on st.AuthorizationID = MM.ID where MM.EmpName like '%' + @condtion + '%'

END
GO
/****** Object:  StoredProcedure [dbo].[SP_Masters]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Masters] 
	-- Add the parameters for the stored procedure here
	@Type nvarchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if(@Type = 'company') -- company
		begin
		select id, Company as MasterName from TBL_Company
		end
	else if(@Type = 'department') -- department
		begin
		select id, Department as MasterName from TBL_Department
		end
	else if(@Type = 'job') -- job
		begin
		select id, JobTitle as MasterName from TBL_JOB
		end
	else if(@Type = 'location') -- location
		begin
		select id, [Location] as MasterName from TBL_Location
		end
	else if(@Type = 'vehicle') -- type
		begin
		select id, [Type] as MasterName from TBL_Type
		end
	else if(@Type = 'staff') -- Staff
		begin
		select ID as id,  empName as MasterName from TBL_MainMaster
		end
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveMaster]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_SaveMaster]
	@MasterName nvarchar(50),
	@Type nvarchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   if(@Type = 'company') -- company
		begin		
		insert into TBL_Company (Company) values(@MasterName)
		select id, Company as MasterName from TBL_Company where id= SCOPE_IDENTITY()
		end
	else if(@Type = 'department') -- department
		begin
		insert into TBL_Department (Department) values(@MasterName)
		select id, Department as MasterName from TBL_Department where id= SCOPE_IDENTITY()
		end
	else if(@Type = 'job') -- job
		begin
		insert into TBL_JOB (JobTitle) values(@MasterName)
		select id, JobTitle as MasterName from TBL_JOB where id= SCOPE_IDENTITY()
		end
	else if(@Type = 'location') -- location
		begin
		insert into TBL_Location ([Location]) values(@MasterName)
		select id, [Location] as MasterName from TBL_Location where id= SCOPE_IDENTITY()
		end
	else if(@Type = 'vehicle') -- type
		begin
		insert into TBL_Type ([Type]) values(@MasterName)
		select id, [Type] as MasterName from TBL_Type  where id= SCOPE_IDENTITY()
		end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SaveStaff]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[SP_SaveStaff] 
	-- Add the parameters for the stored procedure here
	@EmpName varchar(100) = null,
	@EMPID  varchar(20) = null,
	@TGID varchar(20) = null,
	@Email nvarchar(30) = null,
	@Mobile nvarchar(20) = null,
	@IqamaID nvarchar(20) = null,
	@IqamaExpDate nvarchar(20) = null,
	@SARID nvarchar(30) = null,
	@SARExpDate nvarchar(20) = null,
	@Passport nvarchar(30) = null,
	@PassportExpDate nvarchar(20) = null,
	@Company nvarchar(10) = null,
	@Department nvarchar(10) = null,
	@JobTitle nvarchar(10) = null,
	@Location nvarchar(10) = null,
	@IqamacontentType varchar(200) = null,
	@iqamaData varbinary(max) = null,
	@SARcontentType varchar(200) = null,
	@SARData varbinary(max) = null,
	@PassportcontentType varchar(200) = null,
	@PassportData varbinary(max) = null,
	@PhotoData varbinary(max) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	INSERT INTO [dbo].[TBL_MainMaster]
           ([EmpName]
           ,[EMPID]
           ,[TGID]
           ,[CompanyID]
           ,[JobTitle]
           ,[Depart]
           ,[Location]
           ,[Mobile]
           ,[Email]
           ,[IqamaID]
           ,[IqamaExpDate]
           ,[SARID]
           ,[SARExpDate]
           ,[Passport]
           ,[PassportExpDate]
           ,[Photo])
     VALUES
           (@EmpName
           ,@EMPID
           ,@TGID
           ,@Company
           ,@JobTitle
           ,@Department
           ,@Location
           ,@Mobile
           ,@Email
           ,@IqamaID
           ,@IqamaExpDate
           ,@SARID
           ,@SARExpDate
           ,@Passport
           ,@PassportExpDate
           ,@PhotoData)

		  declare @staffID int =  SCOPE_IDENTITY()

		  --Iqama
	INSERT INTO [dbo].[TBL_PdfFile]
           ([DOCID]
           ,[MasterID]
           ,[PDFFile]
           ,[ContentType])
     VALUES
           (1
           ,@staffID
           ,@iqamaData
           ,@IqamacontentType)

		   --SAR
	INSERT INTO [dbo].[TBL_PdfFile]
           ([DOCID]
           ,[MasterID]
           ,[PDFFile]
           ,[ContentType])
     VALUES
           (2
           ,@staffID
           ,@SARData
           ,@SARcontentType)
	  --Passport
	INSERT INTO [dbo].[TBL_PdfFile]
           ([DOCID]
           ,[MasterID]
           ,[PDFFile]
           ,[ContentType])
     VALUES
           (3
           ,@staffID
           ,@PassportData
           ,@PassportcontentType)

		   select @staffID, 'Data saved'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SaveVehicle]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create   PROCEDURE [dbo].[SP_SaveVehicle] 
	-- Add the parameters for the stored procedure here
	@Vehicle nvarchar(30) = null,
	@VehicleExpDate  nvarchar(20) = null,
	@InsuranceExpDate nvarchar(20) = null,
	@AuthorizationExpDate nvarchar(20) = null,
	@Model nvarchar(20) = null,
	@Type nvarchar(10) = null,
	@plateNo nvarchar(30) = null,
	@AuthorizationID nvarchar(10) = null,	
	@ContentType varchar(200) = null,
	@PDFFile varbinary(max) = null
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
INSERT INTO [TBL_Vehicle]
           ([Vehicle]
           ,[VehicleExpDate]
           ,[InsuranceExpDate]
           ,[AuthorizationExpDate]
           ,[Model]
           ,[Type]
           ,[plateNo]
           ,[AuthorizationID]
           ,[PDFFile]
           ,[ContentType])
     VALUES
           (@Vehicle
           ,@VehicleExpDate
           ,@InsuranceExpDate
           ,@AuthorizationExpDate
           ,@Model
           ,@Type
           ,@plateNo
           ,@AuthorizationID
           ,@PDFFile
           ,@ContentType)

		  declare @VehicleID int =  SCOPE_IDENTITY()

		   select @VehicleID, 'Data saved'
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateMaster]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_UpdateMaster]
	@ID int,
	@MasterName nvarchar(50),
	@Type nvarchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   if(@Type = 'company') -- company
		begin	
		update TBL_Company set Company = @MasterName where id = @ID
		select id, Company as MasterName from TBL_Company where id= @ID
		end
	else if(@Type = 'department') -- department
		begin
		update TBL_Department set Department = @MasterName where id = @ID
		select id, Department as MasterName from TBL_Department where id= @ID
		end
	else if(@Type = 'job') -- job
		begin
		update TBL_JOB set JobTitle = @MasterName where id = @ID
		select id, JobTitle as MasterName from TBL_JOB where id= @ID
		end
	else if(@Type = 'location') -- location
		begin
		update TBL_Location set [Location] = @MasterName where id = @ID
		select id, [Location] as MasterName from TBL_Location where id= @ID
		end
	else if(@Type = 'vehicle') -- type
		begin
		update TBL_Type set [Type] = @MasterName where id = @ID
		select id, [Type] as MasterName from TBL_Type  where id= @ID
		end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Warning]    Script Date: 11/5/2023 11:14:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[SP_Warning] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	select su.* from(
select id,EMPID, EmpName,Mobile, IqamaID TypeId, IqamaExpDate ExpDate, 'Iqama' as trantypes, 0 as VehicleID from TBL_MainMaster
where IqamaExpDate < DATEADD(d,(select top 1 IqamaExpWarnDay from TBL_WarningDays), GETDATE() )
union
select id,EMPID, EmpName,Mobile, SARID TypeId, SARExpDate ExpDate , 'SAR' as trantypes, 0 as VehicleID from TBL_MainMaster
where SARExpDate < DATEADD(d,(select top 1 SarExpWarnDay from TBL_WarningDays), GETDATE() )
union
select id,EMPID, EmpName,Mobile, Passport TypeId, PassportExpDate ExpDate , 'Passport' as trantypes, 0 as VehicleID from TBL_MainMaster
where SARExpDate < DATEADD(d,(select top 1 PassportExpWarnDay from TBL_WarningDays), GETDATE() )
--vehicle
union
select AuthorizationID id,EMPID, tm.EmpName,Mobile, plateNo TypeId, tv.VehicleExpDate ExpDate , 'Vehicle Registration' as trantypes , tv.ID as VehicleID from [TBL_Vehicle] tv
  left join TBL_MainMaster tm on tv.AuthorizationID = tm.ID
where tv.VehicleExpDate < DATEADD(d,(select top 1 VehicleWarnDay from TBL_WarningDays), GETDATE() )
union
select AuthorizationID id,EMPID, tm.EmpName,Mobile, plateNo TypeId, tv.InsuranceExpDate ExpDate , 'Vehicle Insurance' as trantypes, tv.ID as VehicleID from [TBL_Vehicle] tv
  left join TBL_MainMaster tm on tv.AuthorizationID = tm.ID
where tv.InsuranceExpDate < DATEADD(d,(select top 1 InsuranceWarnDay from TBL_WarningDays), GETDATE() )
union
select AuthorizationID id,EMPID, tm.EmpName,Mobile, plateNo TypeId, tv.AuthorizationExpDate ExpDate , 'Vehicle Authorization' as trantypes, tv.ID as VehicleID from [TBL_Vehicle] tv
  left join TBL_MainMaster tm on tv.AuthorizationID = tm.ID
where tv.AuthorizationExpDate < DATEADD(d,(select top 1 AuthorizationWarnDay from TBL_WarningDays), GETDATE() )
) as su order by su.trantypes, su.ExpDate

END
GO
USE [master]
GO
ALTER DATABASE [OperationPro] SET  READ_WRITE 
GO
