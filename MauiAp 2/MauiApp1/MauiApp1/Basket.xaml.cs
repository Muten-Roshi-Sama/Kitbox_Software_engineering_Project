using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Tls.Crypto;

namespace MauiApp1;

public partial class Basket : ContentPage
{
    public List<Casier> casier;
    public Compose compose; 
    public ObservableCollection<Element> elements = new ObservableCollection<Element>();
    private Boolean confirmer= false;
    
    public Basket()
    {
        this.casier = new List<Casier>();
        InitializeComponent();
    }

    public Basket(List<Casier>casier, Compose compose)
    {
        this.compose = compose; 
        bool isEditE= false;
        bool isEditL = true; 
        this.casier = casier;
        for (int i = 0; i < this.casier.Count; i++)
        {
            for (int j = 0; j < this.casier[i]._boxes.Count(); j++)
            {
                Element elem; 
                if (this.casier[i]._boxes[j].getTypeDoors().ToString() == "Yes")
                {
                     elem = new Element(i.ToString(), this.casier[i].depth, this.casier[i].length, this.casier[i]._boxes.Count.ToString(),
                        this.casier[i].color, this.casier[i]._boxes[j].getHeight(), this.casier[i]._boxes[j].getDoors().ToString(), "Glas"
                        ,this.casier[i]._boxes[j].getColor(), j.ToString(), isEditL, isEditE); 
                }
                else
                {
                     elem = new Element(i.ToString(), this.casier[i].depth, this.casier[i].length, this.casier[i]._boxes.Count.ToString(),
                        this.casier[i].color, this.casier[i]._boxes[j].getHeight(), this.casier[i]._boxes[j].getDoors().ToString(), "Normal"
                        ,this.casier[i]._boxes[j].getColor(),j.ToString(),isEditL, isEditE);  
                }
                
                
                elements.Add(elem);
            }
            
        }
        
        InitializeComponent();
        getComponents();
        
    }


    public void setCasiers(List<Casier> cas)
    {
        this.casier = cas; 
    }

    public List<Casier> getCassiers()
    {
        return this.casier;
    }
    
    public void setConfirmer(Boolean conf)
    {
        this.confirmer = conf; 
    }

    public Boolean getConformer()
    {
        return this.confirmer;
    }
    public void getComponents(){
        MyListView.ItemsSource = this.elements;
    }
    
    
    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    
    public static void LaunchAppShell()
    {
        App.Current.MainPage = new AppShell();
    }
    
    public async void ConfirmCommand(object sender, EventArgs e)
    {
        await DisplayAlert("Commande validé", "Votre commande a été validé avec succes ! ", "OK");
        LaunchAppShell();
        
    }

    public void OnImageButtonClicked(object sender, EventArgs e)
    {
        
        
        var button = (ImageButton)sender;
        var component = (Element)button.BindingContext;
        switch (button.ClassId)
        {
            case "Button1":
                component.isEditingL = false;
                component.isEditingE = true;
                break;
            case "Button3":
                component.isEditingL = true;
                component.isEditingE = false; 
                break;
            case "Button2":
                var button2 = (ImageButton)sender;
                
                var itemToDelete = (Element)button2.CommandParameter;

               
                if (MyListView.ItemsSource is ObservableCollection<Element> items)
                {
                    items.Remove(itemToDelete);
                    elements.Remove(itemToDelete);
                    
                    compose.setCasiers(int.Parse(itemToDelete.Casier), int.Parse(itemToDelete.boxe));
                }
                break;
        }
        MyListView.ItemsSource = null;
        MyListView.ItemsSource = elements;
    }
}