use [GreenHouseDWH]

--Populating Greenhouse dimension

TRUNCATE TABLE [stage].[Dim_Greenhouse]
INSERT INTO [stage].[Dim_Greenhouse](
[GreenHouse_ID]
)
SELECT
    [GreenHouseId]
FROM Greenhouse.[dbo].Greenhouses

    /*  
        [Country] ,
        [City] 
       to be added to the stage.[Dim_Greenhouse]
    
    */
    --Populating Fact Measurements
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
WHERE tmp.Time=hmd.Time AND tmp.Time=dioc.Time AND hmd.Time=dioc.Time




--Populating Greenhouse dimension for Fact_MoisturePots
--Might be good to create a new schema
    TRUNCATE TABLE [stage].[Dim_Greenhouse]
INSERT INTO [stage].[Dim_Greenhouse](
[GreenHouse_ID]
)
SELECT
    [GreenHouseId]
FROM Greenhouse.[dbo].Greenhouses

    --Populating Pot dimension for Fact_MoisturePots
    TRUNCATE TABLE [stage].[Dim_Pot]
INSERT INTO [stage].[Dim_Pot](
    [Pot_ID],
[Plant]
)
SELECT
    [Id],
    [Name]
FROM Greenhouse.[dbo].Pot


    --Populating Fact Moisture Pots
    TRUNCATE TABLE [stage].[Fact_MoisturePots]
INSERT INTO [stage].[Fact_MoisturePots](
    [Moisture_ID],
    [Pot_ID],
    [GreenHouse_ID],
    [Moisture],
[MeasurementDateTime]
)
SELECT
    mst.[Id],
    pot.[Id],
    pot.[GreenHouseId],
    mst.[Moisture],
    mst.Time
FROM [Greenhouse].[dbo].[Pot] pot
    inner join [Greenhouse].[dbo].MoistureMeasurement mst
on mst.potId=pot.Id
