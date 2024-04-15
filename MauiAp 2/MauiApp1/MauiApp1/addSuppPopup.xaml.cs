
using CommunityToolkit.Maui.Views;
namespace aaa;
public partial class AddSuppPopup : Popup
{
    public AddSuppPopup()
    {
        
        InitializeComponent();
        this.Size = new Size(600,215);
    }
    async void OnAddSuppConfirmed(object sender, EventArgs e){
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        CompoSupplier supplier = new CompoSupplier(Int32.Parse(IdEntry.Text), float.Parse(PriceEntry.Text), 
        Int32.Parse(DelayEntry.Text), Int32.Parse(StockAEntry.Text),0, Int32.Parse(StockREntry.Text), Int32.Parse(MinStockEntry.Text));
        await CloseAsync(supplier, cts.Token);
    }
}