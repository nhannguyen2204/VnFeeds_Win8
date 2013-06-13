using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace VnFeeds.UserControls
{
    public sealed partial class ImageLoader : UserControl
    {
        public static DependencyProperty LoadingContentProperty =
          DependencyProperty.Register("LoadingContent",
            typeof(object),
            typeof(ImageLoader), null);

        public static DependencyProperty LoadedContentProperty =
          DependencyProperty.Register("LoadedContent",
            typeof(object),
            typeof(ImageLoader), null);

        public static DependencyProperty FailedContentProperty =
          DependencyProperty.Register("FailedContent",
            typeof(object),
            typeof(ImageLoader), null);

        public static DependencyProperty SourceProperty =
          DependencyProperty.Register("Source",
            typeof(ImageSource),
            typeof(ImageLoader),
            new PropertyMetadata(null, OnSourceChanged));

        public ImageLoader()
        {
            this.InitializeComponent();
        }
        public object LoadingContent
        {
            get
            {
                return (base.GetValue(LoadingContentProperty));
            }
            set
            {
                base.SetValue(LoadingContentProperty, value);
            }
        }

        public object LoadedContent
        {
            get
            {
                return (base.GetValue(LoadedContentProperty));
            }
            set
            {
                base.SetValue(LoadedContentProperty, value);
            }
        }

        public object FailedContent
        {
            get
            {
                return (base.GetValue(FailedContentProperty));
            }
            set
            {
                base.SetValue(FailedContentProperty, value);
            }
        }
        ImageSource _ImageSource = null;
        public ImageSource Source
        {
            get
            {
                return ((ImageSource)base.GetValue(SourceProperty));
            }
            set
            {
                base.SetValue(SourceProperty, value);
            }
        }
        static void OnSourceChanged(DependencyObject sender,
          DependencyPropertyChangedEventArgs args)
        {
            ImageLoader loader = (ImageLoader)sender;
            if (loader.Source is ImageSource && !loader.isFirstLoadedFail)
            {
                loader._ImageSource = loader.Source;
            }
            VisualStateManager.GoToState(loader, "Loading", true);
        }

        bool isFirstLoadedFail = false;
        void OnImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            if (!isFirstLoadedFail)
            {
                //imgDisplay.ReadLocalValue(SourceProperty);
                //imgDisplay.Source = null;
                ////base.SetValue(SourceProperty, imageSource);
                //imgDisplay.Source = _ImageSource;
                //Image image = sender as Image;
                BitmapImage source = imgDisplay.Source as BitmapImage;
                source.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                imgDisplay.Source = null;
                imgDisplay.Source = new BitmapImage(source.UriSource) as ImageSource;

                isFirstLoadedFail = true;
            }
            else
            {
                VisualStateManager.GoToState(this, "Failed", true);
            }
        }
        void OnImageOpened(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Displaying", true);
        }
    }
}

/*

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace App30
{
  public sealed partial class ImageLoader : UserControl
  {
    public static DependencyProperty IsLoadedProperty =
      DependencyProperty.Register("IsLoaded",
        typeof(bool),
        typeof(ImageLoader), new PropertyMetadata(false));

    public static DependencyProperty IsLoadingProperty =
      DependencyProperty.Register("IsLoading",
        typeof(bool),
        typeof(ImageLoader), new PropertyMetadata(false));

    public static DependencyProperty IsFailedProperty =
      DependencyProperty.Register("IsFailed",
        typeof(bool),
        typeof(ImageLoader), new PropertyMetadata(false));

    public static DependencyProperty LoadingContentProperty =
      DependencyProperty.Register("LoadingContent",
        typeof(object),
        typeof(ImageLoader), null);

    public static DependencyProperty FailedContentProperty =
      DependencyProperty.Register("FailedContent",
        typeof(object),
        typeof(ImageLoader), null);

    public static DependencyProperty SourceProperty =
      DependencyProperty.Register("Source",
        typeof(ImageSource),
        typeof(ImageLoader),
        new PropertyMetadata(null, OnSourceChanged));

    public ImageLoader()
    {
      this.InitializeComponent();
    }
    public bool IsLoading
    {
      get
      {
        return ((bool)base.GetValue(IsLoadingProperty));
      }
      set
      {
        base.SetValue(IsLoadingProperty, value);
      }
    }
    public bool IsLoaded
    {
      get
      {
        return ((bool)base.GetValue(IsLoadedProperty));
      }
      set
      {
        base.SetValue(IsLoadedProperty, value);
      }
    }
    public bool IsFailed
    {
      get
      {
        return ((bool)base.GetValue(IsFailedProperty));
      }
      set
      {
        base.SetValue(IsFailedProperty, value);
      }
    }
    public object LoadingContent
    {
      get
      {
        return (base.GetValue(LoadingContentProperty));
      }
      set
      {
        base.SetValue(LoadingContentProperty, value);
      }
    }
    public object FailedContent
    {
      get
      {
        return (base.GetValue(FailedContentProperty));
      }
      set
      {
        base.SetValue(FailedContentProperty, value);
      }
    }
    public ImageSource Source
    {
      get
      {
        return ((ImageSource)base.GetValue(SourceProperty));
      }
      set
      {
        base.SetValue(SourceProperty, value);
      }
    }
    static void OnSourceChanged(DependencyObject sender,
      DependencyPropertyChangedEventArgs args)
    {
      ImageLoader loader = (ImageLoader)sender;
      VisualStateManager.GoToState(loader, "Loading", true);
      //loader.IsFailed = false;
      //loader.IsLoaded = false;
      //loader.IsLoading = true;
    }
    void OnImageFailed(object sender, ExceptionRoutedEventArgs e)
    {
      VisualStateManager.GoToState(this, "Failed", true);
      //this.IsLoading = false;
      //this.IsFailed = true;
    }
    void OnImageOpened(object sender, RoutedEventArgs e)
    {
      VisualStateManager.GoToState(this, "Loaded", true);
      //this.IsLoading = false;
      //this.IsLoaded = true;
    }
  }
}
*/