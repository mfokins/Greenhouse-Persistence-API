USE [TestGreenHouseDWH]
GO

UPDATE [stage].[Dim_Greenhouse]
SET Latitude = -1 WHERE Latitude IS NULL

UPDATE [stage].[Dim_Greenhouse]
SET Longitude = -1 WHERE Longitude IS NULL

UPDATE [stage].[Dim_Pot]
SET Plant = 'UNKNOWN' WHERE Plant IS NULL

UPDATE [stage].[Fact_Measurements]
SET Humidity = -1 WHERE Humidity IS NULL

UPDATE [stage].[Fact_Measurements]
SET Temperature = -100 WHERE Temperature IS NULL

UPDATE [stage].[Fact_Measurements]
SET CarbonDioxide = -1 WHERE CarbonDioxide IS NULL

UPDATE [stage].[Fact_MoisturePots]
SET Moisture = -1 WHERE Moisture IS NULL
