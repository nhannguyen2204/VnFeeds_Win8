using VnFeeds.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace VnFeeds
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class GroupedItemsPage : VnFeeds.Common.LayoutAwarePage
    {
        public GroupedItemsPage()
        {
            ViewModel.ViewModelLocator.Current.GroupedItems.SetDataContext(this);
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (this.DataContext is VnFeeds.ViewModel.IHandleNavigation)
            {
                VnFeeds.ViewModel.IHandleNavigation handleNavigation = (VnFeeds.ViewModel.IHandleNavigation)this.DataContext;
                handleNavigation.HandleOnNavigatedTo(e);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (this.DataContext is VnFeeds.ViewModel.IHandleNavigation)
            {
                VnFeeds.ViewModel.IHandleNavigation handleNavigation = (VnFeeds.ViewModel.IHandleNavigation)this.DataContext;
                handleNavigation.HandleOnNavigatedFrom(e);
            }
        }
        
    }
}
