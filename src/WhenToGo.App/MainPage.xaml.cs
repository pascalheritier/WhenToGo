using WhenToGo.App.ViewModels;

namespace WhenToGo.App
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            MainViewModel mainViewModel = new();
            this.BindingContext = mainViewModel;
        }
    }
}