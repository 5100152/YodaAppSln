using YodaApp.ViewModels;

namespace YodaApp.Views;

public partial class YodaPageView : ContentPage

	
{
    YodaPageViewModel _viewModel;
    public YodaPageView(YodaPageViewModel vm)
    {
        _viewModel = vm;
        InitializeComponent();
        BindingContext = _viewModel;

    }
}