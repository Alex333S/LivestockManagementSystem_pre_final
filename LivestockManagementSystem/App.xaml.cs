namespace LivestockManagementSystem
{   
    public partial class App : Application
    {
        public static string ImagesFolderPath => "Resources/Images/";

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
