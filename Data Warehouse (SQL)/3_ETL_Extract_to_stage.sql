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

--Populating Fact Measurements 
use [GreenHouseDWH]
TRUNCATE TABLE [stage].[Fact_Measurements]
INSERT INTO [stage].[Fact_Measurements](
    [GreenHouse_ID],
    [Temperature],
    [Humidity],
    [CarbonDioxide],
[MeasurementDateTime]
)
SELECT 
    g.GreenHouseId,
    t.Temperature,
    h.[Humidity],
    c.[Co2Measurement],
    t.Time
FROM [GreenhouseDB].[dbo].Greenhouses g
     JOIN [GreenhouseDB].[dbo].[TemperatureMeasurement] t on (g.GreenHouseId=t.GreenHouseId)
     JOIN [GreenhouseDB].[dbo].[HumidityMeasurement] h on (g.GreenHouseId=h.GreenHouseId and t.Time = h.Time)
     JOIN [GreenhouseDB].[dbo].[DioxideCarbonMeasurement] c on (g.GreenHouseId=c.GreenHouseId and t.Time = c.Time)


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
