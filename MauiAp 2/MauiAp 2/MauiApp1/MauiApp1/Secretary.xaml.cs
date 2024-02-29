using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1;



public partial class Secretary : ContentPage
{
    List<Component> components;
    DBConnection connection;

    public Secretary()
    {
        this.components = new List<Component>();
        InitializeComponent();
        this.connection = new DBConnection("Secretary","1234","projet");  
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
        //App.Current.MainPage = new AppShell();
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
                this.connection.updatePriceDelayComponents(component);
                break;
            default:break;
        }
        MyListView.ItemsSource = null;
        MyListView.ItemsSource = components;
    }
    
}