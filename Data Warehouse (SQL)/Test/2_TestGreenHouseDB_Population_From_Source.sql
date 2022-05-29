USE [TestGreenHouseDB]
GO




  INSERT INTO [dbo].[Greenhouses] (
  [GreenHouseId],
  [Latitude],
  [Longitude]
  )
SELECT
    [GreenHouseId],
    [Latitude],
    [Longitude]
FROM [GreenHouseDB].[dbo].[Greenhouses]
    -- WHERE [GreenHouseId]%10 = 7 


	set identity_insert [dbo].[Pot] on
	INSERT INTO [dbo].[Pot]
	([Id]
      ,[GreenHouseId]
      ,[Name]
      ,[MoistureThresholdId])
	SELECT [Id]
      ,[GreenHouseId]
      ,[Name]
      ,[MoistureThresholdId]
  FROM [GreenHouseDB].[dbo].[Pot]
   --WHERE [GreenHouseId]%10 = 7
	set identity_insert [dbo].[Pot] off

	set identity_insert [dbo].[DioxideCarbonMeasurement] on
INSERT INTO [dbo].[DioxideCarbonMeasurement]
	([Id]
      ,[Co2Measurement]
      ,[Time]
      ,[GreenHouseId])
SELECT [Id]
      ,[Co2Measurement]
      ,[Time]
      ,[GreenHouseId]
  FROM [GreenHouseDB].[dbo].[DioxideCarbonMeasurement]
 --WHERE [GreenHouseId]%10 = 7
  set identity_insert [dbo].[DioxideCarbonMeasurement] off

	set identity_insert [dbo].[TemperatureMeasurement] on
	INSERT INTO [dbo].[TemperatureMeasurement]
	([Id]
      ,[Temperature]
      ,[Time]
      ,[GreenHouseId])
	SELECT [Id]
      ,[Temperature]
      ,[Time]
      ,[GreenHouseId]
  FROM [GreenHouseDB].[dbo].[TemperatureMeasurement]
  --WHERE [GreenHouseId]%10 = 7
	set identity_insert [dbo].[TemperatureMeasurement] off

	set identity_insert [dbo].[Threshold] on
	INSERT INTO [dbo].[Threshold]
	( [Id]
      ,[Type]
      ,[LowerThreshold]
      ,[HigherThreshold]
      ,[GreenHouseId])
	SELECT [Id]
      ,[Type]
      ,[LowerThreshold]
      ,[HigherThreshold]
      ,[GreenHouseId]
  FROM [GreenHouseDB].[dbo].[Threshold]
  --WHERE [GreenHouseId]%10 = 7
	set identity_insert [dbo].[Threshold] off


	set identity_insert [dbo].[HumidityMeasurement] on
  INSERT INTO [dbo].[HumidityMeasurement]
		([Id]
      ,[Humidity]
      ,[Time]
      ,[GreenHouseId])
  SELECT [Id]
      ,[Humidity]
      ,[Time]
      ,[GreenHouseId]
  FROM [GreenHouseDB].[dbo].[HumidityMeasurement]
   --WHERE [GreenHouseId]%10 = 7
  set identity_insert [dbo].[HumidityMeasurement] off


	set identity_insert [dbo].[MoistureMeasurement] on
	INSERT INTO [dbo].[MoistureMeasurement]
	( [Id]
      ,[Moisture]
      ,[Time]
      ,[PotId])
	SELECT [Id]
      ,[Moisture]
      ,[Time]
      ,[PotId]
  FROM [GreenHouseDB].[dbo].[MoistureMeasurement] m
  /*inner join [GreenHouseDB].[dbo].[Pot] p
  on m.PotId = p.Id
  WHERE p.GreenHouseId%10 = 7*/
	set identity_insert [dbo].[MoistureMeasurement] off