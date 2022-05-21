USE [GreenHouseDWH]
GO

UPDATE [stage].[Dim_Greenhouse]
SET City = 'UNKNOWN' WHERE City IS NULL

UPDATE [stage].[Dim_Greenhouse]
SET Country = 'UNKNOWN' WHERE Country IS NULL

UPDATE [stage].[Dim_Pot]
SET Plant = 'UNKNOWN' WHERE Plant IS NULL

UPDATE [stage].[Fact_Measurements]
SET Humidity = 0 WHERE Humidity IS NULL

UPDATE [stage].[Fact_Measurements]
SET Temperature = 0 WHERE Temperature IS NULL

UPDATE [stage].[Fact_Measurements]
SET CarbonDioxide = 0 WHERE CarbonDioxide IS NULL

UPDATE [stage].[Fact_MoisturePots]
SET Moisture = 0 WHERE Moisture IS NULL
