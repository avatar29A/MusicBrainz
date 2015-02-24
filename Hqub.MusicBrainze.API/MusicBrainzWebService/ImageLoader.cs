using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;
using Windows.Storage.Streams;
using System.Diagnostics;
using System.IO; // recommended. (don't use System.Net.Http)

namespace MusicBrainzWebService
{
    public class ImageLoader
    {
        private Uri _uri;
        private IBuffer _imageBuffer = null;

        public ImageLoader(Uri uri)
        {
            _uri = uri;
        }

        // it may throw HttpClientException
        public async Task LoadImageAsync()
        {
            MyHttpClient client = new MyHttpClient(_uri);
            HttpResponseMessage response = await client.SendRequestAsync();
            _imageBuffer = await response.Content.ReadAsBufferAsync();
        }

        public async Task<BitmapImage> AsBitmapImageAsync()
        {
            if (_imageBuffer == null)
                return null;

            BitmapImage image = new BitmapImage();
            try
            {
                await image.SetSourceAsync(_imageBuffer.AsStream().AsRandomAccessStream());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
                _imageBuffer = null;
                image = null;
            }
            return image;
        }

        public Stream AsStream()
        {
            if (_imageBuffer == null)
                return null;

            return _imageBuffer.AsStream();
        }
    }
}
