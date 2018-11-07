USE Master 
GO

CREATE DATABASE ScoutSFTChallenge
GO

USE ScoutSFTChallenge
GO

--It may be helpful to have a price per "bin" as well. At our business we were constantly trying to rid of outdated / not frequenty selling product,
--I was told that larger corporations like walmart know the price per area on a shelf.
CREATE TABLE Bin(
	BinId INT IDENTITY (1,1) PRIMARY KEY,
	BinName NVARCHAR(60) NOT NULL
)

ALTER TABLE Bin ADD CONSTRAINT U_BinName UNIQUE(BinName)

CREATE TABLE Product(
	ProductId INT IDENTITY (1,1) PRIMARY KEY,
	SKU NVARCHAR(60) NOT NULL,
	ProductDescription NVARCHAR(160) NOT NULL,
	Price DECIMAL(10,2) NOT NULL
	)

ALTER TABLE Product ADD CONSTRAINT U_SKU UNIQUE(SKU)

CREATE TABLE BinProduct(
	BinId INT NOT NULL,
	ProductId INT NOT NULL,
	PRIMARY KEY (BinId, ProductId),
	CONSTRAINT FK_Bin_ID FOREIGN KEY (BinId) REFERENCES Bin(BinId),
	CONSTRAINT FK_Product_ID FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
	)

CREATE TABLE Inventory(
	InventoryId INT IDENTITY (1,1) PRIMARY KEY,
	BinId INT FOREIGN KEY REFERENCES Bin(BinId) NOT NULL,
	ProductId INT FOREIGN KEY REFERENCES Product(ProductId) NOT NULL,
	InventoryQuantity INT NOT NULL
	)
	--WOULD PROBABLY WANT TO ADDRESS IF THIS IS QTY PER BIN OR OVERALL (PROBABLY PER BIN)


CREATE TABLE [Order](
	OrderId INT IDENTITY (1,1) PRIMARY KEY,
	DateOrdered DATETIME2 NOT NULL,
	CustomerName NVARCHAR(30) NOT NULL,
	CustomerAddress NVARCHAR(160) NOT NULL,
	OrderNumber NVARCHAR(60) NOT NULL,
	Total DECIMAL(10,2) NOT NULL
	)

ALTER TABLE [Order] ADD CONSTRAINT U_OrderNumber UNIQUE(OrderNumber)

CREATE TABLE OrderLine(
	OrderLineId INT IDENTITY (1,1) PRIMARY KEY,
	OrderId INT FOREIGN KEY REFERENCES [Order](OrderId) NOT NULL,
	ProductId INT FOREIGN KEY REFERENCES Product(ProductId) NOT NULL,
	OrderQuantity INT NOT NULL,
	PricePerLine DECIMAL(10,2) NOT NULL
	)

