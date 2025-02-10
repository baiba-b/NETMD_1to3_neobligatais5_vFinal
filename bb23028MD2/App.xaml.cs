using bb23028_MD1;
using Microsoft.Extensions.Configuration;

namespace bb23028MD2
{
    public partial class App : Application
    {
        //public IConfiguration configuration; //no MauiProgram paņem connection string
        public IConfiguration configuration { get; set; }
        public App(IConfiguration _configuration)
        {
            configuration = _configuration;
            InitializeComponent();

            MainPage = new AppShell();


            try
            {
                IntImp = new DBInterfaceImplementation(configuration["ConnectionStrings:MyUniversityConn"]);
            }
            catch (Exception ex) { Console.WriteLine($"Error connecting to DB: {ex.Message}"); }
                    //string _connectionString = MauiProgram.Configuration.GetConnectionString("MyUniversityConn");


        }
        public static DBInterfaceImplementation? IntImp { get; set; } //Definēju statisku mainīgu DataManager (interface implementation)

    }
}
