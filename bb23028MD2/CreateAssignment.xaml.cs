using bb23028_MD1;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace bb23028MD2;

public partial class CreateAssignment : ContentPage
{
    public InterfaceImplementation? IntImp;
    private bool _reload = false;
    private bool _isEditing;
    public CreateAssignment()
    {
        IntImp = App.IntImp;
        BindingContext = this; //norāda, ka jāmeklē bindotos datus šajā lapā.
        InitializeComponent();
        _isEditing = false;

    }
    private Assignement _a;
    public CreateAssignment(Assignement a)
    {
        _a = a;
        IntImp = App.IntImp;
        InitializeComponent();
        DateTxt.Date = _a.Deadline.ToDateTime(TimeOnly.MinValue);
        DescriptionTxt.Text = _a.Description.ToString();
        CoursePicker.ItemsSource = CourseList;
        CoursePicker.SelectedItem = _a.Course;

        BindingContext = this; //norāda, ka jāmeklē bindotos datus šajā lapā.

        CreateAssignmentBtn.IsVisible = false;
        EditAssignmentBtn.IsVisible = true;
        _isEditing = true;
    }
    //tiek izmantota īpašība CourseList  bindingam
    public List<bb23028_MD1.Course> CourseList
    {
        get { return IntImp?.collection._courseList; }
    }

    private void CreateAssignmentBtn_clicked(object sender, EventArgs e)
    {
        var assignment = new Assignement();
        assignment.Course = CoursePicker.SelectedItem as bb23028_MD1.Course;
        assignment.Deadline = DateOnly.FromDateTime(DateTxt.Date);
        assignment.Description = DescriptionTxt.Text;
        ResultLbl.Text = assignment.ToString();
        App.IntImp?.collection.Add(assignment);
        if (UniversityComponents.timeClicked > 0) //atjauno printēto saraksts, ja tas ir iepriekš atvērts
        {
            WeakReferenceMessenger.Default.Send(new UpdateResultMessage(App.IntImp.collection));
        }
    }

    private void EditAssignmentBtn_clicked(object sender, EventArgs e)
    {

        _a.Course = CoursePicker.SelectedItem as bb23028_MD1.Course;
        _a.Deadline = DateOnly.FromDateTime(DateTxt.Date);
        _a.Description = DescriptionTxt.Text;
        ResultLbl.Text = _a.ToString();
        if (UniversityComponents.timeClicked > 0) //atjauno printēto saraksts, ja tas ir iepriekš atvērts
        {
            WeakReferenceMessenger.Default.Send(new UpdateResultMessage(App.IntImp.collection));
        }
    }


    private void OnDateSelected(object sender, DateChangedEventArgs e) 
    {
        if (DateTxt.Date < DateTime.Today)
        {
            DisplayAlert("Warning", "You can't select a date before today!", "OK");
            CreateAssignmentBtn.IsEnabled = false;
            EditAssignmentBtn.IsEnabled = false;
        }
    }
    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (_reload) return; //nerāda kļūdu paziņojumus, ja tas ir pēc ClearFields()
        if (string.IsNullOrWhiteSpace(DescriptionTxt.Text))
        {
            DisplayAlert("Warning", "This field is obligatory!", "OK");
            CreateAssignmentBtn.IsEnabled = false;
            EditAssignmentBtn.IsEnabled = false;
        }
    }
    private void ValidateFields(object sender, FocusEventArgs e) //Pārbaude, vai ierakstītie dati atbilst sagaidītajam formātam
    {

        bool isCourseSelected = CoursePicker.SelectedIndex >= 0;

        bool isDescriptionValid = !string.IsNullOrWhiteSpace(DescriptionTxt.Text);
        if (isCourseSelected && isDescriptionValid && DateTxt.Date >= DateTime.Today)
        {
            CreateAssignmentBtn.IsEnabled = true;
            EditAssignmentBtn.IsEnabled = true;

        }
        else
        {
            CreateAssignmentBtn.IsEnabled = false;
            EditAssignmentBtn.IsEnabled = false;

        }

    }
   
    private void ClearFields() //restartē visas vērtības
    {
        DescriptionTxt.Text = string.Empty; 
        DateTxt.Date = DateTime.Today; 
        CreateAssignmentBtn.IsEnabled = false; 
        EditAssignmentBtn.IsEnabled = false;
        _reload = true;
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = null;
        BindingContext = this;
        if(!_isEditing) ClearFields();

    }

}