use projet;
GRANT SELECT, INSERT, UPDATE, DELETE ON CommandsSupplier TO 'StockManager'@'%';
GRANT SELECT, INSERT, UPDATE, DELETE ON CommandsSupplier TO 'StockKeeper'@'%';
GRANT SELECT ON CommandsSupplier TO 'Secretary'@'%';
GRANT SELECT, INSERT, UPDATE, DELETE ON Components TO 'Secretary'@'%';
FLUSH PRIVILEGES;

ALTER TABLE Components ADD MinStock INT(6);
UPDATE Components SET MinStock = ROUND(RAND() * (100 - 5) + 5);
ALTER TABLE Components ALTER MinStock SET DEFAULT 50;