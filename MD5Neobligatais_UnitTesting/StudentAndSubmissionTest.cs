using bb23028_MD1;

namespace MD5Neobligatais_UnitTesting
{
    public class StudentAndSubmissionTest
    {

        //Kopā ir 15 testi, kur daļa pārbauda Student klasi un otra daļa Submission klasi, tā kā 
        //Pārbauda vai konstruktors inicializē Student ar norādītajām vērtībām.
        //Sadalīts 5 daļās, lai nebūt vairāk par vienu assert testā
        [Fact]
        public void StudentConstructorInitializesName()
        {
            string name = "John";
            string surname = "Doemer";
            Gender gender = Gender.Man;
            string studentID = "jd12345";

            var student = new Student(name, surname, gender, studentID);

            Assert.Equal(name, student.Name);
        }

        [Fact]
        public void StudentConstructorInitializesSurname()
        {
            string name = "John";
            string surname = "Doemer";
            Gender gender = Gender.Man;
            string studentID = "jd12345";

            var student = new Student(name, surname, gender, studentID);

            Assert.Equal(surname, student.Surname);
        }

        [Fact]
        public void StudentConstructorInitializesGender()
        {
            string name = "John";
            string surname = "Doemer";
            Gender gender = Gender.Man;
            string studentID = "jd12345";

            var student = new Student(name, surname, gender, studentID);

            Assert.Equal(gender, student.PersonGender);
        }
        [Fact]
        public void StudentConstructorInitializesDefaultGender()
        {
            string name = "John";
            string surname = "Doemer";
            Gender gender = Gender.Woman;
            string studentID = "jd12345";

            var student = new Student() { Name = name, Surname = surname, StudentIdNumder= studentID };

            Assert.Equal(gender, student.PersonGender);
        }

        [Fact]
        public void StudentConstructorInitializesStudentIdNumder()
        {
            string name = "John";
            string surname = "Doemer";
            Gender gender = Gender.Man;
            string studentID = "jd12345";

            var student = new Student(name, surname, gender, studentID);

            Assert.Equal(studentID, student.StudentIdNumder);
        }
    
        [Fact]
        public void StudentFullNameTestNotEqual()
        {
            string name = "Anna";
            string surname = "Ozolina";
            string notexpected = "";
            var student = new Student() { Name = name, Surname = surname };
            Assert.NotEqual(student.FullName, notexpected);
        }
        [Theory]
        [InlineData("Anna","Ozolina", "Anna Ozolina")]
        [InlineData("Anna", "", "Anna ")]
        [InlineData("", "Ozolina", " Ozolina")]

        public void StudentFullNameTestEqual(string name, string surname, string fullname)
        {
            var student = new Student() { Name = name, Surname = surname};
            Assert.Equal(student.FullName, fullname);
        }
      
        [Fact]
        public void StudentToStringIsEqual()
        {
            string name = "Anna";
            string surname = "Ozolina";
            string studentid = "ao12345";
            string expectedToString = $"Student - name: {name}, surname: {surname}, full name: {name} {surname}, gender: Woman, student ID number: {studentid}";
            var student = new Student() { Name = name, Surname = surname, StudentIdNumder = studentid, };
            Assert.Equal(student.ToString(), expectedToString);
        }
        //Sadalīts 4 daļās, lai nebūt vairāk par vienu assert testā
        [Fact]
        public void SubmissionConstructorInitializesAssignment()
        {
            var assignment = new Assignement { };
            var student = new Student { Name = "John", Surname = "Doe" };
            DateTime submissionTime = new DateTime(2023, 12, 25);
            int score = 95;

            var submission = new Submission(assignment, student, submissionTime, score);

            Assert.Equal(assignment, submission.Assignment);
        }

        [Fact]
        public void SubmissionConstructorInitializesStudent()
        {
            var assignment = new Assignement { };
            var student = new Student { Name = "John", Surname = "Doe" };
            DateTime submissionTime = new DateTime(2023, 12, 25);
            int score = 95;

            var submission = new Submission(assignment, student, submissionTime, score);

            Assert.Equal(student, submission.Student);
        }

        [Fact]
        public void SubmissionConstructorInitializesSubmissionTime()
        {
            var assignment = new Assignement { };
            var student = new Student { Name = "John", Surname = "Doe" };
            DateTime submissionTime = new DateTime(2023, 12, 25);
            int score = 95;

            var submission = new Submission(assignment, student, submissionTime, score);

            Assert.Equal(submissionTime, submission.SubmissionTime);
        }

        [Fact]
        public void SubmissionConstructorInitializesScore()
        {
            var assignment = new Assignement { };
            var student = new Student { Name = "John", Surname = "Doe" };
            DateTime submissionTime = new DateTime(2023, 12, 25);
            int score = 95;

            var submission = new Submission(assignment, student, submissionTime, score);

            Assert.Equal(score, submission.Score);
        }
        [Fact]
        public void SubmissionToStringIsEqual()
        {
            var assignment = new Assignement { Description = "Math Assignment" };
            var student = new Student { Name = "John", Surname = "Doe" };
            DateTime submissionTime = new DateTime(2023, 12, 25);
            int score = 85;
            var submission = new Submission(assignment, student, submissionTime, score);

            string expectedString = $"Submission - assignment:[{assignment}],\n" + $" student: [{student}],\n" + $" submissiondate: {submissionTime},\n" + $" score: [{score}]";

            string actualString = submission.ToString();

            Assert.Equal(expectedString, actualString);
        }
    }
}
