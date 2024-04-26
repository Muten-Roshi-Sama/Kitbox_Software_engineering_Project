using System.ComponentModel;


public class CompoSupplier: INotifyPropertyChanged{
    public int idSupplier{get;set;}
    private float _priceSupplier;
    public float priceSupplier{
        get { return _priceSupplier; }
        set
        {
            if (_priceSupplier != value)
            {
                _priceSupplier = value;
                OnPropertyChanged("priceSupplier");
            }
        }
    }
    private int _delaySupplier;
    public int delaySupplier{
        get { return _delaySupplier; }
        set
        {
            if (_delaySupplier != value)
            {
                _delaySupplier = value;
                OnPropertyChanged("delaySupplier");
            }
        }
    }

    private bool _isSuppEditingL;
    public bool isSuppEditingL
    {
        get { return _isSuppEditingL; }
        set
        {
            if (_isSuppEditingL != value)
            {
                _isSuppEditingL = value;
                OnPropertyChanged("isSuppEditingL");
            }
        }
    }
    private bool _isSuppEditingE;
    public bool isSuppEditingE
    {
        get { return _isSuppEditingE; }
        set
        {
            if (_isSuppEditingE != value)
            {
                _isSuppEditingE = value;
                OnPropertyChanged("isSuppEditingE");
            }
        }
    }
    private bool _showOrderBtn;
    public bool showOrderBtn
    {
        get { return _showOrderBtn; }
        set
        {
            if (_showOrderBtn != value)
            {
                _showOrderBtn = value;
                OnPropertyChanged("showOrderBtn");
            }
        }
    }

    private bool _showRemoveOrderBtn;
    public bool showRemoveOrderBtn
    {
        get { return _showRemoveOrderBtn; }
        set
        {
            if (_showRemoveOrderBtn != value)
            {
                _showRemoveOrderBtn = value;
                OnPropertyChanged("showRemoveOrderBtn");
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

    private int _minimumStock;
    public int minimumStock
    {
        get { return _minimumStock; }
        set
        {
            if (_minimumStock != value)
            {
                _minimumStock = value;
                OnPropertyChanged("minimumStock");
            }
        }
    }

    public CompoSupplier(int id, float price, int delay, int stockAvailable, int? stockOrdered, int stockReserved, int minimumStock){
        this.idSupplier = id;
        this.priceSupplier = price;
        this.delaySupplier = delay;
        this.stockAvailable = stockAvailable;
        if(stockOrdered is null){
            this.stockOrdered = 0;
        }else{
            this.stockOrdered = (int)stockOrdered;
        }
        
        this.stockReserved = stockReserved;
        this.isSuppEditingL = true;
        this.isSuppEditingE = false;
        this.showOrderBtn = false;
        this.showRemoveOrderBtn = false;
        this.minimumStock = minimumStock;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class Component: INotifyPropertyChanged{
    public string reference{get;set;}
    public string code{get;set;}
    public int length{get;set;}
    public int height{get;set;}
    public int depth{get;set;}
    public string color{get;set;}
    public List<CompoSupplier> listSuppliers{get;set;}
    
    private int _GlobalStockAvailable;
    public int GlobalStockAvailable
    {
        get { return _GlobalStockAvailable; }
        set
        {
            if (_GlobalStockAvailable != value)
            {
                _GlobalStockAvailable = value;
                OnPropertyChanged("GlobalStockAvailable");
            }
        }
    }
    private int _GlobalStockOrdered;
    public int GlobalStockOrdered
    {
        get { return _GlobalStockOrdered; }
        set
        {
            if (_GlobalStockOrdered != value)
            {
                _GlobalStockOrdered = value;
                OnPropertyChanged("GlobalStockOrdered");
            }
        }
    }
    private int _GlobalStockReserved;
    public int GlobalStockReserved
    {
        get { return _GlobalStockReserved; }
        set
        {
            if (_GlobalStockReserved != value)
            {
                _GlobalStockReserved = value;
                OnPropertyChanged("GlobalStockReserved");
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

    public Component( string reference, string code, int length, int height, int depth, int color,
        bool editL, bool editE){

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

        this.GlobalStockAvailable = 0;
        this.GlobalStockOrdered = 0;
        this.GlobalStockReserved = 0;

        this.isEditingE = editE;
        this.isEditingL = editL;
        this.infoSupOff = true;
        this.infoSupOn = false;
        this.listSuppliers = new List<CompoSupplier>();
    }
    public static int getColorCode(string color){
        switch (color){
            case "White": return 1;break;
            case "Black": return 2;break;
            case "Marron": return 3;break;
            case "Galva": return 4;break;
            default: return 0;break;
        }
    }

    public void addSupplier(CompoSupplier supplier){
        this.listSuppliers.Add(supplier);
        OnPropertyChanged("listSuppliers");
    }
    public void modifySupplier(int id, float? price=null, int? delay=null){
        foreach (var supp in listSuppliers)
        {
            if(supp.idSupplier == id){
                if(price is not null){
                    supp.priceSupplier = (float)price;
                }
                if(delay is not null){
                    supp.delaySupplier = (int)delay;
                }
                break;
            }
        }
        OnPropertyChanged("listSuppliers");
    }

    public void deleteSupplier(int id){
        foreach (var supp in listSuppliers)
        {
            if(supp.idSupplier==id){
                listSuppliers.Remove(supp);
                break;
            }   
        }
        OnPropertyChanged("listSuppliers");
    }

    public void setGeneralStock(){
        this.GlobalStockAvailable = 0;
        this.GlobalStockOrdered = 0;
        this.GlobalStockReserved = 0;
        foreach (var supplier in listSuppliers)
        {
            this.GlobalStockAvailable += supplier.stockAvailable;
            this.GlobalStockOrdered += supplier.stockOrdered;
            this.GlobalStockReserved += supplier.stockReserved;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}