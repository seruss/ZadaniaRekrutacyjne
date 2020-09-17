--SQL Server syntax

DECLARE @from DATE = '20200101'
DECLARE @to DATE = '20201231'

SELECT DATEADD(DAY, step - 1, @from)
FROM (SELECT ROW_NUMBER() OVER (ORDER BY c.object_id) AS step FROM sys.columns c) steps
WHERE step - 1 <= DATEDIFF(DAY, @from, @to)