using MySqlConnector;
using VariousExceptions;


public class DBConnection{
    String user;
    String database;
    String server;
    int port;
    MySqlConnection connection;


    public DBConnection(string user, string passwd, string database, string server, int port){
        this.user = user;
        this.database = database;
        this.server = server;
        this.port = port;
        connection = new MySqlConnection($"Server={this.server};Port={this.port};User ID={this.user};Password={passwd};Database={this.database}");
        this.connection.Open();
        Console.WriteLine("connected");
    }

    public void connect(){
        this.connection.Open();
    }

    public void disconnection(){
        this.connection.Close();
    }

    public List<Component> getAllComponents(){
        List<Component> components = new List<Component>();
        using (var command = new MySqlCommand("SELECT * FROM Components;", connection)){
            using (var reader = command.ExecuteReader()){
                while (reader.Read()){
                    Component compo = new Component(reader.GetInt16("Id"),reader.GetString("Reference"),
                    reader.GetString("Code"),reader.GetInt16("LengthC"),reader.GetInt16("HeightC"),reader.GetInt16("DepthC"), 
                    reader.GetInt16("Color"), reader.GetFloat("PriceSupplier1"),reader.GetInt16("DelaySupplier1"),
                    reader.GetFloat("PriceSupplier2"), reader.GetInt16("DelaySupplier2"),reader.GetInt16("StockAvailable"),
                    reader.GetInt16("StockOrdered"), reader.GetInt16("StockReserved"), true, false);
                    components.Add(compo);
                }
                return components;
            }
        }
    }

    public void updateStockComponents(Component compo){
        using var command = new MySqlCommand($"UPDATE Components SET StockAvailable={compo.stockAvailable}, StockOrdered={compo.stockOrdered},StockReserved={compo.stockReserved} WHERE Id={compo.id};", connection);
		using var reader = command.ExecuteReader();
        Console.WriteLine("UPDATE");
    }
    public void updatePriceDelayComponents(Component compo){
        using var command = new MySqlCommand($"UPDATE Components SET PriceSupplier1={compo.priceSupplier1.ToString().Replace(',','.')}, DelaySupplier1={compo.delaySupplier1},PriceSupplier2={compo.priceSupplier2.ToString().Replace(',','.')},DelaySupplier2={compo.delaySupplier2} WHERE Id={compo.id};", connection);
        using var reader = command.ExecuteReader();
        Console.WriteLine("UPDATE");
    }

    public void updateComponents(Component compo){
        using var command = new MySqlCommand($"UPDATE Components SET PriceSupplier1={compo.priceSupplier1.ToString().Replace(',','.')}, DelaySupplier1={compo.delaySupplier1},PriceSupplier2={compo.priceSupplier2.ToString().Replace(',','.')},DelaySupplier2={compo.delaySupplier2}, StockAvailable={compo.stockAvailable}, StockOrdered={compo.stockOrdered},StockReserved={compo.stockReserved} WHERE Id={compo.id};", connection);
        using var reader = command.ExecuteReader();
        Console.WriteLine("UPDATE");
    }
    public void addComponent(Component compo){
        Console.WriteLine($"INSERT INTO Components (Id,Reference, Code, LengthC, HeightC, DepthC , Color, PriceSupplier1, DelaySupplier1, PriceSupplier2, DelaySupplier2, StockAvailable,StockOrdered,StockReserved) VALUES({compo.id},'{compo.reference}','{compo.code}',{compo.length},{compo.height},{compo.depth}, {compo.getColorCode()},{compo.priceSupplier1.ToString().Replace(',','.')},{compo.delaySupplier1},{compo.priceSupplier2.ToString().Replace(',','.')},{compo.delaySupplier2},{compo.stockAvailable},{compo.stockOrdered},{compo.stockReserved});");
        using var command = new MySqlCommand($"INSERT INTO Components (Id,Reference, Code, LengthC, HeightC, DepthC , Color, PriceSupplier1, DelaySupplier1, PriceSupplier2, DelaySupplier2, StockAvailable,StockOrdered,StockReserved) VALUES({compo.id},'{compo.reference}','{compo.code}',{compo.length},{compo.height},{compo.depth}, {compo.getColorCode()},{compo.priceSupplier1.ToString().Replace(',','.')},{compo.delaySupplier1},{compo.priceSupplier2.ToString().Replace(',','.')},{compo.delaySupplier2},{compo.stockAvailable},{compo.stockOrdered},{compo.stockReserved});",this.connection);
        command.ExecuteNonQuery();
        Console.WriteLine("INSERT");
    }

    public void deleteComponent(int id){
        using var command = new MySqlCommand($"DELETE FROM Components WHERE Id = {id};",this.connection);
        command.ExecuteNonQuery();
        Console.WriteLine($"DELETE FROM Components WHERE Id = {id};");
    }

    public Account getUserAccount(string name){
        try{
            using (var command = new MySqlCommand($"SELECT Password, Fonction FROM Account WHERE Name='{name}';", connection)){
                using (var reader = command.ExecuteReader()){
                    reader.Read();
                    return new Account(name, reader.GetString("Password"), reader.GetString("Fonction"));
                }
            }
        }catch(Exception){
            throw new ErrorConnectionException("Account not found");
        }
         
    }


}