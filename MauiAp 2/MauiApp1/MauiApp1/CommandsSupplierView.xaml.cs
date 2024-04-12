
namespace MauiApp1;
using CommunityToolkit.Maui.Views;
using System.ComponentModel;
public partial class CommandsSupplierView:ContentPage{
    DBConnection connection;
    String type;

    public CommandsSupplierView(DBConnection connection, String type) {
        InitializeComponent();
        this.connection = connection;
        OrderListView.ItemsSource = getCommands();
        this.type = type;
    }
    public static void LaunchAppShell()
    {
        App.Current.MainPage = new AppShell();
    }

    private List<CommandsSupplier> getCommands(){
        return connection.getCommandsSupplier();
    }

    public void disconnect(object sender, EventArgs e)
    {
        this.connection.disconnection();
        LaunchAppShell();        
    }

    void ReceivedBtn(object sender, EventArgs  e){
        var labelBtn = (Label)sender;
        var command = (CommandsSupplier)labelBtn.BindingContext;
        command.received = true;
        command.notReceived = false;
        try{
            foreach (var item in command.ordersList)
            {
                connection.setStockReceivedCommand(item.quantity,item.code,item.id);
            }
            connection.updateCommand(true);
        }catch(Exception ex){
            Console.WriteLine(ex.Message);
        }
        
    }

    void MoreMenuBtn(object sender, EventArgs  e){
        var menu = (MenuFlyoutItem)sender;
        switch (menu.Text)
        {
            case "Stock": 
                switch (type)
                {
                    case "Manager":
                        App.Current.MainPage = new Manager(connection);
                        break;
                    case "StockKeeper":
                        App.Current.MainPage = new StockKeeper(connection);
                        break;
                    default:break;
                }
                break;
            default:break;
        }
    }
}