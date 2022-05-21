/* -- To use GreenHouseDWH --*/
USE [GreenHouseDWH]
GO

/* -- CREATE EDW SCHEMA for GreenHouseDWH --*/
CREATE SCHEMA [edw];
GO

/* -- CREATE and populate Dim_Date TABLE --*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id=OBJECT_ID(N'[edw].[Dim_Date]') AND type IN (N'U'))
CREATE TABLE [edw].[Dim_Date] (
 [D_ID] INT NOT NULL,
 [Date] DATETIME NOT NULL,
 [Day] INT NOT NULL,
 [Week] INT NOT NULL,
 [Month] INT NOT NULL,
 [Year] INT NOT NULL,
 [Quarter] INT NOT NULL,
 [DayOfWeek] INT NOT NULL,
 [WeekdayName] NVARCHAR(10) NOT NULL,
 [MonthName] NVARCHAR(10) NOT NULL
) ON [PRIMARY]

DECLARE @StartDate DATETIME;
DECLARE @EndDate DATETIME;

SET @StartDate = '2022-01-01'
SET @EndDate = DATEADD(YEAR, 100, getdate())

WHILE @StartDate <= @EndDate
    BEGIN
        INSERT INTO [edw].[Dim_Date]
            ([D_ID],
            [Date],
            [Day],
			[Week],
            [Month],
			[Year],
			[Quarter],         
            [DayOfWeek],
            [WeekdayName],
			[MonthName])
        SELECT
            CONVERT(CHAR(8), @StartDate, 112) as D_ID,
            @StartDate as [Date],
            DATEPART(day, @StartDate) as Day,
			DATEPART(week, @StartDate) as Week,
            DATEPART(month, @StartDate) as Month,
			DATEPART(YEAR, @StartDate) as Year,
			DATEPART(QUARTER, @StartDate) as Quarter,
            DATEPART(WEEKDAY, @StartDate) as DayOfWeek,
            DATENAME(weekday, @StartDate) as WeekdayName,
			DATENAME(month, @StartDate) as MonthName
    
        SET @StartDate = DATEADD(dd, 1, @StartDate) --adding one day for loop to go forward
    END
GO

ALTER TABLE [edw].[Dim_Date] ADD CONSTRAINT PK_Dim_Date PRIMARY KEY (D_ID);
GO

/* -- CREATE and populate Dim_Time TABLE --*/
CREATE TABLE [edw].[Dim_Time] (
			[T_ID] INT NOT NULL,
			[Hour] INT NOT NULL,
			[Minute] INT NOT NULL) ON [PRIMARY]

DECLARE @StartTime DATETIME;
DECLARE @EndTime DATETIME;

SET @StartTime = '2022-01-01'
SET @EndTime = '2022-01-02'

WHILE @StartTime < @EndTime
    BEGIN
        INSERT INTO [edw].[Dim_Time]
            ([T_ID],
			[Hour],
			[Minute])
        SELECT
             CONVERT(INT, REPLACE(
                       CONVERT(VARCHAR(5), @StartTime, 108),
                       ':',   --replacing semicolons with empty space to convert time to int for key
                       ''
                    )) as T_ID,
            DATEPART(HOUR, @StartTime) as Hour,
			DATEPART(MINUTE, @StartTime) as Minute
    
        SET @StartTime = DATEADD(MINUTE, 1, @StartTime) --adding one minute for loop to go forward
    END
GO

ALTER TABLE [edw].[Dim_Time] ADD CONSTRAINT PK_Dim_Time PRIMARY KEY (T_ID);
GO

/* -- CREATE Dim_Greenhouse TABLE --*/
CREATE TABLE [edw].[Dim_Greenhouse] (
 G_ID INT IDENTITY NOT NULL,
 GreenHouse_ID NVARCHAR(100) NOT NULL,
 Country NVARCHAR(50),
 City NVARCHAR(50)
);

ALTER TABLE [edw].[Dim_Greenhouse] ADD CONSTRAINT PK_Dim_Greenhouse PRIMARY KEY (G_ID);
GO

/* -- CREATE Dim_Pot TABLE --*/
CREATE TABLE [edw].[Dim_Pot] (
 P_ID INT IDENTITY NOT NULL,
 Pot_ID INT NOT NULL,
 Plant NVARCHAR(100)
);

ALTER TABLE [edw].[Dim_Pot] ADD CONSTRAINT PK_Dim_Pot PRIMARY KEY (P_ID);
GO

/* -- CREATE Fact_Measurements TABLE --*/
CREATE TABLE [edw].[Fact_Measurements] (
 M_ID INT IDENTITY NOT NULL,
 G_ID INT NOT NULL,
 D_ID INT NOT NULL,
 T_ID INT NOT NULL,
 Temperature DECIMAL,
 Humidity DECIMAL,
 CarbonDioxide INT
);

ALTER TABLE [edw].[Fact_Measurements] ADD CONSTRAINT PK_Fact_Measurements PRIMARY KEY (M_ID, G_ID, D_ID, T_ID);

ALTER TABLE [edw].[Fact_Measurements] ADD CONSTRAINT FK_Fact_Measurements_0  FOREIGN KEY (G_ID) REFERENCES [edw].[Dim_Greenhouse] (G_ID);
ALTER TABLE [edw].[Fact_Measurements] ADD CONSTRAINT FK_Fact_Measurements_1  FOREIGN KEY (D_ID) REFERENCES [edw].[Dim_Date] (D_ID);
ALTER TABLE [edw].[Fact_Measurements] ADD CONSTRAINT FK_Fact_Measurements_2  FOREIGN KEY (T_ID) REFERENCES [edw].[Dim_Time] (T_ID);
GO

/* -- CREATE Fact_MoisturePots TABLE --*/
CREATE TABLE [edw].[Fact_MoisturePots] (
 MP_ID INT IDENTITY NOT NULL,
 P_ID INT NOT NULL,
 G_ID INT NOT NULL,
 D_ID INT NOT NULL,
 T_ID INT NOT NULL,
 Moisture DECIMAL
);

ALTER TABLE [edw].[Fact_MoisturePots] ADD CONSTRAINT PK_Fact_MoisturePots PRIMARY KEY (MP_ID, P_ID, G_ID, D_ID, T_ID);

ALTER TABLE [edw].[Fact_MoisturePots] ADD CONSTRAINT FK_Fact_MoisturePots_0  FOREIGN KEY (P_ID) REFERENCES [edw].[Dim_Pot] (P_ID);
ALTER TABLE [edw].[Fact_MoisturePots] ADD CONSTRAINT FK_Fact_MoisturePots_1  FOREIGN KEY (G_ID) REFERENCES [edw].[Dim_Greenhouse] (G_ID);
ALTER TABLE [edw].[Fact_MoisturePots] ADD CONSTRAINT FK_Fact_MoisturePots_2  FOREIGN KEY (D_ID) REFERENCES [edw].[Dim_Date] (D_ID);
ALTER TABLE [edw].[Fact_MoisturePots] ADD CONSTRAINT FK_Fact_MoisturePots_3  FOREIGN KEY (T_ID) REFERENCES [edw].[Dim_Time] (T_ID);
GO