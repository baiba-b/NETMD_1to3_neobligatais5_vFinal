using System.Runtime.InteropServices;
using System.Text.Json;
using System.Xml.Serialization;

namespace bb23028_MD1;

public class InterfaceImplementation : IDataManager
{
    public Collections collection = new Collections();
    //Metode “print”, kas atgriež kā tekstu informāciju par visiem kolekcijās (7. Punkts) esošajiem elementiem.
    public string Print() // kods ņemts no lekcijas piemēra
    {
        string text = "";
      
        if (collection._studentList != null)
        {
            foreach (var student in collection._studentList)
            {
                text += student.ToString() + Environment.NewLine;

            }
        }
        if (collection._teacherList != null)
        {
            foreach (var teacher in collection._teacherList)
            {
                text += teacher.ToString() + Environment.NewLine;

            }
        }
        if (collection._courseList != null)
        {
            foreach (var course in collection._courseList)
            {
                text += course.ToString() + Environment.NewLine;

            }
        }
        if (collection._assignementList != null)
        {
            foreach (var assignement in collection._assignementList)
            {
                text += assignement.ToString() + Environment.NewLine;

            }
        }
        if (collection._submissionList != null)
        {
            foreach (var submission in collection._submissionList)
            {
                text += submission.ToString() + Environment.NewLine;

            }
        }
        return text;

    }

    //Metode “save”, kas visu Kolekciju datus saglabātu failā.
    public void Save(string path) // kods ņemts no lekcijas piemēra
    {

        XmlSerializer serializer = new XmlSerializer(typeof(Collections));
        using (TextWriter writer = new StreamWriter(path))
        {
            serializer.Serialize(writer, collection);
        }

    }

    //Metode “load”, kas visu Kolekciju datus nolasītu no faila.
    public void Load(string path)
    {
        if (File.Exists(path)) // kods ņemts no lekcijas piemēra
        {

            XmlSerializer serializer = new XmlSerializer(typeof(Collections));
            using (TextReader reader = new StreamReader(path))
            {
                var deserializedCollection = (Collections?)serializer.Deserialize(reader);
                if (deserializedCollection != null) 
                {
                    collection = deserializedCollection;
                }
            }
        }
    }

    //    Metode “createTestData”, kas radītu testa datus.

    public void CreateTestData()
    {
        Student student01 = new("Amelija", "Saba", Gender.Other, "as11234");
        Student student02 = new("George", "SillyMan", Gender.Man, "gs12334");
        Student student03 = new("Dzons", "Burkans", Gender.Man, "jc12224");
        Student student04 = new("Kate", "Abele", Gender.Woman, "kj13334");
        collection.Add(student01);
        collection.Add(student02);
        collection.Add(student03);
        collection.Add(student04);

        Teacher teacher01 = new("Kens", "Rabats", Gender.Man, "14.06.2020.");
        Teacher teacher02 = new("Roberts", "Ozolins", Gender.Man, "24.05.2018");
        collection.Add(teacher01);
        collection.Add(teacher02);

        Course course01 = new("Fizika", teacher01);
        Course course02 = new("Sports", teacher02);
        collection.Add(course01);
        collection.Add(course02);

        Assignement assignement01 = new(new DateOnly(2024, 10, 31), course01, "Finish tasks 1 - 7 in the workbook.");
        Assignement assignement02 = new(new DateOnly(2024, 11, 20), course02, "Finish tasks 12 - 30 in the workbook.");
        collection.Add(assignement01);
        collection.Add(assignement02);

        Submission submission01 = new Submission(assignement01, student01, DateTime.Now, 85);
        Submission submission02 = new Submission(assignement01, student02, DateTime.Now.AddDays(-1), 90);
        Submission submission03 = new Submission(assignement02, student03, DateTime.Now, 75);
        Submission submission04 = new Submission(assignement02, student04, DateTime.Now.AddDays(-2), 80);

        collection.Add(submission01);
        collection.Add(submission02);
        collection.Add(submission03);
        collection.Add(submission04);
    }

    //    Metode "reset", kas izdzēš visus datus.
    public void Reset() //kods ņemts no lekcijas piemēra
    {
        collection._studentList?.Clear();
        collection._teacherList?.Clear();
        collection._courseList?.Clear();
        collection._assignementList?.Clear();
        collection._submissionList?.Clear();
    }
}
