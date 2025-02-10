using bb23028_MD1;
internal class Program
{
    static void Main()
    {

        string path = ".\\..\\dataFile.xml"; //pamainiet norādot savu ceļu uz datni, skat. P.S.1  
        var dm = new InterfaceImplementation(); // pamainiet lietojot savu Data Manager
        dm.CreateTestData();
        Console.WriteLine(dm.Print());
        dm.Save(path);
        dm.Reset();
        Console.WriteLine("Saraksta izvade pēc tā datu izdzēšanas (tukšs):\n"); //te būs tukšums tā kā teksts = ""
        Console.WriteLine(dm.Print());
        dm.Load(path);
        Console.WriteLine("Saraksta izvade pēc nolasīšanas no faila:\n");
        Console.WriteLine(dm.Print());
        Console.ReadLine();
       
    }
}

