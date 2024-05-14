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
        //connection = new MySqlConnection($"Server=localhost;Port=3306;User ID={this.user};Password={passwd};Database={this.database}");
        this.connection.Open();
        Console.WriteLine("connected");
        // alert('test');
    }

    public void connect(){
        this.connection.Open();
    }

    public void disconnection(){
        this.connection.Close();
    }

    //ici il faut voir quand y a 2 stockordered, ils faut les coupler ->la il faudra qu'on imprime pour code manuellement
    public List<Component> getAllComponents(){
        List<Component> components = new List<Component>();
        using (var command = new MySqlCommand("SELECT c.Reference, c.Code, c.HeightC, c.LengthC, c.DepthC, c.Color, c.IdSupplier, c.PriceSupplier, c.DelaySupplier, c.StockAvailable, s.Quantity AS StockOrdered, c.StockReserved, c.MinStock FROM Components c LEFT JOIN CommandsSupplier s ON (c.Code=s.Code AND c.IdSupplier=s.IdSupplier);",connection)){
            using (var reader = command.ExecuteReader()){
                Console.WriteLine("SELECT c.Reference, c.Code, c.HeightC, c.LengthC, c.DepthC, c.Color, c.IdSupplier, c.PriceSupplier, c.DelaySupplier, c.StockAvailable, s.Quantity AS StockOrdered, c.StockReserved, c.MinStock FROM Components c LEFT JOIN CommandsSupplier s ON (c.Code=s.Code AND c.IdSupplier=s.IdSupplier) WHERE s.Received = FALSE OR s.Received IS NULL;");
                Component compo = null;
                string Code = null;
                int idCurrentSupp = -1;

                CompoSupplier suppCurrent = null;
                int stockO = 0;
                int idProv = -1;

                while (reader.Read()){
                    if(Code != reader.GetString("Code")){
                        
                        if(compo is not null){
                            if(suppCurrent is not null){
                                suppCurrent.stockOrdered = stockO;
                                
                                compo.listSuppliers.Add(suppCurrent);
                            }
                            compo.setGeneralStock();
                            
                            components.Add(compo);
                        }
                        stockO = 0;
                        idProv = -1;
                        Code = reader.GetString("Code");
                        //Console.WriteLine(Code);
                        compo = new Component(reader.GetString("Reference"),
                            reader.GetString("Code"),reader.GetInt16("LengthC"),reader.GetInt16("HeightC"),
                            reader.GetInt16("DepthC"), reader.GetInt16("Color"), true, false);
                        idCurrentSupp = -1;
                        suppCurrent = null;
                    }
                        if(idProv == -1){
                            try{
                                stockO += reader.GetInt16("StockOrdered");
                            }catch{}
                            
                            suppCurrent = new CompoSupplier(reader.GetInt16("IdSupplier"), reader.GetFloat("PriceSupplier"),
                                reader.GetInt16("DelaySupplier"),reader.GetInt16("StockAvailable"), null, 
                                reader.GetInt16("StockReserved"),reader.GetInt16("MinStock"));
                            idProv = reader.GetInt16("IdSupplier");
                        }
                        else if( idProv != reader.GetInt16("IdSupplier")){
                            suppCurrent.stockOrdered = stockO;
                            stockO = 0;
                            compo.listSuppliers.Add(suppCurrent);
                            try{
                                stockO += reader.GetInt16("StockOrdered");
                            }catch{}
                            idProv = reader.GetInt16("IdSupplier");
                            suppCurrent = new CompoSupplier(reader.GetInt16("IdSupplier"), reader.GetFloat("PriceSupplier"),
                                reader.GetInt16("DelaySupplier"),reader.GetInt16("StockAvailable"), null, 
                                reader.GetInt16("StockReserved"),reader.GetInt16("MinStock"));

                        }
                        else{
                            try{
                                stockO += reader.GetInt16("StockOrdered");
                            }catch{}
                        }
                    
                }
                if(suppCurrent is not null){
                    suppCurrent.stockOrdered = stockO;
                    compo.listSuppliers.Add(suppCurrent);
                }
                compo.setGeneralStock();
                components.Add(compo);
                return components;
            }
        }
    }

    public void updateStockComponents(CompoSupplier supplier, string code){
        using var command = new MySqlCommand($"UPDATE Components SET StockAvailable={supplier.stockAvailable}, StockReserved={supplier.stockReserved}, MinStock={supplier.minimumStock} WHERE Code='{code}' AND IdSupplier={supplier.idSupplier};", connection);
        command.ExecuteNonQuery();
        Console.WriteLine("UPDATE");
    }
    public void updatePriceDelayComponents(string Code, CompoSupplier supp){
        Console.WriteLine($"UPDATE Components SET PriceSupplier={supp.priceSupplier.ToString().Replace(',','.')}, DelaySupplier={supp.delaySupplier} WHERE Code='{Code}' AND IdSupplier={supp.idSupplier};");
        using var command = new MySqlCommand($"UPDATE Components SET PriceSupplier={supp.priceSupplier.ToString().Replace(',','.')}, DelaySupplier={supp.delaySupplier} WHERE Code='{Code}' AND IdSupplier={supp.idSupplier};", connection);
        command.ExecuteNonQuery();
        Console.WriteLine("UPDATE");
    }

    public void addComponent(Component compo){
        int i = 0;
        foreach (var supp in compo.listSuppliers)
        {
            Console.WriteLine($"INSERT INTO Components (Reference, Code, LengthC, HeightC, DepthC , Color, IdSupplier, PriceSupplier, DelaySupplier, StockAvailable,StockReserved, MinStock) VALUES('{compo.reference}','{compo.code}',{compo.length},{compo.height},{compo.depth}, {Component.getColorCode(compo.color)},{supp.idSupplier.ToString()},{supp.priceSupplier.ToString().Replace(',','.')},{supp.delaySupplier},{supp.stockAvailable},{supp.stockReserved},{supp.minimumStock});");
            using var command = new MySqlCommand($"INSERT INTO Components (Reference, Code, LengthC, HeightC, DepthC , Color, IdSupplier, PriceSupplier, DelaySupplier, StockAvailable,StockReserved, MinStock) VALUES('{compo.reference}','{compo.code}',{compo.length},{compo.height},{compo.depth}, {Component.getColorCode(compo.color)},{supp.idSupplier.ToString()},{supp.priceSupplier.ToString().Replace(',','.')},{supp.delaySupplier},{supp.stockAvailable},{supp.stockReserved},{supp.minimumStock});",this.connection);
            command.ExecuteNonQuery();
            i++;
        }
        
        Console.WriteLine("INSERT");
    }

    public void addSupplier(Component compo, CompoSupplier supp){
        Console.WriteLine($"INSERT INTO Components (Reference, Code, LengthC, HeightC, DepthC , Color, IdSupplier, PriceSupplier, DelaySupplier, StockAvailable,StockOrdered,StockReserved, MinStock) VALUES('{compo.reference}','{compo.code}',{compo.length},{compo.height},{compo.depth}, {Component.getColorCode(compo.color)},{supp.idSupplier.ToString()},{supp.priceSupplier.ToString().Replace(',','.')},{supp.delaySupplier},{supp.stockAvailable},{supp.stockReserved},{supp.minimumStock});");
        using var command = new MySqlCommand($"INSERT INTO Components (Reference, Code, LengthC, HeightC, DepthC , Color, IdSupplier, PriceSupplier, DelaySupplier, StockAvailable,StockReserved, MinStock) VALUES('{compo.reference}','{compo.code}',{compo.length},{compo.height},{compo.depth}, {Component.getColorCode(compo.color)},{supp.idSupplier.ToString()},{supp.priceSupplier.ToString().Replace(',','.')},{supp.delaySupplier},{supp.stockAvailable},{supp.stockReserved},{supp.minimumStock});",this.connection);
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
                        reader.GetDateTime("DateC").ToString(),reader.GetString("Description"),reader.GetString("NameFile"), reader.GetBoolean("Payed"));
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

    public void orderComponentSupplier(List<SupplierCompoOrder> componentsList){
        try{
        DateTime now = DateTime.Now;
        // Calculate seconds since Unix epoch (January 1, 1970)
        TimeSpan timeSpan = now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        long secondsSinceEpoch = (long)timeSpan.TotalSeconds;
        foreach (var supp in componentsList)
        {
            using var command = new MySqlCommand($"INSERT INTO CommandsSupplier (Reference, Code, IdSupplier, Price, Delay, Quantity) VALUES('{secondsSinceEpoch}','{supp.code}',{supp.id}, {supp.price.ToString().Replace(',','.')}, {supp.delay}, {supp.quantity});",this.connection);
            Console.WriteLine($"INSERT INTO CommandsSupplier (Reference, Code, IdSupplier, Price, Delay, Quantity) VALUES('{secondsSinceEpoch}','{supp.code}',{supp.id}, {supp.price.ToString().Replace(',','.')}, {supp.delay}, {supp.quantity});");
            command.ExecuteNonQuery();
        }
        }catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
    }

    public List<CommandsSupplier> getCommandsSupplier(){
        List<CommandsSupplier> commandsSupplierList = new List<CommandsSupplier>();
        using (var request = new MySqlCommand("SELECT * FROM CommandsSupplier ORDER BY Reference;",connection)){
            using (var reader = request.ExecuteReader()){
                CommandsSupplier commands = null;
                while(reader.Read()){
                    if(commands is null || commands.referenceGlobal != reader.GetString("Reference")){
                        if(commands is not null){
                            commandsSupplierList.Add(commands);
                        }
                        commands = new CommandsSupplier(reader.GetString("Reference"), reader.GetBoolean("Received"));
                    }
                    commands.addCompoSupp(new SupplierCompoOrder(reader.GetString("Code"), reader.GetString("Reference"),
                    reader.GetInt16("IdSupplier"),reader.GetFloat("Price"),reader.GetInt16("Delay"), reader.GetInt16("Quantity")));
                }
                commandsSupplierList.Add(commands);
                return commandsSupplierList;

            }
        }
        
    }

    public void updateCommand(Boolean received){
        using var command = new MySqlCommand($"UPDATE CommandsSupplier SET Received={received}",this.connection);
        command.ExecuteNonQuery();
    }
    public void setStockReceivedCommand(int stock, string code, int idSupp){
        int value = 0;
        using (var command1 = new MySqlCommand($"SELECT StockAvailable FROM Components WHERE Code='{code}' AND IdSupplier={idSupp};",this.connection)){
            using (var reader = command1.ExecuteReader()){
                reader.Read();
                value = reader.GetInt16("StockAvailable");
            }
        }
        value +=stock;
        using var command2 = new MySqlCommand($"UPDATE Components SET StockAvailable={value} WHERE Code='{code}' AND IdSupplier={idSupp};",this.connection);
        command2.ExecuteNonQuery();
        
    }

    public void reserverCustomerCompo(String commandDB){
        Console.WriteLine(commandDB);
        String[] componentProv = commandDB.Split(";");
        List<String[]> listCompo = new List<string[]>();

        for (int i = 0; i < componentProv.Length; i++)
        {
            listCompo.Add(componentProv[i].Split(":"));
        }
        String refCompo = "";
        String colorCompo = "";
        String[]sizeCompo;
        String nbrCompo = "";
        try{
        foreach (String[] item in listCompo)
        {
            refCompo = item[0];
            colorCompo = item[1];
            sizeCompo = item[2].Split("x");
            for (int i = 0; i < sizeCompo.Length; i++)
            {
                sizeCompo[i]=sizeCompo[i].Replace("cm","");
            }
            nbrCompo = item[3];
            /*String request = $"UPDATE Components SET StockReserved=StockReserved+{nbrCompo},StockAvailable=StockAvailable-{nbrCompo}";
            request+= $" WHERE Reference='{refCompo}' AND Color={Component.getColorCode(colorCompo)} AND LengthC={sizeCompo[0]}";
            request+= $" AND HeightC={sizeCompo[1]} AND DepthC={sizeCompo[2]} AND PriceSupplier=(";
            request+= $"SELECT MIN(PriceSupplier) FROM Components WHERE Reference='{refCompo}' AND ";
            request+= $"Color={Component.getColorCode(colorCompo)} AND LengthC={sizeCompo[0]} AND HeightC={sizeCompo[1]} AND ";
            request+= $"DepthC={sizeCompo[2]});";*/
            String request = $"UPDATE Components AS c1 JOIN (SELECT MIN(PriceSupplier) AS MinPrice ";
            request += $"FROM Components WHERE Reference='{refCompo}' AND Color={Component.getColorCode(colorCompo)} ";
            request += $"AND LengthC={sizeCompo[0]} AND HeightC={sizeCompo[1]} AND DepthC={sizeCompo[2]} ) AS c2 ";
            request += $"SET c1.StockReserved=c1.StockReserved+{nbrCompo}, c1.StockAvailable=c1.StockAvailable-{nbrCompo} ";
            request += $"WHERE c1.Reference='{refCompo}' AND c1.Color={Component.getColorCode(colorCompo)} ";
            request += $"AND c1.LengthC={sizeCompo[0]} AND c1.HeightC={sizeCompo[1]} AND c1.DepthC={sizeCompo[2]} ";
            request += $"AND c1.PriceSupplier = c2.MinPrice;";
            Console.WriteLine(request);
            using var command = new MySqlCommand(request, this.connection);
            command.ExecuteNonQuery();
        }}catch(Exception ex){
            Console.WriteLine(ex.Message);
        }
    }

    public void confirmPayementCustomer(string reference){
        using var command = new MySqlCommand($"UPDATE Commands SET Payed=1 WHERE Reference='{reference}';", this.connection);
        command.ExecuteNonQuery();
    }
}