CREATE DATABASE ProductsDatabase;
--DROP DATABASE ProductsDatabase;

USE ProductsDatabase;

CREATE TABLE Products
(
    Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    Name VARCHAR (50),
    Price INT,
    NumberOfPieces INT
)

SELECT * FROM Products;