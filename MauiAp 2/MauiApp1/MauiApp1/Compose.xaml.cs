using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1;

public partial class Compose : ContentPage
{
    
    List<Picker> ColorsDoors= new List<Picker>();
    List<Picker> DoorsBoxes = new List<Picker>();
    List<Picker> TypeDoors = new List<Picker>();
    List<Picker> HeightBoxes = new List<Picker>();
    private List<Label> labels = new List<Label>();
    private List<Button> confirm = new List<Button>();
    List<String> largeur = new List<string>();
    List<String> profondeur = new List<string>();
    List<String> nombreBox = new List<string>();
    List<String> colors= new List<string>();

    List<Casier> Casiers = new List<Casier>();
    Basket menu = new Basket();
    
    
    Picker choixVerre = new Picker();
    Picker choiceColor = new Picker();
    public Compose()
    {
        InitializeComponent();
        menu.setCasiers(Casiers);
        
        largeur.Add("32");
        largeur.Add("42");
        largeur.Add("52");
        largeur.Add("62");
        largeur.Add("80");
        largeur.Add("100");
        largeur.Add("120");
        profondeur.Add("32");
        profondeur.Add("42");
        profondeur.Add("52");
        profondeur.Add("62");
        colors.Add("Black");
        colors.Add("White");
        colors.Add("Marron");
        for (int i = 1; i < 8; i++)
        {
            nombreBox.Add(i.ToString());
        }
        
        AddPartPicker.ItemsSource = largeur;
        AddPartPicker1.ItemsSource = profondeur;
        AddPartPicker2.ItemsSource = nombreBox;
        ColorsPicker.ItemsSource = colors;
        Colors2Picker.ItemsSource = colors; 
    }
    
    public Compose(List<Casier> Casiers)
    {
        InitializeComponent();
        menu.setCasiers(Casiers);
        List<String> largeur = new List<string>();
        List<String> profondeur = new List<string>();
        List<String> nombreBox = new List<string>();
        List<String> colors= new List<string>();
        largeur.Add("32");
        largeur.Add("42");
        largeur.Add("52");
        largeur.Add("62");
        largeur.Add("80");
        largeur.Add("100");
        largeur.Add("120");
        profondeur.Add("32");
        profondeur.Add("42");
        profondeur.Add("52");
        profondeur.Add("62");
        colors.Add("Black");
        colors.Add("White");
        colors.Add("Marron");
        for (int i = 1; i < 8; i++)
        {
            nombreBox.Add(i.ToString());
        }
        
        AddPartPicker.ItemsSource = largeur;
        AddPartPicker1.ItemsSource = profondeur;
        AddPartPicker2.ItemsSource = nombreBox;
        ColorsPicker.ItemsSource = colors; 
        Colors2Picker.ItemsSource = colors;
        this.Casiers = Casiers; 
    }


    public void OnPicker1SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker2 = sender as Picker;
        if (picker2.SelectedItem == "No")
        {
            choixVerre.IsEnabled = false;
            choiceColor.IsEnabled = false;
        }
        else
        {
            choixVerre.IsEnabled = true;
            choiceColor.IsEnabled = true;
        }
    }    
        private void DisplayBoxes(object sender, EventArgs e)
        {
            int space = 0;
            int rowDecale = 0;
            if (AddPartPicker2.SelectedItem == null)
            {
                AddPartPicker2.BackgroundColor = new Microsoft.Maui.Graphics.Color(1.0f, 0.0f, 0.0f);
            }
            else
            {
                AddPartPicker2.BackgroundColor = new Microsoft.Maui.Graphics.Color(1.0f, 1.0f, 1.0f);
                
                for (int i = 1; i <= int.Parse(AddPartPicker2.SelectedItem.ToString()); i++)
                {
                    if (i == 3 || i == 5 || i == 7)
                    {
                        rowDecale = rowDecale + 5;
                        space = 0;
                    }

                    Label boxe = new Label();
                    labels.Add(boxe);
                    boxe.Text = "Boxe " + (i).ToString();

                    Label porte = new Label();
                    labels.Add(porte);
                    porte.Text = "Do you want a door for this box ? ";

                    Label verre = new Label();
                    labels.Add(verre);
                    verre.Text = "Do you want a glass door ? ";

                    Picker choixPorte = new Picker();
                    choixPorte.Items.Add("Yes");
                    choixPorte.Items.Add("No");
                    DoorsBoxes.Add(choixPorte);

                    choixVerre = new Picker();
                    choixVerre.Items.Add("Yes");
                    choixVerre.Items.Add("No");
                    TypeDoors.Add(choixVerre);
                    choixPorte.SelectedIndexChanged += OnPicker1SelectedIndexChanged; 

                    Label color = new Label();
                    labels.Add(color);
                    color.Text = "Chose the color of the door: ";
                    choiceColor = new Picker();
                    choiceColor.Items.Add("Marron");
                    choiceColor.Items.Add("Black");
                    choiceColor.Items.Add("White");
                    ColorsDoors.Add(choiceColor);
                    

                    Label height = new Label();
                    labels.Add(height);
                    height.Text = "Chose the height of the box: ";
                    Picker choixHeight = new Picker();
                    choixHeight.Items.Add("32cm");
                    choixHeight.Items.Add("42cm");
                    choixHeight.Items.Add("52cm");
                    HeightBoxes.Add(choixHeight);

                    // Picker placeholder text
                    // choixPorte.Title = "Select";
                    // choixVerre.Title = "Y/N";
                    // choiceColor.Title = "Select";
                    // choixHeight.Title = "Select";
                    
                    //Picker Colors
                    choixPorte.BackgroundColor = new Microsoft.Maui.Graphics.Color(239, 239, 239);  // #C5FCEF Pale Blue (197, 252, 239)
                    choixVerre.BackgroundColor = new Microsoft.Maui.Graphics.Color(239, 239, 239) ; // light grey (239, 239, 239)
                    choiceColor.BackgroundColor = new Microsoft.Maui.Graphics.Color(239, 239, 239);
                    choixHeight.BackgroundColor = new Microsoft.Maui.Graphics.Color(239, 239, 239);

                    boxe.FontSize = 23;
                    porte.FontSize = 16;
                    verre.FontSize = 16;
                    choixPorte.FontSize = 16;
                    choixVerre.FontSize = 16;
                    choixVerre.WidthRequest = 200;
                    choixVerre.HeightRequest = 5;
                    choixPorte.WidthRequest = 200;
                    choixPorte.HeightRequest = 5;

                    boxe.Margin = new Thickness(10, 20, 0, 0);

                    porte.Margin = new Thickness(10, 70, 0, 0);
                    choixPorte.Margin = new Thickness(270, 70, 0, 0);

                    verre.Margin = new Thickness(10, 0, 0, 0);
                    choixVerre.Margin = new Thickness(200, 0, 0, 0);

                    color.Margin = new Thickness(10, 0, 0, 0);
                    choiceColor.Margin = new Thickness(210, 0, 0, 0);

                    height.Margin = new Thickness(10, 0, 0, 0);
                    choixHeight.Margin = new Thickness(200, 0, 0, 0);

                    MyGrid.SetColumn(boxe, 0 + space);
                    MyGrid.SetRow(boxe, 7 + rowDecale);

                    MyGrid.SetColumn(porte, 0 + space);
                    MyGrid.SetRow(porte, 7 + rowDecale);
                    MyGrid.SetColumn(choixPorte, 0 + space);
                    MyGrid.SetRow(choixPorte, 7 + rowDecale);

                    MyGrid.SetColumn(verre, 0 + space);
                    MyGrid.SetRow(verre, 8 + rowDecale);
                    MyGrid.SetColumn(choixVerre, 0 + space);
                    MyGrid.SetRow(choixVerre, 8 + rowDecale);

                    MyGrid.SetColumn(color, 0 + space);
                    MyGrid.SetRow(color, 9 + rowDecale);
                    MyGrid.SetColumn(choiceColor, 0 + space);
                    MyGrid.SetRow(choiceColor, 9 + rowDecale);

                    MyGrid.SetColumn(height, 0 + space);
                    MyGrid.SetRow(height, 10 + rowDecale);
                    MyGrid.SetColumn(choixHeight, 0 + space);
                    MyGrid.SetRow(choixHeight, 10 + rowDecale);

                    // Ajouter le nouvel élément au layout principal
                    MyGrid.Children.Add(porte);
                    MyGrid.Children.Add(boxe);
                    MyGrid.Children.Add(verre);
                    MyGrid.Children.Add(choixPorte);
                    MyGrid.Children.Add(choixVerre);
                    MyGrid.Children.Add(color);
                    MyGrid.Children.Add(choiceColor);
                    MyGrid.Children.Add(height);
                    MyGrid.Children.Add(choixHeight);
                    space = 2;

                }

                Button confirm = new Button();
                this.confirm.Add(confirm);
                confirm.Text = "Confirm";
                confirm.BackgroundColor = Color.FromHex("#65BAA9");
                confirm.TextColor = Color.FromHex("#C5FCEF");
                confirm.Clicked += AddToBasket;
                MyGrid.SetColumn(confirm, 1);
                MyGrid.SetRow(confirm, 11 + rowDecale);
                MyGrid.Children.Add(confirm);
                

            }
        }
        
        public static void LaunchAppShell()
        {
            App.Current.MainPage = new AppShell();
        }

        private async void AddToBasket(object sender, EventArgs e)
        {
            try
            {
                List<String> ReceiptContent = new List<string>();
                String Titre = "Command number 1234";
                ReceiptContent.Add(Titre);

                List<Box> boxes = new List<Box>();
                int numberOfBoxes = int.Parse(AddPartPicker2.SelectedItem?.ToString() ?? "0");

                for (int i = 0; i < numberOfBoxes; i++)
                {
                    string height = HeightBoxes[i].SelectedItem?.ToString() ?? "/";
                    string color = ColorsDoors[i].SelectedItem?.ToString() ?? "/";
                    string type = TypeDoors[i].SelectedItem?.ToString() ?? "/";
                    bool hasDoors = DoorsBoxes[i].SelectedItem?.ToString() == "Yes";

                    Box box = new Box(height, color, type, hasDoors);
                    boxes.Add(box);
                }

                Casier cas = new Casier(AddPartPicker.SelectedItem?.ToString(), AddPartPicker1.SelectedItem?.ToString(), 
                    ColorsPicker.SelectedItem?.ToString(), AddPartPicker2.SelectedItem?.ToString(), boxes, Colors2Picker.SelectedItem?.ToString());

                Casiers.Add(cas);
                WriteFile("OK");

                // Clear UI elements after adding to basket
                ClearUIElements();

                await DisplayAlert("Success", "Cabinet added. Consult your basket.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to add items to the basket: {ex.Message}", "OK");
            }
        }

        private void ClearUIElements()
        {
            foreach (var item in HeightBoxes)
                MyGrid.Children.Remove(item);
            foreach (var item in TypeDoors)
                MyGrid.Children.Remove(item);
            foreach (var item in DoorsBoxes)
                MyGrid.Children.Remove(item);
            foreach (var item in ColorsDoors)
                MyGrid.Children.Remove(item);

            HeightBoxes.Clear();
            TypeDoors.Clear();
            DoorsBoxes.Clear();
            ColorsDoors.Clear();

            if (confirm.Any())
                MyGrid.Children.Remove(confirm.First());
            confirm.Clear();

            foreach (var item in labels)
                MyGrid.Children.Remove(item);
            labels.Clear();

            ResetPickers();
        }

        private void ResetPickers()
        {
            AddPartPicker.ItemsSource = largeur;
            AddPartPicker1.ItemsSource = profondeur;
            AddPartPicker2.ItemsSource = nombreBox;
            ColorsPicker.ItemsSource = colors;
            Colors2Picker.ItemsSource = colors;
        }

        public void giveBasket(object sender, EventArgs e)
        {
            
            if (Casiers.Count == 0)
            {
                Page menu1 = new Basket();
                Navigation.PushModalAsync(menu1);
            }
            else
            {
                Page menu = new Basket(Casiers,this);
                Navigation.PushModalAsync(menu); 
            }
                
        }
        
        static void WriteFile(string text)
        {
            try
            {
                // Vérifier si le fichier existe, sinon le créer
                if (!File.Exists("score.txt"))
                {
                    File.Create("score.txt").Close();
                }

                // Écrire dans le fichier
                File.AppendAllText("score.txt", text + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de l'écriture dans le fichier : " + ex.Message);
            }
        }


        public List<Casier> getCasiers()
        {
            return this.Casiers;
        }

        public void setCasiers(int i, int j)
        {
            this.Casiers[i]._boxes.RemoveAt(j);
        }

        public void SetHeight(int i, int j, String h)
        {
            this.Casiers[i]._boxes[j].setHeight(h);
        }
        
        public void SetColor(int i, int j, String h)
        {
            this.Casiers[i]._boxes[j].setColor(h);
        }
        
        public void setTypeDoors(int i, int j, String h)
        {
            this.Casiers[i]._boxes[j].setTypeDoors(h);
        }
        
        public void setDoors(int i, int j, Boolean h)
        {
            this.Casiers[i]._boxes[j].setDoors(h);
        }
        
        
        
        
    

        
        //Changer le nombre passer de 4 a 2 
        //CHangement du panier 
}