using WhenToGo.App.Utils;
using WhenToGo.App.ViewModels;

namespace WhenToGo.App.Views;

[QueryProperty(nameof(ViewModel), AppConstants.HolidayDetailParameter)]
public partial class HolidayResultDetailsView : ContentPage
{
	public HolidayResultDetailsView()
	{
		InitializeComponent();
	}


    public HolidayResultDetailsViewModel ViewModel
    {
        get => m_ViewModel;
        set
        {
            if (m_ViewModel == value)
                return;
            m_ViewModel = value;
            BindingContext = m_ViewModel;
            OnPropertyChanged(nameof(ViewModel));
        }
    }
    private HolidayResultDetailsViewModel m_ViewModel;

    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.ViewModel.DoRenderingDone();
    }
}