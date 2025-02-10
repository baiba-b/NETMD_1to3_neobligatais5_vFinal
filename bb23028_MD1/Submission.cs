using System.ComponentModel.DataAnnotations;

namespace bb23028_MD1;

public class Submission
{
    [Key]
    public int Id { get; set; }
    public Assignement? Assignment { get; set; }
    public Student? Student { get; set; }
    public DateTime SubmissionTime { get; set; }
    public int Score { get; set; }
    public Submission(Assignement assignement, Student student, DateTime SubmissionTime, int score)
    {
        this.Assignment = assignement;
        this.Student = student;
        this.SubmissionTime = SubmissionTime;
        this.Score = score;
    }
    public Submission()
    {

    }
    public override string ToString()
    {
        return $"Submission - assignment:[{Assignment}],\n" + //saliku \n, jo teksts nebija pārskatāms
            $" student: [{Student}],\n" +
            $" submissiondate: {SubmissionTime},\n" +
            $" score: [{Score}]";
    }
}
