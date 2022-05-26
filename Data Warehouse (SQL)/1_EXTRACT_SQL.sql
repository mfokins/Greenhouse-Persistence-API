DROP DATABASE IF EXISTS [GreenHouseDWH]
go

CREATE DATABASE [GreenHouseDWH]
go

use [GreenHouseDWH];
go

create SCHEMA [stage];
go


DROP TABLE IF EXISTS stage.Dim_Greenhouse
CREATE TABLE stage.Dim_Greenhouse (
 GreenHouse_ID NVARCHAR(100) PRIMARY KEY NOT NULL,
 Country NVARCHAR(50),
 City NVARCHAR(50)
);



DROP TABLE IF EXISTS stage.Dim_Pot
CREATE TABLE stage.Dim_Pot (
 Pot_ID INT PRIMARY KEY NOT NULL,
 Plant NVARCHAR(100)
);


DROP TABLE IF EXISTS stage.Fact_Measurements
CREATE TABLE stage.Fact_Measurements (
 GreenHouse_ID NVARCHAR(100) NOT NULL,
 Temperature REAL,
 Humidity FLOAT,
 CarbonDioxide INT,
  MeasurementDateTime DateTime
);

DROP TABLE IF EXISTS stage.Fact_MoisturePots
CREATE TABLE stage.Fact_MoisturePots (
 Pot_ID INT NOT NULL,
 GreenHouse_ID NVARCHAR(100) NOT NULL,
 Moisture FLOAT,
  MeasurementDateTime DateTime
);


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
