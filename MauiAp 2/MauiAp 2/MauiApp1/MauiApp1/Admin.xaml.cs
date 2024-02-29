using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariousExceptions;


namespace MauiApp1;




public partial class Admin : ContentPage
{
    DBConnection connection;
    public Admin()
    {
        InitializeComponent();
        connection = new DBConnection("verifUser","1234","projet");
    }
    
    void RetourMenu(object sender, EventArgs e){
        this.connection.disconnection();
        Page menu = new MainPage();
        Navigation.PushModalAsync(new MainPage());
    }

    void connexion(object sender, EventArgs e)
    {
        try{
            Account verif = this.connection.getUserAccount(myEntry2.Text);
            if (verif.isPasswdGood(myEntry.Text))
            {
                switch (verif.function)
                {
                    case "Stock Manager": 
                        this.connection.disconnection();
                        Manager main = new Manager();
                        Navigation.PushModalAsync(new Manager());
                        break;
                    case "Secretary":
                        this.connection.disconnection();
                        Secretary main2 = new Secretary();
                        Navigation.PushModalAsync(new Secretary());
                        break;
                    case "Stock Keeper":
                        this.connection.disconnection();
                        StockKeeper main3 = new StockKeeper();
                        Navigation.PushModalAsync(new StockKeeper());
                        break;
                    
                    default:
                        throw new ErrorConnectionException("Unknow Function or not have autorisation");
                        break;
                }
                    
            }else if(!verif.isPasswdGood(myEntry.Text)){
                throw new ErrorConnectionException("Password is not good");
            }
        }catch(ErrorConnectionException err){
            wPasswd.Text = err.Message;
            wPasswd.IsVisible=true;
        }
        
        
    }
}