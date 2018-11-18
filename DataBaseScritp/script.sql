USE [Demo]
GO
/****** Object:  Table [dbo].[Todo]    Script Date: 11/18/2018 3:44:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Todo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CreateDate] [datetime] NULL,
	[DeleteDate] [datetime] NULL,
	[Ordering] [int] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Todo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
