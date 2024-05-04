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
        string description = getDescription();
        string details= getDetails();
        this.connexion = new DBConnection("interface","1234","projet","pat.infolab.ecam.be",63416);
        List<Command> c =connexion.getAllCommand();
        DateTime currentdate= DateTime.Now;
        String reference =currentdate.Day+currentdate.Month+currentdate.Year+ "_"+ (c.Count + 1 ).ToString();
        connexion.addComand(new Command(1, reference, " ", description, details));
        await DisplayAlert("Commande validé", "Votre commande a été validé avec succes ! ", "OK");
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
                
                var button3 = (ImageButton)sender;
                
                var itemToDelete1 = (Element)button3.CommandParameter;
                if (MyListView.ItemsSource is ObservableCollection<Element> items1)
                {
                    //myEntry.Text = "10";
                    compose.SetHeight(int.Parse(itemToDelete1.Casier), int.Parse(itemToDelete1.boxe), "52");
                    //compose.setCasiers(int.Parse(itemToDelete.Casier), int.Parse(itemToDelete.boxe));
                }
                MyListView.ItemsSource = null;
                MyListView.ItemsSource = elements;
                
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

    


    public String getDetails()
    {
        Console.WriteLine("Start");
        String details = " ";

        int c = 0;
        List<Element> elem2 = new List<Element>();

        int i = 0; 
        Console.WriteLine("Test");
        while( i < elements.Count())
        {
            Console.WriteLine("ST");
            while (int.Parse(elements[i].Casier) == c && i<elements.Count)
            {
                Console.WriteLine("ST2");
                elem2.Add(elements[i]);
                i++;
            }
            
            c++;



            int casier = int.Parse(elem2[i].Casier) + 1;

            details += "Locker " + casier + ": \n";

            Console.WriteLine("Test1");
            for (int j = 0; j < elem2.Count; j++)
            {
                details += "Boxe " + (i + 1) + " \n";
                details += " 4 Vertical Batten, "+ elem2[i].heigth+" cm, "+ elem2[i].color+"\n" ;
                details += " 2 Front crossbars, " + elem2[i].length+" cm, "+ elem2[i].color+"\n" ;
                details += " 2 Back crossbars, "+ elem2[i].length+" cm, "+ elem2[i].color+"\n" ;
                details += " 4 Side crossbars, "+ elem2[i].depth+" cm, "+ elem2[i].color+ "\n" ;
                details += " 2 Horizontal panels,"+ "L: "+elem2[i].length+"cm, W: "+elem2[i].depth+ ", "+elem2[i].color+"\n" ;
                details += " 2 Side panels,"+ "H: "+elem2[i].heigth+"cm, W: "+elem2[i].depth+ ", "+elem2[i].color+ " \n" ;
                details += " 1 Back panels, " + "L: "+elem2[i].length+"cm, H: "+elem2[i].heigth+  ", " +elem2[i].color+"\n" ;

            }
            details+= "\n";


        }

        return details; 
    }
}