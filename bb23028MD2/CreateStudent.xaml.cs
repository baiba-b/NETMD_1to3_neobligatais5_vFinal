namespace bb23028MD2;
using bb23028_MD1;
using CommunityToolkit.Mvvm.Messaging;
using System.Text.RegularExpressions; //Lai varētu pārbaudīt, ka satur tikai burtus
using static System.Net.Mime.MediaTypeNames;

public partial class CreateStudent : ContentPage
{
    private bool _reload = false;

    public CreateStudent()
    {
        InitializeComponent();
        GenderPicker.SelectedIndex = 2; //noklusētais gender ir "Other", kods ņemts no pasniedzējas piemēra lekcijā
    }
    public void ValidateFields(object sender, EventArgs e) //Pārbaude, vai ierakstītie dati atbilst sagaidītajam formātam
    {
        bool isNameValid = !string.IsNullOrWhiteSpace(NameTxt.Text);
        bool isSurnameValid = !string.IsNullOrWhiteSpace(SurnameTxt.Text);
        bool isStudentIdValid = !string.IsNullOrWhiteSpace(studentIDTxt.Text);

        if (isNameValid && isSurnameValid && isStudentIdValid) CreateStudentBtn.IsEnabled = true;
        else CreateStudentBtn.IsEnabled = false;
    }
    private void CreateStudentBtn_Clicked(object sender, EventArgs e)
    {
        var student = new Student();
        student.Name = NameTxt.Text;
        student.Surname = SurnameTxt.Text;
        student.StudentIdNumder = studentIDTxt.Text;
        student.PersonGender = (bb23028_MD1.Gender)Enum.Parse(typeof(bb23028_MD1.Gender), (string)GenderPicker.SelectedItem);
        ResultLbl.Text = student.ToString();
        App.IntImp?.collection.Add(student);
        if (UniversityComponents.timeClicked > 0) //atjauno printēto saraksts, ja tas ir iepriekš atvērts
        {
            WeakReferenceMessenger.Default.Send(new UpdateResultMessage(App.IntImp.collection));
        }
    }

    private void OnEntryTextChanged(object sender, EventArgs e)
    {
        if (_reload) return; //nerāda kļūdu paziņojumus, ja tas ir pēc ClearFields()
        string text = ((Entry)sender).Text;//Saņem ievadīto tekstu. Ņemts no: https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/entry?view=net-maui-8.0
        if (text == "" || text == null)
        {
            DisplayAlert("Warning", "This field is obligatory!", "OK");
            CreateStudentBtn.IsEnabled = false;
        }
        else if (!(Regex.IsMatch(text, @"^[a-zA-Z]*$")))
        {
            DisplayAlert("Warning", "This can only contain letters!", "OK");
            if (text.Length != 0) text = text.Remove(text.Length - 1); //Noņem pēdējo ievadīto simbolu. Pieliku != 0, jo citādi rādija error, ka initial index nevar būt < 0.
            ((Entry)sender).Text = text;
            CreateStudentBtn.IsEnabled = false;
        }
    }
    
    private void OnIDTextChanged(object sender, TextChangedEventArgs e)
    {
        if (_reload) return; //nerāda kļūdu paziņojumus, ja tas ir pēc ClearFields()
        if (string.IsNullOrWhiteSpace(((Entry)sender).Text))
        {
            DisplayAlert("Warning", "This field is obligatory!", "OK");
            CreateStudentBtn.IsEnabled = false;
        }
    }

    private void ClearFields() //restartē visas vērtības
    {
        NameTxt.Text = string.Empty;
        SurnameTxt.Text = string.Empty;
        studentIDTxt.Text = string.Empty;
        GenderPicker.SelectedIndex = 2;
        CreateStudentBtn.IsEnabled = false;
        _reload = true;
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        ClearFields();
    }

}