RESTORE DATABASE PriceCompare
FROM DISK = N'E:\rdinvent.bak'
WITH FILE = 1,  
MOVE N'rdinvent' TO N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA)PriceCompare.MDF',  
MOVE N'rdinvent_LOG' TO N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA)PriceCompare_LOG.LDF',  
NOUNLOAD,  
REPLACE,  
STATS = 10
GO