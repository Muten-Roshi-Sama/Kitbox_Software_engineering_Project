
using CommunityToolkit.Maui.Views;
using Google.Protobuf;
namespace aaa;
public partial class SimplePopup : Popup
{
    private List<CompoSupplier> supplierList;
    public SimplePopup()
    {
        this.Size = new Size(910,350);
        this.supplierList = new List<CompoSupplier>();
        this.supplierList.Add(new CompoSupplier(0,0,0,0,0,0,0));
        InitializeComponent();
        SuppListView.ItemsSource = this.supplierList;
    }
    async void OnAddLineConfirmed(object sender, EventArgs e){
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        Component compo = new Component(ReferenceEntry.Text,CodeEntry.Text,Int32.Parse(LengthEntry.Text),
        Int32.Parse(HeightEntry.Text),Int32.Parse(DepthEntry.Text),Component.getColorCode(ColorEntry.Text),
        true,false);
        foreach (var item in supplierList)
        {
            Console.WriteLine(item.priceSupplier);
        }
        compo.listSuppliers = this.supplierList;
        compo.setGeneralStock();
        await CloseAsync(compo, cts.Token);
    }

    void OnButtonClicked(object sender, EventArgs e){
        this.supplierList.Add(new CompoSupplier(0,0,0,0,0,0,0));
        SuppListView.ItemsSource = null;
        SuppListView.ItemsSource = this.supplierList;
    }
}