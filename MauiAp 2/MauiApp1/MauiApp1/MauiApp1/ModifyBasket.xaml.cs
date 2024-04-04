using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1;

public partial class ModifyBasket : ContentPage
{
    public List<String> height= new List<string>();
    List<String> colors = new List<string>();
    List<String> type = new List<string>();
    List<String> doorsP = new List<string>();

    private Compose compose;
    private Element elem;
    private int index; 
    
    public ModifyBasket()
    {
        InitializeComponent();
        
        
        /*LHeight.Text= "Height: "+ elem.heigth;
        LType.Text= "Type of the doors: "+ elem.typeDoors;
        LColor.Text= "Color of the doors: "+ elem.colorDoors;
        LDoors.Text = "Doors: " + elem.doors; */
        
        colors.Add("Black");
        colors.Add("White");
        colors.Add("Marron"); 
        
        doorsP.Add("Yes");
        doorsP.Add("No");
        
        height.Add("32");
        height.Add("42");
        height.Add("52");
        
        type.Add("Glas");
        type.Add("Normal");

        
        AddPartPicker1.ItemsSource = type;
        AddPartPicker2.ItemsSource = height;
        AddPartPicker.ItemsSource = doorsP;
        ColorsPicker.ItemsSource = colors;
        
        
        Label choiceColor = new Label();
        choiceColor.Text = AddPartPicker.Items[0];
        choiceColor.Text = AddPartPicker.Items[1];


        MyGrid2.SetColumn(choiceColor, 0 );
        MyGrid2.SetRow(choiceColor, 6);

        MyGrid2.Children.Add(choiceColor);
       
    }

    public ModifyBasket(Compose compose, Element elem, int index)
    {
        this.compose = compose;
        this.elem = elem;
        this.index = index; 
        InitializeComponent();
        
        /*LHeight.Text= "Height: "+ elem.heigth;
        LType.Text= "Type of the doors: "+ elem.typeDoors;
        LColor.Text= "Color of the doors: "+ elem.colorDoors;
        LDoors.Text = "Doors: " + elem.doors; */
        
        colors.Add("Black");
        colors.Add("White");
        colors.Add("Marron"); 
        
        doorsP.Add("Yes");
        doorsP.Add("No");
        
        height.Add("32");
        height.Add("42");
        height.Add("52");
        
        
        type.Add("Normal");
        type.Add("Glas");

        
        AddPartPicker1.ItemsSource = type;
        AddPartPicker2.ItemsSource = height;
        AddPartPicker.ItemsSource = doorsP;
        ColorsPicker.ItemsSource = colors;
        
        
        Label choiceColor = new Label(); 
        //Ca affiche bien yes et no 
        choiceColor.Text = AddPartPicker.Items[0] + " "+ AddPartPicker.Items[1];


        MyGrid2.SetColumn(choiceColor, 0 );
        MyGrid2.SetRow(choiceColor, 6);

        MyGrid2.Children.Add(choiceColor);

    }



    public async void ConfirmModification(object sender, EventArgs e)
    {
       /* Boolean yes;
        if (AddPartPicker.SelectedItem.ToString() == "Yes")
        {
           yes = true; 
        }
        else
        {
            yes = false;
        }
        compose.SetHeight(int.Parse(elem.Casier),index,AddPartPicker2.SelectedItem.ToString());
        compose.setDoors(int.Parse(elem.Casier),index,yes);
        compose.setTypeDoors(int.Parse(elem.Casier),index,AddPartPicker1.SelectedItem.ToString());
        compose.SetColor(int.Parse(elem.Casier),index,ColorsPicker.SelectedItem.ToString());*/
        
        await Navigation.PopModalAsync();
    }
}