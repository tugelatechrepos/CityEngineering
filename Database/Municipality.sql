/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4422)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [MunicipalityDevEnv]
GO
/****** Object:  Table [dbo].[Amenity]    Script Date: 10/19/2017 5:20:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Amenity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AmenityType] [varchar](50) NULL,
	[Description] [varchar](100) NULL,
 CONSTRAINT [PK_Amenity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AmenityArea]    Script Date: 10/19/2017 5:20:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AmenityArea](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AmenityId] [int] NOT NULL,
	[AreaCode] [int] NOT NULL,
	[AreaAdress] [varchar](100) NULL,
	[Description] [varchar](100) NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_AmenityArea] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Municipality]    Script Date: 10/19/2017 5:20:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Municipality](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Province] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[MayorName] [nvarchar](50) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Municipality] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MunicipalityAmenities]    Script Date: 10/19/2017 5:20:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MunicipalityAmenities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MunicipalityId] [int] NOT NULL,
	[AmenityAreaId] [int] NOT NULL,
 CONSTRAINT [PK_MunicipalityAmenities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MunicipalityAnnouncements]    Script Date: 10/19/2017 5:20:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MunicipalityAnnouncements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MunicipalityId] [int] NOT NULL,
	[AnnouncementText] [varchar](100) NULL,
	[IsAreaWildCard] [bit] NOT NULL,
	[AreaCode] [int] NOT NULL,
	[AnnouncementExpiryDate] [datetime] NULL,
	[IsActiveAnnouncement] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_MunicipalityAnnouncements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MunicipalityAreaCodes]    Script Date: 10/19/2017 5:20:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MunicipalityAreaCodes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MunicipalityId] [int] NOT NULL,
	[AreaCode] [int] NOT NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_MunicipalityAreaCodes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MunicipalityUser]    Script Date: 10/19/2017 5:20:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MunicipalityUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NULL,
	[MunicipalityId] [int] NOT NULL,
	[MunicipalityUserTypeId] [int] NOT NULL,
	[IsVerified] [bit] NULL,
	[IsActive] [bit] NULL,
	[DateCreated] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[DateModified] [datetime] NULL,
	[ModifiedBy] [datetime] NULL,
 CONSTRAINT [PK_MunicipalityUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MunicipalityUserType]    Script Date: 10/19/2017 5:20:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MunicipalityUserType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MunicipalityUserType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MunicipalityUserVerfication]    Script Date: 10/19/2017 5:20:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MunicipalityUserVerfication](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[VerificationText] [int] NOT NULL,
	[VerificationTextDateTime] [datetime] NOT NULL,
	[VerificationTextExpiryInSeconds] [datetime] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_MunicipalityUserVerfication] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AmenityArea]  WITH CHECK ADD  CONSTRAINT [FK_AmenityArea_Amenity] FOREIGN KEY([AmenityId])
REFERENCES [dbo].[Amenity] ([Id])
GO
ALTER TABLE [dbo].[AmenityArea] CHECK CONSTRAINT [FK_AmenityArea_Amenity]
GO
ALTER TABLE [dbo].[MunicipalityAmenities]  WITH CHECK ADD  CONSTRAINT [FK_MunicipalityAmenities_AmenityArea] FOREIGN KEY([AmenityAreaId])
REFERENCES [dbo].[AmenityArea] ([Id])
GO
ALTER TABLE [dbo].[MunicipalityAmenities] CHECK CONSTRAINT [FK_MunicipalityAmenities_AmenityArea]
GO
ALTER TABLE [dbo].[MunicipalityAmenities]  WITH CHECK ADD  CONSTRAINT [FK_MunicipalityAmenities_Municipality] FOREIGN KEY([MunicipalityId])
REFERENCES [dbo].[Municipality] ([Id])
GO
ALTER TABLE [dbo].[MunicipalityAmenities] CHECK CONSTRAINT [FK_MunicipalityAmenities_Municipality]
GO
ALTER TABLE [dbo].[MunicipalityAnnouncements]  WITH CHECK ADD  CONSTRAINT [FK_MunicipalityAnnouncements_Municipality] FOREIGN KEY([MunicipalityId])
REFERENCES [dbo].[Municipality] ([Id])
GO
ALTER TABLE [dbo].[MunicipalityAnnouncements] CHECK CONSTRAINT [FK_MunicipalityAnnouncements_Municipality]
GO
ALTER TABLE [dbo].[MunicipalityAreaCodes]  WITH CHECK ADD  CONSTRAINT [FK_MunicipalityAreaCodes_Municipality] FOREIGN KEY([MunicipalityId])
REFERENCES [dbo].[Municipality] ([Id])
GO
ALTER TABLE [dbo].[MunicipalityAreaCodes] CHECK CONSTRAINT [FK_MunicipalityAreaCodes_Municipality]
GO
ALTER TABLE [dbo].[MunicipalityUser]  WITH CHECK ADD  CONSTRAINT [FK_MunicipalityUser_Municipality] FOREIGN KEY([MunicipalityId])
REFERENCES [dbo].[Municipality] ([Id])
GO
ALTER TABLE [dbo].[MunicipalityUser] CHECK CONSTRAINT [FK_MunicipalityUser_Municipality]
GO
ALTER TABLE [dbo].[MunicipalityUser]  WITH CHECK ADD  CONSTRAINT [FK_MunicipalityUser_MunicipalityUserType] FOREIGN KEY([MunicipalityUserTypeId])
REFERENCES [dbo].[MunicipalityUserType] ([Id])
GO
ALTER TABLE [dbo].[MunicipalityUser] CHECK CONSTRAINT [FK_MunicipalityUser_MunicipalityUserType]
GO
ALTER TABLE [dbo].[MunicipalityUserVerfication]  WITH CHECK ADD  CONSTRAINT [FK_MunicipalityUserVerfication_MunicipalityUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[MunicipalityUser] ([Id])
GO
ALTER TABLE [dbo].[MunicipalityUserVerfication] CHECK CONSTRAINT [FK_MunicipalityUserVerfication_MunicipalityUser]
GO
