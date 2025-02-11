USE [CloudHRMS]
GO

/****** Object:  Table [dbo].[DailyAttendance]    Script Date: 11-Feb-25 8:26:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DailyAttendance](
	[Id] [char](36) NOT NULL,
	[AttendanceDate] [datetime] NOT NULL,
	[InTime] [time](7) NOT NULL,
	[OutTime] [time](7) NOT NULL,
	[EmployeeId] [char](36) NOT NULL,
	[DepartmentId] [char](36) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedAt] [datetime] NULL,
	[Ip] [nvarchar](255) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_DailyAttendance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[DailyAttendance]  WITH CHECK ADD  CONSTRAINT [FK_DailyAttendance_DailyAttendance] FOREIGN KEY([Id])
REFERENCES [dbo].[DailyAttendance] ([Id])
GO

ALTER TABLE [dbo].[DailyAttendance] CHECK CONSTRAINT [FK_DailyAttendance_DailyAttendance]
GO

