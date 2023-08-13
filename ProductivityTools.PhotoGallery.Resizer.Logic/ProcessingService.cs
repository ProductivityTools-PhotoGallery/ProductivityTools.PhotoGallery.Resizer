namespace ProductivityTools.PhotoGallery.Resizer.Logic
{
    public class ProcessingService
    {
        public void ConvertImage(string path, string thumbnailPath, int targetSize)
        {
            var image = NetVips.Image.NewFromFile(path);

            NetVips.Image thumbnail = image.ThumbnailImage(targetSize);
            //var targetThumbnailPath = Path.Join(thumbnailPath,)
            thumbnail.WriteToFile(thumbnailPath);
        }
    }
}