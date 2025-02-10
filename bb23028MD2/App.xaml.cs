using bb23028_MD1;

namespace bb23028MD2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            IntImp = new InterfaceImplementation();
        }
        public static InterfaceImplementation? IntImp { get; set; } //Definēju statisku mainīgu DataManager (interface implementation)

        
        
    }
}
