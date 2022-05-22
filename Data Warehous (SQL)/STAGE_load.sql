use [GreenHouseDWH]

--Populating Greenhouse dimension

TRUNCATE TABLE [stage].[Dim_Greenhouse]
INSERT INTO [stage].[Dim_Greenhouse](
[GreenHouse_ID]
)
SELECT
    [GreenHouseId]
FROM [GreenhouseDB].[dbo].Greenhouses

    /*  
        [Country] ,
        [City] 
       to be added to the stage.[Dim_Greenhouse]
    
    */


 /*     --Populating Fact Measurements (Outdated version)
    TRUNCATE TABLE [stage].[Fact_Measurements]
INSERT INTO [stage].[Fact_Measurements](
    [GreenHouse_ID],
    [Temperature],
    [Humidity],
    [CarbonDioxide],
[MeasurementDateTime]
)
SELECT
    grn.[GreenHouseId],
    tmp.[Temperature],
    hmd.[Humidity],
    dioc.[Co2Measurement],
    tmp.Time
FROM [Greenhouse].[dbo].[Greenhouses] grn
    left join [Greenhouse].[dbo].TemperatureMeasurement tmp
on grn.GreenhouseId=tmp.GreenhouseId
    left join [Greenhouse].[dbo].HumidityMeasurement hmd
    on grn.GreenhouseId=hmd.GreenhouseId
    left join [Greenhouse].[dbo].DioxideCarbonMeasurement dioc
    on grn.GreenhouseId=dioc.GreenhouseId
WHERE tmp.Time=hmd.Time AND tmp.Time=dioc.Time AND hmd.Time=dioc.Time  */

--version 2 of the fact measurement load
    use [GreenHouseDWH]
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


    --Populating Pot dimension for Fact_MoisturePots
    TRUNCATE TABLE [stage].[Dim_Pot]
INSERT INTO [stage].[Dim_Pot](
    [Pot_ID],
[Plant]
)
SELECT
    [Id],
    [Name]
FROM [GreenhouseDB].[dbo].Pot


    --Populating Fact Moisture Pots
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
