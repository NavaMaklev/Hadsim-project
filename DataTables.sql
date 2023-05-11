/*Data Tables*/
CREATE TABLE client (
  Id VARCHAR(9)  PRIMARY KEY NOT NULL,
  FirstName VARCHAR(50) NOT NULL,
  LastName VARCHAR(50) NOT NULL,
  City VARCHAR(50) NOT NULL,
  Street VARCHAR(50) NOT NULL,
  HouseNumber INT NOT NULL,
  DateOfBirth DATE NOT NULL,
  Telephone VARCHAR(13) NOT NULL,
  MobilePhone VARCHAR(13),
  ClientImage NVARCHAR(MAX)
);
CREATE TABLE disease (
  Code INT IDENTITY(1,1) PRIMARY KEY,
  ClientId VARCHAR(9) NOT NULL,
  StartDate DATE NOT NULL,
  EndDate DATE NOT NULL,
  FOREIGN KEY(clientId) REFERENCES client(Id)
);
CREATE TABLE vaccination (
  Code INT IDENTITY(1,1) PRIMARY KEY,
  ClientId VARCHAR(9) NOT NULL,
  Manufacturer VARCHAR(50) NOT NULL,
  VaccinationDate DATE NOT NULL,
  FOREIGN KEY(ClientId) REFERENCES client(Id)
);