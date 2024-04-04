
using CommunityToolkit.Maui.Views;
namespace aaa;
public partial class AddSuppPopup : Popup
{
    public AddSuppPopup()
    {
        //this.Size = new Size(800,600);
        InitializeComponent();
    }
    async void OnAddSuppConfirmed(object sender, EventArgs e){
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        CompoSupplier supplier = new CompoSupplier(Int32.Parse(IdEntry.Text), float.Parse(PriceEntry.Text), Int32.Parse(DelayEntry.Text));
        await CloseAsync(supplier, cts.Token);
    }
}