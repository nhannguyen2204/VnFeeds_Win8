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
            string contentStr = string.Empty;
            try
            {
                contentStr = await Define.DownloadStringAsync(_item.Link);
            }
            catch (Exception ex)
            {

                //throw;
            }

            if (contentStr != string.Empty)
            {
                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(contentStr);

                HtmlAgilityPack.HtmlNode htmlNode = htmlDoc.GetElementbyId("ContentContainer");
                while (htmlNode.Descendants("script").Count() > 0)
                {
                    htmlNode.Descendants("script").ElementAt(0).Remove();
                }
                while (htmlNode.Descendants("meta").Count() > 0)
                {
                    htmlNode.Descendants("meta").ElementAt(0).Remove();
                }

                //contentStr = "<p><i>This blog post was authored by Andrew Byrne (<a href=\"http://twitter.com/AndrewJByrne\" target=\"_blank\">@AndrewJByrne</a>), a Senior Content Developer on the Windows Phone Developer Content team.</i> <p><i></i> <p><i>- Adam</i></p> <hr>  <p> <table cellspacing=\"1\" cellpadding=\"2\" width=\"722\" border=\"0\"> <tbody> <tr> <td valign=\"top\" width=\"397\"> <p>The Windows Phone Developer Content team is continually adding new code samples that you can download from MSDN. In this post, we introduce you to the 10 latest samples that we’ve posted on MSDN. Each sample includes a readme file that walks you through building and running the sample, and includes links to relevant, related documentation. We hope these samples help you with your app development and we look forward to providing more samples as we move forward. You can check out all of our <a href=\"http://code.msdn.microsoft.com/wpapps/site/search?f%5B0%5D.Type=Contributors&amp;f%5B0%5D.Value=Windows%20Phone%20SDK%20Team&amp;f%5B0%5D.Text=Windows%20Phone%20SDK&amp;sortBy=Date\" target=\"_blank\">samples on MSDN</a>.</p></td> <td valign=\"top\" width=\"320\"> <p><a href=\"http://blogs.windows.com/cfs-file.ashx/__key/communityserver-blogs-components-weblogfiles/00-00-00-53-84-metablogapi/clip_5F00_image002_5F00_1765A66A.png\"><img title=\"clip_image002\" style=\"border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; border-left: 0px; display: block; padding-right: 0px; margin-right: auto\" border=\"0\" alt=\"clip_image002\" src=\"http://blogs.windows.com/cfs-file.ashx/__key/communityserver-blogs-components-weblogfiles/00-00-00-53-84-metablogapi/clip_5F00_image002_5F00_thumb_5F00_7B083E7C.png\" width=\"121\" height=\"213\"></a></p></td></tr></tbody></table></p> <h3><a href=\"http://go.microsoft.com/fwlink/?LinkId=306704\" target=\"_blank\">Stored Contacts Sample</a></h3> <p>This sample illustrates how to use the <a href=\"http://msdn.microsoft.com/en-us/library/windowsphone/develop/windows.phone.personalinformation.contactstore(v=vs.105).aspx\" target=\"_blank\">ContactStore</a> class and related APIs to create a contact store for your app. This feature is useful if your app uses an existing cloud-based contact store. You can use the APIs you to create contacts on the phone that represent the contacts in your remote store. You can display and modify the contacts in the People Hub on your phone, just like contacts that are added through the built-in experience. You can use the APIs to update and delete contacts you have created on the phone and also to query for any changes the user has made to the contacts locally so you can sync those changes to your remote store. <h3><a href=\"http://go.microsoft.com/fwlink/?LinkId=306701\" target=\"_blank\">Basic Storage Recipes</a></h3> <p>This is a “Windows Runtime Storage 101” sample for Windows Phone developers moving from isolated storage and <b>System.IO</b> to <a href=\"http://msdn.microsoft.com/en-us/library/windowsphone/develop/windows.storage.aspx\" target=\"_blank\">Windows.Storage</a> and <a href=\"http://msdn.microsoft.com/en-us/library/windowsphone/develop/windows.storage.streams.aspx\" target=\"_blank\">Windows.Storage.Streams</a>. The sample demonstrates how to write to and read files, in addition to how to enumerate directory trees. It also demonstrates how to pass data from one page to the next, and how to persist application state when the app is deactivated. <h3><a href=\"http://go.microsoft.com/fwlink/?LinkId=301509\" target=\"_blank\">Trial Experience Sample</a></h3> <p>This sample shows you how to design your app to detect its license state when the app launches, and how to detect changes to the license state while running. It comes with a helper class that you can use in your app to wrap <a href=\"http://msdn.microsoft.com/en-us/library/windowsphone/develop/windows.applicationmodel.store.licenseinformation.aspx\" target=\"_blank\">LicenseInformation</a> functionality. <h3><a href=\"http://go.microsoft.com/fwlink/?LinkId=302059\" target=\"_blank\">Windows Runtime Component Sample</a></h3> <p>This sample demonstrates the basics of creating a Windows Phone Runtime component in C++ and consuming it in a XAML app. The sample demonstrates three scenarios: the first scenario illustrates how to call synchronous and asynchronous methods to perform a computation. The second scenario uses the same computation component to demonstrate progress reporting and cancellation of long-running tasks. Finally, the third scenario shows how to use a component to wrap logic that uses <a href=\"http://msdn.microsoft.com/en-us/library/windowsphone/develop/jj206944(v=vs.105).aspx\" target=\"_blank\">XAudio2 APIs</a> to play a sound. <h3><a href=\"http://go.microsoft.com/fwlink/?LinkId=306097\" target=\"_blank\">Company Hub Sample</a></h3> <p>This sample demonstrates the construction of an app that is capable of deploying line-of-business (LOB) apps to employees of a corporation. The sample uses an example XML file to define the company XAPs that are available to employees for secure download, and shows you how to dynamically access that file at run time. Then it shows you how to install company apps, enumerate the apps, and then launch the installed company apps. This app is just an example framework and requires additional work beyond the sample to be functional. <h3><a href=\"http://go.microsoft.com/fwlink/?LinkId=306702\" target=\"_blank\">Image Recipes</a></h3> <p>This sample illustrates how to use images in your app efficiently, while giving your users a great experience. It tackles downsampling images, implementing pinch and zoom, and downloading images with a progress display and an option to cancel the download. We’ve taken a recipe approach: each recipe is delivered in a self-contained page in the app so you can focus your attention on the particular part of the sample you are most interested in.  <h3><a href=\"http://go.microsoft.com/fwlink/?LinkId=306026\" target=\"_blank\">Azure Voice Notes</a></h3> <p>This sample uses Windows Phone speech recognition APIs and Windows Azure Mobile Services to record voice notes as text and store the notes in the cloud. It shows how Mobile Services can be used to authenticate a user with their Microsoft Account. It also demonstrates how to use Mobile Services to store, retrieve, and delete data from an Azure database table. The app generates text from speech using the Windows Phone speech recognition APIs and the phone’s predefined dictation grammar. <h3><a href=\"http://go.microsoft.com/fwlink/?LinkId=299241\" target=\"_blank\">Kid's Corner Sample</a></h3> <p>This sample illustrates how to use the <a href=\"http://msdn.microsoft.com/en-us/library/windowsphone/develop/windows.phone.applicationmodel.applicationprofile.modes(v=vs.105).aspx\" target=\"_blank\">ApplicationProfile.Modes</a> property to recognize Kid’s Corner mode. When the app runs, it checks the <a href=\"http://msdn.microsoft.com/en-us/library/windowsphone/develop/windows.phone.applicationmodel.applicationprofile.modes(v=vs.105).aspx\" target=\"_blank\">ApplicationProfile.Modes</a> property. If the value is <b>ApplicationProfileModes.Alternate</b>, you’ll know that the app is running in Kid’s Corner mode. Depending on the content of your app, you may want to change its appearance or features when it runs in Kid’s Corner mode. Some features that you should consider disabling when running in Kid’s Corner mode include in-app purchases, launching the web browser, and the ad control. <h3><a href=\"http://go.microsoft.com/fwlink/?LinkId=267468\" target=\"_blank\">URI Association Sample</a></h3> <p>Use this sample to learn how to automatically launch your app via URI association. This sample includes three apps: a URI launcher, and two apps that handle the URI schemes that are built in to the launcher app. You can launch an app that is included with the sample or edit the URI and launch a different app. There is also a button for launching a URI on another phone using Near Field Communication (NFC).  <h3><a href=\"http://go.microsoft.com/fwlink/?LinkId=275007\" target=\"_blank\">Speech for Windows Phone: Speech recognition using a custom grammar</a></h3> <p>A grammar defines the words and phrases that an app will recognize in speech input. This sample shows you how to go beyond the basics to create a powerful grammar (based on the grammar schema) with which your app can recognize commands and phrases constructed in different ways. <p>&nbsp; <p>This post is just a glimpse of the latest Windows Phone samples we’ve added to the MSDN code gallery. From launching apps with URI associations, to dictating notes and storing them in the cloud, we hope that there’s something for everyone. We’ll be sure to keep you posted as new samples are added to the collection, so stay tuned. In the meantime, grab the samples, experiment with them, and use the code to light up your apps. You can download all Windows Phone samples at <a href=\"http://code.msdn.microsoft.com/wpapps\" target=\"_blank\">http://code.msdn.microsoft.com/wpapps</a>. <div style=\"clear:both;\"></div><img src=\"http://blogs.windows.com/aggbug.aspx?PostID=588575&AppID=5384&AppType=Weblog&ContentType=0\" width=\"1\" height=\"1\">";
                Items[Items.IndexOf(_item)].Content = htmlNode.InnerHtml.Replace("\r", "").Replace("\n", "");
            }
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
                if (isFirstLoaded && ItemSelected != null && ItemSelected.Content == string.Empty)
                {
                    LoadContent(ItemSelected);
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