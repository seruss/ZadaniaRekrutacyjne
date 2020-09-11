/*stworzenie bazy danych*/
CREATE DATABASE "dbo";

/*stworzenie tabeli Faktura*/
CREATE TABLE "dbo"."Faktura"
(
   Klient INT(11) NOT NULL,
   KwotaBrutto DECIMAL(13, 2) NOT NULL
);

/*stworzenie tabeli Faktury*/
CREATE TABLE "dbo"."Faktury" (
  id INT(11) NOT NULL AUTO_INCREMENT,
  nr_klienta INT(11) NOT NULL,
  nr_faktury INT(11) NOT NULL,
  PRIMARY KEY (id),
  UNIQUE INDEX nr_klienta (nr_klienta, nr_faktury)
);

/*ustawienie wyzwalacza automatycznie generującego dane w tabeli Faktury po dodaniu rekordu do tabeli Faktura*/
DROP TRIGGER IF EXISTS autoInsertTrigger;
DELIMITER $$
CREATE TRIGGER autoInsertTrigger AFTER INSERT ON Faktura FOR EACH ROW
BEGIN
	INSERT INTO Faktury(nr_klienta, nr_faktury)
	SELECT NEW.Klient, MAX(nr_faktury)+1 FROM Faktury WHERE nr_klienta = NEW.Klient GROUP BY nr_klienta;
END $$
DELIMITER ;