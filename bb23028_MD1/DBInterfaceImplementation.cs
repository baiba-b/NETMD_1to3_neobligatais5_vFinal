using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bb23028_MD1
{
    public class DBInterfaceImplementation //all of the methods for the databse - similar to interfaceimplementation
    {
        private UniversityContext context;
        private string _connectionString;
        public DBInterfaceImplementation(string cs)
        {
            _connectionString = cs;
            try
            {
                context = new UniversityContext(_connectionString);
            }
            catch (Exception ex) { Console.WriteLine($"Error connecting to the database: {ex.Message}"); }
        }

        public List<Student> StudentLists()
        {
            try { return context.Students.ToList(); }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving list: {ex.Message}");
                return new List<Student>();
            }
        }
        public List<Course> CourseLists()
        {
            try { return context.Courses.ToList(); }
            catch (Exception ex) { Console.WriteLine($"Error retrieving list: {ex.Message}"); return new List<Course>(); }

        }
        public List<Assignement> AssignmentLists()
        {
            try { return context.Assignements.ToList(); }
            catch (Exception ex) { Console.WriteLine($"Error retrieving list: {ex.Message}"); return new List<Assignement>(); }

        }
        public List<Submission> SubmissionLists()
        {
            try { return context.Submissions.ToList(); }
            catch (Exception ex) { Console.WriteLine($"Error retrieving list: {ex.Message}"); return new List<Submission>(); }

        }
        public void SubmissionRemove(Submission submission)
        {
            try
            {
                if (submission != null)
                {
                    context.Submissions.Remove(submission);
                    context.SaveChanges(); // Saglabā izmaiņas datubāzē
                }
            }
            catch (Exception ex) { Console.WriteLine($"Error deleting entity: {ex.Message}"); }
        }
        public void StudentRemove(Student student)
        {
            try
            {
                if (student != null)
                {
                    context.Students.Remove(student);
                    context.SaveChanges(); //Saglabā izmaiņas datubāzē
                }
            }
            catch (Exception ex) { Console.WriteLine($"Error deleting entity: {ex.Message}"); }
        }
        public void AssignmentRemove(Assignement assignement)
        {
            try
            {
                if (assignement != null)
                {
                    context.Assignements.Remove(assignement);
                    context.SaveChanges(); // Saglabā izmaiņas datubāzē
                }
            }
            catch (Exception ex) { Console.WriteLine($"Error deleting entity: {ex.Message}"); }
        }
        public void CourseRemove(Course course)
        {
            try
            {
                if (course != null)
                {
                    context.Courses.Remove(course);
                    context.SaveChanges(); // Saglabā izmaiņas datubāzē
                }
            }
            catch (Exception ex) { Console.WriteLine($"Error deleting entity: {ex.Message}"); }
        }
        public void Add(Student student) //override Add metode katrai klasei
        {
            try
            {
                if (student != null)
                    context.Students.Add(student);
            }
            catch (Exception ex) { Console.WriteLine($"Error adding entity: {ex.Message}"); }
        }
        public void Add(Teacher teacher)
        {
            try
            {
                if (teacher != null)
                    context.Teachers.Add(teacher);
            }
            catch (Exception ex) { Console.WriteLine($"Error adding entity: {ex.Message}"); }
        }
        public void Add(Course course)
        {
            try
            {
                if (course != null)
                    context.Courses.Add(course);
            }
            catch (Exception ex) { Console.WriteLine($"Error adding entity: {ex.Message}"); }
        }
        public void Add(Assignement assignement)
        {
            try
            {
                if (assignement != null)
                    context.Assignements.Add(assignement);
            }
            catch (Exception ex) { Console.WriteLine($"Error adding entity: {ex.Message}"); }
        }
        public void Add(Submission submission)
        {
            try
            {
                if (submission != null)
                    context.Submissions.Add(submission);
            }
            catch (Exception ex) { Console.WriteLine($"Error adding entity: {ex.Message}"); }
        }
        public void Save() //saglabā izmaiņas datu bāzē
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine($"Error saving changes: {ex.Message}"); }
        }

        //Metode “print”, kas atgriež kā tekstu informāciju par visiem kolekcijās (7. Punkts) esošajiem elementiem

        public string Print() // kods ņemts no lekcijas piemēra
        {
            string text = "";
            try
            {
                if (context.Students.Any())
                {
                    foreach (var f in context.Students)
                        text += f.ToString() + Environment.NewLine;
                }
                if (context.Teachers.Any())
                {
                    foreach (var f in context.Teachers)
                        text += f.ToString() + Environment.NewLine;
                }
                if (context.Courses.Any())
                {
                    foreach (var f in context.Courses)
                        text += f.ToString() + Environment.NewLine;

                }
                if (context.Assignements.Any())
                {
                    foreach (var f in context.Assignements)
                        text += f.ToString() + Environment.NewLine;

                }

                if (context.Submissions.Any())
                {
                    foreach (var f in context.Submissions)
                        text += f.ToString() + Environment.NewLine;
                }
            }
            
            catch (Exception ex) { Console.WriteLine($"Error printing information: {ex.Message}"); return text; }
            return text;
        }

        //    Metode “createTestData”, kas radītu testa datus.
        public void CreateTestData()
        {
            try
            {
                Student student01 = new("Amelija", "Saba", Gender.Other, "as11234");
                Student student02 = new("George", "SillyMan", Gender.Man, "gs12334");
                Student student03 = new("Dzons", "Burkans", Gender.Man, "jc12224");
                Student student04 = new("Kate", "Abele", Gender.Woman, "kj13334");
                Add(student01);
                Add(student02);
                Add(student03);
                Add(student04);

                Teacher teacher01 = new("Kens", "Rabats", Gender.Man, "14.06.2020.");
                Teacher teacher02 = new("Roberts", "Ozolins", Gender.Man, "24.05.2018");
                Add(teacher01);
                Add(teacher02);

                Course course01 = new("Fizika", teacher01);
                Course course02 = new("Sports", teacher02);
                Add(course01);
                Add(course02);

                Assignement assignement01 = new(new DateOnly(2024, 10, 31), course01, "Finish tasks 1 - 7 in the workbook.");
                Assignement assignement02 = new(new DateOnly(2024, 11, 20), course02, "Finish tasks 12 - 30 in the workbook.");
                Add(assignement01);
                Add(assignement02);

                Submission submission01 = new Submission(assignement01, student01, DateTime.Now, 85);
                Submission submission02 = new Submission(assignement01, student02, DateTime.Now.AddDays(-1), 90);
                Submission submission03 = new Submission(assignement02, student03, DateTime.Now, 75);
                Submission submission04 = new Submission(assignement02, student04, DateTime.Now.AddDays(-2), 80);

                Add(submission01);
                Add(submission02);
                Add(submission03);
                Add(submission04);
                context.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine($"Error creating test data: {ex.Message}"); }
        }

        //    Metode "reset", kas izdzēš visus datus.
        public void Reset() //kods ņemts no lekcijas piemēra
        {
            try
            {
                if (context.Students != null)
                    context.Students.RemoveRange(context.Students);

                if (context.Teachers != null)
                    context.Teachers.RemoveRange(context.Teachers);

                if (context.Courses != null)
                    context.Courses.RemoveRange(context.Courses);

                if (context.Assignements != null)
                    context.Assignements.RemoveRange(context.Assignements);

                if (context.Submissions != null)
                    context.Submissions.RemoveRange(context.Submissions);

                context.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine($"Error deliting data: {ex.Message}"); }
        }
    }
}
