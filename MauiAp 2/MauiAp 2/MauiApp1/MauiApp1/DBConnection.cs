using MySqlConnector;
using VariousExceptions;


public class DBConnection{
    String user;
    String database;
    String server;
    MySqlConnection connection;


    public DBConnection(string user, string passwd, string database, string server="localhost"){
        this.user = user;
        this.database = database;
        connection = new MySqlConnection($"Server=localhost;User ID={this.user};Password={passwd};Database={this.database}");
        this.connection.Open();
        Console.WriteLine("connected");
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
                    reader.GetString("Code"), reader.GetString("Dimensions"),reader.GetInt16("Color"), 
                    reader.GetFloat("PriceSupplier1"),reader.GetInt16("DelaySupplier1"),reader.GetFloat("PriceSupplier2"),
                    reader.GetInt16("DelaySupplier2"),reader.GetInt16("StockAvailable"),reader.GetInt16("StockOrdered"),
                    reader.GetInt16("StockReserved"), true, false);
                    components.Add(compo);
                }
                return components;
            }
            
        }
        
    }

    public void updateStockComponents(Component compo){
        using var command = new MySqlCommand($"UPDATE Components SET StockAvailable={compo.stockAvailable}, StockOrdered={compo.stockOrdered},StockReserved={compo.stockReserved} WHERE Id={compo.Id};", connection);
		using var reader = command.ExecuteReader();
    }
    public void updatePriceDelayComponents(Component compo){
        using var command = new MySqlCommand($"UPDATE Components SET PriceSupplier1={compo.priceSupplier1.ToString().Replace(',','.')}, DelaySupplier1={compo.delaySupplier1},PriceSupplier2={compo.priceSupplier2.ToString().Replace(',','.')},DelaySupplier2={compo.delaySupplier2} WHERE Id={compo.Id};", connection);
        using var reader = command.ExecuteReader();
    }

    public Account getUserAccount(string name){
        try{
            using (var command = new MySqlCommand($"SELECT Password, Function FROM Account WHERE Name='{name}';", connection)){
                using (var reader = command.ExecuteReader()){
                    reader.Read();
                    return new Account(name, reader.GetString("Password"), reader.GetString("Function"));
                }
            }
        }catch(Exception){
            throw new ErrorConnectionException("Account not found");
        }
         
    }


}