--SUB-STEP 1 Creating data warehouse entities-- 

-- To use GreenHouseDWH 
USE [GreenHouseDWH]
GO

-- CREATE EDW SCHEMA for GreenHouseDWH
IF NOT EXISTS 
  (SELECT  *
	FROM    sys.schemas
    WHERE   name = 'edw' )
EXEC
	('CREATE SCHEMA [edw]');
GO
--!!!!! MAKE SURE YOUR USER HAS RIGHTS TO MAKE CHANGES TO THE CREATED SCHEMA !!!!!!--

 -- CREATE Date Dimension  
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

-- CREATE boundary values for date 
DECLARE @StartDate DATETIME;
DECLARE @EndDate DATETIME;

-- SETTING the start date AND end date
SET @StartDate = '2020-01-01'  
SET @EndDate = DATEADD(YEAR, 100, getdate())

 -- GENERATES in a loop the the date starting from the given start date 
-- and inserts the data into the dimension table
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
    
        SET @StartDate = DATEADD(dd, 1, @StartDate) 
    END
GO

ALTER TABLE [edw].[Dim_Date] ADD CONSTRAINT PK_Dim_Date PRIMARY KEY (D_ID);
GO

 -- CREATE Time Dimension 
CREATE TABLE [edw].[Dim_Time] (
			[T_ID] INT NOT NULL,
			[Hour] INT NOT NULL,
			[Minute] INT NOT NULL) ON [PRIMARY]

-- SETTING the start date AND end date
DECLARE @StartTime DATETIME;
DECLARE @EndTime DATETIME;

-- SETTING the start date AND end date
SET @StartTime = '2022-01-01'
SET @EndTime = '2022-01-02'

 -- GENERATES in a loop the the date starting from the given start date 
-- and inserts the data into the dimension table
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

-- CREATE Greenhouse Dimension TABLE
CREATE TABLE [edw].[Dim_Greenhouse] (
 G_ID INT IDENTITY NOT NULL,
 GreenHouse_ID NVARCHAR(100) NOT NULL,
 Country NVARCHAR(50),
 City NVARCHAR(50)
);

ALTER TABLE [edw].[Dim_Greenhouse] ADD CONSTRAINT PK_Dim_Greenhouse PRIMARY KEY (G_ID);
GO

-- CREATE Pot Dimension TABLE
CREATE TABLE [edw].[Dim_Pot] (
 P_ID INT IDENTITY NOT NULL,
 Pot_ID INT NOT NULL,
 Plant NVARCHAR(100)
);

ALTER TABLE [edw].[Dim_Pot] ADD CONSTRAINT PK_Dim_Pot PRIMARY KEY (P_ID);
GO

-- CREATE Measurement Fact TABLE
CREATE TABLE [edw].[Fact_Measurements] (
 M_ID INT IDENTITY NOT NULL,
 G_ID INT NOT NULL,
 D_ID INT NOT NULL,
 T_ID INT NOT NULL,
 Temperature REAL,
 Humidity FLOAT,
 CarbonDioxide INT
);
-- ADDING constraints to Measurement Fact TABLE
ALTER TABLE [edw].[Fact_Measurements] ADD CONSTRAINT PK_Fact_Measurements PRIMARY KEY (M_ID, G_ID, D_ID, T_ID);

ALTER TABLE [edw].[Fact_Measurements] ADD CONSTRAINT FK_Fact_Measurements_0  FOREIGN KEY (G_ID) REFERENCES [edw].[Dim_Greenhouse] (G_ID);
ALTER TABLE [edw].[Fact_Measurements] ADD CONSTRAINT FK_Fact_Measurements_1  FOREIGN KEY (D_ID) REFERENCES [edw].[Dim_Date] (D_ID);
ALTER TABLE [edw].[Fact_Measurements] ADD CONSTRAINT FK_Fact_Measurements_2  FOREIGN KEY (T_ID) REFERENCES [edw].[Dim_Time] (T_ID);
GO

-- CREATE Moisture Fact TABLE
CREATE TABLE [edw].[Fact_MoisturePots] (
 MP_ID INT IDENTITY NOT NULL,
 P_ID INT NOT NULL,
 G_ID INT NOT NULL,
 D_ID INT NOT NULL,
 T_ID INT NOT NULL,
 Moisture FLOAT
);
-- ADDING constraints to Moisture Fact TABLE
ALTER TABLE [edw].[Fact_MoisturePots] ADD CONSTRAINT PK_Fact_MoisturePots PRIMARY KEY (MP_ID, P_ID, G_ID, D_ID, T_ID);

ALTER TABLE [edw].[Fact_MoisturePots] ADD CONSTRAINT FK_Fact_MoisturePots_0  FOREIGN KEY (P_ID) REFERENCES [edw].[Dim_Pot] (P_ID);
ALTER TABLE [edw].[Fact_MoisturePots] ADD CONSTRAINT FK_Fact_MoisturePots_1  FOREIGN KEY (G_ID) REFERENCES [edw].[Dim_Greenhouse] (G_ID);
ALTER TABLE [edw].[Fact_MoisturePots] ADD CONSTRAINT FK_Fact_MoisturePots_2  FOREIGN KEY (D_ID) REFERENCES [edw].[Dim_Date] (D_ID);
ALTER TABLE [edw].[Fact_MoisturePots] ADD CONSTRAINT FK_Fact_MoisturePots_3  FOREIGN KEY (T_ID) REFERENCES [edw].[Dim_Time] (T_ID);
GO


use [GreenHouseDWH]
go

-- POPULATE Pot Dimension TABLE
--TRUNCATE TABLE [edw].[Dim_Pot]
INSERT INTO [edw].[Dim_Pot]
	([Pot_ID]
	,[Plant])
SELECT
    [Pot_ID],
    [Plant]
FROM [stage].[Dim_Pot]

-- POPULATE Greenhouse Dimension TABLE
--TRUNCATE TABLE [edw].[Dim_Greenhouse]
INSERT INTO [edw].[Dim_Greenhouse]
([GreenHouse_ID],
    [Country],
[City])
SELECT
    [GreenHouse_ID],
    [Country],
    [City]
FROM [stage].[Dim_Greenhouse]

-- POPULATE Measurement Fact TABLE
--TRUNCATE TABLE [edw].[Fact_Measurements]
INSERT INTO [edw].[Fact_Measurements]
([G_ID]
    ,[D_ID]
    ,[T_ID]
    ,[Temperature]
    ,[Humidity]
    ,[CarbonDioxide])
SELECT
    g.G_ID
     ,d.[D_ID]
     ,t.[T_ID]
     ,ms.[Temperature]
     ,ms.[Humidity]
     ,ms.[CarbonDioxide]
FROM [stage].[Fact_Measurements] ms
    inner join [edw].[Dim_Greenhouse] g
on g.GreenHouse_ID = ms.GreenHouse_ID
    inner join [edw].[Dim_Date] d
    on d.Day = DATEPART(DAY, ms.MeasurementDateTime)
    AND d.Month = DATEPART(MONTH, ms.MeasurementDateTime)
    AND d.Year = DATEPART(YEAR, ms.MeasurementDateTime)
    inner join [edw].[Dim_Time] t
    on t.Hour = DATEPART(HOUR, ms.MeasurementDateTime)
    AND t.Minute = DATEPART(MINUTE, ms.MeasurementDateTime)

-- POPULATE Fact Moisture TABLE
--TRUNCATE TABLE [edw].[Fact_MoisturePots]
INSERT INTO [edw].[Fact_MoisturePots]
([P_ID]
    ,[G_ID]
    ,[D_ID]
    ,[T_ID]
    ,[Moisture])
SELECT
    p.[P_ID]
     ,g.[G_ID]
     ,d.[D_ID]
     ,t.[T_ID]
     ,[Moisture]
FROM [GreenHouseDWH].[stage].[Fact_MoisturePots] m
    inner join [edw].[Dim_Greenhouse] g
on g.GreenHouse_ID = m.GreenHouse_ID
    inner join [edw].[Dim_Pot] p
    on p.Pot_ID = m.Pot_ID
    inner join [edw].[Dim_Date] d
    on d.Day = DATEPART(DAY, m.MeasurementDateTime)
    AND d.Month = DATEPART(MONTH, m.MeasurementDateTime)
    AND d.Year = DATEPART(YEAR, m.MeasurementDateTime)
    inner join [edw].[Dim_Time] t
    on t.Hour = DATEPART(HOUR, m.MeasurementDateTime)
    AND t.Minute = DATEPART(MINUTE, m.MeasurementDateTime)
