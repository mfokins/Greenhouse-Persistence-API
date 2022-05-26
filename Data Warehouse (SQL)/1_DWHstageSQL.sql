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
