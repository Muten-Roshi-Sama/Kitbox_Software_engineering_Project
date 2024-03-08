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
        Navigation.PushModalAsync(new Admin());
        //LaunchAppShell();
    }

    public void getComponents(){
        this.components = this.connection.getAllComponents();
        MyListView.ItemsSource = components;
    }

    void OnImageButtonClicked(object sender, EventArgs e){
        var button = (ImageButton)sender;
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
    
}