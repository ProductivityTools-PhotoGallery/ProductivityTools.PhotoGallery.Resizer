using ProductivityTools.PhotoGallery.Resizer.Logic.GaleryMetadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProductivityTools.PhotoGallery.Resizer.Logic
{
    internal class PhotoJasonManager
    {
        private const string MetadataName = ".photo.json";
        void AddPhotoSize(string directory, string fileName, int width, int height)
        {
            var photoMetadataPath = Path.Join(directory, MetadataName);
            Gallery gallery = null;
            if (System.IO.File.Exists(photoMetadataPath))
            {
                gallery = JsonSerializer.Deserialize<Gallery>(photoMetadataPath);
            }
            else
            {
                gallery = new Gallery();
            }
            var image = gallery.ImageList.FirstOrDefault(x => x.Name == fileName);
            if (image == null)
            {
                image = new Image();
                gallery.ImageList.Add(image);
            }
            image.Height = height;
            image.Width = width;

            string json = JsonSerializer.Serialize(gallery);
            File.WriteAllText(photoMetadataPath, json);
        }
    }
}
