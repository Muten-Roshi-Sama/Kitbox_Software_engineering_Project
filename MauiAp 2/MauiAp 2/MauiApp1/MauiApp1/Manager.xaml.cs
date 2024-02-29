using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1;



public partial class Manager : ContentPage
{
    List<Component> components;
    DBConnection connection;

    public Manager()
    {
        this.components = new List<Component>();
        InitializeComponent();
        this.connection = new DBConnection("StockManager","1234","projet");  
        getComponents();
        

    }
    public static void LaunchAppShell()
    {
        App.Current.MainPage = new AppShell();
    }

    public void disconnect(object sender, EventArgs e)
    {
        this.connection.disconnection();
        //LaunchAppShell();
        Navigation.PushModalAsync(new Admin());
        
    }

    public void getComponents(){
        this.components = this.connection.getAllComponents();
        MyListView.ItemsSource = components;
    }
}