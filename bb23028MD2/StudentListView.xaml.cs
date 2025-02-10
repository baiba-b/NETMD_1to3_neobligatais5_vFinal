using bb23028_MD1;

namespace bb23028MD2;

public partial class StudentListView : ContentPage
{
    public DBInterfaceImplementation? IntImp;

    public StudentListView()
	{
        IntImp = App.IntImp;
        BindingContext = this;
        InitializeComponent();
	}

    public List<bb23028_MD1.Student>? StudentList
    {
        get { return IntImp.StudentLists(); }
    }

    private async void EditClicked(object sender, EventArgs e)
    {
        var b = sender as Button;
        if (b != null)
        {
            if (b.BindingContext is Student)
            {
                var s = (Student)b.BindingContext;

                var aPage = new CreateStudent(s);
                await Navigation.PushAsync(aPage);
            }
        }
    }

    private async void DeleteClicked(object sender, EventArgs e) //async await dēļ
    {
        var b = sender as Button;
        if (b != null)
        {
            var f = b.BindingContext as Student;
            if (f != null)
            {
                bool answer = await DisplayAlert("Question?", "Would you like to delete this student?", "Yes", "No");
                if (answer)
                {
                    IntImp.StudentRemove(f);
                    IntImp.Save();
                    BindingContext = null;
                    BindingContext = this;
                    //if (UniversityComponents.timeClicked > 0) //atjauno printēto saraksts, ja tas ir iepriekš atvērts
                    //{
                    //    WeakReferenceMessenger.Default.Send(new UpdateResultMessage(App.IntImp.collection));
                    //}
                }
            }
        }
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = null;
        BindingContext = this;

    }

}