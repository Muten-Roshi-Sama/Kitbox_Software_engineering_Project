using System.ComponentModel;

public class Component: INotifyPropertyChanged{
    public string id{get;set;}
    public string reference{get;set;}
    public string code{get;set;}
    public int length{get;set;}
    public int height{get;set;}
    public int depth{get;set;}
    public string color{get;set;}
    private float _priceSupplier1;
    public float priceSupplier1
    {
        get { return _priceSupplier1; }
        set
        {
            if (_priceSupplier1 != value)
            {
                _priceSupplier1 = value;
                OnPropertyChanged("priceSupplier1");
            }
        }
    }
    private int _delaySupplier1;
    public int delaySupplier1
    {
        get { return _delaySupplier1; }
        set
        {
            if (_delaySupplier1 != value)
            {
                _delaySupplier1 = value;
                OnPropertyChanged("delaySupplier1");
            }
        }
    }
    private float _priceSupplier2;
    public float priceSupplier2
    {
        get { return _priceSupplier2; }
        set
        {
            if (_priceSupplier2 != value)
            {
                _priceSupplier2 = value;
                OnPropertyChanged("priceSupplier2");
            }
        }
    }
    private int _delaySupplier2;
    public int delaySupplier2
    {
        get { return _delaySupplier2; }
        set
        {
            if (_delaySupplier2 != value)
            {
                _delaySupplier2 = value;
                OnPropertyChanged("delaySupplier2");
            }
        }
    }
    private int _stockAvailable;
    public int stockAvailable
    {
        get { return _stockAvailable; }
        set
        {
            if (_stockAvailable != value)
            {
                _stockAvailable = value;
                OnPropertyChanged("stockAvailable");
            }
        }
    }
    private int _stockOrdered;
    public int stockOrdered
    {
        get { return _stockOrdered; }
        set
        {
            if (_stockOrdered != value)
            {
                _stockOrdered = value;
                OnPropertyChanged("stockOrdered");
            }
        }
    }
    private int _stockReserved;
    public int stockReserved
    {
        get { return _stockReserved; }
        set
        {
            if (_stockReserved != value)
            {
                _stockReserved = value;
                OnPropertyChanged("stockReserved");
            }
        }
    }
    private bool _isEditingL;
    public bool isEditingL
    {
        get { return _isEditingL; }
        set
        {
            if (_isEditingL != value)
            {
                _isEditingL = value;
                OnPropertyChanged("isEditingL");
            }
        }
    }
    private bool _isEditingE;
    public bool isEditingE
    {
        get { return _isEditingE; }
        set
        {
            if (_isEditingE != value)
            {
                _isEditingE = value;
                OnPropertyChanged("isEditingE");
            }
        }
    }
    private bool _infoSupOn;
    public bool infoSupOn
    {
        get { return _infoSupOn; }
        set
        {
            if (_infoSupOn != value)
            {
                _infoSupOn = value;
                OnPropertyChanged("infoSupOn");
            }
        }
    }
    private bool _infoSupOff;
    public bool infoSupOff
    {
        get { return _infoSupOff; }
        set
        {
            if (_infoSupOff != value)
            {
                _infoSupOff = value;
                OnPropertyChanged("infoSupOff");
            }
        }
    }
    
    
    public Component(int id, string reference, string code, int length, int height, int depth, int color, float price1,
     int delay1, float price2, int delay2, int stockAvailable, int stockOrdered, int stockReserved, 
     bool editL, bool editE){

        this.id = id.ToString();
        this.reference = reference;
        this.code = code;
        this.length = length;
        this.height = height;
        this.depth = depth;
        
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
        this.infoSupOff = true;
        this.infoSupOn = false;
    }
    public int getColorCode(){
        switch (this.color){
            case "White": return 1;break;
            case "Black": return 2;break;
            case "Marron": return 3;break;
            case "Galva": return 4;break;
            default: return 0;break;
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}