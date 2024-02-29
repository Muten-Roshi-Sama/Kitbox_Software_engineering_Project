public class Component{
    public string Id{get;set;}
    public string Reference{get;set;}
    public string Code{get;set;}
    public string Dimension{get;set;}
    public string color{get;set;}
    public float priceSupplier1{get;set;}
    public int delaySupplier1{get;set;}
    public float priceSupplier2{get;set;}
    public int delaySupplier2{get;set;}
    public int stockAvailable{get;set;}
    public int stockOrdered{get;set;}
    public int stockReserved{get;set;}
    public bool isEditingL{get;set;}
    public bool isEditingE{get;set;}
    
    public Component(int id, string reference, string code, string dimension, int color, float price1,
        int delay1, float price2, int delay2, int stockAvailable, int stockOrdered, int stockReserved, 
        bool editL, bool editE){

        this.Id = id.ToString();
        this.Reference = reference;
        this.Code = code;
        this.Dimension = dimension;
        
        switch (color){
            case 1: this.color = "White";break;
            case 2: this.color = "Black";break;
            case 3: this.color = "Marron";break;
            case 4: this.color = "Galva";break;
            default: this.color = "No color";break;
        }
        this.priceSupplier1 = price1;
        this.delaySupplier1 = delay1;
        this.priceSupplier2 = price2;
        this.delaySupplier2 = delay2;
        this.stockAvailable = stockAvailable;
        this.stockOrdered = stockOrdered;
        this.stockReserved = stockReserved;

        this.isEditingE = editE;
        this.isEditingL = editL;
    }
}