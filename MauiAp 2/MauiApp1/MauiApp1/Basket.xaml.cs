using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Tls.Crypto;
using System.Diagnostics;

namespace MauiApp1;

public partial class Basket : ContentPage
{
    public List<Casier> casier;
    public Compose compose; 
    public ObservableCollection<Element> elements = new ObservableCollection<Element>();
    private Boolean confirmer= false;
    public ObservableCollection<string> DepthsOptions { get; set; }
    private DBConnection connexion; 

    
    public Basket()
    {

        this.casier = new List<Casier>();
        InitializeComponent();
    }

    public Basket(List<Casier>casier, Compose compose)
    {
        DepthsOptions = new ObservableCollection<string>
        {
            "Option 1",
            "Option 2",
            "Option 3"
        };
        this.compose = compose; 
        bool isEditE= false;
        bool isEditL = true; 
        this.casier = casier;
        for (int i = 0; i < this.casier.Count; i++)
        {
            for (int j = 0; j < this.casier[i]._boxes.Count(); j++)
            {
                Element elem; 
                if (this.casier[i]._boxes[j].getTypeDoors().ToString() == "Yes" && this.casier[i]._boxes[j].getDoors().ToString() == "Yes")
                {
                    elem = new Element(i.ToString(), this.casier[i].depth, this.casier[i].length, this.casier[i]._boxes.Count.ToString(),
                        this.casier[i].color, this.casier[i]._boxes[j].getHeight(), this.casier[i]._boxes[j].getDoors().ToString(), "Glas"
                        ,"/", j.ToString(), isEditL, isEditE); 
                }
                else if (this.casier[i]._boxes[j].getTypeDoors().ToString() == "No" && this.casier[i]._boxes[j].getDoors().ToString() == "Yes")
                {
                    elem = new Element(i.ToString(), this.casier[i].depth, this.casier[i].length, this.casier[i]._boxes.Count.ToString(),
                        this.casier[i].color, this.casier[i]._boxes[j].getHeight(), this.casier[i]._boxes[j].getDoors().ToString(), "Normal"
                        ,this.casier[i]._boxes[j].getColor(),j.ToString(),isEditL, isEditE);  
                }else{
                    elem = new Element(i.ToString(), this.casier[i].depth, this.casier[i].length, this.casier[i]._boxes.Count.ToString(),
                        this.casier[i].color, this.casier[i]._boxes[j].getHeight(), this.casier[i]._boxes[j].getDoors().ToString(), "/"
                        ,"/",j.ToString(),isEditL, isEditE);  
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
        // Vérifier si le panier est vide avant de continuer
        if (!elements.Any()) 
        {
            await DisplayAlert("Empty Basket", "Your basket is empty. Please add items before confirming.", "OK");
            return; 
        }
        
        string description = getDescription();
        (string details, string commandDB)= getDetails();
        this.connexion = new DBConnection("interface","1234","projet","pat.infolab.ecam.be",63416);
        List<Command> c =connexion.getAllCommand();
        DateTime currentdate= DateTime.Now;
        String reference =currentdate.Day+currentdate.Month+currentdate.Year+ "_"+ (c.Count + 1 ).ToString();
        connexion.reserverCustomerCompo(commandDB);
        connexion.addComand(new Command(1, reference, " ", description, details,false));
        await DisplayAlert("Order confirmed", "Order confirmed with great succes ! ", "Proceed to payment");
        LaunchAppShell();
        //this.connexion.disconnection();
        
    }

    public async void OnImageButtonClicked(object sender, EventArgs e)
    {
        
        
        var button = (ImageButton)sender;
        var component = (Element)button.BindingContext;
        int index = elements.IndexOf(component);
        switch (button.ClassId)
        {
            case "Button1":
                component.isEditingL = false;
                component.isEditingE = true;
                
                var button12 = (ImageButton)sender;
                var itemToDelete12 = (Element)button12.CommandParameter;
                
                
                ModifyBasket popup = new ModifyBasket();
                await Navigation.PushModalAsync(new ModifyBasket(compose, itemToDelete12, index)); 
                break;
            case "Button3":

                component.isEditingL = true;
                component.isEditingE = false;

                MyListView.ItemsSource = null;
                MyListView.ItemsSource = elements;
                
                break;
            case "Button2":
                var button2 = (ImageButton)sender;
                
                var itemToDelete = (Element)button2.CommandParameter;


                if (MyListView.ItemsSource is ObservableCollection<Element> items)
                {
                    try
                    {
                        if (items.Remove(itemToDelete))
                        {
                            items.Remove(itemToDelete);
                            elements.Remove(itemToDelete);
                            compose.setCasiers(int.Parse(itemToDelete.Casier), int.Parse(itemToDelete.boxe));
                            MyListView.ItemsSource = null;
                            MyListView.ItemsSource = elements; // Réaffectez pour rafraîchir
                            await DisplayAlert("Item Removed", "The item has been successfully removed from the basket.", "OK"); // pas ouf masi seule solution pour l'instant
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Erreur lors de la suppression : " + ex.Message);
                        await DisplayAlert("Item Removed", "The item has been successfully removed from the basket.", "OK"); // pas ouf masi seule solution pour l'instant
                    }
                    
                    
                }
                break;
        }
        MyListView.ItemsSource = null;
        MyListView.ItemsSource = elements;
    }
    public String getDescription()
    {
        String desc="Nombre de casier = " + getNbArmoir()+ "\n";
        for (int i = 0; i < int.Parse(getNbArmoir()); i++)
        {
            desc += "Locker " + (i + 1) + ": " + getNbBox(i.ToString()) + " Boxes \n";
        }

        return desc; 
    }
public String getNbArmoir()
    {
        int nbArmoir= int.Parse(elements[elements.Count-1].Casier) +1;

        return nbArmoir.ToString();
    }

    public String getNbBox(String nbArmoir)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].Casier == nbArmoir)
            {
                return elements[i].nbBoxe;
            }
        }

        return "";
    }

    


    public (String,String) getDetails()
    {
        String details = "";
        String commandDB = "";        //pour chaque commande: Reference:color:size:nbre;

        for (int i = 0; i < casier.Count(); i++)
        {
            details += "Locker " + i.ToString() +": \n";
            for (int j = 0; j < casier[i]._boxes.Count(); j++)
            {

                String color = casier[i].color;
                String height = casier[i]._boxes[j].getHeight();
                String depth = casier[i].depth;
                String length = casier[i].length;


                details += "Boxe " + j.ToString() + ": \n";
                details +=" 4 Vertical Batten, " +color +", "+ height +"\n" ;
                commandDB += "Vertical batten:No:0x"+height+"x0:4;";

                details +=" 2 Front crossbars, " +color +", "+ length +" \n" ;
                commandDB += "Crossbar front:"+color+":"+length+"x0x0:2;";

                details +=" 2 Back crossbars, "  +color +", "+ length +" \n" ;
                commandDB += "Crossbar back:"+color+":"+length+"x0x0:2;";

                details +=" 4 Side crossbars, "  +color +", "+ depth +"\n" ;
                commandDB += "Crossbar left or right:"+color+":0x0x"+depth+":4;";

                details +=" 2 Horizontal panels, " +color +", "+ depth + "x" + length +"\n";
                commandDB += "Panel horizontal:"+color+":"+length+"x0x"+depth+":2;";

                details +=" 2 Side panels, " +color +", "+ depth + "X" + height +"\n" ;
                commandDB += "Panel left or right:"+color+":0x"+height+"x"+depth+":2;";

                details +=" 1 Back panels, " +color +", "+ length + "X" + height +" \n" ;
                commandDB += "Panel back:"+color+":"+length+"x"+height+"x0:1;";

                if (casier[i]._boxes[j].getDoors())
                {
                    if(casier[i]._boxes[j].getTypeDoors() == "Normal" ){
                        details +=" 1 Doors, " +color +", "+ length + "X" + height +" \n" ;
                        commandDB += "Door:"+color+":"+length+"x"+height+"x0:1;";
                    }else{
                         details +=" 1 Doors, " +"Glas" +", "+ length + "X" + height +" \n" ;
                         commandDB += "Door glass:No:"+length+"x"+height+"x0:1;";
                    }
                   
                }

                details += "\n";


            }
        }

        return (details,commandDB);
    }
}