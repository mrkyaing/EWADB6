USE [CloudHRMS]
GO

/****** Object:  Table [dbo].[AttendancePolicy]    Script Date: 11-Feb-25 8:24:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AttendancePolicy](
	[Id] [char](36) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[NumberOfLateTime] [int] NOT NULL,
	[NumberOfEarlyOutTime] [int] NOT NULL,
	[DeductionInAmount] [decimal](18, 2) NOT NULL,
	[DeductionInDay] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](255) NULL,
	[Ip] [nvarchar](255) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_AttendancePolicy] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

