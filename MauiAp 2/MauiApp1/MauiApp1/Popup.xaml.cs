
using CommunityToolkit.Maui.Views;
namespace aaa;
public partial class SimplePopup : Popup
{
    private int id{get;}
    public SimplePopup(int id)
    {
        this.id = id;
        InitializeComponent();
    }
    async void OnAddLineConfirmed(object sender, EventArgs e){
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        Component compo = new Component((this.id+1),ReferenceEntry.Text,CodeEntry.Text,Int32.Parse(LengthEntry.Text),
        Int32.Parse(HeightEntry.Text),Int32.Parse(DepthEntry.Text),Int32.Parse(ColorEntry.Text),
        float.Parse(priceSupplier1Entry.Text),Int32.Parse(delaySupplier1Entry.Text),
        float.Parse(priceSupplier2Entry.Text),Int32.Parse(delaySupplier2Entry.Text),Int32.Parse(AvailableEntry.Text),
        Int32.Parse(OrderedEntry.Text),Int32.Parse(ReservedEntry.Text),true,false);
        await CloseAsync(compo, cts.Token);
        //AddLinePopup.IsVisible = false;
    }
}