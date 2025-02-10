using bb23028_MD1;
internal class Program
{
    static void Main()
    {

        string path = ".\\..\\dataFile.xml"; // fails tiek izveidots bin failā zem debug
        var dm = new InterfaceImplementation(); // pamainiet lietojot savu Data Manager
        dm.CreateTestData();
        dm.Print();
        dm.Save(path);
        dm.Reset();
        Console.WriteLine("Saraksta izvade pēc tā datu izdzēšanas (tukšs):\n"); //te būs tukšums tā kā teksts = ""
        dm.Print();
        dm.Load(path);
        Console.WriteLine("Saraksta izvade pēc nolasīšanas no faila:\n");
        dm.Print();
        Console.ReadLine();
       
    }
}

