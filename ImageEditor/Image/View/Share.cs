using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using nativeWindows = Windows;

namespace Syncfusion.SampleBrowser.UWP.ImageEditor
{
    public class Share
    {
        private string _title;
        private string _message;
        private readonly DataTransferManager _dataTransferManager;
        private Stream _imagestream;
        public Share()
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(ShareTextHandler);
        }

        /// <summary>
        /// MUST BE CALLED FROM THE UI THREAD
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task Show(string title, string message, Stream imagestream)
        {
            _title = title ?? string.Empty;
            _message = message ?? string.Empty;
            _imagestream = imagestream;
            try
            {
                DataTransferManager.ShowShareUI();
            }
            catch (Exception)
            {

               
            }
            return Task.FromResult(true);
        }

        private async void ShareTextHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            DataRequest request = e.Request;
            request.Data.Properties.Title = string.IsNullOrEmpty(_title) ? nativeWindows.ApplicationModel.Package.Current.DisplayName : _title;
            DataRequestDeferral deferral = request.GetDeferral();

            try
            {
                var bytearray = ConvertToByte(_imagestream);
                var randomstream = ConvertToStream(bytearray);
                RandomAccessStreamReference iReferenceStream = RandomAccessStreamReference.CreateFromStream(randomstream.Result);
                request.Data.SetBitmap(iReferenceStream);
            }
            catch
            {

            }
            finally
            {
                deferral.Complete();
            }
        }
        public static byte[] ConvertToByte(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static async Task<InMemoryRandomAccessStream> ConvertToStream(byte[] arr)
        {
            InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream();
            await randomAccessStream.WriteAsync(arr.AsBuffer());
            randomAccessStream.Seek(0);
            return randomAccessStream;
        }
    }
}
