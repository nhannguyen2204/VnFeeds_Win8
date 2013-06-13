using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnFeeds.Common;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace VnFeeds.DataModel
{
    /// <summary>
    /// Base class for <see cref="DataItem"/> and <see cref="DataGroup"/> that
    /// defines properties common to both.
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class DataCommon : BindableBase
    {
        public DataCommon()
        {

        }

        public DataCommon(String uniqueId, String title, String subtitle, String imagePath, String description)
        {
            this._uniqueId = uniqueId;
            this._title = title;
            this._subtitle = subtitle;
            this._description = description;
            this._ImageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
        }

        private int _index = 0;
        public int index
        {
            get { return _index; }
            set { this.SetProperty(ref this._index, value); }
        }

        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        private string _subtitle = string.Empty;
        public string Subtitle
        {
            get { return this._subtitle; }
            set { this.SetProperty(ref this._subtitle, value); }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
        }

        private Uri _ImageUri = null;
        public Uri ImageUri
        {
            get { return _ImageUri; }
            set { this.SetProperty(ref this._ImageUri, value); }
        }

        private ImageSource _Image = null;
        public ImageSource Image
        {
            get
            {
                if (this._ImageUri != null)
                {
                    BitmapImage _BitmapImage = new BitmapImage();
                    _BitmapImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    _BitmapImage.UriSource = ImageUri;
                    //_BitmapImage.Dispatcher.ProcessEvents(Windows.UI.Core.CoreProcessEventsOption.ProcessAllIfPresent);
                    //_BitmapImage.SetSource();
                    this._Image = _BitmapImage;
                }
                return this._Image;
            }

            set
            {
                this.SetProperty(ref this._Image, value);
            }
        }

        private Uri _link;
        public Uri Link
        {
            get { return this._link; }
            set { this.SetProperty(ref this._link, value); }
        }
    }




    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class DataItem : DataCommon
    {
        public DataItem()
            : base()
        {

        }

        public DataItem(String uniqueId, String title, String subtitle, String imagePath, String description, String content, DataGroup group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this._content = content;
            this._group = group;
        }

        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }

        private DataGroup _group;
        public DataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }

        private string _pubDate;
        public string PubDate
        {
            get { return this._pubDate; }
            set { this.SetProperty(ref this._pubDate, value); }
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class DataGroup : DataCommon
    {
        public DataGroup()
            : base()
        {

        }

        public DataGroup(String uniqueId, String title, String subtitle, String imagePath, String description)
            : base(uniqueId, title, subtitle, imagePath, description)
        {

        }

        private ObservableCollection<DataItem> _items = new ObservableCollection<DataItem>();
        public ObservableCollection<DataItem> Items
        {
            get { return this._items; }
            set { this.SetProperty(ref this._items, value); }
        }

        private CategoryType _CateType;
        public CategoryType CateType
        {
            get { return this._CateType; }
            set { this.SetProperty(ref this._CateType, value); }
        }

        private RelayCommand<object> _HeaderClickCommand = null;
        public RelayCommand<object> HeaderClickCommand
        {
            get { return _HeaderClickCommand; }
            set { _HeaderClickCommand = value; }
        }

    }

    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// </summary>
    public sealed class SampleDataSource
    {
        private ObservableCollection<DataGroup> _itemGroups = new ObservableCollection<DataGroup>();
        public ObservableCollection<DataGroup> ItemGroups
        {
            get { return this._itemGroups; }
        }

        public SampleDataSource(MagazineType magazineType)
        {
            switch (magazineType)
            {
                case MagazineType.NewsGoVn:
                    Init_NewsGoVn();
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


            /*
            String ITEM_CONTENT = String.Format("Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                        "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");
            for (int g = 1; g < 8; g++)
            {
                var group = new DataGroup(string.Format("Group-{0}", g.ToString()),
                    string.Format("Group Title: {0}", g.ToString()),
                    string.Format("Group Subtitle: {0}", g.ToString()),
                    "http://1anh.com/500/Z6RKdtZWuK5Ns7Ucjof7iqK-wvKbZvdhI-dbwzJMU_yPIxtF7ROLSCzmk84MSzhuFSBO1IN6OubwU7sI4AYW2w/",
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");

                for (int i = 1; i < 10; i++)
                {
                    group.Items.Add(new DataItem(string.Format("Group-{0}-Item-{1}", g.ToString(), i.ToString()),
                    string.Format("Item Title: {0}", i.ToString()),
                    string.Format("Item Subtitle: {0}", i.ToString()),
                    "http://farm10.gox.vn/tinmoi/store/images/thumb/07062013/104/1352811/iphone_gia_re_se_co_5_mau_sac_khac_nhau_cung_thiet_ke_giong_iphone_5_0.jpg",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group));
                }

                group.HeaderClickCommand = ViewModel.ViewModelLocator.Current.GroupedItems.HeaderClickCommand;
                group.index = g - 1;
                this.ItemGroups.Add(group);
            }
            */
        }

        private void Init_NewsGoVn()
        {
            List<CategoryType> cateTypes = new List<CategoryType>() {
                CategoryType.XaHoi,
                CategoryType.TheGioi,
                CategoryType.VanHoa,
                CategoryType.Sao,
                CategoryType.TheThao,
                CategoryType.DoiSong,
                CategoryType.CongNghe,
                CategoryType.TheGioiXe,
                CategoryType.PhapLuat,
                CategoryType.KinhTe,
                CategoryType.GiaiTri,
                CategoryType.Game,
                CategoryType.Blog
            };

            for (int i = 0; i < cateTypes.Count; i++)
            {
                DataGroup group = new DataGroup();
                group = NewsGoVn_InitDataGroupByCateType(i, cateTypes[i]);
                this.ItemGroups.Add(group);
            }
        }

        public static DataGroup NewsGoVn_InitDataGroupByCateType(int i, CategoryType cateType)
        {
            DataGroup group = new DataGroup();
            switch (cateType)
            {
                case CategoryType.XaHoi:
                    group.CateType = CategoryType.XaHoi;
                    group.Link = new Uri("http://news.go.vn/rss/1/xa-hoi.htm", UriKind.Absolute);
                    group.index = i;
                    group.Title = GetStringNameFromCateType(group.CateType);
                    break;
                case CategoryType.TheGioi:
                    group.CateType = CategoryType.TheGioi;
                    group.Link = new Uri("http://news.go.vn/rss/2/the-gioi.htm", UriKind.Absolute);
                    group.index = i;
                    group.Title = GetStringNameFromCateType(group.CateType);
                    break;
                case CategoryType.VanHoa:
                    group.CateType = CategoryType.VanHoa;
                    group.Link = new Uri("http://news.go.vn/rss/6/van-hoa.htm", UriKind.Absolute);
                    group.index = i;
                    group.Title = GetStringNameFromCateType(group.CateType);
                    break;
                case CategoryType.Sao:
                    group.CateType = CategoryType.Sao;
                    group.Link = new Uri("http://news.go.vn/rss/42/sao.htm", UriKind.Absolute);
                    group.index = i;
                    group.Title = GetStringNameFromCateType(group.CateType);
                    break;
                case CategoryType.TheThao:
                    group.CateType = CategoryType.TheThao;
                    group.Link = new Uri("http://news.go.vn/rss/10/the-thao.htm", UriKind.Absolute);
                    group.index = i;
                    group.Title = GetStringNameFromCateType(group.CateType);
                    break;
                case CategoryType.DoiSong:
                    group.CateType = CategoryType.DoiSong;
                    group.Link = new Uri("http://news.go.vn/rss/5/doi-song.htm", UriKind.Absolute);
                    group.index = i;
                    group.Title = GetStringNameFromCateType(group.CateType);
                    break;
                case CategoryType.CongNghe:
                    group.CateType = CategoryType.CongNghe;
                    group.Link = new Uri("http://news.go.vn/rss/9/cong-nghe.htm", UriKind.Absolute);
                    group.index = i;
                    group.Title = GetStringNameFromCateType(group.CateType);
                    break;
                case CategoryType.TheGioiXe:
                    group.CateType = CategoryType.TheGioiXe;
                    group.Link = new Uri("http://news.go.vn/rss/30/the-gioi-xe.htm", UriKind.Absolute);
                    group.index = i;
                    group.Title = GetStringNameFromCateType(group.CateType);
                    break;
                case CategoryType.PhapLuat:
                    group.CateType = CategoryType.PhapLuat;
                    group.Link = new Uri("http://news.go.vn/rss/7/phap-luat.htm", UriKind.Absolute);
                    group.index = i;
                    group.Title = GetStringNameFromCateType(group.CateType);
                    break;
                case CategoryType.KinhTe:
                    group.CateType = CategoryType.KinhTe;
                    group.Link = new Uri("http://news.go.vn/rss/3/kinh-te.htm", UriKind.Absolute);
                    group.index = i;
                    group.Title = GetStringNameFromCateType(group.CateType);
                    break;
                case CategoryType.GiaiTri:
                    group.CateType = CategoryType.GiaiTri;
                    group.Link = new Uri("http://news.go.vn/rss/8/giai-tri.htm", UriKind.Absolute);
                    group.index = i;
                    group.Title = GetStringNameFromCateType(group.CateType);
                    break;
                case CategoryType.Game:
                    group.CateType = CategoryType.Game;
                    group.Link = new Uri("http://news.go.vn/rss/41/game.htm", UriKind.Absolute);
                    group.index = i;
                    group.Title = GetStringNameFromCateType(group.CateType);
                    break;
                case CategoryType.Blog:
                    group.CateType = CategoryType.Blog;
                    group.Link = new Uri("http://news.go.vn/rss/53/blog.htm", UriKind.Absolute);
                    group.index = i;
                    group.Title = GetStringNameFromCateType(group.CateType);
                    break;
                default:
                    break;
            }
            return group;
        }

        public static string GetStringNameFromCateType(CategoryType cateType)
        {
            switch (cateType)
            {
                case CategoryType.XaHoi:
                    return "Xã Hội";
                case CategoryType.TheGioi:
                    return "Thế Giới";
                case CategoryType.VanHoa:
                    return "Văn Hóa";
                case CategoryType.Sao:
                    return "Sao";
                case CategoryType.TheThao:
                    return "Thể Thao";
                case CategoryType.DoiSong:
                    return "Đời Sống";
                case CategoryType.CongNghe:
                    return "Công Nghệ";
                case CategoryType.TheGioiXe:
                    return "Thế Giới Xe";
                case CategoryType.PhapLuat:
                    return "Pháp Luật";
                case CategoryType.KinhTe:
                    return "Kinh Tế";
                case CategoryType.GiaiTri:
                    return "Giải Trí";
                case CategoryType.Game:
                    return "Game";
                case CategoryType.Blog:
                    return "Blog";
                default:
                    return string.Empty;
            }
        }

        public static string GetStringShortNameFromCateType(CategoryType cateType)
        {
            switch (cateType)
            {
                case CategoryType.XaHoi:
                    return "XaHoi";
                case CategoryType.TheGioi:
                    return "TheGioi";
                case CategoryType.VanHoa:
                    return "VanHoa";
                case CategoryType.Sao:
                    return "Sao";
                case CategoryType.TheThao:
                    return "TheThao";
                case CategoryType.DoiSong:
                    return "DoiSong";
                case CategoryType.CongNghe:
                    return "CongNghe";
                case CategoryType.TheGioiXe:
                    return "TheGioiXe";
                case CategoryType.PhapLuat:
                    return "PhapLuat";
                case CategoryType.KinhTe:
                    return "KinhTe";
                case CategoryType.GiaiTri:
                    return "GiaiTri";
                case CategoryType.Game:
                    return "Game";
                case CategoryType.Blog:
                    return "Blog";
                default:
                    return string.Empty;
            }
        }

        public static string GetStringNameFromMagazineType(MagazineType magazineType)
        {
            switch (magazineType)
            {
                case MagazineType.NewsGoVn:
                    return "Go News";
                case MagazineType.VnExpressNet:
                    return "Vnexpress";
                case MagazineType.DanTriComVn:
                    return "Dân Trí";
                case MagazineType.Hcm24hComVn:
                    return "24h";
                case MagazineType.BaoMoiCom:
                    return "Báo Mới";
                case MagazineType.VietnamNetVn:
                    return "Vietnamnet";
                case MagazineType.LaoDongComVn:
                    return "Lao Động";
                case MagazineType.TuoiTreVn:
                    return "Tuổi Trẻ";
                case MagazineType.TienPhongVn:
                    return "Tiền Phong";
                case MagazineType.NewsZingVn:
                    return "Zing News";
                case MagazineType.NgoiSaoNet:
                    return "Ngôi Sao";
                case MagazineType.Kenh14Vn:
                    return "Mương 14";
                default:
                    return string.Empty;
            }
        }

        public static string GetStringShortNameFromMagazineType(MagazineType magazineType)
        {
            switch (magazineType)
            {
                case MagazineType.NewsGoVn:
                    return "NewsGoVn";
                case MagazineType.VnExpressNet:
                    return "VnExpressNet";
                case MagazineType.DanTriComVn:
                    return "DanTriComVn";
                case MagazineType.Hcm24hComVn:
                    return "Hcm24hComVn";
                case MagazineType.BaoMoiCom:
                    return "BaoMoiCom";
                case MagazineType.VietnamNetVn:
                    return "VietnamNetVn";
                case MagazineType.LaoDongComVn:
                    return "LaoDongComVn";
                case MagazineType.TuoiTreVn:
                    return "TuoiTreVn";
                case MagazineType.TienPhongVn:
                    return "TienPhongVn";
                case MagazineType.NewsZingVn:
                    return "NewsZingVn";
                case MagazineType.NgoiSaoNet:
                    return "NgoiSaoNet";
                case MagazineType.Kenh14Vn:
                    return "Kenh14Vn";
                default:
                    return string.Empty;
            }
        }
    }



    public enum MagazineType
    {
        NewsGoVn,
        VnExpressNet,
        DanTriComVn,
        Hcm24hComVn,
        BaoMoiCom,
        VietnamNetVn,
        LaoDongComVn,
        TuoiTreVn,
        TienPhongVn,
        NewsZingVn,
        NgoiSaoNet,
        Kenh14Vn
    }

    public enum CategoryType
    {
        XaHoi, TheGioi, VanHoa, Sao, TheThao, DoiSong,
        CongNghe, TheGioiXe, PhapLuat, KinhTe, GiaiTri, Game, Blog
    }
}
