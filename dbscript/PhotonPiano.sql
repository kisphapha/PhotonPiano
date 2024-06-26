CREATE DATABASE [PhotonPiano]
GO

USE [PhotonPiano]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Level] [int] NOT NULL,
	[start_date] [date] NOT NULL,
	[end_date] [date] NOT NULL,
	[status] [varchar](255) NOT NULL,
	[Instructor_Id] [bigint] NOT NULL,
 CONSTRAINT [class_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[ownerId] [bigint] NOT NULL,
	[ReplyToCommentId] [bigint] NULL,
	[Upvote] [int] NOT NULL,
	[Post_id] [bigint] NOT NULL,
	[Comment_date] [datetime] NULL,
 CONSTRAINT [comment_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommentVote]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommentVote](
	[CommentId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Up_or_down] [bit] NULL,
 CONSTRAINT [PK_CommentVote] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Criteria]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Criteria](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Weight] [decimal](8, 2) NOT NULL,
	[Description] [varchar](255) NULL,
 CONSTRAINT [criteria_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EntranceTest]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntranceTest](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StudentId] [bigint] NULL,
	[LocationId] [bigint] NULL,
	[Shift] [int] NULL,
	[Date] [date] NULL,
	[BandScore] [decimal](8, 2) NULL,
	[Rank] [varchar](255) NULL,
 CONSTRAINT [PK_EntranceTest_] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EntranceTestResult]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntranceTestResult](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[entrance_test_id] [bigint] NOT NULL,
	[Score] [decimal](8, 2) NULL,
	[Criteria_id] [bigint] NOT NULL,
	[Year] [int] NULL,
 CONSTRAINT [entrancetest_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instructor](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Level] [int] NOT NULL,
	[Contribute_score] [bigint] NOT NULL,
 CONSTRAINT [PK__Instruct__3213E83F976F1BA2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lesson]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lesson](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[shift] [int] NOT NULL,
	[locationId] [bigint] NOT NULL,
	[ClassId] [bigint] NULL,
	[ExamType] [varchar](50) NULL,
	[Date] [date] NULL,
 CONSTRAINT [lesson_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Status] [varchar](255) NOT NULL,
	[Capacity] [bigint] NOT NULL,
 CONSTRAINT [location_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Picture] [varchar](max) NULL,
	[RecieverId] [bigint] NULL,
	[RefUrl] [varchar](max) NULL,
	[View_status] [bit] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[ownerId] [bigint] NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[posted_date] [datetime] NOT NULL,
	[upvote] [int] NOT NULL,
	[downvote] [int] NOT NULL,
	[class_id] [bigint] NULL,
 CONSTRAINT [post_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostVote]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostVote](
	[PostId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Up_or_down] [bit] NULL,
 CONSTRAINT [PK_PostVote] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[JoinedDate] [date] NULL,
	[CurrentClassId] [bigint] NULL,
	[Level] [int] NOT NULL,
	[Status] [varchar](255) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[RegistrationDate] [datetime] NOT NULL,
 CONSTRAINT [student_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentClass]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentClass](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[studentId] [bigint] NOT NULL,
	[classId] [bigint] NOT NULL,
	[Result] [varchar](255) NULL,
	[GPA] [decimal](8, 2) NULL,
	[Rank] [varchar](255) NULL,
	[Instructor_comment] [nvarchar](max) NULL,
 CONSTRAINT [studentclass_id_primary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentClassScore]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentClassScore](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[student_class_id] [bigint] NOT NULL,
	[score] [decimal](8, 2) NULL,
	[criteriaId] [bigint] NOT NULL,
 CONSTRAINT [studentclassscore_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentClassTuition]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentClassTuition](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[student_class_id] [bigint] NOT NULL,
	[month] [int] NOT NULL,
	[amount] [bigint] NULL,
	[status] [bit] NOT NULL,
	[create_date] [datetime] NULL,
	[due_date] [datetime] NOT NULL,
	[transaction_id] [varchar](255) NULL,
	[transaction_date] [datetime] NULL,
	[transaction_description] [varchar](255) NULL,
 CONSTRAINT [studentclasstuition_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentLesson]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentLesson](
	[studentId] [bigint] NOT NULL,
	[lessonId] [bigint] NOT NULL,
	[Attendence] [bit] NOT NULL,
 CONSTRAINT [PK__StudentL__D2997F4B71CCF9B2] PRIMARY KEY CLUSTERED 
(
	[studentId] ASC,
	[lessonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/04/2024 11:15:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Phone] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[Password] [varchar](255) NULL,
	[Role] [varchar](50) NOT NULL,
	[Picture] [nvarchar](max) NULL,
	[DoB] [date] NULL,
	[Address] [nvarchar](255) NULL,
	[Gender] [varchar](50) NULL,
	[BankAccount] [varchar](100) NULL,
 CONSTRAINT [user_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Instructor] FOREIGN KEY([Instructor_Id])
REFERENCES [dbo].[Instructor] ([id])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Instructor]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Comment] FOREIGN KEY([ReplyToCommentId])
REFERENCES [dbo].[Comment] ([id])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Comment]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Post] FOREIGN KEY([Post_id])
REFERENCES [dbo].[Post] ([id])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Post]
GO
ALTER TABLE [dbo].[Comment]  WITH NOCHECK ADD  CONSTRAINT [FK_Comment_User] FOREIGN KEY([ownerId])
REFERENCES [dbo].[User] ([id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_User]
GO
ALTER TABLE [dbo].[CommentVote]  WITH CHECK ADD  CONSTRAINT [FK_CommentVote_Comment] FOREIGN KEY([CommentId])
REFERENCES [dbo].[Comment] ([id])
GO
ALTER TABLE [dbo].[CommentVote] CHECK CONSTRAINT [FK_CommentVote_Comment]
GO
ALTER TABLE [dbo].[CommentVote]  WITH CHECK ADD  CONSTRAINT [FK_CommentVote_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[CommentVote] CHECK CONSTRAINT [FK_CommentVote_User]
GO
ALTER TABLE [dbo].[EntranceTest]  WITH CHECK ADD  CONSTRAINT [FK_EntranceTest_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([id])
GO
ALTER TABLE [dbo].[EntranceTest] CHECK CONSTRAINT [FK_EntranceTest_Location]
GO
ALTER TABLE [dbo].[EntranceTest]  WITH CHECK ADD  CONSTRAINT [FK_EntranceTest_Student1] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([id])
GO
ALTER TABLE [dbo].[EntranceTest] CHECK CONSTRAINT [FK_EntranceTest_Student1]
GO
ALTER TABLE [dbo].[EntranceTestResult]  WITH CHECK ADD  CONSTRAINT [FK_EntranceTest_Criteria] FOREIGN KEY([Criteria_id])
REFERENCES [dbo].[Criteria] ([id])
GO
ALTER TABLE [dbo].[EntranceTestResult] CHECK CONSTRAINT [FK_EntranceTest_Criteria]
GO
ALTER TABLE [dbo].[EntranceTestResult]  WITH CHECK ADD  CONSTRAINT [FK_EntranceTestResult_EntranceTest] FOREIGN KEY([entrance_test_id])
REFERENCES [dbo].[EntranceTest] ([Id])
GO
ALTER TABLE [dbo].[EntranceTestResult] CHECK CONSTRAINT [FK_EntranceTestResult_EntranceTest]
GO
ALTER TABLE [dbo].[Instructor]  WITH CHECK ADD  CONSTRAINT [FK_Instructor_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Instructor] CHECK CONSTRAINT [FK_Instructor_User]
GO
ALTER TABLE [dbo].[Lesson]  WITH CHECK ADD  CONSTRAINT [FK_Lesson_Class] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([id])
GO
ALTER TABLE [dbo].[Lesson] CHECK CONSTRAINT [FK_Lesson_Class]
GO
ALTER TABLE [dbo].[Lesson]  WITH CHECK ADD  CONSTRAINT [FK_Lesson_Location] FOREIGN KEY([locationId])
REFERENCES [dbo].[Location] ([id])
GO
ALTER TABLE [dbo].[Lesson] CHECK CONSTRAINT [FK_Lesson_Location]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_User] FOREIGN KEY([RecieverId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_User]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_User] FOREIGN KEY([ownerId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_User]
GO
ALTER TABLE [dbo].[PostVote]  WITH CHECK ADD  CONSTRAINT [FK_PostVote_Post] FOREIGN KEY([PostId])
REFERENCES [dbo].[Post] ([id])
GO
ALTER TABLE [dbo].[PostVote] CHECK CONSTRAINT [FK_PostVote_Post]
GO
ALTER TABLE [dbo].[PostVote]  WITH CHECK ADD  CONSTRAINT [FK_PostVote_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[PostVote] CHECK CONSTRAINT [FK_PostVote_User]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Class] FOREIGN KEY([CurrentClassId])
REFERENCES [dbo].[Class] ([id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Class]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_User]
GO
ALTER TABLE [dbo].[StudentClass]  WITH CHECK ADD  CONSTRAINT [FK_StudentClass_Class] FOREIGN KEY([classId])
REFERENCES [dbo].[Class] ([id])
GO
ALTER TABLE [dbo].[StudentClass] CHECK CONSTRAINT [FK_StudentClass_Class]
GO
ALTER TABLE [dbo].[StudentClass]  WITH CHECK ADD  CONSTRAINT [FK_StudentClass_Student] FOREIGN KEY([studentId])
REFERENCES [dbo].[Student] ([id])
GO
ALTER TABLE [dbo].[StudentClass] CHECK CONSTRAINT [FK_StudentClass_Student]
GO
ALTER TABLE [dbo].[StudentClassScore]  WITH CHECK ADD  CONSTRAINT [FK_StudentClassScore_Criteria] FOREIGN KEY([criteriaId])
REFERENCES [dbo].[Criteria] ([id])
GO
ALTER TABLE [dbo].[StudentClassScore] CHECK CONSTRAINT [FK_StudentClassScore_Criteria]
GO
ALTER TABLE [dbo].[StudentClassScore]  WITH CHECK ADD  CONSTRAINT [FK_StudentClassScore_StudentClass] FOREIGN KEY([student_class_id])
REFERENCES [dbo].[StudentClass] ([Id])
GO
ALTER TABLE [dbo].[StudentClassScore] CHECK CONSTRAINT [FK_StudentClassScore_StudentClass]
GO
ALTER TABLE [dbo].[StudentClassTuition]  WITH CHECK ADD  CONSTRAINT [FK_StudentClassTuition_StudentClass] FOREIGN KEY([student_class_id])
REFERENCES [dbo].[StudentClass] ([Id])
GO
ALTER TABLE [dbo].[StudentClassTuition] CHECK CONSTRAINT [FK_StudentClassTuition_StudentClass]
GO
ALTER TABLE [dbo].[StudentLesson]  WITH CHECK ADD  CONSTRAINT [FK_StudentLesson_Lesson] FOREIGN KEY([lessonId])
REFERENCES [dbo].[Lesson] ([id])
GO
ALTER TABLE [dbo].[StudentLesson] CHECK CONSTRAINT [FK_StudentLesson_Lesson]
GO
ALTER TABLE [dbo].[StudentLesson]  WITH CHECK ADD  CONSTRAINT [FK_StudentLesson_Student] FOREIGN KEY([studentId])
REFERENCES [dbo].[Student] ([id])
GO
ALTER TABLE [dbo].[StudentLesson] CHECK CONSTRAINT [FK_StudentLesson_Student]
GO
