USE [AssignmentPrj]
GO
/****** Object:  Table [dbo].[admin]    Script Date: 8/3/2023 2:06:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin](
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[Name] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[booking]    Script Date: 8/3/2023 2:06:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[booking](
	[booking_id] [int] IDENTITY(1,1) NOT NULL,
	[room_id] [int] NOT NULL,
	[account] [varchar](50) NOT NULL,
	[in_date] [date] NOT NULL,
	[confirmroom] [bit] NOT NULL,
 CONSTRAINT [PK__booking__5DE3A5B1BC2D30A7] PRIMARY KEY CLUSTERED 
(
	[booking_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [unique_in_date] UNIQUE NONCLUSTERED 
(
	[in_date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[buildings]    Script Date: 8/3/2023 2:06:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[buildings](
	[building_id] [int] NOT NULL,
	[buildingname] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[building_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[feedback]    Script Date: 8/3/2023 2:06:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[feedback](
	[name] [varchar](50) NOT NULL,
	[email] [varchar](100) NULL,
	[feedback] [varchar](500) NULL,
	[felling] [nchar](10) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[account] [varchar](50) NULL,
 CONSTRAINT [PK_feedback] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rooms]    Script Date: 8/3/2023 2:06:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rooms](
	[roomid] [int] IDENTITY(1,1) NOT NULL,
	[roomname] [varchar](10) NOT NULL,
	[building_id] [int] NOT NULL,
	[rtype_id] [int] NOT NULL,
	[room_img] [varchar](50) NULL,
	[member] [int] NULL,
 CONSTRAINT [PK__rooms__6CC40996FF668AC6] PRIMARY KEY CLUSTERED 
(
	[roomid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[roomtypes]    Script Date: 8/3/2023 2:06:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roomtypes](
	[rtype_id] [int] IDENTITY(1,1) NOT NULL,
	[number] [int] NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK__roomtype__7E61DA819643C428] PRIMARY KEY CLUSTERED 
(
	[rtype_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/3/2023 2:06:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[account] [varchar](50) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[password] [varchar](50) NULL,
	[name] [varchar](50) NULL,
	[gender] [bit] NULL,
	[dateofbirth] [date] NULL,
	[address] [varchar](200) NULL,
	[money] [decimal](10, 2) NULL,
	[inroom] [bit] NULL,
	[roles] [varchar](2) NULL,
 CONSTRAINT [PK__Users__EA162E10A640852E] PRIMARY KEY CLUSTERED 
(
	[account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[admin] ADD  DEFAULT ('Unknown') FOR [Name]
GO
ALTER TABLE [dbo].[rooms] ADD  CONSTRAINT [DF__rooms__member__3F466844]  DEFAULT ((0)) FOR [member]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__money__403A8C7D]  DEFAULT ((0)) FOR [money]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__inroom__59063A47]  DEFAULT ((0)) FOR [inroom]
GO
ALTER TABLE [dbo].[feedback]  WITH CHECK ADD  CONSTRAINT [fk_feedback_users] FOREIGN KEY([account])
REFERENCES [dbo].[Users] ([account])
GO
ALTER TABLE [dbo].[feedback] CHECK CONSTRAINT [fk_feedback_users]
GO
ALTER TABLE [dbo].[rooms]  WITH CHECK ADD  CONSTRAINT [FK__rooms__building___2B3F6F97] FOREIGN KEY([building_id])
REFERENCES [dbo].[buildings] ([building_id])
GO
ALTER TABLE [dbo].[rooms] CHECK CONSTRAINT [FK__rooms__building___2B3F6F97]
GO
ALTER TABLE [dbo].[rooms]  WITH CHECK ADD  CONSTRAINT [FK__rooms__rtype_id__2C3393D0] FOREIGN KEY([rtype_id])
REFERENCES [dbo].[roomtypes] ([rtype_id])
GO
ALTER TABLE [dbo].[rooms] CHECK CONSTRAINT [FK__rooms__rtype_id__2C3393D0]
GO
ALTER TABLE [dbo].[feedback]  WITH CHECK ADD  CONSTRAINT [CK__feedback__email__2E1BDC42] CHECK  (([email] like '%@gmail.com'))
GO
ALTER TABLE [dbo].[feedback] CHECK CONSTRAINT [CK__feedback__email__2E1BDC42]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [chk_gender] CHECK  (([gender]=(1) OR [gender]=(0)))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [chk_gender]
GO
