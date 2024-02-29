using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1;

public partial class Compose : ContentPage
{
    public Compose()
    {
        InitializeComponent();
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
                    boxe.Text = "Boxe " + (i).ToString();
                    Label porte = new Label();
                    porte.Text = "Do you want a door for this box ? ";
                    Label verre = new Label();
                    verre.Text = "Do you want a glass door ? ";
                    Picker choixPorte = new Picker();
                    choixPorte.Items.Add("Yes");
                    choixPorte.Items.Add("No");
                    Picker choixVerre = new Picker();
                    choixVerre.Items.Add("Yes");
                    choixVerre.Items.Add("No");

                    Label color = new Label();
                    color.Text = "Chose the color of the door: ";
                    Picker choiceColor = new Picker();
                    choiceColor.Items.Add("Marron");
                    choiceColor.Items.Add("Black");
                    choiceColor.Items.Add("White");
                    

                    Label height = new Label();
                    height.Text = "Chose the height of the box: ";
                    Picker choixHeight = new Picker();
                    choixHeight.Items.Add("32cm");
                    choixHeight.Items.Add("42cm");
                    choixHeight.Items.Add("52cm");
                    
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
                    MyGrid.SetRow(boxe, 6 + rowDecale);

                    MyGrid.SetColumn(porte, 0 + space);
                    MyGrid.SetRow(porte, 6 + rowDecale);
                    MyGrid.SetColumn(choixPorte, 0 + space);
                    MyGrid.SetRow(choixPorte, 6 + rowDecale);

                    MyGrid.SetColumn(verre, 0 + space);
                    MyGrid.SetRow(verre, 7 + rowDecale);
                    MyGrid.SetColumn(choixVerre, 0 + space);
                    MyGrid.SetRow(choixVerre, 7 + rowDecale);

                    MyGrid.SetColumn(color, 0 + space);
                    MyGrid.SetRow(color, 8 + rowDecale);
                    MyGrid.SetColumn(choiceColor, 0 + space);
                    MyGrid.SetRow(choiceColor, 8 + rowDecale);

                    MyGrid.SetColumn(height, 0 + space);
                    MyGrid.SetRow(height, 9 + rowDecale);
                    MyGrid.SetColumn(choixHeight, 0 + space);
                    MyGrid.SetRow(choixHeight, 9 + rowDecale);

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
                confirm.Text = "Confirm";
                MyGrid.SetColumn(confirm, 1);
                MyGrid.SetRow(confirm, 10 + rowDecale);
                MyGrid.Children.Add(confirm);
                

            }
        }

}