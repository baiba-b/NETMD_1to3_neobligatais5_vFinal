using System.Diagnostics.Contracts;

namespace bb23028_MD1;
public class Assignement
{
    //Nebija datu atribūta Date, tādēļ izmantoju DateOnly
    public DateOnly Deadline { get; set; }
    public Course? Course { get; set; }
    public string? Description { get; set; }

    public Assignement()
    {

    }
    public Assignement(DateOnly deadline, Course course, string description)
    {

        Deadline = deadline;
        Course = course;
        Description = description;

    }
    public override string ToString()
    {
        return $"Assignment - deadline: {Deadline}, description: {Description}, course: [{Course}]";
    }
}
