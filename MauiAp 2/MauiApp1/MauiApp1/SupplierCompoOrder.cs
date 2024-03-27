using System.ComponentModel;

public class SupplierCompoOrder: INotifyPropertyChanged
{

    private int _quantity;
    public int quantity{
        get { return _quantity; }
        set
        {
            if (_quantity != value)
            {
                _quantity = value;
                OnPropertyChanged("quantity");
            }
        }
    }
    private bool _showLabelQtt;
    public bool showLabelQtt{
        get { return _showLabelQtt; }
        set
        {
            if (_showLabelQtt != value)
            {
                _showLabelQtt = value;
                OnPropertyChanged("showLabelQtt");
            }
        }
    }
    private bool _showEntryQtt;
    public bool showEntryQtt{
        get { return _showEntryQtt; }
        set
        {
            if (_showEntryQtt != value)
            {
                _showEntryQtt = value;
                OnPropertyChanged("showEntryQtt");
            }
        }
    }
    public string code{get;set;}
    public string reference{get;set;}
    public int id{get;set;}
    public float price{get;set;}
    public int delay{get;set;}

    public SupplierCompoOrder(string code, string reference, int id, float price, int delay, int quantity){
        this.code = code;
        this.reference = reference;
        this.id = id;
        this.price = price;
        this.delay = delay;
        this.quantity = quantity;
        this.showLabelQtt = true;
        this.showEntryQtt = false;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}