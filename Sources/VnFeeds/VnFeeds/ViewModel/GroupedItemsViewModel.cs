using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using VnFeeds.DataModel;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;

namespace VnFeeds.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class GroupedItemsViewModel : ViewModelBase,IDataContext,IHandleNavigation
    {


        /// <summary>
        /// Initializes a new instance of the GroupedItemsViewModel class.
        /// </summary>
        public GroupedItemsViewModel()
        {
            
        }

        #region Define IDataContext functions

        public void SetDataContext(object sender)
        {
            if (sender is GroupedItemsPage)
            {
                _view = (GroupedItemsPage)sender;
                _view.DataContext = this;
            }
        }

        #endregion

        #region Define IHandleNavigation functions

        public void HandleOnNavigatedTo(NavigationEventArgs e)
        {
            Groups = (new SampleDataSource(this.MagazineType)).ItemGroups;
        }

        public void HandleOnNavigatedFrom(NavigationEventArgs e)
        {
 
        }

        #endregion

        #region Properties

        GroupedItemsPage _view;


        /// <summary>
        /// The <see cref="MagazineType" /> property's name.
        /// </summary>
        public const string MagazineTypePropertyName = "MagazineType";

        private MagazineType _MagazineType = MagazineType.NewsGoVn;

        /// <summary>
        /// Sets and gets the MagazineType property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public MagazineType MagazineType
        {
            get
            {
                return _MagazineType;
            }

            set
            {
                if (_MagazineType == value)
                {
                    return;
                }

                RaisePropertyChanging(MagazineTypePropertyName);
                _MagazineType = value;
                RaisePropertyChanged(MagazineTypePropertyName);
            }
        }


        /// <summary>
        /// The <see cref="Groups" /> property's name.
        /// </summary>
        public const string GroupsPropertyName = "Groups";
        private ObservableCollection<DataGroup> _Groups = new ObservableCollection<DataGroup>();
        /// <summary>
        /// Sets and gets the Groups property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<DataGroup> Groups
        {
            get
            {
                return _Groups;
            }

            set
            {
                if (_Groups == value)
                {
                    return;
                }

                RaisePropertyChanging(GroupsPropertyName);
                _Groups = value;
                RaisePropertyChanged(GroupsPropertyName);
            }
        }


        #endregion

        #region Commands

        private RelayCommand<object> _HeaderClickCommand;
        /// <summary>
        /// Gets the HeaderClickCommand.
        /// </summary>
        public RelayCommand<object> HeaderClickCommand
        {
            get
            {
                return _HeaderClickCommand
                    ?? (_HeaderClickCommand = new RelayCommand<object>(
                                          (obj) =>
                                          {
                                              _view.Frame.Navigate(typeof(GroupDetailPage), obj);
                                          }));
            }
        }


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