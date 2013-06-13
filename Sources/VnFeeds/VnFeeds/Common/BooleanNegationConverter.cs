using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace VnFeeds.Common
{
    /// <summary>
    /// Value converter that translates true to false and vice versa.
    /// </summary>
    public sealed class BooleanNegationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(value is bool && (bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !(value is bool && (bool)value);
        }
    }



    public sealed class ImageUri2ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is Uri)
            {
                Uri imageUri = (Uri)value;
                return getImage(imageUri).Result;
            }

            return null;
        }

        private async Task<BitmapImage> getImage(Uri uri)
        {
            
            //StorageFile file = await StorageFile.GetFileFromPathAsync(uri.AbsoluteUri);

            //IRandomAccessStreamReference thumbnail = RandomAccessStreamReference.CreateFromUri(uri);
            //StorageFile file = await StorageFile.CreateStreamedFileFromUriAsync("duyuru.jpg", uri, thumbnail);

            Stream stream = await Define.DownloadStreamAsync(new Uri("http://www.adobe.com/content/dam/Adobe/en/products/acrobat/pdfs/adobe-acrobat-xi-esign-pdf-file-tutorial-ue.pdf"));

            BitmapImage bitmapImage = new BitmapImage();
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            IRandomAccessStream a1 = await ConvertToRandomAccessStream(ms);
            await bitmapImage.SetSourceAsync(a1);
            return bitmapImage;
        }

        public static async Task<IRandomAccessStream> ConvertToRandomAccessStream(MemoryStream memoryStream)
        {
            var randomAccessStream = new InMemoryRandomAccessStream();
            var outputStream = randomAccessStream.GetOutputStreamAt(0);
            var dw = new DataWriter(outputStream);
            var task = Task.Factory.StartNew(() => dw.WriteBytes(memoryStream.ToArray()));
            await task;
            await dw.StoreAsync();
            await outputStream.FlushAsync();
            return randomAccessStream;
        }


        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !(value is bool && (bool)value);
        }
    }
}
