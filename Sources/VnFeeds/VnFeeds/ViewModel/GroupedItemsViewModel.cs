using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using VnFeeds.DataModel;
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
            Groups = (new SampleDataSource()).ItemGroups;
            GroupSource.Source = Groups;
        }

        public void HandleOnNavigatedFrom(NavigationEventArgs e)
        {
 
        }

        #endregion



        #region Properties

        GroupedItemsPage _view;


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


        /// <summary>
        /// The <see cref="GroupSource" /> property's name.
        /// </summary>
        public const string GroupSourcePropertyName = "GroupSource";
        private CollectionViewSource _GroupSource = new CollectionViewSource();
        /// <summary>
        /// Sets and gets the GroupSource property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CollectionViewSource GroupSource
        {
            get
            {
                _GroupSource = new CollectionViewSource();
                _GroupSource.Source = Groups;
                _GroupSource.IsSourceGrouped = true;
                _GroupSource.ItemsPath = new Windows.UI.Xaml.PropertyPath("Items");
                return _GroupSource;
            }

            set
            {
                if (_GroupSource == value)
                {
                    return;
                }

                RaisePropertyChanging(GroupSourcePropertyName);
                _GroupSource = value;
                RaisePropertyChanged(GroupSourcePropertyName);
            }
        }

        #endregion


        #region Commands

        private RelayCommand _HeaderClickCommand;
        /// <summary>
        /// Gets the HeaderClickCommand.
        /// </summary>
        public RelayCommand HeaderClickCommand
        {
            get
            {
                return _HeaderClickCommand
                    ?? (_HeaderClickCommand = new RelayCommand(
                                          () =>
                                          {

                                          }));
            }
        }


        private RelayCommand _ItemView_ItemClickCommand;
        /// <summary>
        /// Gets the ItemView_ItemClickCommand.
        /// </summary>
        public RelayCommand ItemView_ItemClickCommand
        {
            get
            {
                return _ItemView_ItemClickCommand
                    ?? (_ItemView_ItemClickCommand = new RelayCommand(
                                          () =>
                                          {

                                          }));
            }
        }


        

        #endregion
    }
}