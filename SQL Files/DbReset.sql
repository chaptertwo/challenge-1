USE ScoutSFTChallenge
GO

	Create Procedure DbReset As
	Begin
		Delete From OrderLine;
		Delete From [Order];
		Delete From Inventory;
		Delete From BinProduct;
		Delete From Product;
		Delete From Bin;

	SET IDENTITY_INSERT Bin ON
INSERT INTO Bin(BinId, BinName)
VALUES (1, 'S34A1'), (2, 'S43B2'), (3, 'S54C3')
SET IDENTITY_INSERT Bin OFF

SET IDENTITY_INSERT Product ON
INSERT INTO Product(ProductId, SKU, ProductDescription, Price)
VALUES (1, '5985640', 'iPad Pro 12.9-inch', 1149.99), (2, '6237354', 'HP ENVY x360 2-in-1 15.6" Touch-Screen Laptop - Intel Core i5', 849.99),
(3, '9091221', 'iPad Pro 12.9-inch 2nd Generation', 799.99)
SET IDENTITY_INSERT Product OFF


SET IDENTITY_INSERT Inventory ON
INSERT INTO Inventory(InventoryId, BinId, ProductId, InventoryQuantity)
VALUES(1, 1, 1, 33), (2, 1, 2, 12), (3, 2, 1, 10), (4, 2, 2, 9), (5, 3, 2, 10)
SET IDENTITY_INSERT Inventory OFF

SET IDENTITY_INSERT [Order] ON
INSERT INTO [Order](OrderId, DateOrdered, CustomerName, CustomerAddress, OrderNumber, Total)
VALUES(1, '2011-12-30', 'Holidazzle', 'Paseo Conde de Sepúlveda, 31, 40006 Segovia, Spain', 32014, 4849.95), 
(2, '2012-03-01', 'Jersey''s Shore', '26 Journal Square Plaza #200, Jersey City, NJ 07306', 330021, 849.99),
(3, '2015-09-15', 'Mamma Mios', 'Pedregal 24, Lomas - Virreyes, Molino del Rey, 11040 Ciudad de México, CDMX, Mexico', 330051, 3999.95)
SET IDENTITY_INSERT [Order] OFF

SET IDENTITY_INSERT OrderLine ON
INSERT INTO OrderLine(OrderLineId, OrderId, ProductId, OrderQuantity, PricePerLine)
VALUES (1, 1, 1, 2, 2299.98), (2, 1, 2, 3, 2549.97), (3, 2, 2, 1, 849.99), (4, 3, 3, 5, 3999.95)
SET IDENTITY_INSERT OrderLine OFF

END

