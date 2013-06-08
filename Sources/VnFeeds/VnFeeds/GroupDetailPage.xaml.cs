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

// The Group Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234229

namespace VnFeeds
{
    /// <summary>
    /// A page that displays an overview of a single group, including a preview of the items
    /// within the group.
    /// </summary>
    public sealed partial class GroupDetailPage : VnFeeds.Common.LayoutAwarePage
    {
        public GroupDetailPage()
        {
            ViewModel.ViewModelLocator.Current.GroupDetail.SetDataContext(this);
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
