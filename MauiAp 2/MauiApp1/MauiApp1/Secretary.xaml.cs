using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;

namespace MauiApp1;
using aaa;



public partial class Secretary : ContentPage
{
    List<Component> components;
    DBConnection connection;

    public Secretary()
    {
        this.components = new List<Component>();
        InitializeComponent();
        this.connection = new DBConnection("Secretary","1234","projet","pat.infolab.ecam.be",63416);  
        getComponents();
        

    }
    public static void LaunchAppShell()
    {
        App.Current.MainPage = new AppShell();
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

    void OnImageButtonClicked(object sender, EventArgs e){
        var button = (Image)sender;
        var component = (Component)button.BindingContext;
        switch (button.ClassId)
        {
            case "Button3":
            component.infoSupOn = true;
            component.infoSupOff = false;
            break;
            case "Button4":
            component.infoSupOn = false;
            component.infoSupOff = true;
            break;
            default:break;
        }
        //MyListView.ItemsSource = null;
        //MyListView.ItemsSource = components;
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
            try{
                component.deleteSupplier(supp.idSupplier);
                connection.deleteSuppOfComponent(supp.idSupplier, component.code);
                component.setGeneralStock();
                var SuppListView = viewCell2.FindByName<ListView>("SuppListView");
                SuppListView.ItemsSource  = null;
                SuppListView.ItemsSource  = component.listSuppliers;}catch(Exception ex){
                    Console.WriteLine(ex.Message);
                }
                break;
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
            connection.addSupplier(component,result);
            component.addSupplier(result);
            var SuppListView = viewCell2.FindByName<ListView>("SuppListView");
            SuppListView.ItemsSource  = null;
            SuppListView.ItemsSource  = component.listSuppliers;
        }catch{}
    }
    
}