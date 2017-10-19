/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4422)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [MainDevEnv]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 10/19/2017 5:18:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyCategories]    Script Date: 10/19/2017 5:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[CategoryDescription] [nvarchar](100) NOT NULL,
	[AreaCode] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CompanyCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyProfile]    Script Date: 10/19/2017 5:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](1000) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_CompanyProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyPromotion]    Script Date: 10/19/2017 5:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyPromotion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[PromotionText] [nvarchar](1000) NOT NULL,
	[AreaCode] [int] NOT NULL,
	[IsAreaCodeWildCard] [bit] NULL,
	[PromotionExpiryDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_CompanyPromotion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanySubscribedCategories]    Script Date: 10/19/2017 5:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanySubscribedCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CompanySubscribedCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyUserType]    Script Date: 10/19/2017 5:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyUserType](
	[Id] [int] NOT NULL,
	[CompanyUserType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CompanyUserType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/19/2017 5:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[EmailAddress] [nvarchar](100) NULL,
	[Phone] [nvarchar](50) NULL,
	[UserTypeId] [int] NOT NULL,
	[CompanyUserTypeId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAreaCode]    Script Date: 10/19/2017 5:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAreaCode](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[AreaCode] [int] NULL,
	[AreaAdress] [nvarchar](1000) NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_UserAreaCode] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCategories]    Script Date: 10/19/2017 5:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [datetime] NOT NULL,
 CONSTRAINT [PK_UserCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 10/19/2017 5:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[Id] [int] NOT NULL,
	[UserType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompanyPromotion]  WITH CHECK ADD  CONSTRAINT [FK_CompanyPromotion_CompanyProfile] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyProfile] ([Id])
GO
ALTER TABLE [dbo].[CompanyPromotion] CHECK CONSTRAINT [FK_CompanyPromotion_CompanyProfile]
GO
ALTER TABLE [dbo].[CompanySubscribedCategories]  WITH CHECK ADD  CONSTRAINT [FK_CompanySubscribedCategories_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[CompanySubscribedCategories] CHECK CONSTRAINT [FK_CompanySubscribedCategories_Categories]
GO
ALTER TABLE [dbo].[CompanySubscribedCategories]  WITH CHECK ADD  CONSTRAINT [FK_CompanySubscribedCategories_CompanyProfile] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyProfile] ([Id])
GO
ALTER TABLE [dbo].[CompanySubscribedCategories] CHECK CONSTRAINT [FK_CompanySubscribedCategories_CompanyProfile]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_CompanyUserType] FOREIGN KEY([CompanyUserTypeId])
REFERENCES [dbo].[CompanyUserType] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_CompanyUserType]
GO
ALTER TABLE [dbo].[UserCategories]  WITH CHECK ADD  CONSTRAINT [FK_UserCategories_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[UserCategories] CHECK CONSTRAINT [FK_UserCategories_Categories]
GO
ALTER TABLE [dbo].[UserCategories]  WITH CHECK ADD  CONSTRAINT [FK_UserCategories_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserCategories] CHECK CONSTRAINT [FK_UserCategories_User]
GO
