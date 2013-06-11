using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VnFeeds.DataModel;
using Windows.Storage;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;
using System;
using System.Diagnostics;

namespace VnFeeds.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class GroupedItemsViewModel : ViewModelBase, IDataContext, IHandleNavigation
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
                Debug.WriteLine("[GroupedItemsViewModel] SetDataContext ...");
            }
        }

        #endregion

        #region Define IHandleNavigation functions

        public async void HandleOnNavigatedTo(NavigationEventArgs e)
        {
            Debug.WriteLine("[GroupedItemsViewModel] HandleOnNavigatedTo ...");
            if (e.NavigationMode == NavigationMode.New)
            {
                Groups = (new SampleDataSource(this.MagazineType)).ItemGroups;
                await LoadData();
            }
        }

        private async Task LoadData()
        {
            Debug.WriteLine("[GroupedItemsViewModel] LoadData ...");
            for (int i = 0; i < Groups.Count; i++)
            {
                var group = Groups[i];

                Debug.WriteLine(string.Format("[GroupedItemsViewModel] LoadData : Downloading ... {0}", group.Title));
                string xmlString = await Define.DownloadStringAsync(group.Link);
                Debug.WriteLine(string.Format("[GroupedItemsViewModel] LoadData : Downloaded ./. {0}", group.Title));


                await SaveFeed(xmlString, group.CateType);

                DataGroup datagroup = new DataGroup();
                switch (this.MagazineType)
                {
                    case MagazineType.NewsGoVn:
                        Debug.WriteLine(string.Format("[GroupedItemsViewModel] NewsGoVnGroup_Parse : Parsing ... {0}", group.Title));
                        datagroup = await VnFeeds.Common.ParseDocHelper.NewsGoVnGroup_Parse(xmlString, group, Define.SummaryFeedsNum);
                        Debug.WriteLine(string.Format("[GroupedItemsViewModel] NewsGoVnGroup_Parse : Parsed ./. {0}", group.Title));
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

        private async Task SaveFeed(string xmlString, CategoryType cateType)
        {
            Debug.WriteLine(string.Format("[GroupedItemsViewModel] SaveFeed : Saving ... {0}", SampleDataSource.GetStringShortNameFromCateType(cateType)));
            StorageFolder feedContainer = await ApplicationData.Current.LocalFolder.CreateFolderAsync(Define.FeedContainerFolderName, CreationCollisionOption.OpenIfExists);

            string magazineName = SampleDataSource.GetStringShortNameFromMagazineType(this.MagazineType);
            string cateName = SampleDataSource.GetStringShortNameFromCateType(cateType);
            string feedNameStr =  string.Format(Define.FeedData_FormatName,magazineName,cateName);

            StorageFile feedFile = await feedContainer.CreateFileAsync(feedNameStr, CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(feedFile, xmlString, Windows.Storage.Streams.UnicodeEncoding.Utf8);
            Debug.WriteLine(string.Format("[GroupedItemsViewModel] SaveFeed : Saved ./. {0}", SampleDataSource.GetStringShortNameFromCateType(cateType)));
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
                                              Debug.WriteLine("[GroupedItemsViewModel] HeaderClickCommand ...");
                                              int gIndex = (int)obj;
                                              if (Groups[gIndex].Items.Count > 0)
                                              {
                                                  _view.Frame.Navigate(typeof(GroupDetailPage), Groups[gIndex].CateType);
                                              }
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
                                              Debug.WriteLine("[GroupedItemsViewModel] ItemView_ItemClickCommand ...");
                                              var item = ((DataItem)obj.ClickedItem);
                                              _view.Frame.Navigate(typeof(ItemDetailPage), item);
                                          }));
            }
        }


        #endregion
    }
}