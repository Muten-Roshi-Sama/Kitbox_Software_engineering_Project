use projet;

ALTER TABLE Commands MODIFY NameFile LONGTEXT;

insert into Account(Name,Password,Fonction) values
("Manager", "Manager", "Stock Manager"),
("Secretary","Secretary","Secretary"),
("StockKeeper","StockKeeper", "Stock Keeper"),
("Seller","Seller","Seller");