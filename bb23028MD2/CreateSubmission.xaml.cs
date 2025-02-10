namespace bb23028MD2;
using bb23028_MD1;
using System.Text.RegularExpressions; //Lai varētu pārbaudīt, ka satur tikai burtus
//using static Android.Renderscripts.ScriptGroup;


public partial class CreateSubmission : ContentPage
{
    // tiek izmantota pasniedzējas rādītā metode bindingam no 7.lekcijas
    public DBInterfaceImplementation? IntImp;
    private bool _isEditing = false; //mainīgais, lai neidzēš datus pie editing


    public CreateSubmission()
    {
        InitializeComponent();
        IntImp = App.IntImp;
        BindingContext = this; //norāda, ka jāmeklē bindotos datus šajā lapā.
        _isEditing = false;
        ScoreErrorLbl.IsVisible = false;
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
        ScoreErrorLbl.IsVisible = false;


    }

    //tiek izmantotas īpašības StudentList un AssignmentList bindingam
    public List<bb23028_MD1.Student> StudentList
    {
        get { 
            return IntImp.StudentLists(); 
        }

    }
    public List<bb23028_MD1.Assignement>? AssignmentList
    {
        get { return IntImp.AssignmentLists(); }
    }

    private void CreateSubmissionBtn_clicked(object sender, EventArgs e)
    {
        var submission = new Submission();
        submission.Student = StudentPicker.SelectedItem as bb23028_MD1.Student;
        submission.Assignment = AssignmentPicker.SelectedItem as bb23028_MD1.Assignement;
        submission.SubmissionTime = DateTxt.Date + TimeTxt.Time;
        submission.Score = int.Parse(ScoreTxt.Text);
        ResultLbl.Text = submission.ToString();
        App.IntImp?.Add(submission);
        App.IntImp.Save();

    }


    private void EditSubmissionBtn_clicked(object sender, EventArgs e)
    {
        _s.Student = StudentPicker.SelectedItem as bb23028_MD1.Student;
        _s.Assignment = AssignmentPicker.SelectedItem as bb23028_MD1.Assignement;
        _s.SubmissionTime = DateTxt.Date + TimeTxt.Time;
        _s.Score = int.Parse(ScoreTxt.Text);
        ResultLbl.Text = _s.ToString();
        App.IntImp.Save();
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs e) //Kļūdu paziņojumi, ja nepareizi ievada datus
    {
        if (!Regex.IsMatch(ScoreTxt.Text, @"^\d+$")) 
        {
            ScoreErrorLbl.Text = "Warning! This can only contain numbers!";
            ScoreErrorLbl.IsVisible = true;
            if (ScoreTxt.Text.Length != 0) ScoreTxt.Text = ScoreTxt.Text.Remove(ScoreTxt.Text.Length - 1); //Noņem pēdējo ievadīto simbolu. Pieliku != 0, jo citādi rādija error, ka initial index nevar būt < 0.
            CreateSubmissionBtn.IsEnabled = false;
            EditSubmissionBtn.IsEnabled = false;
        }
        else if (ScoreTxt.Text != null && ScoreTxt.Text != "" && int.Parse(ScoreTxt.Text) > 100)
        {
            ScoreErrorLbl.Text = "Warning! Score must be between 0-100!";
            ScoreErrorLbl.IsVisible = true;
            CreateSubmissionBtn.IsEnabled = false;
            EditSubmissionBtn.IsEnabled = false;
        }
        else if (ScoreTxt.Text == "" || ScoreTxt.Text == null)
        {
            ScoreErrorLbl.Text = "Warning! This field is obligatory!";
            ScoreErrorLbl.IsVisible = true;
            CreateSubmissionBtn.IsEnabled = false;
            EditSubmissionBtn.IsEnabled = false;
        }
        else ScoreErrorLbl.IsVisible = false;

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
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = null;
        BindingContext = this;
        if(!_isEditing) ClearFields();
    }

}
