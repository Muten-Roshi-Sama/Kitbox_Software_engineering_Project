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
                this.connection.updateComponents(component);
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
                    this.connection.deleteComponent(Int32.Parse(component.id));
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
        SimplePopup popup = new SimplePopup(Int32.Parse(components.Last().id));
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
        
        
        //AddLinePopup.IsVisible = true;
    }
    public void disconnect(object sender, EventArgs e)
    {
        this.connection.disconnection();
        LaunchAppShell();
        //Navigation.PushModalAsync(new Admin());
        
    }

    public void getComponents(){
        this.components = this.connection.getAllComponents();
        MyListView.ItemsSource = components;
    }
}

