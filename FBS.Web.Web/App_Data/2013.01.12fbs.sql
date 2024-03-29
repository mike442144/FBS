SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_Account]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_Account](
	[AccountID] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](255) NOT NULL CONSTRAINT [DF_fbs_Account_Name]  DEFAULT (N'飞行者'),
	[Role] [nvarchar](255) NOT NULL,
	[Salt] [binary](32) NOT NULL,
	[HashPsd] [binary](32) NOT NULL,
	[Points] [int] NOT NULL CONSTRAINT [DF_fbs_Account_Points]  DEFAULT ((0)),
	[Head] [nvarchar](255) NOT NULL,
	[Tiny] [nvarchar](255) NOT NULL,
	[sinaBlogName] [nchar](255) NULL,
 CONSTRAINT [PK_fbs_Account] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_UserFriend]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_UserFriend](
	[UserID] [uniqueidentifier] NOT NULL,
	[FriendID] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[FriendHead] [nvarchar](255) NULL,
	[FriendName] [nvarchar](50) NULL,
 CONSTRAINT [PK_fbs_UserFriend] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[FriendID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_BlogQuestionCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_BlogQuestionCategory](
	[QuestionCategoryID] [uniqueidentifier] NOT NULL,
	[CategoryName] [nvarchar](255) NOT NULL,
	[Description] [ntext] NOT NULL,
	[IconName] [nvarchar](50) NULL,
	[OrderPriority] [smallint] NOT NULL,
 CONSTRAINT [PK_fbs_BlogQuestionCategory] PRIMARY KEY CLUSTERED 
(
	[QuestionCategoryID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_Goods]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_Goods](
	[GoodsID] [uniqueidentifier] NOT NULL,
	[GoodsName] [nvarchar](4000) NULL,
	[GoodsNowPrice] [float] NULL,
	[GoodsPicURL] [nvarchar](250) NULL,
	[GoodsOldPrice] [float] NULL,
	[GoodsBuyCount] [int] NULL,
	[GoodsBeginTime] [datetime] NULL,
	[GoodsEndTime] [datetime] NULL,
	[GoodsDetailsContent] [ntext] NULL,
	[GoodsIsOn] [bit] NULL,
 CONSTRAINT [PK_fbs_Goods] PRIMARY KEY CLUSTERED 
(
	[GoodsID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_Feed]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_Feed](
	[FeedID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[UserHead] [nvarchar](255) NOT NULL,
	[Subject] [nvarchar](1000) NOT NULL,
	[Content] [ntext] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[FeedType] [nvarchar](60) NOT NULL,
	[ReplayCount] [int] NULL,
 CONSTRAINT [PK_fbs_Feed] PRIMARY KEY CLUSTERED 
(
	[FeedID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_UserFollow]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_UserFollow](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[UserHead] [nvarchar](255) NOT NULL,
	[FolloweeID] [uniqueidentifier] NOT NULL,
	[FolloweeName] [nvarchar](255) NOT NULL,
	[FolloweeHead] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_fbs_UserFollow] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_Roles](
	[RoleID] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[LoweredRoleName] [nvarchar](50) NULL,
 CONSTRAINT [PK_fbs_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_UserInRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_UserInRoles](
	[UserID] [uniqueidentifier] NOT NULL,
	[RoleID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_fbs_UserInRoles] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_Demand]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_Demand](
	[DemandID] [uniqueidentifier] NOT NULL,
	[CustomerName] [nvarchar](50) NULL,
	[CustomerPhoneNum] [nvarchar](15) NULL,
	[CustomerOtherConnect] [nvarchar](20) NULL,
	[DemandCity] [nvarchar](25) NULL,
	[BusinessmanName] [nvarchar](25) NULL,
	[GroupOnType] [nvarchar](50) NULL,
	[DemandContent] [ntext] NULL,
 CONSTRAINT [PK_fbs_Demand] PRIMARY KEY CLUSTERED 
(
	[DemandID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_ForumThread]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_ForumThread](
	[ThreadID] [uniqueidentifier] NOT NULL,
	[ForumID] [uniqueidentifier] NOT NULL,
	[RootMessageID] [uniqueidentifier] NOT NULL,
	[RewardPoints] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreationDate] [datetime] NULL,
	[ClickCount] [int] NOT NULL CONSTRAINT [DF_fbs_ForumThread_ClickCount]  DEFAULT ((0)),
	[MessageCount] [int] NOT NULL CONSTRAINT [DF_fbs_ForumThread_MessageCount]  DEFAULT ((0)),
	[MessageSubject] [text] NULL,
	[UserID] [uniqueidentifier] NULL,
	[UserName] [nvarchar](255) NULL,
 CONSTRAINT [PK_fbs_ForumThread] PRIMARY KEY CLUSTERED 
(
	[ThreadID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_ForbiddenAccount]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_ForbiddenAccount](
	[ForbiddenID] [uniqueidentifier] NOT NULL,
	[AccountID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[IP] [nchar](16) NOT NULL,
	[ForbiddenTime] [datetime] NOT NULL,
	[RefreshTime] [datetime] NULL,
	[State] [nchar](100) NULL,
	[ForbiddenType] [nchar](100) NULL,
 CONSTRAINT [PK_fbs_ForbiddenAccount] PRIMARY KEY CLUSTERED 
(
	[ForbiddenID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_TopForumThread]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_TopForumThread](
	[ID] [uniqueidentifier] NOT NULL,
	[TopForumThreadID] [uniqueidentifier] NOT NULL,
	[TopForumID] [uniqueidentifier] NOT NULL,
	[CreatTime] [datetime] NOT NULL,
 CONSTRAINT [PK_TopForumThread_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_BlogCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_BlogCategory](
	[BlogCategoryID] [uniqueidentifier] NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[IconName] [nvarchar](50) NULL,
	[Description] [nvarchar](4000) NOT NULL,
	[OrderPriority] [smallint] NOT NULL,
 CONSTRAINT [PK_fbs_BlogCategory] PRIMARY KEY CLUSTERED 
(
	[BlogCategoryID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductCategory](
	[CategoryID] [uniqueidentifier] NOT NULL,
	[CategoryName] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](4000) NOT NULL,
	[Picture] [nvarchar](500) NULL,
	[OrderPriority] [int] NOT NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Products](
	[ProductID] [uniqueidentifier] NOT NULL,
	[ProductName] [nvarchar](500) NULL,
	[SupplierID] [uniqueidentifier] NULL,
	[CategoryID] [uniqueidentifier] NULL,
	[Price] [float] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderDetails](
	[OrderID] [uniqueidentifier] NOT NULL,
	[ProductID] [uniqueidentifier] NOT NULL,
	[Price] [float] NOT NULL,
	[Discount] [float] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_About]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_About](
	[AboutID] [uniqueidentifier] NOT NULL,
	[Title] [nchar](255) NOT NULL,
	[Body] [ntext] NOT NULL,
 CONSTRAINT [PK_fbs_About] PRIMARY KEY CLUSTERED 
(
	[AboutID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_Link]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_Link](
	[LinkID] [uniqueidentifier] NOT NULL,
	[LinkName] [nvarchar](255) NULL,
	[LinkHref] [nchar](255) NULL,
	[Priority] [int] NULL,
 CONSTRAINT [PK_fbs_Link] PRIMARY KEY CLUSTERED 
(
	[LinkID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_CMSCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_CMSCategory](
	[CategoryID] [uniqueidentifier] NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[ParentID] [uniqueidentifier] NULL,
	[Description] [nvarchar](4000) NOT NULL,
	[IconName] [nvarchar](50) NULL,
	[Deepth] [smallint] NOT NULL CONSTRAINT [DF_fbs_CMSCategory_Deepth]  DEFAULT ((1)),
	[Priority] [smallint] NOT NULL,
 CONSTRAINT [PK_fbs_CMSCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_Advertisement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_Advertisement](
	[AdvertisementID] [uniqueidentifier] NOT NULL,
	[AdvertisementType] [nvarchar](15) NULL CONSTRAINT [DF_fbs_Advertisement_AdvertisementType]  DEFAULT ((1)),
	[AdvertisementContentURL] [nchar](100) NULL,
	[AdvertisementPriority] [int] NULL,
	[AdvertisementBeginTime] [datetime] NULL,
	[AdvertisementEndTime] [datetime] NULL,
	[AdvertisementURL] [nvarchar](50) NULL,
 CONSTRAINT [PK_fbs_Advertisement] PRIMARY KEY CLUSTERED 
(
	[AdvertisementID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_ForumRecommend]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_ForumRecommend](
	[ID] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_Needs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_Needs](
	[NeedsID] [uniqueidentifier] NOT NULL,
	[NeedsContent] [nvarchar](2000) NOT NULL,
 CONSTRAINT [PK_fbs_Needs_1] PRIMARY KEY CLUSTERED 
(
	[NeedsID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_SiteInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_SiteInfo](
	[SiteID] [uniqueidentifier] NOT NULL,
	[SiteName] [nvarchar](255) NOT NULL,
	[SiteDescription] [nvarchar](255) NOT NULL,
	[SiteUrl] [nvarchar](255) NOT NULL,
	[CopyRight] [nvarchar](50) NOT NULL,
	[Version] [nvarchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_fbs_SiteInfo] PRIMARY KEY CLUSTERED 
(
	[SiteID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_AdvertisePage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_AdvertisePage](
	[PageID] [uniqueidentifier] NOT NULL,
	[PageURL] [nvarchar](200) NULL,
	[PageDescription] [nvarchar](2000) NULL,
 CONSTRAINT [PK_fbs_AdvertisePage] PRIMARY KEY CLUSTERED 
(
	[PageID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_FormInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_FormInfo](
	[FormID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Display] [bit] NOT NULL,
 CONSTRAINT [PK_fbs_FormInfo] PRIMARY KEY CLUSTERED 
(
	[FormID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_Forum]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_Forum](
	[ForumID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nchar](1000) NULL,
	[ModifiedDate] [datetime] NULL,
	[CreationDate] [datetime] NULL,
	[MessageCount] [int] NULL,
	[ThreadCount] [int] NULL,
	[Priority] [int] NULL,
 CONSTRAINT [PK_fbs_Forum] PRIMARY KEY CLUSTERED 
(
	[ForumID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_SitePages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_SitePages](
	[PageName] [varchar](255) NULL,
	[PageContent] [ntext] NULL,
	[PageID] [uniqueidentifier] NOT NULL,
	[PageTitle] [nvarchar](255) NULL,
	[PageDescription] [nvarchar](255) NULL,
 CONSTRAINT [PK_fbs_SitePages] PRIMARY KEY CLUSTERED 
(
	[PageID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_SiteSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_SiteSettings](
	[ThemeName] [nvarchar](255) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[xxx]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[xxx]
	(
	@p int,
	@q int
	)
AS
BEGIN

select * from fbs_Feed

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_ForumProperty]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_ForumProperty](
	[ForumID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[PropValue] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_ShareThread]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_ShareThread](
	[VideoID] [uniqueidentifier] NOT NULL,
	[PlayUrl] [nvarchar](500) NOT NULL,
	[Subject] [nvarchar](200) NOT NULL,
	[Body] [nvarchar](1000) NOT NULL,
	[MediaType] [int] NOT NULL,
	[ThumbnailUrl] [nvarchar](500) NOT NULL,
	[ShareTime] [datetime] NOT NULL,
	[Source] [nvarchar](255) NOT NULL,
	[RawUrl] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_fbs_ShareThread] PRIMARY KEY CLUSTERED 
(
	[VideoID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_ShortMessage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_ShortMessage](
	[ShortMessageID] [uniqueidentifier] NOT NULL,
	[SenderID] [uniqueidentifier] NOT NULL,
	[SenderName] [nvarchar](255) NOT NULL,
	[SenderHead] [nvarchar](255) NOT NULL,
	[SendToID] [uniqueidentifier] NOT NULL,
	[SendToName] [nvarchar](255) NOT NULL,
	[SendToHead] [nvarchar](255) NOT NULL,
	[MessageTitle] [ntext] NOT NULL,
	[MessageBody] [ntext] NOT NULL,
	[SentOn] [datetime] NOT NULL,
	[HasRead] [bit] NOT NULL CONSTRAINT [DF_fbs_ShortMessage_HasRead]  DEFAULT ((0)),
 CONSTRAINT [PK_fbs_ShortMessage] PRIMARY KEY CLUSTERED 
(
	[ShortMessageID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_Comment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_Comment](
	[CommentID] [uniqueidentifier] NOT NULL,
	[TargetID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UserTiny] [nvarchar](255) NOT NULL,
	[Body] [nvarchar](4000) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_fbs_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_UserProperty]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_UserProperty](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[BlogTheme] [nvarchar](50) NOT NULL,
	[BlogName] [nvarchar](50) NOT NULL,
	[UserType] [nvarchar](15) NOT NULL,
	[Brief] [ntext] NOT NULL,
	[FollowersCount] [int] NOT NULL CONSTRAINT [DF_fbs_UserProperty_FollowersCount]  DEFAULT ((0)),
	[FolloweesCount] [int] NOT NULL CONSTRAINT [DF_fbs_UserProperty_FolloweesCount]  DEFAULT ((0)),
	[FriendsCount] [int] NOT NULL CONSTRAINT [DF_fbs_UserProperty_FriendsCount]  DEFAULT ((0)),
	[Display] [bit] NULL,
 CONSTRAINT [PK_fbs_UserProperty] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_UserProfile]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_UserProfile](
	[UserID] [uniqueidentifier] NOT NULL,
	[Sex] [bit] NOT NULL,
	[BirthDay] [datetime] NOT NULL,
	[Province] [nvarchar](20) NOT NULL,
	[City] [nvarchar](20) NOT NULL,
	[Company] [nvarchar](255) NOT NULL,
	[Hobby] [nvarchar](4000) NOT NULL,
	[QQ] [nvarchar](15) NOT NULL,
	[MSN] [nvarchar](50) NOT NULL,
	[MobilePIN] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](500) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_Story]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_Story](
	[StoryID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Description] [ntext] NOT NULL,
	[Url] [nvarchar](1000) NULL,
	[CategoryID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[UserTiny] [nvarchar](500) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[IsPublishedToHomepage] [bit] NOT NULL,
	[ClickCount] [int] NOT NULL,
	[CommentCount] [int] NOT NULL,
	[ImgName] [nvarchar](500) NULL,
 CONSTRAINT [PK_fbs_Story] PRIMARY KEY CLUSTERED 
(
	[StoryID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_Message]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_Message](
	[MessageID] [uniqueidentifier] NOT NULL,
	[ParentMessageID] [uniqueidentifier] NULL,
	[ThreadID] [uniqueidentifier] NOT NULL,
	[ForumID] [uniqueidentifier] NOT NULL,
	[AccountID] [uniqueidentifier] NOT NULL,
	[Subject] [nvarchar](255) NULL,
	[Body] [text] NULL,
	[RewardPoints] [int] NULL,
	[CreationDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_fbs_Message] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_CMSArticle]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_CMSArticle](
	[ArticleID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[BriefTitle] [nvarchar](255) NULL,
	[Body] [ntext] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[CategoryID] [uniqueidentifier] NOT NULL,
	[CategoryName] [nvarchar](100) NOT NULL,
	[SourceUrl] [nvarchar](4000) NULL,
	[SourceSite] [nvarchar](255) NULL,
	[ClickCount] [int] NOT NULL,
	[CommentCount] [int] NOT NULL,
	[ImgName] [nvarchar](255) NULL,
 CONSTRAINT [PK_fbs_CMSArticle] PRIMARY KEY CLUSTERED 
(
	[ArticleID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_Album]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_Album](
	[AlbumID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[PhotoName] [nvarchar](50) NOT NULL,
	[Description] [ntext] NOT NULL,
	[Time] [datetime] NULL,
 CONSTRAINT [PK_fbs_Album_1] PRIMARY KEY CLUSTERED 
(
	[AlbumID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_Suggest]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_Suggest](
	[SuggestID] [uniqueidentifier] NOT NULL,
	[Body] [ntext] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Reply] [ntext] NOT NULL,
	[Type] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_fbs_Suggest_1] PRIMARY KEY CLUSTERED 
(
	[SuggestID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_BlogState]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_BlogState](
	[BlogStateID] [uniqueidentifier] NOT NULL,
	[Body] [nvarchar](255) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UserHead] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_fbs_BlogState] PRIMARY KEY CLUSTERED 
(
	[BlogStateID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_BlogAnswer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_BlogAnswer](
	[AnswerID] [uniqueidentifier] NOT NULL,
	[Body] [ntext] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UserTiny] [nvarchar](255) NULL,
	[CreationDate] [datetime] NOT NULL,
	[QuestionID] [uniqueidentifier] NOT NULL,
	[GainPoints] [int] NOT NULL CONSTRAINT [DF_fbs_BlogAnswer_GainPoints]  DEFAULT ((0)),
 CONSTRAINT [PK_fbs_BlogAnswer] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_BlogQuestion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_BlogQuestion](
	[QuestionID] [uniqueidentifier] NOT NULL,
	[Subject] [nvarchar](255) NOT NULL,
	[Body] [ntext] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UserTiny] [nvarchar](255) NOT NULL,
	[CategoryID] [uniqueidentifier] NOT NULL,
	[CategoryName] [nvarchar](50) NULL,
	[ClickCount] [int] NOT NULL CONSTRAINT [DF_fbs_BlogQuestion_ClickCount]  DEFAULT ((0)),
	[RewardPoints] [int] NOT NULL CONSTRAINT [DF_fbs_BlogQuestion_RewardPoints]  DEFAULT ((0)),
	[AnswerCount] [int] NOT NULL CONSTRAINT [DF_fbs_BlogQuestion_AnswerCount]  DEFAULT ((0)),
	[CreationDate] [datetime] NOT NULL,
	[BestAnswerID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_fbs_BlogQuestion] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_AdvertiseFilling]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_AdvertiseFilling](
	[ID] [uniqueidentifier] NOT NULL,
	[PageID] [uniqueidentifier] NULL,
	[AdvertisementID] [uniqueidentifier] NULL,
	[PositionName] [nchar](20) NULL,
 CONSTRAINT [PK_fbs_AdvertiseFilling] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_SheetQuestion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_SheetQuestion](
	[QuestionID] [uniqueidentifier] NOT NULL,
	[FormID] [uniqueidentifier] NOT NULL,
	[QuestionName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_fbs_SheetQuestion] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_CMSImage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_CMSImage](
	[ImageID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Url] [nvarchar](255) NOT NULL,
	[ArticleID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_fbs_CMSImage] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fbs_SheetAnswer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fbs_SheetAnswer](
	[AnswerID] [uniqueidentifier] NOT NULL,
	[QuestionID] [uniqueidentifier] NOT NULL,
	[AnswerName] [nvarchar](50) NOT NULL,
	[Count] [int] NOT NULL,
	[FormID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_fbs_SheetAnswer] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFriendFeedsByUserIDANDCategory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetFriendFeedsByUserIDANDCategory]
	(
	@id uniqueidentifier,
    @Category nvarchar(255),
	@startIndex int,
	@count int
)
AS
BEGIN
	SET NOCOUNT ON;
if(@Category=''NewStory'')
Begin
SELECT * FROM(
SELECT ROW_NUMBER() OVER (ORDER BY [t0].CreatedOn DESC) AS [ROW_NUMBER],* 
    FROM (SELECT fbs_Feed.*
            FROM fbs_Feed
            WHERE (fbs_Feed.UserID=@id   OR (fbs_Feed.UserID IN
         (
SELECT FriendID
FROM fbs_UserFriend AS fbs_UserFriend_1
WHERE (UserID = @id)
union 
select FolloweeID as FriendID
from fbs_UserFollow where (UserID = @id)
)
)) and fbs_Feed.FeedType=@Category )  as [t0]
) AS [t1]
WHERE [t1].[ROW_NUMBER] BETWEEN @startIndex AND @startIndex+@count
ORDER BY [t1].[ROW_NUMBER]
END
else
BEGIN
SELECT * FROM(

SELECT ROW_NUMBER() OVER (ORDER BY [t0].CreatedOn DESC) AS [ROW_NUMBER],* 
   FROM (SELECT fbs_Feed.*
            FROM fbs_Feed
            WHERE (fbs_Feed.UserID=@id   OR (fbs_Feed.UserID IN
         (
SELECT FriendID
FROM fbs_UserFriend AS fbs_UserFriend_1
WHERE (UserID = @id)
union 
select FolloweeID as FriendID
from fbs_UserFollow where (UserID = @id)
))) and fbs_Feed.FeedType<>''NewStory'' )  as [t0]
) AS [t1]
WHERE [t1].[ROW_NUMBER] BETWEEN @startIndex AND @startIndex+@count
ORDER BY [t1].[ROW_NUMBER]
END


END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFriendFeedsCountByUserIDANDCategory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetFriendFeedsCountByUserIDANDCategory] 

	(
	@id uniqueidentifier,
    @Category nvarchar(255)	
)
AS
BEGIN
	SET NOCOUNT ON;
if(@Category=''NewStory'')
Begin
SELECT count(*) FROM(

SELECT * 
    FROM (SELECT fbs_Feed.*
            FROM fbs_Feed
            WHERE (fbs_Feed.UserID=@id   OR (fbs_Feed.UserID IN
         (SELECT FriendID
         FROM fbs_UserFriend AS fbs_UserFriend_1
         WHERE (UserID = @id)
union 
select FolloweeID as FriendID
from fbs_UserFollow where (UserID = @id)
))) and fbs_Feed.FeedType=@Category )  as [t0]
) AS [t1]

END
else
BEGIN
SELECT count(*) FROM(

SELECT * 
   FROM (SELECT fbs_Feed.*
            FROM fbs_Feed
            WHERE (fbs_Feed.UserID=@id   OR (fbs_Feed.UserID IN
         (SELECT FriendID
         FROM fbs_UserFriend AS fbs_UserFriend_1
         WHERE (UserID = @id)
union 
select FolloweeID as FriendID
from fbs_UserFollow where (UserID = @id)
))) and fbs_Feed.FeedType<>''NewStory'' )  as [t0]
) AS [t1]
END


END












' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFriendAndFloweeFeedsByCategoryName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetFriendAndFloweeFeedsByCategoryName]
	(
	@id uniqueidentifier,
    @Category nvarchar(255),
	@startIndex int,
	@count int
)
AS
BEGIN
	SET NOCOUNT ON;
if(@Category=''NewStory'')
Begin
SELECT * FROM(
SELECT ROW_NUMBER() OVER (ORDER BY [t0].CreatedOn DESC) AS [ROW_NUMBER],* 
    FROM (SELECT fbs_Feed.*
            FROM fbs_Feed
            WHERE (fbs_Feed.UserID=@id   OR (fbs_Feed.UserID IN
         (
SELECT FriendID
FROM fbs_UserFriend AS fbs_UserFriend_1
WHERE (UserID = @id)
union 
select FolloweeID as FriendID
from fbs_UserFollow where (UserID = @id)
)
)) and fbs_Feed.FeedType=@Category )  as [t0]
) AS [t1]
WHERE [t1].[ROW_NUMBER] BETWEEN @startIndex AND @startIndex+@count
ORDER BY [t1].[ROW_NUMBER]
END
else
BEGIN
SELECT * FROM(

SELECT ROW_NUMBER() OVER (ORDER BY [t0].CreatedOn DESC) AS [ROW_NUMBER],* 
   FROM (SELECT fbs_Feed.*
            FROM fbs_Feed
            WHERE (fbs_Feed.UserID=@id   OR (fbs_Feed.UserID IN
         (
SELECT FriendID
FROM fbs_UserFriend AS fbs_UserFriend_1
WHERE (UserID = @id)
union 
select FolloweeID as FriendID
from fbs_UserFollow where (UserID = @id)
))) and fbs_Feed.FeedType<>''NewStory'' )  as [t0]
) AS [t1]
WHERE [t1].[ROW_NUMBER] BETWEEN @startIndex AND @startIndex+@count
ORDER BY [t1].[ROW_NUMBER]
END


END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFriendFeedsByUserID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetFriendFeedsByUserID] 

	(
	@id uniqueidentifier,
	@startIndex int,
	@count int
)
AS
BEGIN
	SET NOCOUNT ON;
	
SELECT * FROM(

SELECT ROW_NUMBER() OVER (ORDER BY [t0].CreatedOn DESC) AS [ROW_NUMBER],* 
    FROM (SELECT fbs_Feed.*
            FROM fbs_Feed
            WHERE fbs_Feed.UserID=@id OR (fbs_Feed.UserID IN
         (SELECT FriendID
         FROM fbs_UserFriend AS fbs_UserFriend_1
         WHERE (UserID = @id)))) as [t0]
) AS [t1]
WHERE [t1].[ROW_NUMBER] BETWEEN @startIndex AND @startIndex+@count
ORDER BY [t1].[ROW_NUMBER]

END





' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_Comment_fbs_Account]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_Comment]'))
ALTER TABLE [dbo].[fbs_Comment]  WITH CHECK ADD  CONSTRAINT [FK_fbs_Comment_fbs_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[fbs_Account] ([AccountID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_UserProperty_fbs_Account]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_UserProperty]'))
ALTER TABLE [dbo].[fbs_UserProperty]  WITH CHECK ADD  CONSTRAINT [FK_fbs_UserProperty_fbs_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[fbs_Account] ([AccountID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_UserProfile_fbs_Account]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_UserProfile]'))
ALTER TABLE [dbo].[fbs_UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_fbs_UserProfile_fbs_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[fbs_Account] ([AccountID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_Story_fbs_Account]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_Story]'))
ALTER TABLE [dbo].[fbs_Story]  WITH CHECK ADD  CONSTRAINT [FK_fbs_Story_fbs_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[fbs_Account] ([AccountID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_Story_fbs_BlogCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_Story]'))
ALTER TABLE [dbo].[fbs_Story]  WITH CHECK ADD  CONSTRAINT [FK_fbs_Story_fbs_BlogCategory] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[fbs_BlogCategory] ([BlogCategoryID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_Message_fbs_Forum]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_Message]'))
ALTER TABLE [dbo].[fbs_Message]  WITH CHECK ADD  CONSTRAINT [FK_fbs_Message_fbs_Forum] FOREIGN KEY([ForumID])
REFERENCES [dbo].[fbs_Forum] ([ForumID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_Message_fbs_Message]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_Message]'))
ALTER TABLE [dbo].[fbs_Message]  WITH CHECK ADD  CONSTRAINT [FK_fbs_Message_fbs_Message] FOREIGN KEY([ThreadID])
REFERENCES [dbo].[fbs_ForumThread] ([ThreadID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_Message_fbs_Message1]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_Message]'))
ALTER TABLE [dbo].[fbs_Message]  WITH CHECK ADD  CONSTRAINT [FK_fbs_Message_fbs_Message1] FOREIGN KEY([AccountID])
REFERENCES [dbo].[fbs_Account] ([AccountID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_CMSArticle_fbs_Account]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_CMSArticle]'))
ALTER TABLE [dbo].[fbs_CMSArticle]  WITH CHECK ADD  CONSTRAINT [FK_fbs_CMSArticle_fbs_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[fbs_Account] ([AccountID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_CMSArticle_fbs_CMSCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_CMSArticle]'))
ALTER TABLE [dbo].[fbs_CMSArticle]  WITH CHECK ADD  CONSTRAINT [FK_fbs_CMSArticle_fbs_CMSCategory] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[fbs_CMSCategory] ([CategoryID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_Album_fbs_Account]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_Album]'))
ALTER TABLE [dbo].[fbs_Album]  WITH CHECK ADD  CONSTRAINT [FK_fbs_Album_fbs_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[fbs_Account] ([AccountID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_Suggest_fbs_Account]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_Suggest]'))
ALTER TABLE [dbo].[fbs_Suggest]  WITH CHECK ADD  CONSTRAINT [FK_fbs_Suggest_fbs_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[fbs_Account] ([AccountID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_BlogState_fbs_Account]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_BlogState]'))
ALTER TABLE [dbo].[fbs_BlogState]  WITH CHECK ADD  CONSTRAINT [FK_fbs_BlogState_fbs_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[fbs_Account] ([AccountID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_BlogAnswer_fbs_Account]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_BlogAnswer]'))
ALTER TABLE [dbo].[fbs_BlogAnswer]  WITH CHECK ADD  CONSTRAINT [FK_fbs_BlogAnswer_fbs_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[fbs_Account] ([AccountID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_BlogAnswer_fbs_BlogQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_BlogAnswer]'))
ALTER TABLE [dbo].[fbs_BlogAnswer]  WITH CHECK ADD  CONSTRAINT [FK_fbs_BlogAnswer_fbs_BlogQuestion] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[fbs_BlogQuestion] ([QuestionID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_BlogQuestion_fbs_Account]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_BlogQuestion]'))
ALTER TABLE [dbo].[fbs_BlogQuestion]  WITH CHECK ADD  CONSTRAINT [FK_fbs_BlogQuestion_fbs_Account] FOREIGN KEY([UserID])
REFERENCES [dbo].[fbs_Account] ([AccountID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_BlogQuestion_fbs_BlogQuestionCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_BlogQuestion]'))
ALTER TABLE [dbo].[fbs_BlogQuestion]  WITH CHECK ADD  CONSTRAINT [FK_fbs_BlogQuestion_fbs_BlogQuestionCategory] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[fbs_BlogQuestionCategory] ([QuestionCategoryID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_AdvertiseFilling_fbs_Advertisement]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_AdvertiseFilling]'))
ALTER TABLE [dbo].[fbs_AdvertiseFilling]  WITH CHECK ADD  CONSTRAINT [FK_fbs_AdvertiseFilling_fbs_Advertisement] FOREIGN KEY([AdvertisementID])
REFERENCES [dbo].[fbs_Advertisement] ([AdvertisementID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_AdvertiseFilling_fbs_AdvertisePage]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_AdvertiseFilling]'))
ALTER TABLE [dbo].[fbs_AdvertiseFilling]  WITH CHECK ADD  CONSTRAINT [FK_fbs_AdvertiseFilling_fbs_AdvertisePage] FOREIGN KEY([PageID])
REFERENCES [dbo].[fbs_AdvertisePage] ([PageID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_SheetQuestion_fbs_FormInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_SheetQuestion]'))
ALTER TABLE [dbo].[fbs_SheetQuestion]  WITH CHECK ADD  CONSTRAINT [FK_fbs_SheetQuestion_fbs_FormInfo] FOREIGN KEY([FormID])
REFERENCES [dbo].[fbs_FormInfo] ([FormID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_CMSImage_fbs_CMSImage]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_CMSImage]'))
ALTER TABLE [dbo].[fbs_CMSImage]  WITH CHECK ADD  CONSTRAINT [FK_fbs_CMSImage_fbs_CMSImage] FOREIGN KEY([ArticleID])
REFERENCES [dbo].[fbs_CMSArticle] ([ArticleID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_fbs_SheetAnswer_fbs_SheetQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[fbs_SheetAnswer]'))
ALTER TABLE [dbo].[fbs_SheetAnswer]  WITH CHECK ADD  CONSTRAINT [FK_fbs_SheetAnswer_fbs_SheetQuestion] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[fbs_SheetQuestion] ([QuestionID])
ON UPDATE CASCADE
ON DELETE CASCADE
