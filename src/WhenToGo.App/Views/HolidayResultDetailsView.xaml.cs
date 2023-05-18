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

    /// <summary>
    /// We use this warkaround to display an activity indicator, because MAUI is quite slow at rendering nested collection views
    /// </summary>
    private int _collectionViewItemsCount;

    // When detail page is shown for the first time, this event is triggered when rendering is done
    private void HolidayResultDetailsView_LayoutChanged(object sender, EventArgs e)
    {
        this.ViewModel.DoRenderingDone();
    }

    private void CollectionView_ChildAdded(object sender, ElementEventArgs e)
    {
        _collectionViewItemsCount++;
        if (_collectionViewItemsCount == this.ViewModel.Holidays.Count())
            this.ViewModel.DoRenderingDone();
    }

    private void CollectionView_ChildRemoved(object sender, ElementEventArgs e)
    {
        _collectionViewItemsCount--;
    }
}