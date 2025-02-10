namespace bb23028MD2;
using bb23028_MD1;
using CommunityToolkit.Mvvm.Messaging;
using System.Text.RegularExpressions; //Lai varētu pārbaudīt, ka satur tikai burtus


public partial class CreateSubmission : ContentPage
{
    // tiek izmantota pasniedzējas rādītā metode bindingam no 7.lekcijas
    public InterfaceImplementation? IntImp;
    private bool _reload = false; //mainīgais, lai nerādītu brīdinājumus, restartējot saturu p
    private bool _isEditing = false; //mainīgais, lai neidzēš datus pie editing


    public CreateSubmission()
    {
        InitializeComponent();
        IntImp = App.IntImp;
        BindingContext = this; //norāda, ka jāmeklē bindotos datus šajā lapā.
        _isEditing = false;

    }
    
    private Submission _s;
    // Kods ņemts no pasniedzējas piemēra 7.lekcijā. Šis konstruktors tiek izmantots, lai rediģētu "submission".
    public CreateSubmission(Submission s)
    {
        InitializeComponent();
        IntImp = App.IntImp;
        BindingContext = this; //norāda, ka jāmeklē bindotos datus šajā lapā.
        _s = s;
        StudentPicker.ItemsSource = StudentList;
        StudentPicker.SelectedItem = _s.Student;

        AssignmentPicker.ItemsSource = AssignmentList;
        AssignmentPicker.SelectedItem = _s.Assignment;

        DateTxt.Date = _s.SubmissionTime.Date;
        TimeTxt.Time = _s.SubmissionTime.TimeOfDay;

        ScoreTxt.Text = _s.Score.ToString();
        CreateSubmissionBtn.IsVisible = false; //lai nekopētu kodu, parādas edit poga create pogas vietā
        EditSubmissionBtn.IsVisible = true;
        _isEditing = true;

    }

    //tiek izmantotas īpašības StudentList un AssignmentList bindingam
    public List<bb23028_MD1.Student> StudentList
    {
        get { return IntImp.collection._studentList; }

    }
    public List<bb23028_MD1.Assignement>? AssignmentList
    {
        get { return IntImp.collection._assignementList; }
    }

    private void CreateSubmissionBtn_clicked(object sender, EventArgs e)
    {
        var submission = new Submission();
        submission.Student = StudentPicker.SelectedItem as bb23028_MD1.Student;
        submission.Assignment = AssignmentPicker.SelectedItem as bb23028_MD1.Assignement;
        submission.SubmissionTime = DateTxt.Date + TimeTxt.Time;
        submission.Score = int.Parse(ScoreTxt.Text);
        ResultLbl.Text = submission.ToString();
        App.IntImp?.collection.Add(submission);
        if (UniversityComponents.timeClicked > 0) //atjauno printēto saraksts, ja tas ir iepriekš atvērts
        {
            WeakReferenceMessenger.Default.Send(new UpdateResultMessage(App.IntImp.collection));
        }
    }
    

    private void EditSubmissionBtn_clicked(object sender, EventArgs e)
    {
        _s.Student = StudentPicker.SelectedItem as bb23028_MD1.Student;
        _s.Assignment = AssignmentPicker.SelectedItem as bb23028_MD1.Assignement;
        _s.SubmissionTime = DateTxt.Date + TimeTxt.Time;
        _s.Score = int.Parse(ScoreTxt.Text);
        ResultLbl.Text = _s.ToString();
        if (UniversityComponents.timeClicked > 0) //atjauno printēto saraksts, ja tas ir iepriekš atvērts
        {
            WeakReferenceMessenger.Default.Send(new UpdateResultMessage(App.IntImp.collection));
        }

    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs e) //Kļūdu paziņojumi, ja nepareizi ievada datus
    {
        if (_reload) return; //nerāda kļūdu paziņojumus, ja tas ir pēc ClearFields()
        if (!(Regex.IsMatch(ScoreTxt.Text, @"^[0-9]*$")))
        {
            DisplayAlert("Warning", "This can only contain numbers!", "OK");
            if (ScoreTxt.Text.Length != 0) ScoreTxt.Text = ScoreTxt.Text.Remove(ScoreTxt.Text.Length - 1); //Noņem pēdējo ievadīto simbolu. Pieliku != 0, jo citādi rādija error, ka initial index nevar būt < 0.
            CreateSubmissionBtn.IsEnabled = false;
            EditSubmissionBtn.IsEnabled = false;
        }
        if (ScoreTxt.Text != null && ScoreTxt.Text != "" && int.Parse(ScoreTxt.Text) > 100)
        {
            DisplayAlert("Warning", "Score must be between 0-100", "OK");
            CreateSubmissionBtn.IsEnabled = false;
            EditSubmissionBtn.IsEnabled = false;
        }

        if (ScoreTxt.Text == "" || ScoreTxt.Text == null)
        {
            DisplayAlert("Warning", "This field is obligatory!", "OK");
            CreateSubmissionBtn.IsEnabled = false;
            EditSubmissionBtn.IsEnabled = false;
        }
    }

    private void ValidateFields(object sender, FocusEventArgs e) //Pārbaude, vai ierakstītie dati atbilst sagaidītajam formātam
    {
        bool isScoreValid = !string.IsNullOrWhiteSpace(ScoreTxt.Text);
        bool isStudentSelected = StudentPicker.SelectedIndex >= 0;
        bool isAssignmentSelected = AssignmentPicker.SelectedIndex >= 0;

        if (isScoreValid && isStudentSelected && isAssignmentSelected && int.Parse(ScoreTxt.Text) <= 100)
        {
            CreateSubmissionBtn.IsEnabled = true;
            EditSubmissionBtn.IsEnabled = true;

        }
        else
        {
            CreateSubmissionBtn.IsEnabled = false;
            EditSubmissionBtn.IsEnabled = false;

        }
    }
    private void ClearFields() //restartē visas vērtības
    {
        ScoreTxt.Text = string.Empty;
        DateTxt.Date = DateTime.Today;
        TimeTxt.Time = DateTime.Now.TimeOfDay;
        CreateSubmissionBtn.IsEnabled = false;
        EditSubmissionBtn.IsEnabled = false;
        _reload = true;
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = null;
        BindingContext = this;
        if(!_isEditing) ClearFields();
    }

}
