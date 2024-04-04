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

    public StockKeeper()
    {
        this.components = new List<Component>();
        InitializeComponent();
        this.connection = new DBConnection("StockKeeper","1234","projet","pat.infolab.ecam.be",63416);  
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
            case "Button1":
                component.isEditingL = false;
                component.isEditingE = true;
                break;
            case "Button2":
                component.isEditingL = true;
                component.isEditingE = false;
                this.connection.updateStockComponents(component);
                break;
            default:break;
        }
        //MyListView.ItemsSource = null;
        //MyListView.ItemsSource = components;
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
    
}