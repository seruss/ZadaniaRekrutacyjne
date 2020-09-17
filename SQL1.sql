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

	INSERT INTO Faktury(nr_klienta, nr_faktury)
	SELECT Klient, ROW_NUMBER() OVER (partition by Klient order by Klient) + f.nr
	FROM inserted i
	JOIN (
        SELECT DISTINCT nr_klienta, MAX(nr_faktury) as nr
        FROM Faktury
        GROUP BY nr_klienta
        ) f ON f.nr_klienta = i.Klient
END





