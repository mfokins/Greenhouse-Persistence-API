use [TestGreenHouseDWH]

--Populating Greenhouse dimension

TRUNCATE TABLE [stage].[Dim_Greenhouse]
INSERT INTO [stage].[Dim_Greenhouse](
[GreenHouse_ID]
)
SELECT
    [GreenHouseId]
FROM [TestGreenHouseDB].[dbo].Greenhouses

    /*  
        [Country] ,
        [City] 
       to be added to the stage.[Dim_Greenhouse]
    
    */


--Populating Fact Measurements 
    use [TestGreenHouseDWH]
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
         FROM [TestGreenHouseDB].[dbo].[TemperatureMeasurement]
         UNION
         SELECT GreenHouseId,Humidity,Time
         FROM [TestGreenHouseDB].[dbo].[HumidityMeasurement]
         UNION
         SELECT GreenHouseId,Co2Measurement,Time
         FROM [TestGreenHouseDB].[dbo].[DioxideCarbonMeasurement]
     ) src
         LEFT JOIN [TestGreenHouseDB].[dbo].[TemperatureMeasurement] t on (src.Time=t.Time and src.GreenHouseId=t.GreenHouseId)
    LEFT JOIN [TestGreenHouseDB].[dbo].[HumidityMeasurement] h on (src.Time=h.Time and src.GreenHouseId=h.GreenHouseId)
    LEFT JOIN [TestGreenHouseDB].[dbo].[DioxideCarbonMeasurement] c on (src.Time=c.Time and src.GreenHouseId=c.GreenHouseId)


    --Populating Pot dimension for Fact_MoisturePots
    TRUNCATE TABLE [stage].[Dim_Pot]
INSERT INTO [stage].[Dim_Pot](
    [Pot_ID],
[Plant]
)
SELECT
    [Id],
    [Name]
FROM [TestGreenHouseDB].[dbo].Pot


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
FROM [TestGreenHouseDB].[dbo].[Pot] pot
    inner join [TestGreenHouseDB].[dbo].MoistureMeasurement mst
on mst.potId=pot.Id