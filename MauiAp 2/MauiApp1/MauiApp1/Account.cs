public class Account{
    public string user{get;set;}
    public string function{get;}
    private string passwd;

    public Account(string user, string passwd, string function){
        this.user = user;
        this.passwd = passwd;
        this.function = function;
    }

    public Boolean isPasswdGood(String passwd){
        return this.passwd==passwd;
    }
}