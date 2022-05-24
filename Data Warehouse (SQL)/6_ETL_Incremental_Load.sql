--use dwh
use GreenHouseDWH
GO

--create etl schema
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


--populating log table in an 'initial load' way with timestamp on last date of transaction records
INSERT INTO [etl].[LogUpdate] ([Table], [LastLoadDate]) VALUES ('Dim_Greenhouse', 20220505)
INSERT INTO [etl].[LogUpdate] ([Table], [LastLoadDate]) VALUES ('Dim_Pot', 20220505)
INSERT INTO [etl].[LogUpdate] ([Table], [LastLoadDate]) VALUES ('Fact_Measurements', 20220505)
INSERT INTO [etl].[LogUpdate] ([Table], [LastLoadDate]) VALUES ('Fact_MoisturePots', 20220505)
GO

--alter tables to add ValidTo and ValidFrom
alter table [edw].[Dim_Pot]
add ValidFrom int, ValidTo int

alter table [edw].[Dim_Greenhouse]
add ValidFrom int, ValidTo int
GO

--updating current records
UPDATE [edw].[Dim_Greenhouse]
set ValidFrom = 20220505, ValidTo = 99990101

UPDATE [edw].[Dim_Pot]
set ValidFrom = 20220505, ValidTo = 99990101
GO


--to insert changed/new data into Dim_Greenhouse
DECLARE @LastLoadDate int
SET @LastLoadDate = (SELECT MAX([LastLoadDate]) FROM [etl].[LogUpdate] where [Table]='Dim_Greenhouse')
DECLARE @NewLoadDate int
SET @NewLoadDate = CONVERT(CHAR(8), GETDATE(), 112)
DECLARE @FutureDate int
SET @FutureDate = 99990101

INSERT INTO [edw].[Dim_Greenhouse]
    (
    [GreenHouse_ID]
	,[Country]
	,[City]
	,[ValidFrom]
	,[ValidTo]
    )
SELECT
     [GreenHouse_ID]
	  ,[Country]
	  ,[City]
	  , @NewLoadDate
	  , @FutureDate
FROM [GreenhouseDWH].[stage].[Dim_Greenhouse]
WHERE [GreenHouse_ID] in (SELECT [GreenHouse_ID]
FROM [stage].[Dim_Greenhouse] EXCEPT SELECT [GreenHouse_ID]
FROM [edw].[Dim_Greenhouse]
WHERE ValidTo = 99990101)

INSERT INTO [etl].[LogUpdate] ([Table], [LastLoadDate]) VALUES ('Dim_Greenhouse', @NewLoadDate)
GO


--to insert changed/new data into Dim_Pot
DECLARE @LastLoadDate int
SET @LastLoadDate = (SELECT MAX([LastLoadDate]) FROM [etl].[LogUpdate] where [Table]='Dim_Pot')
DECLARE @NewLoadDate int
SET @NewLoadDate = CONVERT(CHAR(8), GETDATE(), 112)
DECLARE @FutureDate int
SET @FutureDate = 99990101

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
DECLARE @LastLoadDate datetime
SET @LastLoadDate = (SELECT [Date]
FROM [edw].[Dim_Date]
WHERE D_ID in (SELECT MAX([LastLoadDate]) FROM [etl].[LogUpdate] where [Table]='Fact_Measurements'))

TRUNCATE TABLE [stage].[Fact_Measurements]
INSERT INTO [stage].[Fact_Measurements](
    [GreenHouse_ID],
    [Temperature],
    [Humidity],
    [CarbonDioxide],
[MeasurementDateTime]
)
SELECT DISTINCT
    src.GreenHouseId,
    t.Temperature,
    h.[Humidity],
    c.[Co2Measurement],
    src.Time
FROM (
         SELECT GreenHouseId,Temperature,Time
         FROM [GreenhouseDB].[dbo].[TemperatureMeasurement]
         UNION
         SELECT GreenHouseId,Humidity,Time
         FROM [GreenhouseDB].[dbo].[HumidityMeasurement]
         UNION
         SELECT GreenHouseId,Co2Measurement,Time
         FROM [GreenhouseDB].[dbo].[DioxideCarbonMeasurement]
     ) src
         LEFT JOIN [GreenhouseDB].[dbo].[TemperatureMeasurement] t on (src.Time=t.Time and src.GreenHouseId=t.GreenHouseId)
    LEFT JOIN [GreenhouseDB].[dbo].[HumidityMeasurement] h on (src.Time=h.Time and src.GreenHouseId=h.GreenHouseId)
    LEFT JOIN [GreenhouseDB].[dbo].[DioxideCarbonMeasurement] c on (src.Time=c.Time and src.GreenHouseId=c.GreenHouseId)
  WHERE src.[Time] > (@LastLoadDate)
GO


--to insert all data into Fact_Measurements after staging relevant data
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

--to update last load date for measurements
DECLARE @NewLoadDate int
SET @NewLoadDate = CONVERT(CHAR(8), GETDATE(), 112)
INSERT INTO [etl].[LogUpdate] ([Table], [LastLoadDate]) VALUES ('Fact_Measurements', @NewLoadDate)
GO

-------------------------------------------------------------------------------

---stage.Fact_MoisturePots table insert from source (only for data after the last load date)

---getting last load date for fact staging
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

--to insert all data into Fact_MoisturePots after staging relevant data
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

--to update last load date for moisture pots
DECLARE @NewLoadDate int
SET @NewLoadDate = CONVERT(CHAR(8), GETDATE(), 112)
INSERT INTO [etl].[LogUpdate] ([Table], [LastLoadDate]) VALUES ('Fact_MoisturePots', @NewLoadDate)
GO