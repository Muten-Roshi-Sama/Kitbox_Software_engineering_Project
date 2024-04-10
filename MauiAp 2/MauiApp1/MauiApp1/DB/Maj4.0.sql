use projet;
GRANT SELECT, INSERT, UPDATE, DELETE ON CommandsSupplier TO 'StockManager'@'%';
GRANT SELECT, INSERT, UPDATE, DELETE ON CommandsSupplier TO 'StockKeeper'@'%';
GRANT SELECT ON CommandsSupplier TO 'Secretary'@'%';
FLUSH PRIVILEGES;