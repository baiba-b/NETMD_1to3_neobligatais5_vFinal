using bb23028_MD1;
using CommunityToolkit.Mvvm.Messaging;

namespace bb23028MD2;

public partial class SubmissionListView : ContentPage
{

    public InterfaceImplementation? IntImp; //Definēts mainīgais, lai varētu tajā ierakstīt Item datus.

    public SubmissionListView()
    {
        InitializeComponent();
        IntImp = App.IntImp; //Izveidots, lai inicializētu InterfaceImplementation.
        BindingContext = this; //Vajadzīgs, lai izmantotu bindingu.
    }
    public List<bb23028_MD1.Submission>? SubmissionList //īpašību, ko rādīs CollectionView Binding (no šejienes ņems datus)
    {
        get { return IntImp?.collection._submissionList; }
    }
    private async void EditClicked(object sender, EventArgs e)
    {
        var b = sender as Button;
        if (b != null)
        {
            if (b.BindingContext is Submission) //pārbauda vai tas ir Assignment
            {
                var submission = (Submission)b.BindingContext; //Cast par Assignment
                var sPage = new CreateSubmission(submission);
                await Navigation.PushAsync(sPage); //aizved uz createSubmission page
            }
        }
    }

    private async void DeleteClicked(object sender, EventArgs e) //nepieciešams async tā kā tiek lietots "await"
    {
        //kods ņemts no pasniedzējas piemēra 7.lekcijā

        var b = sender as Button; //castot sender kā pogu
        if (b != null)
        {
            var submission = b.BindingContext as Submission; //Pointeris uz konkrētu submission,  lai varētu izvēlēto izdzēst.
            if (submission != null)
            {
                bool answer = await DisplayAlert("Caution", "Would you like to delete this assignment?", "Yes", "No"); //Paziņojums, lai pārliecinātos, vai vēlas izdzēst
                if (answer)
                {
                    IntImp?.collection._submissionList?.Remove(submission);
                    BindingContext = null; //Atjaunina skatu
                    BindingContext = this;
                    if (UniversityComponents.timeClicked > 0) //atjauno printēto saraksts, ja tas ir iepriekš atvērts
                    {
                        WeakReferenceMessenger.Default.Send(new UpdateResultMessage(App.IntImp.collection));
                    }
                }
            }
        }
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e) //Lai bindingi atjaunos
    {
        BindingContext = null;
        BindingContext = this;
    }

}