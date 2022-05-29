
use [TestGreenHouseDWH]
go

--TRUNCATE TABLE [edw].[Dim_Pot]
INSERT INTO [edw].[Dim_Pot]
	([Pot_ID]
	,[Plant])
SELECT
    [Pot_ID],
    [Plant]
FROM [stage].[Dim_Pot]

--TRUNCATE TABLE [edw].[Dim_Greenhouse]
INSERT INTO [edw].[Dim_Greenhouse]
([GreenHouse_ID],
    [Latitude],
[Longitude])
SELECT
    [GreenHouse_ID],
    [Latitude],
    [Longitude]
FROM [stage].[Dim_Greenhouse]


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
FROM [TestGreenHouseDWH].[stage].[Fact_MoisturePots] m
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