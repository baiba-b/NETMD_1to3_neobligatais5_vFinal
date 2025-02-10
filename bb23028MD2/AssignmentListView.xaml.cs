﻿namespace bb23028MD2;
using bb23028_MD1;
using CommunityToolkit.Mvvm.Messaging;

public partial class AssignmentListView : ContentPage //Komentāri tie paši, kas SubmissionListView
{
    public InterfaceImplementation dm;
    public AssignmentListView()
	{
        dm = App.IntImp;
        BindingContext = this; 
        InitializeComponent();
	}

    public List<bb23028_MD1.Assignement> AssignmentList
    {
        get { return dm.collection._assignementList; }
    }

    private async void EditClicked(object sender, EventArgs e)
    {
        var b = sender as Button;
        if (b != null)
        {
            if (b.BindingContext is Assignement)
            {
                var a = (Assignement)b.BindingContext; 
                var aPage = new CreateAssignment(a); 
                await Navigation.PushAsync(aPage);
            }
        }
    }

    private async void DeleteClicked(object sender, EventArgs e) //async await dēļ
    {
        var b = sender as Button;
        if (b != null)
        {
            var f = b.BindingContext as Assignement;
            if (f != null)
            {
                bool answer = await DisplayAlert("Question?", "Would you like to delete this assignment?", "Yes", "No");
                if (answer)
                {
                    dm.collection._assignementList.Remove(f);
                    BindingContext = null;
                    BindingContext = this;
                    if (UniversityComponents.timeClicked > 0) //atjauno printēto saraksts, ja tas ir iepriekš atvērts
                    {
                        WeakReferenceMessenger.Default.Send(new UpdateResultMessage(App.IntImp.collection));
                    }
                }
            }
        }
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext=null;
        BindingContext=this;

    }

}