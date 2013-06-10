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

        public async void HandleOnNavigatedTo(NavigationEventArgs e)
        {
            Groups = (new SampleDataSource(this.MagazineType)).ItemGroups;

            for (int i = 0; i < Groups.Count; i++)
            {
                var group = Groups[i];

                string xmlString = await Define.DownloadStringAsync(group.Link);
                DataGroup datagroup = new DataGroup();
                switch (this.MagazineType)
                {
                    case MagazineType.NewsGoVn:
                        datagroup = await VnFeeds.Common.ParseDocHelper.NewsGoVnGroup_Parse(xmlString, group);
                        break;
                    case MagazineType.VnExpressNet:
                        break;
                    case MagazineType.DanTriComVn:
                        break;
                    case MagazineType.Hcm24hComVn:
                        break;
                    case MagazineType.BaoMoiCom:
                        break;
                    case MagazineType.VietnamNetVn:
                        break;
                    case MagazineType.LaoDongComVn:
                        break;
                    case MagazineType.TuoiTreVn:
                        break;
                    case MagazineType.TienPhongVn:
                        break;
                    case MagazineType.NewsZingVn:
                        break;
                    case MagazineType.NgoiSaoNet:
                        break;
                    case MagazineType.Kenh14Vn:
                        break;
                    default:
                        break;
                }
                datagroup.HeaderClickCommand = this.HeaderClickCommand;
                Groups[i] = datagroup;
            }
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