using CommunityToolkit.Maui.Views;
namespace MauiApp1;

public partial class SupplierOrderView : ContentPage
{
    List<SupplierCompoOrder> componentsList;
    DBConnection connection;
    public SupplierOrderView(List<SupplierCompoOrder> supp, DBConnection connection){
        InitializeComponent();
        componentsList = new List<SupplierCompoOrder>();
        foreach (var item in supp)
        {
            componentsList.Add(item);
        }
        OrderListView.ItemsSource = componentsList;
        this.connection = connection;
    }

    public static void LaunchAppShell()
    {
        App.Current.MainPage = new AppShell();
    }

    void OnImageButtonClicked(object sender, EventArgs e){
        var button = (Image)sender;
        var component = (SupplierCompoOrder)button.BindingContext;
        switch (button.ClassId)
        {
            case "editBtn":
                component.showLabelQtt = false;
                component.showEntryQtt = true;
                break;
            case "checkBtn":
                component.showLabelQtt = true;
                component.showEntryQtt = false;
                break;
            case "deleteBtn":
                this.componentsList.Remove(component);
                break;
            default:break;
        }
    }

    void OnAddSuppButtonClicked(object sender, EventArgs e){
        
    }

    void OnSuppButtonClicked(object sender, EventArgs e){
        
    }

    void OrderCompoSuppl(object sender, EventArgs e){
        
    }

    public void disconnect(object sender, EventArgs e)
    {
        this.connection.disconnection();
        LaunchAppShell();        
    }

    async void ConfirmOrder(object sender, EventArgs e){
        //ici mettre la requete pour ajouter la commande fournisseur
        connection.orderComponentSupplier(this.componentsList);
        await DisplayAlert("Commande validé", "Votre commande a été validé avec succes ! ", "OK");
        App.Current.MainPage = new Manager(this.connection);
    }
}