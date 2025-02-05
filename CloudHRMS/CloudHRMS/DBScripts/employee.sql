USE [CloudHRMS]
CREATE TABLE [dbo].[Employee](
	[Id] [char](36) NOT NULL PRIMARY KEY,
	[Code] [nvarchar](max) NOT NULL UNIQUE,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](1) NOT NULL,
	[DOB] [datetime2](7) NOT NULL,
	[DOE] [datetime2](7) NOT NULL,
	[DOR] [datetime2](7) NULL,
	[Address] [nvarchar](max) NOT NULL,
	[BasicSalary] [decimal](18, 2) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[DepartmentId] [nvarchar](max) NOT NULL,
	[PositionId] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[Ip] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL
);
GO

