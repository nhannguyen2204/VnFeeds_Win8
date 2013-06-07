using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnFeeds.Common;
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
            this._ImageUri = new Uri(imagePath,UriKind.RelativeOrAbsolute);
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
    }

    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class DataItem : DataCommon
    {
        public DataItem() : base()
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
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class DataGroup : DataCommon
    {
        public DataGroup() : base()
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

        public SampleDataSource()
        {
            String ITEM_CONTENT = String.Format("Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                        "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");


            for (int g = 1; g < 8; g++)
            {
                var group = new DataGroup(string.Format("Group-{0}",g.ToString()),
                    string.Format("Group Title: {0}",g.ToString()),
                    string.Format("Group Subtitle: {0}",g.ToString()),
                    "http://1anh.com/500/Z6RKdtZWuK5Ns7Ucjof7iqK-wvKbZvdhI-dbwzJMU_yPIxtF7ROLSCzmk84MSzhuFSBO1IN6OubwU7sI4AYW2w/",
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");

                for (int i = 1; i < 10; i++)
                {
                    group.Items.Add(new DataItem(string.Format("Group-{0}-Item-{1}",g.ToString(),i.ToString()),
                    string.Format("Item Title: {0}",i.ToString()),
                    string.Format("Item Subtitle: {0}",i.ToString()),
                    "http://farm10.gox.vn/tinmoi/store/images/thumb/07062013/104/1352811/iphone_gia_re_se_co_5_mau_sac_khac_nhau_cung_thiet_ke_giong_iphone_5_0.jpg",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group));
                }

                group.HeaderClickCommand = ViewModel.ViewModelLocator.Current.GroupedItems.HeaderClickCommand;
                group.index = g - 1;
                this.ItemGroups.Add(group);
            }
        }
    }
}
