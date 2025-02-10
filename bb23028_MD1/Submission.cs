namespace bb23028_MD1;

public class Submission
{
    public Assignement? Assignment { get; set; }

    public Student? Student { get; set; }
    public DateTime SubmissionDate { get; set; }
    public int Score { get; set; }
    public Submission(Assignement assignement, Student student, DateTime submissionDate, int score)
    {
        this.Assignment = assignement;
        this.Student = student;
        this.SubmissionDate = submissionDate;
        this.Score = score;
    }
    public Submission()
    {
        
    }
    public override string ToString()
    {
        return $"Submission - assignment:[{Assignment}],\n" + //saliku \n, jo teksts nebija pārskatāms
            $" student: [{Student}],\n" +
            $" submissiondate: {SubmissionDate},\n" +
            $" socre: [{Score}]";
    }
}
