using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1;



public partial class StockKeeper : ContentPage
{
    List<Component> components;
    DBConnection connection;
    static int valueLimiteStock = 20;
    List<SupplierCompoOrder> supplierOrder;


    public StockKeeper()
    {
        this.components = new List<Component>();
        InitializeComponent();
        this.connection = new DBConnection("StockKeeper","1234","projet","pat.infolab.ecam.be",63416);  
        getComponents();
    }

    public StockKeeper(DBConnection connection)
    {
        this.components = new List<Component>();
        InitializeComponent();
        this.connection = connection; 
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

    void ShowInfoClicked(object sender, EventArgs e){
        var button = (Image)sender;
        var component = (Component)button.BindingContext;
        switch (button.ClassId)
        {
            case "Button3":
                component.infoSupOff = false;
                component.infoSupOn = true;
                break;
            case "Button4":
                component.infoSupOff = true;
                component.infoSupOn = false;
                break;
            default:break;
        }
        //MyListView.ItemsSource = null;
        //MyListView.ItemsSource = components;
    }

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
        }
    }
    void MoreMenuBtn(object sender, EventArgs  e){
        var menu = (MenuFlyoutItem)sender;
        switch (menu.Text)
        {
            case "Orders": 
                App.Current.MainPage = new CommandsSupplierView(connection, "StockKeeper");
                break;
            default:break;
        }
    }

    void FiltreLabelClicked(object sender, EventArgs e){
        var choice = (MenuFlyoutItem)sender;
        switch (choice.Text)
        {
            case "Missing Stock":
                //orderSuppBtn.IsVisible=true;
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
                //orderSuppBtn.IsVisible=false;
                MyListView.ItemsSource = null;
                MyListView.ItemsSource = components;
                break;

            default:break;
        }
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
    
}