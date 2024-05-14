use projet;

ALTER TABLE Commands MODIFY NameFile LONGTEXT;

insert into Account(Name,Password,Fonction) values
("Manager", "Manager", "Stock Manager"),
("Secretary","Secretary","Secretary"),
("StockKeeper","StockKeeper", "Stock Keeper"),
("Seller","Seller","Seller");

ALTER TABLE Commands ADD Payed BOOLEAN DEFAULT 0;
GRANT UPDATE ON Components TO 'interface'@'%';
FLUSH PRIVILEGES;