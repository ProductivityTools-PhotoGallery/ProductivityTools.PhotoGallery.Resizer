namespace ProductivityTools.PhotoGallery.Resizer.Logic
{
    public class ProcessingService
    {

        

        public void ConvertImages(string path, string thumbnailPath, List<int> targetSizes)
        {
            foreach (var size in targetSizes)
            {
                ConvertImage(path, thumbnailPath, size);
            }
        }
        private void ConvertImage(string path, string thumbnailPath, int targetSize)
        {
            var image = NetVips.Image.NewFromFile(path);

            NetVips.Image thumbnail = image.ThumbnailImage(targetSize);
            //var targetThumbnailPath = Path.Join(thumbnailPath,)
            thumbnail.WriteToFile(thumbnailPath);
        }
    }
}