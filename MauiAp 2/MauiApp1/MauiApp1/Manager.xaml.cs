using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using aaa;
using CommunityToolkit.Maui.Views;
using System.ComponentModel;

namespace MauiApp1;



public partial class Manager : ContentPage,INotifyPropertyChanged 
{
    List<Component> components;
    DBConnection connection;
    List<SupplierCompoOrder> supplierOrder;

    public Manager()
    {
        this.components = new List<Component>();
        InitializeComponent();
        orderSuppBtn.IsVisible=false;
        this.connection = new DBConnection("StockManager","1234","projet","pat.infolab.ecam.be",63416);
        getComponents();
    }
    public Manager(DBConnection connection){
        this.components = new List<Component>();
        InitializeComponent();
        orderSuppBtn.IsVisible=false;
        this.connection = connection;
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
                orderSuppBtn.IsVisible=true;
                List<Component> compoFiltre = new List<Component>();
                supplierOrder = new List<SupplierCompoOrder>();
                
                foreach (var item in components)
                {
                    Component compo = new Component(item.reference,item.code,item.length,item.height,
                    item.depth, Component.getColorCode(item.color),true,false);
                    compo.GlobalStockAvailable = item.GlobalStockAvailable;
                    compo.GlobalStockOrdered = item.GlobalStockOrdered;
                    compo.GlobalStockReserved = item.GlobalStockReserved;
                    foreach (var supp in item.listSuppliers)
                    {
                        if((supp.stockAvailable+supp.stockOrdered) < supp.minimumStock){
                            supp.showOrderBtn = true;
                            compo.addSupplier(supp);
                        }
                    }
                    if(compo.listSuppliers.Count !=0){
                        compoFiltre.Add(compo);
                    }
                }
                MyListView.ItemsSource  = null;
                MyListView.ItemsSource  = compoFiltre;
                break;
            case "All":
                orderSuppBtn.IsVisible=false;
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
                component.setGeneralStock();
                supp.isSuppEditingL = true;
                supp.isSuppEditingE = false;
                connection.updatePriceDelayComponents(component.code, supp);
                connection.updateStockComponents(supp,component.code);
                break;
            case "deleteBtn":
                component.deleteSupplier(supp.idSupplier);
                connection.deleteSuppOfComponent(supp.idSupplier, component.code);
                component.setGeneralStock();
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

    public void OrderCompoSuppl(object sender, EventArgs e){
        var button = (Button)sender;
        var supplier = (CompoSupplier)button.BindingContext;
        var component = (Component) button.Parent.Parent.Parent.Parent.BindingContext;
        Console.WriteLine(component.reference);

        switch(button.Text){
            case "Order":
                supplierOrder.Add(new SupplierCompoOrder(component.code,component.reference,supplier.idSupplier,supplier.priceSupplier,supplier.delaySupplier,supplier.minimumStock));
                supplier.showOrderBtn = false;
                supplier.showRemoveOrderBtn = true;
                break;
            case "Remove":
                foreach (var item in supplierOrder)
                {
                    if(item.id==supplier.idSupplier && item.code==component.code){
                        supplierOrder.Remove(item);
                        break;
                    }
                }
                supplier.showOrderBtn = true;
                supplier.showRemoveOrderBtn = false;
                break;
        }
    }

    void OrderLabelTapped(object sender, EventArgs e){
        if(supplierOrder.Count==0){
            List<Component> compoFiltre = (List<Component>) MyListView.ItemsSource;
            foreach (var item in compoFiltre)
            {
                float price = -1;
                CompoSupplier suppCurrent = null;
                foreach (var supp in item.listSuppliers)
                {
                    if(price == -1 || price > supp.priceSupplier){
                        price = supp.priceSupplier;
                        suppCurrent = supp;
                    }
                }
                supplierOrder.Add(new SupplierCompoOrder(item.code,item.reference,suppCurrent.idSupplier,suppCurrent.priceSupplier,suppCurrent.delaySupplier,suppCurrent.minimumStock));
            }
        }
        App.Current.MainPage = new SupplierOrderView(supplierOrder,connection);
    }

    void MoreMenuBtn(object sender, EventArgs  e){
        var menu = (MenuFlyoutItem)sender;
        switch (menu.Text)
        {
            case "Orders": 
                App.Current.MainPage = new CommandsSupplierView(connection, "Manager");
                break;
            default:break;
        }
    }
}

