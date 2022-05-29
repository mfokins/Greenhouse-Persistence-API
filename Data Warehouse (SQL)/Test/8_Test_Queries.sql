-----------------------TESTING---------------------------
-------------------NUBMER OF ROWS----------------------

--testing amount of rows for Dim_Greenhouses
SELECT COUNT(*) as GreenHousesRowsSource FROM [TestGreenHouseDB].[dbo].[Greenhouses]
SELECT COUNT(*) as GreenHousesRowsWarehouse FROM [TestGreenHouseDWH].[edw].[Dim_Greenhouse]

--testing amount of rows for Dim_Pot
SELECT COUNT(*) as PotRowsSource FROM [TestGreenHouseDB].[dbo].[Pot]
SELECT COUNT(*) as PotRowsWarehouse FROM [TestGreenHouseDWH].[edw].[Dim_Pot]

--testing amount of non-duplicate rows for temperature in Fact_Measurements
SELECT COUNT( Time) as TemperatureRowsSource FROM [TestGreenHouseDB].[dbo].[TemperatureMeasurement] WHERE Temperature != -100
SELECT COUNT(*) as TemperatureRowsWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE Temperature != -100

--testing amount of non-duplicate rows for humidity in Fact_Measurements
SELECT COUNT( Time) as HumidityRowsSource FROM [TestGreenHouseDB].[dbo].[HumidityMeasurement] Where Humidity != -1
SELECT COUNT(*) as HumidityRowsWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE Humidity != -1

--testing amount of non-duplicate rows for CO2 in Fact_Measurements
SELECT COUNT( Time) as CarbonDioxideRowsSource FROM [TestGreenHouseDB].[dbo].[DioxideCarbonMeasurement] WHERE Co2Measurement != -1
SELECT COUNT(*) as CarbonDioxideRowsWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE CarbonDioxide != -1

--testing amount of non-duplicate rows for moisture in Fact_MoisturePots
SELECT COUNT( Time) as MoistureRowsSource FROM [TestGreenHouseDB].[dbo].[MoistureMeasurement] WHERE Moisture != -1
SELECT COUNT(*) as MoistureRowsWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_MoisturePots] WHERE Moisture != -1

-------------------------SUM-----------------------------

--testing sum of non-duplicate temperature measurements in Fact_Measurements
SELECT SUM(Temperature) as TemperatureSumSource  FROM [TestGreenHouseDB].[dbo].[TemperatureMeasurement]
SELECT DISTINCT SUM(Temperature) as TemperatureSumWearhouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE Temperature != -100

--testing sum of non-duplicate humidity measurements in Fact_Measurements
SELECT SUM(Humidity) as HumiditySumSource FROM [TestGreenHouseDB].[dbo].[HumidityMeasurement]
SELECT DISTINCT SUM(Humidity) as HumiditySumWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE Humidity != -1

--testing sum of non-duplicate CO2 measurements in Fact_Measurements
SELECT SUM(Co2Measurement) as CarbonDioxideSumSource FROM [TestGreenHouseDB].[dbo].[DioxideCarbonMeasurement]
SELECT DISTINCT SUM(CarbonDioxide) as CarbonDioxideSumWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE CarbonDioxide != -1

--testing sum of non-duplicate moisture measurements in Fact_MoisturePots
SELECT SUM(Moisture) as MoistureSumSource FROM [TestGreenHouseDB].[dbo].[MoistureMeasurement]
SELECT DISTINCT SUM(Moisture) as MoistureSumWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_MoisturePots] WHERE Moisture != -1

-----------------------AVERAGE---------------------------

--testing sum of non-duplicate temperature measurements in Fact_Measurements
SELECT AVG(Temperature) as TemperatureAvgSource FROM [TestGreenHouseDB].[dbo].[TemperatureMeasurement]
SELECT DISTINCT AVG(Temperature) as TemperatureAvgWearhouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE Temperature != -100

--testing sum of non-duplicate humidity measurements in Fact_Measurements
SELECT AVG(Humidity) as HumidityAvgSource FROM [TestGreenHouseDB].[dbo].[HumidityMeasurement]
SELECT DISTINCT AVG(Humidity) as HumidityAvgWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE Humidity != -1

--testing sum of non-duplicate CO2 measurements in Fact_Measurements
SELECT AVG(Co2Measurement) as CarbonDioxideAvgSource FROM [TestGreenHouseDB].[dbo].[DioxideCarbonMeasurement]
SELECT DISTINCT AVG(CarbonDioxide) as CarbonDioxideAvgWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_Measurements] WHERE CarbonDioxide != -1

--testing sum of non-duplicate moisture measurements in Fact_MoisturePots
SELECT AVG(Moisture) FROM [TestGreenHouseDB].[dbo].[MoistureMeasurement]
SELECT DISTINCT AVG(Moisture) as MoistureAvgWarehouse FROM [TestGreenHouseDWH].[edw].[Fact_MoisturePots] WHERE Moisture != -1