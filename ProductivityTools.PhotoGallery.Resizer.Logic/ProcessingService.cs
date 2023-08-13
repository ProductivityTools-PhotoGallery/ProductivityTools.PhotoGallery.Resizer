namespace ProductivityTools.PhotoGallery.Resizer.Logic
{
    public class ProcessingService
    {

        public void ValidateThumbNails(string path, string thumbnailPath, List<int> targetSizes)
        {
            foreach(var size in targetSizes)
            {
                ValidateThumbNails(path, thumbnailPath, size);  
            }
        }
        private bool ValidateThumbNails(string path, string thumbnailPath, int size)
        {
            var thumbNailDirectory = path.Replace(path, thumbnailPath);
            thumbNailDirectory = Path.Join(thumbNailDirectory, size.ToString());
            bool exists = Directory.Exists(thumbNailDirectory);
            if (!exists)
            {
                Directory.CreateDirectory(thumbNailDirectory);
            }
            string[] files = Directory.GetFiles(path, "*jpg");
            foreach (var file in files)
            {
                var pathFileName = Path.GetFullPath(file);
                var thumbNailFileName = pathFileName.Replace(path, thumbnailPath);
                var directory = Path.GetDirectoryName(thumbNailFileName);
                var fileName = Path.GetFileName(thumbNailFileName);
                var thumbNailFileNameWithSize = Path.Join(directory, size.ToString(), fileName);
                if (System.IO.File.Exists(thumbNailFileNameWithSize) == false)
                {
                    ConvertImage(pathFileName, thumbNailFileNameWithSize, size);
                }
            }
            return false;
        }

        //public void ConvertImages(string path, string thumbnailPath, List<int> targetSizes)
        //{
        //    foreach (var size in targetSizes)
        //    {
        //        ConvertImage(path, thumbnailPath, size);
        //    }
        //}
        private void ConvertImage(string path, string thumbnailPath, int targetSize)
        {
            var image = NetVips.Image.NewFromFile(path);

            NetVips.Image thumbnail = image.ThumbnailImage(targetSize);
            //var targetThumbnailPath = Path.Join(thumbnailPath,)
            thumbnail.WriteToFile(thumbnailPath);
        }
    }
}