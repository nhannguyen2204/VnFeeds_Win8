using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VnFeeds.DataModel;
using System.Linq;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Windows.Storage.Streams;
using System.Text.RegularExpressions;

namespace VnFeeds.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ItemDetailViewModel : ViewModelBase, IDataContext, IHandleNavigation
    {
        /// <summary>
        /// Initializes a new instance of the ItemDetailViewModel class.
        /// </summary>
        public ItemDetailViewModel()
        {
        }

        #region Define IDataContext functions

        public void SetDataContext(object sender)
        {
            if (sender is ItemDetailPage)
            {
                _view = (ItemDetailPage)sender;
                _view.DataContext = this;
            }
        }

        #endregion


        #region Define IHandleNavigation functions

        bool isFirstLoaded = false;

        public async void HandleOnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            if (e.NavigationMode == Windows.UI.Xaml.Navigation.NavigationMode.New)
            {
                // TODO: Create an appropriate data model for your problem domain to replace the sample data
                var item = (DataItem)e.Parameter;
                Items = item.Group.Items;
                await Task.Delay(50);
                ItemSelected = item;
                await LoadContent(ItemSelected);
                isFirstLoaded = true;
            }
        }

        private async Task LoadContent(DataItem _item)
        {
            string contentStr = await Define.DownloadStringAsync(_item.Link);

            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(contentStr);

            HtmlAgilityPack.HtmlNode htmlNode = htmlDoc.GetElementbyId("ContentContainer");
            while (htmlNode.Elements("script").Count() > 0)
            {
                htmlNode.Elements("script").ElementAt(0).Remove();
            }
            while (htmlNode.Elements("meta").Count() > 0)
            {
                htmlNode.Elements("meta").ElementAt(0).Remove();
            }

            Items[Items.IndexOf(_item)].Content = htmlNode.InnerText;
        }

        public void HandleOnNavigatedFrom(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {

        }

        #endregion


        #region Properties

        public static ItemDetailPage _view;


        /// <summary>
        /// The <see cref="ItemSelected" /> property's name.
        /// </summary>
        public const string ItemSelectedPropertyName = "ItemSelected";
        private DataItem _ItemSelected = null;
        /// <summary>
        /// Sets and gets the ItemSelected property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DataItem ItemSelected
        {
            get
            {
                return _ItemSelected;
            }

            set
            {
                if (_ItemSelected == value)
                {
                    return;
                }

                RaisePropertyChanging(ItemSelectedPropertyName);
                _ItemSelected = value;
                if (isFirstLoaded && ItemSelected.Content == string.Empty)
                {
                    //LoadContent(ItemSelected).Start();
                    Debug.WriteLine("ItemSelected Changed ...");
                }
                RaisePropertyChanged(ItemSelectedPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="Items" /> property's name.
        /// </summary>
        public const string ItemsPropertyName = "Items";
        private ObservableCollection<DataItem> _Items = null;
        /// <summary>
        /// Sets and gets the Items property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<DataItem> Items
        {
            get
            {
                return _Items;
            }

            set
            {
                if (_Items == value)
                {
                    return;
                }

                RaisePropertyChanging(ItemsPropertyName);
                _Items = value;
                RaisePropertyChanged(ItemsPropertyName);
            }
        }

        #endregion
        
    }
}