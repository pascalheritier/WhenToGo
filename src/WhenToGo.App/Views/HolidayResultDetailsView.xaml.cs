using WhenToGo.App.Utils;
using WhenToGo.App.ViewModels;

namespace WhenToGo.App.Views;

[QueryProperty(nameof(ViewModel), AppConstants.HolidayDetailParameter)]
public partial class HolidayResultDetailsView : ContentPage
{
	public HolidayResultDetailsView()
	{
		InitializeComponent();
        LayoutChanged += HolidayResultDetailsView_LayoutChanged;

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

    // When detail page is shown for the first time, this event is triggered when rendering is done
    private void HolidayResultDetailsView_LayoutChanged(object sender, EventArgs e)
    {
        this.ViewModel.DoRenderingDone();
    }

    // When the number of items in the collection view has changed, this event is triggered when rendering is done (rendering is slow in MAUI)
    private void CollectionView_SizeChanged(object sender, EventArgs e)
    {
        this.ViewModel.DoRenderingDone();
    }
}