-- USE the GreenHouseDWH DATABASE
USE [GreenHouseDWH]
GO
--UPDATE the Greenhouse Dimension to replace the NULL attributes values to UNKNOWN
UPDATE [stage].[Dim_Greenhouse]
SET City = 'UNKNOWN' WHERE City IS NULL

--UPDATE the Greenhouse Dimension  to replace the NULL attributes values to UNKNOWN
UPDATE [stage].[Dim_Greenhouse]
SET Country = 'UNKNOWN' WHERE Country IS NULL

--UPDATE the Greenhouse Dimension to replace the NULL attributes values to UNKNOWN
UPDATE [stage].[Dim_Pot]
SET Plant = 'UNKNOWN' WHERE Plant IS NULL

--UPDATE the Greenhouse Dimension to replace the NULL attributes values to -1
UPDATE [stage].[Fact_Measurements]
SET Humidity = -1 WHERE Humidity IS NULL

--UPDATE the Greenhouse Dimension to replace the NULL attributes values to -1
UPDATE [stage].[Fact_Measurements]
SET Temperature = -100 WHERE Temperature IS NULL

--UPDATE the Greenhouse Dimension to replace the NULL attributes values to -1
UPDATE [stage].[Fact_Measurements]
SET CarbonDioxide = -1 WHERE CarbonDioxide IS NULL

--UPDATE the Greenhouse Dimension to replace the NULL attributes values to -1
UPDATE [stage].[Fact_MoisturePots]
SET Moisture = -1 WHERE Moisture IS NULL