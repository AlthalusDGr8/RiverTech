USE [TechDemo.MoviesDb]
GO
/****** Object:  Schema [Movies]    Script Date: 14/12/2021 22:41:11 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'Movies')
EXEC sys.sp_executesql N'CREATE SCHEMA [Movies]'
GO
/****** Object:  Table [Movies].[GenreMovie]    Script Date: 14/12/2021 22:41:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Movies].[GenreMovie]') AND type in (N'U'))
BEGIN
CREATE TABLE [Movies].[GenreMovie](
	[MoviesMovieId] [bigint] NOT NULL,
	[GenresGenreId] [bigint] NOT NULL,
 CONSTRAINT [PK_GenreMovie] PRIMARY KEY CLUSTERED 
(
	[GenresGenreId] ASC,
	[MoviesMovieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Movies].[LK_Genres]    Script Date: 14/12/2021 22:41:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Movies].[LK_Genres]') AND type in (N'U'))
BEGIN
CREATE TABLE [Movies].[LK_Genres](
	[GenreId] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_LK_Genre] PRIMARY KEY CLUSTERED 
(
	[GenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [Movies].[Movies]    Script Date: 14/12/2021 22:41:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Movies].[Movies]') AND type in (N'U'))
BEGIN
CREATE TABLE [Movies].[Movies](
	[MovieId] [bigint] IDENTITY(1,1) NOT NULL,
	[UniqueKey] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](300) NOT NULL,
	[CriticRating] [decimal](18, 1) NOT NULL,
	[Length] [int] NOT NULL,
	[ImgUrl] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[MovieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (1, 1)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (8, 1)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (9, 1)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (1, 2)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (2, 2)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (8, 2)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (9, 2)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (1, 3)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (2, 3)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (2, 4)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (7, 4)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (3, 5)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (3, 6)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (3, 7)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (7, 9)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (7, 10)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (9, 10)
GO
INSERT [Movies].[GenreMovie] ([MoviesMovieId], [GenresGenreId]) VALUES (8, 11)
GO
SET IDENTITY_INSERT [Movies].[LK_Genres] ON 
GO
INSERT [Movies].[LK_Genres] ([GenreId], [Code], [Description]) VALUES (1, N'ACT', N'Action')
GO
INSERT [Movies].[LK_Genres] ([GenreId], [Code], [Description]) VALUES (2, N'ADV', N'Adventure')
GO
INSERT [Movies].[LK_Genres] ([GenreId], [Code], [Description]) VALUES (3, N'COM', N'Comedy')
GO
INSERT [Movies].[LK_Genres] ([GenreId], [Code], [Description]) VALUES (4, N'CRM', N'Crime')
GO
INSERT [Movies].[LK_Genres] ([GenreId], [Code], [Description]) VALUES (5, N'BIO', N'Biography')
GO
INSERT [Movies].[LK_Genres] ([GenreId], [Code], [Description]) VALUES (6, N'DRM', N'Drama')
GO
INSERT [Movies].[LK_Genres] ([GenreId], [Code], [Description]) VALUES (7, N'HIS', N'History')
GO
INSERT [Movies].[LK_Genres] ([GenreId], [Code], [Description]) VALUES (8, N'SPR', N'Sport')
GO
INSERT [Movies].[LK_Genres] ([GenreId], [Code], [Description]) VALUES (9, N'MYS', N'Mystery')
GO
INSERT [Movies].[LK_Genres] ([GenreId], [Code], [Description]) VALUES (10, N'THR', N'Thriller')
GO
INSERT [Movies].[LK_Genres] ([GenreId], [Code], [Description]) VALUES (11, N'SCI', N'SciFi')
GO
SET IDENTITY_INSERT [Movies].[LK_Genres] OFF
GO
SET IDENTITY_INSERT [Movies].[Movies] ON 
GO
INSERT [Movies].[Movies] ([MovieId], [UniqueKey], [Name], [Description], [CriticRating], [Length], [ImgUrl]) VALUES (1, N'deadpool', N'Deadpool', N'A former Special Forces operative turned mercenary is subjected to a rogue experiment that leaves him with accelerated healing powers, adopting the alter ego Deadpool.', CAST(8.6 AS Decimal(18, 1)), 108, N'deadpool.jpg')
GO
INSERT [Movies].[Movies] ([MovieId], [UniqueKey], [Name], [Description], [CriticRating], [Length], [ImgUrl]) VALUES (2, N'we-are-the-millers', N'We''re the Millers', N'A veteran pot dealer creates a fake family as part of his plan to move a huge shipment of weed into the U.S. from Mexico.', CAST(7.0 AS Decimal(18, 1)), 110, N'we-are-the-millers.jpg')
GO
INSERT [Movies].[Movies] ([MovieId], [UniqueKey], [Name], [Description], [CriticRating], [Length], [ImgUrl]) VALUES (3, N'straight-outta-compton', N'Straight Outta Compton', N'The group NWA emerges from the mean streets of Compton in Los Angeles, California, in the mid-1980s and revolutionizes Hip Hop culture with their music and tales about life in the hood.', CAST(8.0 AS Decimal(18, 1)), 147, N'straight-outta-compton.jpg')
GO
INSERT [Movies].[Movies] ([MovieId], [UniqueKey], [Name], [Description], [CriticRating], [Length], [ImgUrl]) VALUES (4, N'gridiron-gang', N'Gridiron Gang', N'Teenagers at a juvenile detention center, under the leadership of their counselor, gain self-esteem by playing football together.', CAST(6.9 AS Decimal(18, 1)), 125, N'gridiron-gang.jpg')
GO
INSERT [Movies].[Movies] ([MovieId], [UniqueKey], [Name], [Description], [CriticRating], [Length], [ImgUrl]) VALUES (5, N'american-gangster', N'American Gangster', N'In 1970s America, a detective works to bring down the drug empire of Frank Lucas, a heroin kingpin from Manhattan, who is smuggling the drug into the country from the Far East.', CAST(7.8 AS Decimal(18, 1)), 157, N'american-gangster.jpg')
GO
INSERT [Movies].[Movies] ([MovieId], [UniqueKey], [Name], [Description], [CriticRating], [Length], [ImgUrl]) VALUES (6, N'gangster-squad', N'Gangster Squad', N'It''s 1949 Los Angeles, the city is run by gangsters and a malicious mobster, Mickey Cohen. Determined to end the corruption, John O''Mara assembles a team of cops, ready to take down the ruthless leader and restore peace to the city.', CAST(6.8 AS Decimal(18, 1)), 113, N'gangster-squad.jpg')
GO
INSERT [Movies].[Movies] ([MovieId], [UniqueKey], [Name], [Description], [CriticRating], [Length], [ImgUrl]) VALUES (7, N'now-you-see-me', N'Now You See Me', N'An FBI agent and an Interpol detective track a team of illusionists who pull off bank heists during their performances and reward their audiences with the money.', CAST(7.3 AS Decimal(18, 1)), 115, N'now-you-see-me.jpg')
GO
INSERT [Movies].[Movies] ([MovieId], [UniqueKey], [Name], [Description], [CriticRating], [Length], [ImgUrl]) VALUES (8, N'jurassic-world', N'Jurassic World', N'A new theme park is built on the original site of Jurassic Park. Everything is going well until the park''s newest attraction--a genetically modified giant stealth killing machine--escapes containment and goes on a killing spree.', CAST(7.1 AS Decimal(18, 1)), 124, N'jurassic-world.jpg')
GO
INSERT [Movies].[Movies] ([MovieId], [UniqueKey], [Name], [Description], [CriticRating], [Length], [ImgUrl]) VALUES (9, N'mission--impossible--rogue-nation', N'Mission: Impossible: Rogue Nation', N'Ethan and team take on their most impossible mission yet, eradicating the Syndicate - an International rogue organization as highly skilled as they are, committed to destroying the IMF.', CAST(7.5 AS Decimal(18, 1)), 131, N'mission--impossible--rogue-nation.jpg')
GO
SET IDENTITY_INSERT [Movies].[Movies] OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Movies].[FK_MovieGenre_LK_Genre]') AND parent_object_id = OBJECT_ID(N'[Movies].[GenreMovie]'))
ALTER TABLE [Movies].[GenreMovie]  WITH CHECK ADD  CONSTRAINT [FK_MovieGenre_LK_Genre] FOREIGN KEY([GenresGenreId])
REFERENCES [Movies].[LK_Genres] ([GenreId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Movies].[FK_MovieGenre_LK_Genre]') AND parent_object_id = OBJECT_ID(N'[Movies].[GenreMovie]'))
ALTER TABLE [Movies].[GenreMovie] CHECK CONSTRAINT [FK_MovieGenre_LK_Genre]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Movies].[FK_MovieGenre_Movie]') AND parent_object_id = OBJECT_ID(N'[Movies].[GenreMovie]'))
ALTER TABLE [Movies].[GenreMovie]  WITH CHECK ADD  CONSTRAINT [FK_MovieGenre_Movie] FOREIGN KEY([MoviesMovieId])
REFERENCES [Movies].[Movies] ([MovieId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Movies].[FK_MovieGenre_Movie]') AND parent_object_id = OBJECT_ID(N'[Movies].[GenreMovie]'))
ALTER TABLE [Movies].[GenreMovie] CHECK CONSTRAINT [FK_MovieGenre_Movie]
GO
