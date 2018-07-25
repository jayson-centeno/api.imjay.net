CREATE TABLE [dbo].[ContactUs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Email] [nvarchar](200) NULL,
	[Message] [nvarchar](max) NULL,
	[IPAddress] [nvarchar](100) NULL,
	[Approved] [bit] NULL,
	[DatePosted] [datetime] NULL,
 CONSTRAINT [PK_ContactUs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[ContactUs] ADD  CONSTRAINT [DF_ContactUs_Approved]  DEFAULT ((1)) FOR [Approved]


ALTER TABLE [dbo].[ContactUs] ADD  CONSTRAINT [DF_ContactUs_DatePosted]  DEFAULT (getdate()) FOR [DatePosted]