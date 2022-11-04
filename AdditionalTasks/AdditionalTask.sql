use devtest;
GO;

---------Set 1---------
--------Task 1---------
SELECT p.Name AS ProductName,
       p.DefaultQuantity AS DefaultQuantity,
       fp.Quantity AS Quantity,
       fm.Name AS ModelName,
       f.OwnerName AS OwnerName
FROM FridgeProducts fp
JOIN Fridges f on fp.FridgeId = f.Id
JOIN FridgeModels fm on fm.Id = f.FridgeModelId
JOIN Products p on fp.ProductId = p.Id
WHERE fm.Name LIKE 'A%'

--------Task 2---------
SELECT f.Name AS FridgeName,
       f.OwnerName as OwnerName,
       fm.Name AS ModelName,
       p.DefaultQuantity AS DefaultQuantity,
       fp.Quantity AS Quantity
FROM Fridges f
JOIN FridgeModels fm on fm.Id = f.FridgeModelId
JOIN FridgeProducts fp on f.Id = fp.FridgeId
JOIN Products p on p.Id = fp.ProductId
WHERE fp.Quantity < p.DefaultQuantity

--------Task 3---------
SELECT TOP 1 MAX(fm.Year)
FROM Fridges f
JOIN FridgeModels fm on fm.Id = f.FridgeModelId
JOIN FridgeProducts fp on f.Id = fp.FridgeId
GROUP BY (FridgeId)
ORDER BY (SUM(fp.Quantity)) DESC

--------Task 4---------
SELECT fridge.Name, fridge.OwnerName, prod.Name AS ProductName
FROM Fridges fridge
JOIN FridgeProducts fridprod on fridge.Id = fridprod.FridgeId
JOIN Products prod on prod.Id = fridprod.ProductId
WHERE fridge.Id =
(
    SELECT TOP 1 f.Id
    FROM Fridges f
    JOIN FridgeProducts fp on f.Id = fp.FridgeId
    GROUP BY f.Id
    ORDER BY COUNT(fp.ProductId) DESC
)

---------Set 2---------
--------Task 1---------
SELECT fridge.Name as FridgeName,
       fridge.OwnerName as OwnerName,
       prod.Name as ProductName,
       fridprod.Quantity as Quantity
FROM Fridges fridge
JOIN FridgeProducts fridprod on fridge.Id = fridprod.FridgeId
JOIN Products prod on prod.Id = fridprod.ProductId
WHERE fridge.Id = '1FD5EA01-C9E9-4215-B844-FD66E80D3E79'

--------Task 2---------
SELECT fridge.Name as FridgeName,
       fridge.OwnerName as OwnerName,
       prod.Name as ProductName,
       fridprod.Quantity as Quantity
FROM Fridges fridge
JOIN FridgeProducts fridprod on fridge.Id = fridprod.FridgeId
JOIN Products prod on prod.Id = fridprod.ProductId
ORDER BY fridge.Id

--------Task 3---------
SELECT f.Name, f.OwnerName, SUM(fp.Quantity) AS ProductSum
FROM Fridges f
JOIN FridgeProducts fp on f.Id = fp.FridgeId
GROUP BY f.Name, f.OwnerName

--------Task 4---------
SELECT f.Name as FridgeName,
       f.OwnerName as OwnerName,
       fm.Name as ModelName,
       fm.Year as ModelYear,
       Count(fp.FridgeId) AS ProductCount
FROM Fridges f
JOIN FridgeProducts fp on f.Id = fp.FridgeId
JOIN FridgeModels fm on fm.Id = f.FridgeModelId
GROUP BY f.Name, f.OwnerName, fm.Name, fm.Year

--------Task 5---------
SELECT DISTINCT f.Id, f.Name, f.OwnerName
FROM Fridges f
JOIN FridgeProducts fp on f.Id = fp.FridgeId
JOIN Products p on p.Id = fp.ProductId
WHERE fp.Quantity > p.DefaultQuantity

--------Task 6---------
SELECT f.Name, f.OwnerName,
       COUNT(fp.Id) as [Count Of Products Where Quantity more Then Default]
FROM Fridges f
JOIN FridgeProducts fp on f.Id = fp.FridgeId
JOIN Products p on p.Id = fp.ProductId
WHERE fp.Quantity > p.DefaultQuantity
GROUP BY f.Name, f.OwnerName
