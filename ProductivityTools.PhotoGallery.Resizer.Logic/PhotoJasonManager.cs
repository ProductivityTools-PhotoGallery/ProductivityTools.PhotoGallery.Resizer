using ProductivityTools.PhotoGallery.Resizer.Logic.GaleryMetadata;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Image = ProductivityTools.PhotoGallery.Resizer.Logic.GaleryMetadata.Image;

namespace ProductivityTools.PhotoGallery.Resizer.Logic
{
    internal class PhotoJasonManager
    {
        private const string MetadataName = ".photo.json";
        public void  AddPhotoSize(string filePath)
        {
            var photoGalleryPath=Path.GetDirectoryName(filePath);
            var fileName = Path.GetFileName(filePath);
            var photoMetadataPath = Path.Join(photoGalleryPath, MetadataName);
            Gallery gallery = null;
            if (System.IO.File.Exists(photoMetadataPath))
            {
                var joson=File.ReadAllText(photoMetadataPath);
                gallery = JsonSerializer.Deserialize<Gallery>(joson);
            }
            else
            {
                gallery = new Gallery();
            }
            var image = gallery.ImageList.FirstOrDefault(x => x.Name == fileName);
            if (image == null)
            {
                image = new Image();
                image.Name = fileName;
                gallery.ImageList.Add(image);

                Bitmap img = new Bitmap(filePath);

                image.Height = img.Height;
                image.Width = img.Width;
            }
            

            string json = JsonSerializer.Serialize(gallery);
            File.WriteAllText(photoMetadataPath, json);
        }
    }
}
