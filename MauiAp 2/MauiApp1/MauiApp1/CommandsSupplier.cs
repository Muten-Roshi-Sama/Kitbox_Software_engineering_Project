using System.ComponentModel;

public class CommandsSupplier: INotifyPropertyChanged
{

    public List<SupplierCompoOrder> ordersList{get;set;}
    public String referenceGlobal{get;set;}
    private Boolean _received;
    public Boolean received{
        get { return _received; }
        set
        {
            if (_received != value)
            {
                _received = value;
                OnPropertyChanged("received");
            }
        }
    }
    private Boolean _notReceived;
    public Boolean notReceived{
        get { return _notReceived; }
        set
        {
            if (_notReceived != value)
            {
                _notReceived = value;
                OnPropertyChanged("notReceived");
            }
        }
    }

    public CommandsSupplier(String Reference, List<SupplierCompoOrder> ordersList, Boolean Received){
        this.ordersList = ordersList;
        this.referenceGlobal = Reference;
        this.received = Received;
        this.notReceived = !Received;
    }
    public CommandsSupplier(String Reference, Boolean Received){
        this.ordersList = new List<SupplierCompoOrder>();
        this.referenceGlobal = Reference;
        this.received = Received;
        this.notReceived = !Received;
    }

    public void addCompoSupp(SupplierCompoOrder compo){
        ordersList.Add(compo);
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}