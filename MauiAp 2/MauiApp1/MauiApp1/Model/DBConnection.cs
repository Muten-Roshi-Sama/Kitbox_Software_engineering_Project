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
        using (var command = new MySqlCommand("SELECT * FROM Components ORDER BY Code;", connection)){
            using (var reader = command.ExecuteReader()){
                Component compo = null;
                string Code = null;
                
                while (reader.Read()){
                    if(Code != reader.GetString("Code")){
                        if(compo is not null){
                            components.Add(compo);
                        }
                        Code = reader.GetString("Code");
                        compo = new Component(reader.GetString("Reference"),
                            reader.GetString("Code"),reader.GetInt16("LengthC"),reader.GetInt16("HeightC"),reader.GetInt16("DepthC"), 
                            reader.GetInt16("Color"),reader.GetInt16("StockAvailable"),
                            reader.GetInt16("StockOrdered"), reader.GetInt16("StockReserved"), true, false);
                    }
                    compo.addSupplier(new CompoSupplier(reader.GetInt16("IdSupplier"), reader.GetFloat("PriceSupplier"), reader.GetInt16("DelaySupplier")));
                }
                components.Add(compo);
                return components;
            }
        }
    }

    public void updateStockComponents(Component compo){
        using var command = new MySqlCommand($"UPDATE Components SET StockAvailable={compo.stockAvailable}, StockOrdered={compo.stockOrdered},StockReserved={compo.stockReserved} WHERE Code='{compo.code}';", connection);
		using var reader = command.ExecuteReader();
        Console.WriteLine("UPDATE");
    }
    public void updatePriceDelayComponents(string Code, CompoSupplier supp){
        Console.WriteLine($"UPDATE Components SET PriceSupplier={supp.priceSupplier.ToString().Replace(',','.')}, DelaySupplier={supp.delaySupplier} WHERE Code='{Code}' AND IdSupplier={supp.idSupplier};");
        using var command = new MySqlCommand($"UPDATE Components SET PriceSupplier={supp.priceSupplier.ToString().Replace(',','.')}, DelaySupplier={supp.delaySupplier} WHERE Code='{Code}' AND IdSupplier={supp.idSupplier};", connection);
        using var reader = command.ExecuteReader();
        Console.WriteLine("UPDATE");
    }

    public void updateComponents(Component compo){
        using var command = new MySqlCommand($"UPDATE Components SET StockAvailable={compo.stockAvailable}, StockOrdered={compo.stockOrdered},StockReserved={compo.stockReserved} WHERE Code='{compo.code}';", connection);
        using var reader = command.ExecuteReader();
        Console.WriteLine("UPDATE");
    }
    public void addComponent(Component compo){
        int i = 0;
        foreach (var supp in compo.listSuppliers)
        {
            Console.WriteLine($"INSERT INTO Components (Reference, Code, LengthC, HeightC, DepthC , Color, IdSupplier, PriceSupplier, DelaySupplier, StockAvailable,StockOrdered,StockReserved) VALUES('{compo.reference}','{compo.code}',{compo.length},{compo.height},{compo.depth}, {Component.getColorCode(compo.color)},{supp.idSupplier.ToString()},{supp.priceSupplier.ToString().Replace(',','.')},{supp.delaySupplier},{compo.stockAvailable},{compo.stockOrdered},{compo.stockReserved});");
            using var command = new MySqlCommand($"INSERT INTO Components (Reference, Code, LengthC, HeightC, DepthC , Color, IdSupplier, PriceSupplier, DelaySupplier, StockAvailable,StockOrdered,StockReserved) VALUES('{compo.reference}','{compo.code}',{compo.length},{compo.height},{compo.depth}, {Component.getColorCode(compo.color)},{supp.idSupplier.ToString()},{supp.priceSupplier.ToString().Replace(',','.')},{supp.delaySupplier},{compo.stockAvailable},{compo.stockOrdered},{compo.stockReserved});",this.connection);
            command.ExecuteNonQuery();
            i++;
        }
        
        Console.WriteLine("INSERT");
    }

    public void addSupplier(Component compo, CompoSupplier supp){
        Console.WriteLine($"INSERT INTO Components (Reference, Code, LengthC, HeightC, DepthC , Color, IdSupplier, PriceSupplier, DelaySupplier, StockAvailable,StockOrdered,StockReserved) VALUES('{compo.reference}','{compo.code}',{compo.length},{compo.height},{compo.depth}, {Component.getColorCode(compo.color)},{supp.idSupplier.ToString()},{supp.priceSupplier.ToString().Replace(',','.')},{supp.delaySupplier},{compo.stockAvailable},{compo.stockOrdered},{compo.stockReserved});");
        using var command = new MySqlCommand($"INSERT INTO Components (Reference, Code, LengthC, HeightC, DepthC , Color, IdSupplier, PriceSupplier, DelaySupplier, StockAvailable,StockOrdered,StockReserved) VALUES('{compo.reference}','{compo.code}',{compo.length},{compo.height},{compo.depth}, {Component.getColorCode(compo.color)},{supp.idSupplier.ToString()},{supp.priceSupplier.ToString().Replace(',','.')},{supp.delaySupplier},{compo.stockAvailable},{compo.stockOrdered},{compo.stockReserved});",this.connection);
        command.ExecuteNonQuery();
    }
    public void deleteComponent(string Code){
        using var command = new MySqlCommand($"DELETE FROM Components WHERE Code = '{Code}';",this.connection);
        command.ExecuteNonQuery();
        Console.WriteLine($"DELETE FROM Components WHERE Code = {Code};");
    }
    public void deleteSuppOfComponent(int idSupplier, string Code){
        using var command = new MySqlCommand($"DELETE FROM Components WHERE Code = '{Code}' AND IdSupplier = {idSupplier};",this.connection);
        command.ExecuteNonQuery();
        Console.WriteLine($"DELETE FROM Components WHERE Code = '{Code}' AND IdSupplier = {idSupplier};");
    }

    public List<Command> getAllCommand()
    {
        List<Command> commands = new List<Command>();
        using (var command = new MySqlCommand("SELECT * FROM Commands;", connection)){
            using (var reader = command.ExecuteReader()){
                while (reader.Read()){
                    Command com = new Command(reader.GetInt16("Id"),reader.GetString("Reference"),
                        reader.GetDateTime("DateC").ToString(),reader.GetString("Description"),reader.GetString("NameFile"));
                    commands.Add(com);
                }
                return commands;
            }
        }
        
    }
    public void addComand(Command com)
    {
        Console.WriteLine($"INSERT INTO Commands (Reference, Description, NameFile) VALUES({com.Id},'{com.Reference}',{com.Description},{com.NameFile});");
        using var command = new MySqlCommand($"INSERT INTO Commands (Reference, Description, NameFile) VALUES('{com.Reference}','{com.Description}','{com.NameFile}');",this.connection);
        command.ExecuteNonQuery();
        Console.WriteLine("INSERT");
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