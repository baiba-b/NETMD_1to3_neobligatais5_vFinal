using bb23028_MD1;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace bb23028MD2;

public partial class CreateAssignment : ContentPage
{
    public DBInterfaceImplementation? IntImp;
    private bool _isEditing = false;
    public CreateAssignment()
    {
        InitializeComponent();
        IntImp = App.IntImp;
        BindingContext = this; //norāda, ka jāmeklē bindotos datus šajā lapā.
        
        _isEditing = false;
        DescriptionErrorLbl.IsVisible = false;
        CoursePickerErrorLbl.IsVisible = false;
    }
    private Assignement _a;
    public CreateAssignment(Assignement a)
    {
        InitializeComponent();
        _a = a;
        IntImp = App.IntImp;
        

        BindingContext = this;

        DateTxt.Date = _a.Deadline.ToDateTime(TimeOnly.MinValue);
        DescriptionTxt.Text = _a.Description.ToString();

        CoursePicker.ItemsSource = CourseList;
        CoursePicker.SelectedItem = _a.Course;

        //norāda, ka jāmeklē bindotos datus šajā lapā.

        CreateAssignmentBtn.IsVisible = false;
        EditAssignmentBtn.IsVisible = true;
        _isEditing = true;
        DescriptionErrorLbl.IsVisible = false;
        CoursePickerErrorLbl.IsVisible = false;


    }
    //tiek izmantota īpašība CourseList  bindingam
    public List<bb23028_MD1.Course> CourseList
    {
        get { return IntImp?.CourseLists(); }
    }

    private void CreateAssignmentBtn_clicked(object sender, EventArgs e)
    {
        var assignment = new Assignement();
        assignment.Course = CoursePicker.SelectedItem as bb23028_MD1.Course;
        assignment.Deadline = DateOnly.FromDateTime(DateTxt.Date);
        assignment.Description = DescriptionTxt.Text;
        ResultLbl.Text = assignment.ToString();
        App.IntImp?.Add(assignment);
        App.IntImp.Save();

    }

    private void EditAssignmentBtn_clicked(object sender, EventArgs e)
    {

        _a.Course = CoursePicker.SelectedItem as bb23028_MD1.Course;
        _a.Deadline = DateOnly.FromDateTime(DateTxt.Date);
        _a.Description = DescriptionTxt.Text;
        ResultLbl.Text = _a.ToString();
        App.IntImp.Save();


    }


    private void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        if (DateTxt.Date < DateTime.Today)
        {
            CoursePickerErrorLbl.IsVisible = true;
            CoursePickerErrorLbl.Text = "Warning! You can't select a date before today!";
            CreateAssignmentBtn.IsEnabled = false;
            EditAssignmentBtn.IsEnabled = false;
        }
        else CoursePickerErrorLbl.IsVisible = false;

    }
    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(DescriptionTxt.Text))
        {
            DescriptionErrorLbl.IsVisible = true;
            DescriptionErrorLbl.Text = "Warning! This field is obligatory!";
            CreateAssignmentBtn.IsEnabled = false;
            EditAssignmentBtn.IsEnabled = false;
        }
        else DescriptionErrorLbl.IsVisible = false;

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
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = null;
        BindingContext = this;
        if (!_isEditing) ClearFields();

    }

}