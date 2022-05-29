--SUB-STEP 1: Creating stage entities
DROP
DATABASE IF EXISTS [GreenHouseDWH]
go
-- CREATE GreenHouseDWH DATABASE
DATABASE [GreenHouseDWH]
go
-- USE the GreenHouseDWH DATABASE
use [GreenHouseDWH];
go

-- CREATE STAGE SCHEMA for GreenHouseDWH --
create SCHEMA [stage];
go
--!!!!! MAKE SURE YOUR USER HAS RIGHTS TO MAKE CHANGES TO THE CREATED SCHEMA !!!!!!--


--CREATE the Greenhouse Dimension
DROP TABLE IF EXISTS stage.Dim_Greenhouse
CREATE TABLE stage.Dim_Greenhouse
(
    GreenHouse_ID NVARCHAR(100) PRIMARY KEY NOT NULL,
    Latitude DECIMAL(8,6),
    Longitude DECIMAL(9,6)
);



--CREATE the Pot Dimension
DROP TABLE IF EXISTS stage.Dim_Pot
CREATE TABLE stage.Dim_Pot
(
    Pot_ID INT PRIMARY KEY NOT NULL,
    Plant  NVARCHAR(100)
);

--CREATE the Fact Measurement entity
DROP TABLE IF EXISTS stage.Fact_Measurements
CREATE TABLE stage.Fact_Measurements
(
    GreenHouse_ID       NVARCHAR(100) NOT NULL,
    Temperature         REAL,
    Humidity            FLOAT,
    CarbonDioxide       INT,
    MeasurementDateTime DateTime
);

--CREATE the Fact Moisture entity
DROP TABLE IF EXISTS stage.Fact_MoisturePots
CREATE TABLE stage.Fact_MoisturePots
(
    Pot_ID              INT NOT NULL,
    GreenHouse_ID       NVARCHAR(100) NOT NULL,
    Moisture            FLOAT,
    MeasurementDateTime DateTime
);
-- END OF SUB-STEP 1: Creating stage entities

--SUB-STEP 2: Populating stage entities

-- USE the GreenHouseDWH DATABASE
use
[GreenHouseDWH]


--POPULATE Greenhouse dimension
TRUNCATE TABLE [stage].[Dim_Greenhouse]
INSERT INTO [stage].[Dim_Greenhouse](
     [GreenHouse_ID]
    [Country] ,
     [City]
)
SELECT
    [GreenHouseId]
    [Country],
    [City]
FROM [GreenhouseDB].[dbo].Greenhouses


--POPULATE Fact Measurements 
    use [GreenHouseDWH]
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


--POPULATE Pot dimension for Fact_MoisturePots
    TRUNCATE TABLE [stage].[Dim_Pot]
INSERT
INTO [stage].[Dim_Pot]([Pot_ID],
[Plant])
SELECT
    [Id],
    [Name]
FROM [GreenhouseDB].[dbo].Pot


--POPULATE Fact Moisture Pots
    TRUNCATE TABLE [stage].[Fact_MoisturePots]
INSERT
INTO [stage].[Fact_MoisturePots]([Pot_ID],
    [GreenHouse_ID],
    [Moisture],
[MeasurementDateTime])
SELECT pot.[Id],
       pot.[GreenHouseId],
       mst.[Moisture],
       mst.Time
FROM [GreenhouseDB].[dbo].[Pot] pot
    inner join [GreenhouseDB].[dbo].MoistureMeasurement mst
on mst.potId=pot.Id
--END OF SUB-STEP 2: Populating stage entities