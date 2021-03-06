USE [master]
GO
/****** Object:  Database [TestGreenHouseDB]    Script Date: 5/31/2022 1:49:38 PM ******/
CREATE DATABASE [TestGreenHouseDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Greenhouse', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TestGreenHouseDB.mdf' , SIZE = 139264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Greenhouse_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TestGreenHouseDB_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TestGreenHouseDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestGreenHouseDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TestGreenHouseDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TestGreenHouseDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TestGreenHouseDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TestGreenHouseDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestGreenHouseDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TestGreenHouseDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET RECOVERY FULL 
GO
ALTER DATABASE [TestGreenHouseDB] SET  MULTI_USER 
GO
ALTER DATABASE [TestGreenHouseDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TestGreenHouseDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TestGreenHouseDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TestGreenHouseDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TestGreenHouseDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TestGreenHouseDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TestGreenHouseDB', N'ON'
GO
ALTER DATABASE [TestGreenHouseDB] SET QUERY_STORE = OFF
GO
USE [TestGreenHouseDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/31/2022 1:49:39 PM ******/
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
/****** Object:  Table [dbo].[DioxideCarbonMeasurement]    Script Date: 5/31/2022 1:49:39 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[DioxideCarbonMeasurement](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Time] [datetime2](7) NOT NULL,
    [GreenHouseId] [nvarchar](450) NULL,
    [Co2Measurement] [int] NOT NULL,
    CONSTRAINT [PK_DioxideCarbonMeasurement] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Greenhouses]    Script Date: 5/31/2022 1:49:39 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Greenhouses](
    [GreenHouseId] [nvarchar](450) NOT NULL,
    [Latitude] [real] NOT NULL,
    [Longitude] [real] NOT NULL,
    CONSTRAINT [PK_Greenhouses] PRIMARY KEY CLUSTERED
(
[GreenHouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[HumidityMeasurement]    Script Date: 5/31/2022 1:49:39 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[HumidityMeasurement](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Humidity] [float] NOT NULL,
    [Time] [datetime2](7) NOT NULL,
    [GreenHouseId] [nvarchar](450) NULL,
    CONSTRAINT [PK_HumidityMeasurement] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[MoistureMeasurement]    Script Date: 5/31/2022 1:49:39 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[MoistureMeasurement](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Moisture] [float] NOT NULL,
    [Time] [datetime2](7) NOT NULL,
    [PotId] [int] NULL,
    CONSTRAINT [PK_MoistureMeasurement] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Pot]    Script Date: 5/31/2022 1:49:39 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Pot](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [GreenHouseId] [nvarchar](450) NULL,
    [Name] [nvarchar](max) NOT NULL,
    [MoistureThresholdId] [int] NOT NULL,
    [MoistureSensorId] [int] NOT NULL,
    [MoistureSensorStatusId] [int] NOT NULL,
    CONSTRAINT [PK_Pot] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[SensorStatus]    Script Date: 5/31/2022 1:49:39 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[SensorStatus](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Type] [int] NOT NULL,
    [IsWorking] [bit] NOT NULL,
    [GreenHouseId] [nvarchar](450) NULL,
    CONSTRAINT [PK_SensorStatus] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[TemperatureMeasurement]    Script Date: 5/31/2022 1:49:39 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[TemperatureMeasurement](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Temperature] [real] NOT NULL,
    [Time] [datetime2](7) NOT NULL,
    [GreenHouseId] [nvarchar](450) NULL,
    CONSTRAINT [PK_TemperatureMeasurement] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
/****** Object:  Table [dbo].[Threshold]    Script Date: 5/31/2022 1:49:39 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
CREATE TABLE [dbo].[Threshold](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Type] [int] NOT NULL,
    [LowerThreshold] [float] NOT NULL,
    [HigherThreshold] [float] NULL,
    [GreenHouseId] [nvarchar](450) NULL,
    CONSTRAINT [PK_Threshold] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO
    SET ANSI_PADDING ON
    GO
/****** Object:  Index [IX_DioxideCarbonMeasurement_GreenHouseId]    Script Date: 5/31/2022 1:49:39 PM ******/
CREATE NONCLUSTERED INDEX [IX_DioxideCarbonMeasurement_GreenHouseId] ON [dbo].[DioxideCarbonMeasurement]
(
	[GreenHouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HumidityMeasurement_GreenHouseId]    Script Date: 5/31/2022 1:49:39 PM ******/
CREATE NONCLUSTERED INDEX [IX_HumidityMeasurement_GreenHouseId] ON [dbo].[HumidityMeasurement]
(
	[GreenHouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_MoistureMeasurement_PotId]    Script Date: 5/31/2022 1:49:39 PM ******/
CREATE NONCLUSTERED INDEX [IX_MoistureMeasurement_PotId] ON [dbo].[MoistureMeasurement]
(
	[PotId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Pot_GreenHouseId]    Script Date: 5/31/2022 1:49:39 PM ******/
CREATE NONCLUSTERED INDEX [IX_Pot_GreenHouseId] ON [dbo].[Pot]
(
	[GreenHouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Pot_MoistureSensorStatusId]    Script Date: 5/31/2022 1:49:39 PM ******/
CREATE NONCLUSTERED INDEX [IX_Pot_MoistureSensorStatusId] ON [dbo].[Pot]
(
	[MoistureSensorStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Pot_MoistureThresholdId]    Script Date: 5/31/2022 1:49:39 PM ******/
CREATE NONCLUSTERED INDEX [IX_Pot_MoistureThresholdId] ON [dbo].[Pot]
(
	[MoistureThresholdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_SensorStatus_GreenHouseId]    Script Date: 5/31/2022 1:49:39 PM ******/
CREATE NONCLUSTERED INDEX [IX_SensorStatus_GreenHouseId] ON [dbo].[SensorStatus]
(
	[GreenHouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_TemperatureMeasurement_GreenHouseId]    Script Date: 5/31/2022 1:49:39 PM ******/
CREATE NONCLUSTERED INDEX [IX_TemperatureMeasurement_GreenHouseId] ON [dbo].[TemperatureMeasurement]
(
	[GreenHouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Threshold_GreenHouseId]    Script Date: 5/31/2022 1:49:39 PM ******/
CREATE NONCLUSTERED INDEX [IX_Threshold_GreenHouseId] ON [dbo].[Threshold]
(
	[GreenHouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DioxideCarbonMeasurement] ADD  DEFAULT ((0)) FOR [Co2Measurement]
    GO
ALTER TABLE [dbo].[Greenhouses] ADD  DEFAULT (CONVERT([real],(0))) FOR [Latitude]
    GO
ALTER TABLE [dbo].[Greenhouses] ADD  DEFAULT (CONVERT([real],(0))) FOR [Longitude]
    GO
ALTER TABLE [dbo].[Pot] ADD  DEFAULT (N'') FOR [Name]
    GO
ALTER TABLE [dbo].[Pot] ADD  DEFAULT ((0)) FOR [MoistureThresholdId]
    GO
ALTER TABLE [dbo].[Pot] ADD  DEFAULT ((0)) FOR [MoistureSensorId]
    GO
ALTER TABLE [dbo].[Pot] ADD  DEFAULT ((0)) FOR [MoistureSensorStatusId]
    GO
ALTER TABLE [dbo].[DioxideCarbonMeasurement]  WITH CHECK ADD  CONSTRAINT [FK_DioxideCarbonMeasurement_Greenhouses_GreenHouseId] FOREIGN KEY([GreenHouseId])
    REFERENCES [dbo].[Greenhouses] ([GreenHouseId])
    GO
ALTER TABLE [dbo].[DioxideCarbonMeasurement] CHECK CONSTRAINT [FK_DioxideCarbonMeasurement_Greenhouses_GreenHouseId]
    GO
ALTER TABLE [dbo].[HumidityMeasurement]  WITH CHECK ADD  CONSTRAINT [FK_HumidityMeasurement_Greenhouses_GreenHouseId] FOREIGN KEY([GreenHouseId])
    REFERENCES [dbo].[Greenhouses] ([GreenHouseId])
    GO
ALTER TABLE [dbo].[HumidityMeasurement] CHECK CONSTRAINT [FK_HumidityMeasurement_Greenhouses_GreenHouseId]
    GO
ALTER TABLE [dbo].[MoistureMeasurement]  WITH CHECK ADD  CONSTRAINT [FK_MoistureMeasurement_Pot_PotId] FOREIGN KEY([PotId])
    REFERENCES [dbo].[Pot] ([Id])
    GO
ALTER TABLE [dbo].[MoistureMeasurement] CHECK CONSTRAINT [FK_MoistureMeasurement_Pot_PotId]
    GO
ALTER TABLE [dbo].[Pot]  WITH CHECK ADD  CONSTRAINT [FK_Pot_Greenhouses_GreenHouseId] FOREIGN KEY([GreenHouseId])
    REFERENCES [dbo].[Greenhouses] ([GreenHouseId])
    GO
ALTER TABLE [dbo].[Pot] CHECK CONSTRAINT [FK_Pot_Greenhouses_GreenHouseId]
    GO
ALTER TABLE [dbo].[Pot]  WITH CHECK ADD  CONSTRAINT [FK_Pot_SensorStatus_MoistureSensorStatusId] FOREIGN KEY([MoistureSensorStatusId])
    REFERENCES [dbo].[SensorStatus] ([Id])
    ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pot] CHECK CONSTRAINT [FK_Pot_SensorStatus_MoistureSensorStatusId]
    GO
ALTER TABLE [dbo].[Pot]  WITH CHECK ADD  CONSTRAINT [FK_Pot_Threshold_MoistureThresholdId] FOREIGN KEY([MoistureThresholdId])
    REFERENCES [dbo].[Threshold] ([Id])
    ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pot] CHECK CONSTRAINT [FK_Pot_Threshold_MoistureThresholdId]
    GO
ALTER TABLE [dbo].[SensorStatus]  WITH CHECK ADD  CONSTRAINT [FK_SensorStatus_Greenhouses_GreenHouseId] FOREIGN KEY([GreenHouseId])
    REFERENCES [dbo].[Greenhouses] ([GreenHouseId])
    GO
ALTER TABLE [dbo].[SensorStatus] CHECK CONSTRAINT [FK_SensorStatus_Greenhouses_GreenHouseId]
    GO
ALTER TABLE [dbo].[TemperatureMeasurement]  WITH CHECK ADD  CONSTRAINT [FK_TemperatureMeasurement_Greenhouses_GreenHouseId] FOREIGN KEY([GreenHouseId])
    REFERENCES [dbo].[Greenhouses] ([GreenHouseId])
    GO
ALTER TABLE [dbo].[TemperatureMeasurement] CHECK CONSTRAINT [FK_TemperatureMeasurement_Greenhouses_GreenHouseId]
    GO
ALTER TABLE [dbo].[Threshold]  WITH CHECK ADD  CONSTRAINT [FK_Threshold_Greenhouses_GreenHouseId] FOREIGN KEY([GreenHouseId])
    REFERENCES [dbo].[Greenhouses] ([GreenHouseId])
    GO
ALTER TABLE [dbo].[Threshold] CHECK CONSTRAINT [FK_Threshold_Greenhouses_GreenHouseId]
    GO
    USE [master]
    GO
ALTER DATABASE [TestGreenHouseDB] SET  READ_WRITE 
GO
