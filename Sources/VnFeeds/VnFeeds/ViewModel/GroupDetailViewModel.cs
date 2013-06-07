using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VnFeeds.DataModel;

namespace VnFeeds.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class GroupDetailViewModel : ViewModelBase,IDataContext,IHandleNavigation
    {
        /// <summary>
        /// Initializes a new instance of the GroupDetailViewModel class.
        /// </summary>
        public GroupDetailViewModel()
        {
        }


        #region Define IDataContext functions

        public void SetDataContext(object sender)
        {
            if (sender is GroupDetailPage)
            {
                _view = (GroupDetailPage)sender;
                _view.DataContext = this;
            }
        }

        #endregion


        #region Define IHandleNavigation functions

        public void HandleOnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            if (e.NavigationMode == Windows.UI.Xaml.Navigation.NavigationMode.New)
            {
                Group = ViewModel.ViewModelLocator.Current.GroupedItems.Groups[(int)e.Parameter];
            }
        }

        public void HandleOnNavigatedFrom(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {

        }

        #endregion


        #region Properties

        public static GroupDetailPage _view;


        /// <summary>
        /// The <see cref="Group" /> property's name.
        /// </summary>
        public const string GroupPropertyName = "Group";
        private DataModel.DataGroup _Group = null;
        /// <summary>
        /// Sets and gets the Group property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DataModel.DataGroup Group
        {
            get
            {
                return _Group;
            }

            set
            {
                if (_Group == value)
                {
                    return;
                }

                RaisePropertyChanging(GroupPropertyName);
                _Group = value;
                RaisePropertyChanged(GroupPropertyName);
            }
        }

        #endregion


        #region Commands

        private RelayCommand<Windows.UI.Xaml.Controls.ItemClickEventArgs> _ItemView_ItemClickCommand;
        /// <summary>
        /// Gets the ItemView_ItemClickCommand.
        /// </summary>
        public RelayCommand<Windows.UI.Xaml.Controls.ItemClickEventArgs> ItemView_ItemClickCommand
        {
            get
            {
                return _ItemView_ItemClickCommand
                    ?? (_ItemView_ItemClickCommand = new RelayCommand<Windows.UI.Xaml.Controls.ItemClickEventArgs>(
                                          (obj) =>
                                          {
                                              var item = ((DataItem)obj.ClickedItem);
                                              _view.Frame.Navigate(typeof(ItemDetailPage), item);
                                          }));
            }
        }

        #endregion
    }
}