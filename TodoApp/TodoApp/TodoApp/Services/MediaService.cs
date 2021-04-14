using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TodoApp.Services
{
    public class MediaService : IMediaService
    {
        public async Task<string> CapturePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();

                // canceled
                if (photo == null)
                {
                    return null;
                }

                // save the file into local storage
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                return newFile;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
