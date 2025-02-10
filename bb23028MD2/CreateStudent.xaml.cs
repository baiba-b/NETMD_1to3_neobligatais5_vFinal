namespace bb23028MD2;
using bb23028_MD1;
using System.Text.RegularExpressions; //Lai varētu pārbaudīt, ka satur tikai burtus
using static System.Net.Mime.MediaTypeNames;

public partial class CreateStudent : ContentPage
{
    public DBInterfaceImplementation? IntImp;
    private bool _isEditing;

    public CreateStudent()
    {
        InitializeComponent();
        GenderPicker.SelectedIndex = 2; //noklusētais gender ir "Other", kods ņemts no pasniedzējas piemēra lekcijā
        StudentIdErrorLbl.IsVisible = false;
        NameErrorLbl.IsVisible = false;
        SurnameErrorLbl.IsVisible = false;
        _isEditing = false;

    }
    private Student _s;
    public CreateStudent(Student s)
    {
        InitializeComponent();
        IntImp = App.IntImp;
        BindingContext = this; //norāda, ka jāmeklē bindotos datus šajā lapā.
        
        GenderPicker.SelectedIndex = 2; //noklusētais gender ir "Other", kods ņemts no pasniedzējas piemēra lekcijā
        _s = s;
        NameTxt.Text = _s.Name.ToString();
        SurnameTxt.Text = _s.Surname.ToString();
        GenderPicker.SelectedItem = _s.PersonGender;
        studentIDTxt.Text = _s.StudentIdNumder.ToString();

        CreateStudentBtn.IsVisible = false; //lai nekopētu kodu, parādas edit poga create pogas vietā
        EditStudentBtn.IsVisible = true;
        _isEditing = true;
        StudentIdErrorLbl.IsVisible = false;
        NameErrorLbl.IsVisible = false;
        SurnameErrorLbl.IsVisible = false;
    }

    public void ValidateFields(object sender, EventArgs e) //Pārbaude, vai ierakstītie dati atbilst sagaidītajam formātam
    {
        bool isNameValid = !string.IsNullOrWhiteSpace(NameTxt.Text);
        bool isSurnameValid = !string.IsNullOrWhiteSpace(SurnameTxt.Text);
        bool isStudentIdValid = !string.IsNullOrWhiteSpace(studentIDTxt.Text);

        if (isNameValid && isSurnameValid && isStudentIdValid)
        {
            CreateStudentBtn.IsEnabled = true;
            EditStudentBtn.IsEnabled = true;

        }
        else
        {
            CreateStudentBtn.IsEnabled = false;
            EditStudentBtn.IsEnabled = false;
        }
    }

    private void OnEntryTextChangedName(object sender, EventArgs e)
    {
        if (NameTxt.Text == "" || NameTxt.Text == null)
        {
            NameErrorLbl.IsVisible = true;
            NameErrorLbl.Text = "Warning! This field is obligatory!";
            CreateStudentBtn.IsEnabled = false;
            EditStudentBtn.IsEnabled = false;

        }
        else if (!(Regex.IsMatch(NameTxt.Text, @"^[a-zA-Z]*$")))
        {
            //šis nedod error label, jo neļauj ievadīt simbolus, kas nav a-Z
            if (NameTxt.Text.Length != 0) NameTxt.Text = NameTxt.Text.Remove(NameTxt.Text.Length - 1); //Noņem pēdējo ievadīto simbolu. Pieliku != 0, jo citādi rādija error, ka initial index nevar būt < 0.
            CreateStudentBtn.IsEnabled = false;
            EditStudentBtn.IsEnabled = false;
        }
        else
        {
            NameErrorLbl.IsVisible = false;

        }
    }
    private void OnEntryTextChangedSurname(object sender, TextChangedEventArgs e)
    {
        if (SurnameTxt.Text == "" || SurnameTxt.Text == null)
        {
            SurnameErrorLbl.IsVisible = true;
            SurnameErrorLbl.Text = "Warning! This field is obligatory!";
            CreateStudentBtn.IsEnabled = false;
            EditStudentBtn.IsEnabled = false;

        }
        else if (!(Regex.IsMatch(SurnameTxt.Text, @"^[a-zA-Z]*$")))
        {
            //šis nedod error label, jo neļauj ievadīt simbolus, kas nav a-Z
            if (NameTxt.Text.Length != 0) SurnameTxt.Text = SurnameTxt.Text.Remove(SurnameTxt.Text.Length - 1); //Noņem pēdējo ievadīto simbolu. Pieliku != 0, jo citādi rādija error, ka initial index nevar būt < 0.
            CreateStudentBtn.IsEnabled = false;
            EditStudentBtn.IsEnabled = false;
        }
        else
        {
            SurnameErrorLbl.IsVisible = false;
        }
    }

    private void OnIDTextChanged(object sender, TextChangedEventArgs e)
    {

        if (string.IsNullOrWhiteSpace(studentIDTxt.Text))
        {
            StudentIdErrorLbl.IsVisible = true;
            StudentIdErrorLbl.Text = "Warning! This field is obligatory!";
            CreateStudentBtn.IsEnabled = false;
            EditStudentBtn.IsEnabled = false;

        }
        else
        {
            StudentIdErrorLbl.IsVisible = false;
        }
    }

    private void ClearFields() //restartē visas vērtības
    {
        NameTxt.Text = string.Empty;
        SurnameTxt.Text = string.Empty;
        studentIDTxt.Text = string.Empty;
        GenderPicker.SelectedIndex = 2;
        CreateStudentBtn.IsEnabled = false;
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = null;
        BindingContext = this;
        if (!_isEditing) ClearFields();
    }
    private void CreateStudentBtn_Clicked(object sender, EventArgs e)
    {
        var student = new Student();
        student.Name = NameTxt.Text;
        student.Surname = SurnameTxt.Text;
        student.StudentIdNumder = studentIDTxt.Text;
        student.PersonGender = (bb23028_MD1.Gender)Enum.Parse(typeof(bb23028_MD1.Gender), (string)GenderPicker.SelectedItem);
        ResultLbl.Text = student.ToString();
        App.IntImp?.Add(student);
        App.IntImp.Save();

    }
    private void EditSubmissionBtn_clicked(object sender, EventArgs e)
    {
        _s.Name = NameTxt.Text;
        _s.Surname = SurnameTxt.Text;
        _s.PersonGender = (bb23028_MD1.Gender)Enum.Parse(typeof(bb23028_MD1.Gender), (string)GenderPicker.SelectedItem);
        _s.StudentIdNumder = studentIDTxt.Text;
        ResultLbl.Text = _s.ToString();
        App.IntImp.Save();

    }

}