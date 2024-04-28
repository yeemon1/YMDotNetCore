USE [master]
GO
/****** Object:  Database [YMDotNetCore]    Script Date: 4/26/2024 2:22:33 PM ******/
CREATE DATABASE [YMDotNetCore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'YMDotNetCore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\YMDotNetCore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'YMDotNetCore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\YMDotNetCore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [YMDotNetCore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [YMDotNetCore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [YMDotNetCore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [YMDotNetCore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [YMDotNetCore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [YMDotNetCore] SET ARITHABORT OFF 
GO
ALTER DATABASE [YMDotNetCore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [YMDotNetCore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [YMDotNetCore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [YMDotNetCore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [YMDotNetCore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [YMDotNetCore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [YMDotNetCore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [YMDotNetCore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [YMDotNetCore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [YMDotNetCore] SET  DISABLE_BROKER 
GO
ALTER DATABASE [YMDotNetCore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [YMDotNetCore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [YMDotNetCore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [YMDotNetCore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [YMDotNetCore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [YMDotNetCore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [YMDotNetCore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [YMDotNetCore] SET RECOVERY FULL 
GO
ALTER DATABASE [YMDotNetCore] SET  MULTI_USER 
GO
ALTER DATABASE [YMDotNetCore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [YMDotNetCore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [YMDotNetCore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [YMDotNetCore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [YMDotNetCore] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'YMDotNetCore', N'ON'
GO
USE [YMDotNetCore]
GO
/****** Object:  Table [dbo].[Tbl_Blog]    Script Date: 4/26/2024 2:22:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Blog](
	[BlogId] [int] IDENTITY(1,1) NOT NULL,
	[BlogTitle] [nchar](10) NULL,
	[BlogAuthor] [nchar](30) NULL,
	[BlogContent] [nchar](30) NULL,
 CONSTRAINT [PK_Tbl_Blog] PRIMARY KEY CLUSTERED 
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tbl_Blog] ON 

INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (1, N'YeeMon    ', N'Test                          ', N'test                          ')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (2, N'Thant     ', N'Testing                       ', N'Testing                       ')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (3, N'title     ', N'author                        ', N'content                       ')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (4, N'title     ', N'author                        ', N'content                       ')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (6, N'title     ', N'author                        ', N'content                       ')
SET IDENTITY_INSERT [dbo].[Tbl_Blog] OFF
GO
USE [master]
GO
ALTER DATABASE [YMDotNetCore] SET  READ_WRITE 
GO
