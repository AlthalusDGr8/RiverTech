USE [TechDemo.MoviesDb]
GO
/****** Object:  Schema [Movies]    Script Date: 13/12/2021 22:55:06 ******/
CREATE SCHEMA [Movies]
GO
/****** Object:  Table [Movies].[GenreMovie]    Script Date: 13/12/2021 22:55:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Movies].[GenreMovie](
	[MoviesMovieId] [bigint] NOT NULL,
	[GenresGenreId] [bigint] NOT NULL,
 CONSTRAINT [PK_GenreMovie] PRIMARY KEY CLUSTERED 
(
	[GenresGenreId] ASC,
	[MoviesMovieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Movies].[LK_Genres]    Script Date: 13/12/2021 22:55:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Movies].[LK_Genres](
	[GenreId] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_LK_Genre] PRIMARY KEY CLUSTERED 
(
	[GenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Movies].[Movies]    Script Date: 13/12/2021 22:55:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Movies].[Movies](
	[MovieId] [bigint] IDENTITY(1,1) NOT NULL,
	[UniqueKey] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[CriticRating] [decimal](18, 1) NOT NULL,
	[Length] [int] NOT NULL,
	[ImgUrl] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[MovieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [Movies].[GenreMovie]  WITH CHECK ADD  CONSTRAINT [FK_MovieGenre_LK_Genre] FOREIGN KEY([GenresGenreId])
REFERENCES [Movies].[LK_Genres] ([GenreId])
GO
ALTER TABLE [Movies].[GenreMovie] CHECK CONSTRAINT [FK_MovieGenre_LK_Genre]
GO
ALTER TABLE [Movies].[GenreMovie]  WITH CHECK ADD  CONSTRAINT [FK_MovieGenre_Movie] FOREIGN KEY([MoviesMovieId])
REFERENCES [Movies].[Movies] ([MovieId])
GO
ALTER TABLE [Movies].[GenreMovie] CHECK CONSTRAINT [FK_MovieGenre_Movie]
GO
