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

/*ALTER TABLE stage.Dim_Greenhouse ADD CONSTRAINT PK_Dim_Greenhouse PRIMARY KEY (GreenHouse_ID);*/


DROP TABLE IF EXISTS stage.Dim_Pot
CREATE TABLE stage.Dim_Pot (
 Pot_ID INT PRIMARY KEY NOT NULL,
 Plant NVARCHAR(100)
);

/*ALTER TABLE stage.Dim_Pot ADD CONSTRAINT PK_Dim_Pot PRIMARY KEY (Pot_ID);*/

DROP TABLE IF EXISTS stage.Fact_Measurements
CREATE TABLE stage.Fact_Measurements (
 GreenHouse_ID NVARCHAR(100) NOT NULL,
 Temperature REAL,
 Humidity FLOAT,
 CarbonDioxide INT,
  MeasurementDateTime DateTime
);

/*ALTER TABLE stage.Fact_Measurements ADD CONSTRAINT PK_Fact_Measurements PRIMARY KEY (Measurements_ID,GreenHouse_ID);
ALTER TABLE stage.Fact_Measurements ADD CONSTRAINT FK_Fact_Measurements_0 FOREIGN KEY (GreenHouse_ID) REFERENCES stage.Dim_Greenhouse (GreenHouse_ID);
*/

DROP TABLE IF EXISTS stage.Fact_MoisturePots
CREATE TABLE stage.Fact_MoisturePots (
 Pot_ID INT NOT NULL,
 GreenHouse_ID NVARCHAR(100) NOT NULL,
 Moisture FLOAT,
  MeasurementDateTime DateTime
);



/*ALTER TABLE stage.Fact_MoisturePots ADD CONSTRAINT PK_Fact_MoisturePots PRIMARY KEY (MoisturePot_ID,Pot_ID,GreenHouse_ID);
ALTER TABLE stage.Fact_MoisturePots ADD CONSTRAINT FK_Fact_MoisturePot_0 FOREIGN KEY (Pot_ID) REFERENCES stage.Dim_Pot (Pot_ID);
ALTER TABLE stage.Fact_MoisturePots ADD CONSTRAINT FK_Fact_MoisturePot_1 FOREIGN KEY (GreenHouse_ID) REFERENCES stage.Dim_Greenhouse (GreenHouse_ID);
*/