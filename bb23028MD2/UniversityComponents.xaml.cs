namespace bb23028MD2;
using bb23028_MD1;
using CommunityToolkit.Mvvm.Messaging; //Importēju CommunityToolKit messaging, jo iepriekš izmantoju MessagingCetre, bet tas deva brīdinājumus, ka ir "obsolete".
using CommunityToolkit.Mvvm.Messaging.Messages;
public partial class UniversityComponents : ContentPage
{
    public static int timeClicked = 0; //Mainīgais, kas pasaka, vai tika izmantota Print() mentode, vai nē. Tas tiek pielietots, lai zinātu, kad vajag atjaunot Print() datus.

    public UniversityComponents()
    {
        InitializeComponent();
        //radās problēma, ka, kad tiek mainīta collection, izmantojot "Edit" vai "Create", izmaiņas nerādas
        //OnPrintClicked() izdrukātajā sarakstā, tādēļ implementēju Messenger, izmantojot: https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/messenger#sending-and-receiving-messages
      
        WeakReferenceMessenger.Default.Register<UpdateResultMessage>(this, (r, m) => //Saņem "ziņas", ka vajag atjaunot printēto sarakstu
        {
            OnPrintClicked(this, EventArgs.Empty);}
        );

    }
    public void OnPrintClicked(object sender, EventArgs e) //Izprintē visus kolekcijā esošos datus.
    {
        ResultLbl.Text = App.IntImp?.Print();
        timeClicked++; //norāda, ka saraksts tika atvērts, lai to varētu atjaunot pēc datu rediģēšanas/pievienošanas
    }
    private void OnTestDataCreationClicked(object sender, EventArgs e) //Izveido testa datus
    {
        App.IntImp?.CreateTestData();
    }

    private void OnLoadClicked(object sender, EventArgs e) //Ielādē datus no faila
    {
        App.IntImp?.Load("C:\\Temp\\savedFile.xml");
    }

    private void OnSaveClicked(object sender, EventArgs e) //Saglabā datus failā
    {
        App.IntImp?.Save("C:\\Temp\\savedFile.xml");
    }

    private void OnRestartClicked(object sender, EventArgs e) //Izdzēš datus no kolekcijas
    {
        App.IntImp?.Reset();
    }

}