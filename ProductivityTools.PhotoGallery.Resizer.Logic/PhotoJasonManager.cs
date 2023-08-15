using ProductivityTools.PhotoGallery.CoreObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Image = ProductivityTools.PhotoGallery.CoreObjects.Image;

namespace ProductivityTools.PhotoGallery.Resizer.Logic
{
    internal class PhotoJasonManager
    {
        //replace from core objects
        private const string MetadataName = ".photo.json";

        private Gallery Gallery { get; set; }   

        private string GetPhotoMetadataPath(string filePath)
        {
            var photoGalleryPath = Path.GetDirectoryName(filePath);
            var photoMetadataPath = Path.Join(photoGalleryPath, MetadataName);
            return photoMetadataPath; 
        }
        private void LoadGallery(string filePath)
        {
            var photoMetadataPath = GetPhotoMetadataPath(filePath);
            if (System.IO.File.Exists(photoMetadataPath))
            {
                var joson = File.ReadAllText(photoMetadataPath);
                this.Gallery = JsonSerializer.Deserialize<Gallery>(joson);
            }
            else
            {
                this.Gallery = new Gallery();
            }
        }

        private void SaveGallery(string filePath)
        {
            string json = JsonSerializer.Serialize(this.Gallery);
            var photoMetadataPath = GetPhotoMetadataPath(filePath);
            File.WriteAllText(photoMetadataPath, json);
        }

        public void AddPhotoSize(string filePath)
        {
            LoadGallery(filePath);
            var fileName = Path.GetFileName(filePath);
            var image = this.Gallery.ImageList.FirstOrDefault(x => x.Name == fileName);
            if (image == null)
            {
                image = new Image();
                image.Name = fileName;
                this.Gallery.ImageList.Add(image);

                Bitmap img = new Bitmap(filePath);

                image.Height = img.Height;
                image.Width = img.Width;
            }
            SaveGallery(filePath);
        }

        public void AddThumbNailSize(string filePath, int size)
        {
            LoadGallery(filePath);
            if(this.Gallery.ImageSizes.Contains(size)==false)
            {
                this.Gallery.ImageSizes.Add(size);
            }
            SaveGallery(filePath);
        }
    }
}
