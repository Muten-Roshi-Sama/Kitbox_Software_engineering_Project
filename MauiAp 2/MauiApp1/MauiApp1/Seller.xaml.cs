using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1;

public partial class Seller : ContentPage
{
    
    DBConnection connection;
    List<Command> commands = new List<Command>(); 
    public Seller()
    {
        InitializeComponent();
        this.connection = new DBConnection("interface","1234","projet","pat.infolab.ecam.be",63416); 
        getCommands();
    }
    
    public static void LaunchAppShell()
    {
        App.Current.MainPage = new AppShell();
    }
    
    public void getCommands(){
        this.commands = this.connection.getAllCommand();
        MyListView.ItemsSource = commands;
    }
    
    
    public void disconnect(object sender, EventArgs e)
    {
        this.connection.disconnection();
        LaunchAppShell();
        //Navigation.PushModalAsync(new Admin());
        
    }
}