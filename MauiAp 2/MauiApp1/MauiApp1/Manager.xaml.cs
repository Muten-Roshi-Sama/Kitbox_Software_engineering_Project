using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using aaa;
using CommunityToolkit.Maui.Views;

namespace MauiApp1;



public partial class Manager : ContentPage
{
    List<Component> components;
    DBConnection connection;
    static int valueLimiteStock = 20;

    public Manager()
    {
        this.components = new List<Component>();
        InitializeComponent();
        this.connection = new DBConnection("StockManager","1234","projet","pat.infolab.ecam.be",63416);
        getComponents();
    }
    public static void LaunchAppShell()
    {
        App.Current.MainPage = new AppShell();
    }

    void OnImageButtonClicked(object sender, EventArgs e){
        var button = (Image)sender;
        var component = (Component)button.BindingContext;
        switch (button.ClassId)
        {
            case "Button1":
                component.isEditingL = false;
                component.isEditingE = true;
                break;
            case "Button2":
                component.isEditingL = true;
                component.isEditingE = false;
                this.connection.updateStockComponents(component);
                break;
            case "Button3":
                component.infoSupOn = true;
                component.infoSupOff = false;
            break;
            case "Button4":
                component.infoSupOn = false;
                component.infoSupOff = true;
                break;
            case "delLineBtn":
                try{
                    this.connection.deleteComponent(component.code);
                    components.Remove(component);
                    MyListView.ItemsSource = null;
                    MyListView.ItemsSource = components;
                }catch(Exception ex){
                    Console.WriteLine($"Erreur lors de l'insertion : {ex.Message}");
                }
                break;
            default:break;
        }
    }

    async void OnAddLineButtonClicked(object sender, EventArgs e){
        SimplePopup popup = new SimplePopup();
        //this.ShowPopup(new SimplePopup());
        try{
            Component result = (Component) await this.ShowPopupAsync(popup, CancellationToken.None);
            this.connection.addComponent(result);
            this.components.Add(result);
            MyListView.ItemsSource = null;
            MyListView.ItemsSource = components;
        }catch(Exception ex){
            Console.WriteLine($"Erreur lors de l'insertion : {ex.Message}");
        }
    }


    void FiltreLabelClicked(object sender, EventArgs e){
        var choice = (MenuFlyoutItem)sender;
        switch (choice.Text)
        {
            case "Missing Stock":
                List<Component> compoFiltre = new List<Component>();
                foreach (var item in components)
                {
                    if(item.stockAvailable < valueLimiteStock){
                        compoFiltre.Add(item);
                    }
                }
                MyListView.ItemsSource  = null;
                MyListView.ItemsSource  = compoFiltre;
                break;
            case "All":
                MyListView.ItemsSource = null;
                MyListView.ItemsSource = components;
                break;

            default:break;
        }
    }
    async void OnAddSuppButtonClicked(object sender, EventArgs e){
        try{
            var button = (Image)sender;
            var stack3 = (StackLayout)button.Parent;
            var stack4 = (VerticalStackLayout)stack3.Parent;
            var stack5 = (StackLayout)stack4.Parent;
            var viewCell2 = (ViewCell)stack5.Parent;
            var component = (Component)viewCell2.BindingContext;
            AddSuppPopup popup = new AddSuppPopup();
            CompoSupplier result = (CompoSupplier) await this.ShowPopupAsync(popup, CancellationToken.None);
            Console.WriteLine(result.idSupplier);
            Console.WriteLine("---------------------");
            connection.addSupplier(component,result);
            component.addSupplier(result);
            var SuppListView = viewCell2.FindByName<ListView>("SuppListView");
            SuppListView.ItemsSource  = null;
            SuppListView.ItemsSource  = component.listSuppliers;
        }catch{}
    }

    /**
    * lié aux boutons se trouvant sur la liste des différents fournisseurs
    **/
    void OnSuppButtonClicked(object sender, EventArgs e){
        var button = (Image)sender;
        var stack1 = (HorizontalStackLayout)button.Parent;
        var stack2 = (StackLayout)stack1.Parent;
        var viewCell = (ViewCell)stack2.Parent;
        var supp = (CompoSupplier)viewCell.BindingContext;
        var stack3 = (ListView)viewCell.Parent;
        var stack4 = (VerticalStackLayout)stack3.Parent;
        var stack5 = (StackLayout)stack4.Parent;
        var viewCell2 = (ViewCell)stack5.Parent;
        var component = (Component)viewCell2.BindingContext;
        switch (button.ClassId)
        {
            case "editBtn":
                supp.isSuppEditingL = false;
                supp.isSuppEditingE = true;
                break;
            case "confirmBtn":
                supp.isSuppEditingL = true;
                supp.isSuppEditingE = false;
                Console.WriteLine(component.code);
                Console.WriteLine(supp.idSupplier);
                connection.updatePriceDelayComponents(component.code, supp);
                break;
            case "deleteBtn":
                component.deleteSupplier(supp.idSupplier);
                connection.deleteSuppOfComponent(supp.idSupplier, component.code);
                var SuppListView = viewCell2.FindByName<ListView>("SuppListView");
                SuppListView.ItemsSource  = null;
                SuppListView.ItemsSource  = component.listSuppliers;
                break;
        }
    }
    public void disconnect(object sender, EventArgs e)
    {
        this.connection.disconnection();
        LaunchAppShell();        
    }

    public void getComponents(){
        this.components = this.connection.getAllComponents();
        MyListView.ItemsSource = components;
    }
}

