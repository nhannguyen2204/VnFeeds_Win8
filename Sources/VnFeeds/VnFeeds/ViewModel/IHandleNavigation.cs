using Windows.UI.Xaml.Navigation;

namespace VnFeeds.ViewModel
{
    interface IHandleNavigation
    {
        void HandleOnNavigatedTo(NavigationEventArgs e);
        void HandleOnNavigatedFrom(NavigationEventArgs e);
    }
}
