namespace MauiApp1.View;
using MauiApp1;
// using MauiApp1.Models

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        
        InitializeComponent();
        
    }
    
    
    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        //if (count == 1)
            //CounterBtn.Text = $"Clicked {count} time";
        //else
            //CounterBtn.Text = $"Clicked {count} times";

        //SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void OnCounterClicked2(object sender, EventArgs e)
    {
        Console.WriteLine("COUCOU");
    }


    private void ChargeAdmin(object sender, EventArgs e)
    {
        

        Page admin = new Admin();
        Navigation.PushModalAsync(new Admin());
    }

    private void ChargeCompose(object sender, EventArgs e)
    {
        Page compose = new Compose();
        Navigation.PushModalAsync(new Compose());
    }
}