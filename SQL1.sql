--SQL Server syntax

/*stworzenie bazy danych*/
CREATE DATABASE dbo;

/*stworzenie tabeli Faktury*/
CREATE TABLE dbo.Faktura (
    Klient INT NOT NULL,
    KwotaBrutto MONEY NOT NULL
);

/*stworzenie tabeli Faktury*/
CREATE TABLE dbo.Faktury (
  id INT IDENTITY(1,1) PRIMARY KEY,
  nr_klienta INT NOT NULL,
  nr_faktury INT NOT NULL
);

/*ustawienie wyzwalacza automatycznie generującego dane w tabeli Faktury po dodaniu rekordu do tabeli Faktura*/
CREATE TRIGGER [dbo].[autoinsert] 
ON [dbo].[Faktura] INSTEAD OF INSERT
AS
BEGIN

	INSERT Faktura
	SELECT Klient, KwotaBrutto
	FROM inserted;

DECLARE @temp_klient as INT;
DECLARE @ffcursor as CURSOR;

SET @ffcursor = CURSOR FAST_FORWARD FOR
SELECT Klient FROM inserted
 
OPEN @ffcursor;
FETCH NEXT FROM @ffcursor INTO @temp_klient;
 WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO Faktury(nr_klienta, nr_faktury)
	VALUES (@temp_klient, (SELECT CASE WHEN COUNT(1) > 0 THEN COUNT(1)+1 ELSE 1 END FROM Faktury WHERE nr_klienta = @temp_klient));
    FETCH NEXT FROM @ffcursor INTO @temp_klient;
END
CLOSE @ffcursor;
DEALLOCATE @ffcursor;
END






