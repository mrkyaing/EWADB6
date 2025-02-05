USE [CloudHRMS]
CREATE TABLE [dbo].[Position](
	[Id] [char](36) NOT NULL PRIMARY KEY,
	[Code] [nvarchar](max) NOT NULL UNIQUE,
	[Description] [nvarchar](max) NOT NULL,
	[Level] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[Ip] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL
);
GO

