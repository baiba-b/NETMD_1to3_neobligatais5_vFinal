using System.Collections.ObjectModel;

namespace bb23028_MD1;

public class Collections
{
    public List<Student>? _studentList = new();
    public List<Teacher>? _teacherList = new();
    public List<Course>? _courseList = new();
    public List<Assignement>? _assignementList = new();
    public List<Submission>? _submissionList = new();

    public void Add(Student student) //override Add metode katrai klasei
    {
        if (student != null)
        { _studentList?.Add(student); }
    }
    public void Add(Teacher teacher)
    {
        if (teacher != null)
        { _teacherList?.Add(teacher); }
    }
    public void Add(Course course)
    {
        if (course != null)
        { _courseList?.Add(course); }
    }
    public void Add(Assignement assignement)
    {
        if (assignement != null)
        { _assignementList?.Add(assignement); }
    }
    public void Add(Submission submission)
    {
        if (submission != null)
        { _submissionList?.Add(submission); }
    }

}
