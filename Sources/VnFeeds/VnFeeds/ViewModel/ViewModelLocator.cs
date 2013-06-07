/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:VnFeeds"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace VnFeeds.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public static ViewModelLocator Current;


        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            Current = this;
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}



            SimpleIoc.Default.Register<GroupedItemsViewModel>();
            //SimpleIoc.Default.Register<MainViewModel>();
        }

        //public MainViewModel Main
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<MainViewModel>();
        //    }
        //}

        public GroupedItemsViewModel GroupedItems
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GroupedItemsViewModel>();
            }
        }

        public GroupDetailViewModel GroupDetail
        {
            get
            {
                if (!SimpleIoc.Default.ContainsCreated<GroupDetailViewModel>())
                    SimpleIoc.Default.Register<GroupDetailViewModel>();
                return ServiceLocator.Current.GetInstance<GroupDetailViewModel>();
            }
        }

        public ItemDetailViewModel ItemDetail
        {
            get
            {
                if (!SimpleIoc.Default.ContainsCreated<ItemDetailViewModel>())
                    SimpleIoc.Default.Register<ItemDetailViewModel>();
                return ServiceLocator.Current.GetInstance<ItemDetailViewModel>();
            }
        }



        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}