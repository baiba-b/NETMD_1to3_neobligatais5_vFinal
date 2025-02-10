namespace bb23028MD2;
using bb23028_MD1;
public partial class UniversityComponents : ContentPage
{
    public UniversityComponents()
    {
        InitializeComponent();
    }
    public void OnPrintClicked(object sender, EventArgs e) //Izprintē visus kolekcijā esošos datus.
    {
        ResultLbl.Text = App.IntImp?.Print();
    }
    private void OnTestDataCreationClicked(object sender, EventArgs e) //Izveido testa datus DB
    {
        App.IntImp?.CreateTestData();
    }

    private void OnRestartClicked(object sender, EventArgs e) //Izdzēš datus no DB
    {
        App.IntImp?.Reset();
    }
    //izsauc print funkciju, kad navigē uz lapu, lai tiktu atjaunoti parādītie dati
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        ResultLbl.Text = App.IntImp?.Print();
    }
}