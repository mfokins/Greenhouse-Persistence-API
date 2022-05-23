-----------------------TESTING---------------------------
-------------------NUBMER OF ROWS----------------------

--testing amount of rows for Dim_Greenhouses
SELECT COUNT(*) as GreenHousesRowsSource FROM [TestGreenHouseDB].[dbo].[Greenhouses]
SELECT COUNT(*) as GreenHousesRowsWarehouse FROM [TestGreenHouseDWH].[edw].[Dim_Greenhouse]

--testing amount of rows for Dim_Pot
SELECT COUNT(*) as PotRowsSource FROM [TestGreenHouseDB].[dbo].[Pot]
SELECT COUNT(*) as PotRowsWarehouse FROM [TestGreenHouseDWH].[edw].[Dim_Pot]

--testing amount of non-duplicate rows for temperature in Fact_Measurements
SELECT COUNT(DISTINCT Time) as TemperatureRowsSource FROM [TestGreenHouseDB].[dbo].[TemperatureMeasurement] WHERE Temperature != -100
SELECT COUNT(*) as TemperatureRowsWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE Temperature != -100

--testing amount of non-duplicate rows for humidity in Fact_Measurements
SELECT COUNT(DISTINCT Time) as HumidityRowsSource FROM [TestGreenHouseDB].[dbo].[HumidityMeasurement] Where Humidity != -1
SELECT COUNT(*) as HumidityRowsWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE Humidity != -1

--testing amount of non-duplicate rows for CO2 in Fact_Measurements
SELECT COUNT(DISTINCT Time) as CarbonDioxideRowsSource FROM [TestGreenHouseDB].[dbo].[DioxideCarbonMeasurement] WHERE Co2Measurement != -1
SELECT COUNT(*) as CarbonDioxideRowsWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE CarbonDioxide != -1

--testing amount of non-duplicate rows for moisture in Fact_MoisturePots
SELECT COUNT(DISTINCT Time) as MoistureRowsSource FROM [TestGreenHouseDB].[dbo].[MoistureMeasurement] WHERE Moisture != -1
SELECT COUNT(*) as MoistureRowsWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_MoisturePots] WHERE Moisture != -1

-------------------------SUM-----------------------------

--testing sum of non-duplicate temperature measurements in Fact_Measurements
SELECT SUM(Temperature) as TemperatureSumSource  FROM (SELECT DISTINCT Temperature, Time FROM [TestGreenHouseDB].[dbo].[TemperatureMeasurement] WHERE Temperature != -100) as TemperatureSumSource 
SELECT DISTINCT SUM(Temperature) as TemperatureSumWearhouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE Temperature != -100

--testing sum of non-duplicate humidity measurements in Fact_Measurements
SELECT SUM(Humidity) as HumiditySumSource FROM (SELECT DISTINCT Humidity, Time FROM [TestGreenHouseDB].[dbo].[HumidityMeasurement] Where Humidity != -1) as HumiditySumSource
SELECT DISTINCT SUM(Humidity) as HumiditySumWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE Humidity != -1

--testing sum of non-duplicate CO2 measurements in Fact_Measurements
SELECT SUM(Co2Measurement) as CarbonDioxideSumSource FROM (SELECT DISTINCT Co2Measurement, Time FROM [TestGreenHouseDB].[dbo].[DioxideCarbonMeasurement] Where Co2Measurement != -1) as CarbonDioxideSumSource
SELECT DISTINCT SUM(CarbonDioxide) as CarbonDioxideSumWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE CarbonDioxide != -1

--testing sum of non-duplicate moisture measurements in Fact_MoisturePots
SELECT SUM(Moisture) as MoistureSumSource FROM (SELECT DISTINCT Moisture, Time FROM [TestGreenHouseDB].[dbo].[MoistureMeasurement] Where Moisture != -1) as MoistureSumSource
SELECT DISTINCT SUM(Moisture) as MoistureSumWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_MoisturePots] WHERE Moisture != -1

-----------------------AVERAGE---------------------------

--testing sum of non-duplicate temperature measurements in Fact_Measurements
SELECT AVG(Temperature) as TemperatureAvgSource FROM (SELECT DISTINCT Temperature, Time FROM [TestGreenHouseDB].[dbo].[TemperatureMeasurement] WHERE Temperature != -100) as TemperatureAvgSource 
SELECT DISTINCT AVG(Temperature) as TemperatureAvgWearhouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE Temperature != -100

--testing sum of non-duplicate humidity measurements in Fact_Measurements
SELECT AVG(Humidity) as HumidityAvgSource FROM (SELECT DISTINCT Humidity, Time FROM [TestGreenHouseDB].[dbo].[HumidityMeasurement] Where Humidity != -1) as HumidityAvgSource
SELECT DISTINCT AVG(Humidity) as HumidityAvgWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE Humidity != -1

--testing sum of non-duplicate CO2 measurements in Fact_Measurements
SELECT AVG(Co2Measurement) as CarbonDioxideAvgSource FROM (SELECT DISTINCT Co2Measurement, Time FROM [TestGreenHouseDB].[dbo].[DioxideCarbonMeasurement] Where Co2Measurement != -1) as CarbonDioxideAvgSource
SELECT DISTINCT AVG(CarbonDioxide) as CarbonDioxideAvgWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE CarbonDioxide != -1

--testing sum of non-duplicate moisture measurements in Fact_MoisturePots
SELECT AVG(Moisture) FROM (SELECT DISTINCT Moisture, Time FROM [TestGreenHouseDB].[dbo].[MoistureMeasurement] Where Moisture != -1) as MoistureAvgSource
SELECT DISTINCT AVG(Moisture) as MoistureAvgWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_MoisturePots] WHERE Moisture != -1