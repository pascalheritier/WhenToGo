using WhenToGo.App.Views;

namespace WhenToGo.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(HolidayResultDetailsView).ToLower(), typeof(HolidayResultDetailsView));
            MainPage = new AppShell();
        }
    }
}