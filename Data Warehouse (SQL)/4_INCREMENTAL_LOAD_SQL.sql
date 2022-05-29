--USE GreenhouseDWH
use GreenHouseDWH
GO

--CREATE etl schema
IF NOT EXISTS 
  (SELECT  *
	FROM    sys.schemas
    WHERE   name = 'etl' )
EXEC
	('CREATE SCHEMA [etl]');
GO

--creating log table to store update history
IF NOT EXISTS 
( SELECT  *
	FROM    sys.tables
    WHERE   name = 'LogUpdate' )
EXEC
	('Create table [etl].[LogUpdate] (
		[Table] [nvarchar](50) null,
		[LastLoadDate] int null
	) ON [PRIMARY]')
GO


--POPULATE log table in an 'initial load' way with timestamp on last date of transaction records
INSERT INTO [etl].[LogUpdate] ([Table], [LastLoadDate]) VALUES ('Dim_Greenhouse', 20210101)
INSERT INTO [etl].[LogUpdate] ([Table], [LastLoadDate]) VALUES ('Dim_Pot', 20210101)
INSERT INTO [etl].[LogUpdate] ([Table], [LastLoadDate]) VALUES ('Fact_Measurements', 20210101)
INSERT INTO [etl].[LogUpdate] ([Table], [LastLoadDate]) VALUES ('Fact_MoisturePots', 20210101)
GO

--UPDATING Pot Dimension table to ADD ValidTo and ValidFrom
alter table [edw].[Dim_Pot]
add ValidFrom int, ValidTo int

--UPDATING Greenhouse Dimension table to ADD ValidTo and ValidFrom
alter table [edw].[Dim_Greenhouse]
add ValidFrom int, ValidTo int
GO

--UPDATE Greenhouse Dimension current records 
UPDATE [edw].[Dim_Greenhouse]
set ValidFrom = 20210101, ValidTo = 99990101

--UPDATE Pot Dimension current records 
UPDATE [edw].[Dim_Pot]
set ValidFrom = 20210101, ValidTo = 99990101
GO


-- DECLARING variables for dates
DECLARE @LastLoadDate int
SET @LastLoadDate = (SELECT MAX([LastLoadDate]) FROM [etl].[LogUpdate] where [Table]='Dim_Greenhouse')
DECLARE @NewLoadDate int
SET @NewLoadDate = CONVERT(CHAR(8), GETDATE(), 112)
DECLARE @FutureDate int
SET @FutureDate = 99990101

--INSERT UPDATED rows in the Greenhouse Dimension 
-- AND UPDATING the ValidFrom and ValidTo attribute accordingly 
INSERT INTO [edw].[Dim_Greenhouse]
    (
    [GreenHouse_ID]
	,[Latitude]
	,[Longitude]
	,[ValidFrom]
	,[ValidTo]
    )
SELECT
     [GreenHouse_ID]
        ,[Latitude]
        ,[Longitude]
	  , @NewLoadDate
	  , @FutureDate
FROM [GreenhouseDWH].[stage].[Dim_Greenhouse]
WHERE [GreenHouse_ID] in (SELECT [GreenHouse_ID]
FROM [stage].[Dim_Greenhouse] EXCEPT SELECT [GreenHouse_ID]
FROM [edw].[Dim_Greenhouse]
WHERE ValidTo = 99990101)

--UPDATING Log table 
INSERT INTO [etl].[LogUpdate] ([Table], [LastLoadDate]) VALUES ('Dim_Greenhouse', @NewLoadDate)
GO


-- DECLARING variables for dates
DECLARE @LastLoadDate int
SET @LastLoadDate = (SELECT MAX([LastLoadDate]) FROM [etl].[LogUpdate] where [Table]='Dim_Pot')
DECLARE @NewLoadDate int
SET @NewLoadDate = CONVERT(CHAR(8), GETDATE(), 112)
DECLARE @FutureDate int
SET @FutureDate = 99990101

--INSERT UPDATED rows in the Greenhouse Dimension 
-- AND UPDATING the ValidFrom and ValidTo attribute accordingly 
INSERT INTO [edw].[Dim_Pot]
    (
    [Pot_ID]
	,[Plant]
	,[ValidFrom]
	,[ValidTo]
    )
SELECT
     [Pot_ID]
	  ,[Plant]
	  , @NewLoadDate
	  , @FutureDate
FROM [GreenhouseDWH].[stage].[Dim_Pot]
WHERE [Pot_ID] in (SELECT [Pot_ID]
FROM [stage].[Dim_Pot] EXCEPT SELECT [Pot_ID]
FROM [edw].[Dim_Pot]
WHERE ValidTo = 99990101)

--UPDATING Log table 
INSERT INTO [etl].[LogUpdate] ([Table], [LastLoadDate]) VALUES ('Dim_Pot', @NewLoadDate)
GO

-- to start deleting inside Dim_Greenhouse (deleting from edw rows what are not existent in stage anymore after source update)
/* --outcomment to use
UPDATE [edw].[Dim_Greenhouse]
SET ValidTo=@NewLoadDate-1
WHERE [GreenHouse_ID] in 
( SELECT [GreenHouse_ID]
  FROM [edw].[Dim_Greenhouse]
  WHERE [GreenHouse_ID] in (SELECT [GreenHouse_ID] from [edw].[Dim_Greenhouse]
  except SELECT [GreenHouse_ID]
  FROM [stage].[Dim_Greenhouse])) and ValidTo=99990101
  GO
*/

  -- to start deleting inside Dim_Pot (deleting from edw rows what are not existent in stage anymore after source update)
/* --outcomment to use
UPDATE [edw].[Dim_Pot]
SET ValidTo=@NewLoadDate-1
WHERE [Pot_ID] in 
( SELECT [Pot_ID]
  FROM [edw].[Dim_Pot]
  WHERE [Pot_ID] in (SELECT [Pot_ID] from [edw].[Dim_Pot]
  except SELECT [Pot_ID]
  FROM [stage].[Dim_Pot])) and ValidTo=99990101
  GO
*/



---stage.Fact_Measurements table insert from source (only for data after the last load date)

---getting last load date for fact staging
--DECLARING Date variables for updates
DECLARE @LastLoadDate datetime
SET @LastLoadDate = (SELECT [Date]
FROM [edw].[Dim_Date]
WHERE D_ID in (SELECT MAX([LastLoadDate]) FROM [etl].[LogUpdate] where [Table]='Fact_Measurements'))

TRUNCATE TABLE [stage].[Fact_Measurements]
INSERT INTO [stage].[Fact_Measurements]([GreenHouse_ID],
    [Temperature],
    [Humidity],
    [CarbonDioxide],
[MeasurementDateTime])
SELECT g.GreenHouseId,
       t.Temperature,
       h.[Humidity],
       c.[Co2Measurement],
       t.Time
FROM [GreenhouseDB].[dbo].Greenhouses g
    JOIN [GreenhouseDB].[dbo].[TemperatureMeasurement] t
on (g.GreenHouseId=t.GreenHouseId)
    JOIN [GreenhouseDB].[dbo].[HumidityMeasurement] h on (g.GreenHouseId=h.GreenHouseId and t.Time = h.Time)
    JOIN [GreenhouseDB].[dbo].[DioxideCarbonMeasurement] c on (g.GreenHouseId= c.GreenHouseId and t.Time = c.Time)
  WHERE src.[Time] > (@LastLoadDate)
GO


--INSERT new updates from stage 
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
	WHERE g.ValidTo = 99990101
GO

--UPDATE last load date for Measurement Fact TABLE 
DECLARE @NewLoadDate int
SET @NewLoadDate = CONVERT(CHAR(8), GETDATE(), 112)
INSERT INTO [etl].[LogUpdate] ([Table], [LastLoadDate]) VALUES ('Fact_Measurements', @NewLoadDate)
GO

-------------------------------------------------------------------------------

---stage.Fact_MoisturePots table insert from source (only for data after the last load date)

--UPDATE stage last changes in Fact Moisture TABLE
DECLARE @LastLoadDate datetime
SET @LastLoadDate = (SELECT [Date]
FROM [edw].[Dim_Date]
WHERE D_ID in (SELECT MAX([LastLoadDate]) FROM [etl].[LogUpdate] where [Table]='Fact_MoisturePots'))

TRUNCATE TABLE [stage].[Fact_MoisturePots]
INSERT INTO [stage].[Fact_MoisturePots](
    [Pot_ID],
    [GreenHouse_ID],
    [Moisture],
[MeasurementDateTime]
)
SELECT
    pot.[Id],
    pot.[GreenHouseId],
    mst.[Moisture],
    mst.Time
FROM [GreenhouseDB].[dbo].[Pot] pot
    inner join [GreenhouseDB].[dbo].MoistureMeasurement mst
on mst.potId=pot.Id
  WHERE mst.[Time] > (@LastLoadDate)
GO

--UPDATE data warehouse last changes in Fact Moisture TABLE
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
	WHERE g.ValidTo = 99990101
	and p.ValidTo = 99990101
GO

--UPDATE last load date for Fact Moisture TABLE
DECLARE @NewLoadDate int
SET @NewLoadDate = CONVERT(CHAR(8), GETDATE(), 112)
INSERT INTO [etl].[LogUpdate] ([Table], [LastLoadDate]) VALUES ('Fact_MoisturePots', @NewLoadDate)
GO