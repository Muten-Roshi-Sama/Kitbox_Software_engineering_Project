
namespace MauiApp1;
using CommunityToolkit.Maui.Views;
using System.ComponentModel;
public partial class CommandsSupplierView:ContentPage{
    DBConnection connection;

    public CommandsSupplierView(DBConnection connection) {
        InitializeComponent();
        this.connection = connection;
        Console.WriteLine("test");
        OrderListView.ItemsSource = getCommands();
        Console.WriteLine("test");
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
                App.Current.MainPage = new Manager(connection);
                break;
            default:break;
        }
    }
}