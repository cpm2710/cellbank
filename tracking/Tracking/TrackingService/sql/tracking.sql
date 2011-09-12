/****** Object:  Table [dbo].[Tracking]    Script Date: 09/05/2011 13:39:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tracking]') AND type in (N'U'))
DROP TABLE [dbo].[Tracking]
GO
/****** Object:  Table [dbo].[Tracking]    Script Date: 09/05/2011 13:39:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tracking]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tracking](
	[wfinstanceid] [uniqueidentifier] NULL,
	[wfname] [nchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[currentevent] [nchar](50) COLLATE Chinese_PRC_CI_AS NULL
)
END
GO
