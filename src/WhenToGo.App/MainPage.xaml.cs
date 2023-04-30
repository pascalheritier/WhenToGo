using WhenToGo.App.ViewModels;

namespace WhenToGo.App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            MainViewModel mainViewModel = new();
            this.BindingContext = mainViewModel;
        }
    }
}