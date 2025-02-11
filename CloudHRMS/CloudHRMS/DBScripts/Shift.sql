USE [CloudHRMS]
GO

/****** Object:  Table [dbo].[Shift]    Script Date: 11-Feb-25 8:27:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Shift](
	[Id] [char](36) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[InTime] [time](7) NOT NULL,
	[OutTime] [time](7) NOT NULL,
	[LateAfter] [time](7) NOT NULL,
	[EarlyOutBefore] [time](7) NOT NULL,
	[AttendancePolicyId] [char](36) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedAt] [datetime] NULL,
	[Ip] [nvarchar](255) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Shift] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Shift]  WITH CHECK ADD  CONSTRAINT [FK_Shift_Shift] FOREIGN KEY([Id])
REFERENCES [dbo].[Shift] ([Id])
GO

ALTER TABLE [dbo].[Shift] CHECK CONSTRAINT [FK_Shift_Shift]
GO

