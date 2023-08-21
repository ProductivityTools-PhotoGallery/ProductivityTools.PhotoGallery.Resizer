namespace ProductivityTools.PhotoGallery.Resizer.Logic
{
    public class ProcessingService
    {
        private readonly Action<string> Logger;
        private readonly PhotoJasonManager PhotoJasonManager;
        public ProcessingService(Action<string> logger)
        {

            this.Logger = logger;
            this.PhotoJasonManager = new PhotoJasonManager();
        }
        public void ValidateThumNailsForAllDirectories(string photoDirectoryContainer, string thumbnailPath, List<int> targetWidthSizes)
        {
            string[] directories = Directory.GetDirectories(photoDirectoryContainer);
            foreach (string photoDirectory in directories)
            {
                this.Logger(string.Format($"Validating {photoDirectory}"));
                ValidateThumbNails(photoDirectoryContainer, photoDirectory, thumbnailPath, targetWidthSizes);
            }
        }
        private void ValidateThumbNails(string photoDirectoryMasterContainer, string originalPhotoDirectory, string thumbnailPath, List<int> targetWidthSizes)
        {
            var thumbNailDirectory = originalPhotoDirectory.Replace(photoDirectoryMasterContainer, thumbnailPath);
            bool exists = Directory.Exists(thumbNailDirectory);
            if (!exists)
            {
                Directory.CreateDirectory(thumbNailDirectory);
            }
            string[] files = Directory.GetFiles(originalPhotoDirectory, "*jpg");
            foreach (var file in files)
            {
                ProcessPicture(photoDirectoryMasterContainer, originalPhotoDirectory, thumbnailPath, file, targetWidthSizes);
            }

        }

        private void ProcessPicture(string photoDirectoryMasterContainer, string originalPhotoDirectory, string thumbNailDirectory, string filePath, List<int> targetWidthSizes)
        {
            this.PhotoJasonManager.AddPhotoSize(filePath);
            foreach (var targetWidthSize in targetWidthSizes)
            {
                this.Logger(string.Format($"Validating size: {targetWidthSize} for directory: {originalPhotoDirectory}"));
                this.PhotoJasonManager.AddThumbNailSize(filePath,targetWidthSize);
                ValidateThumbNails(photoDirectoryMasterContainer, originalPhotoDirectory, thumbNailDirectory, filePath, targetWidthSize);
            }
        }
        private bool ValidateThumbNails(string photoDirectoryMasterContainer, string photoWorkingDirectory, string thumbnailPath, string filePath, int targetWidthSize)
        {
            //var thumbNailDirectory = photoWorkingDirectory.Replace(photoDirectoryMasterContainer, thumbnailPath);
            var thumbNailSizeDirectory = Path.Join(thumbnailPath, targetWidthSize.ToString());
            
            //string[] files = Directory.GetFiles(photoWorkingDirectory, "*jpg");
            //foreach (var file in files)
            //{
            var pathFileName = Path.GetFullPath(filePath);
            var thumbNailFileName = pathFileName.Replace(photoDirectoryMasterContainer, thumbnailPath);
            var directory = Path.GetDirectoryName(thumbNailFileName);
            var thumbNailDirectory=Path.Join(directory, targetWidthSize.ToString());
            bool exists = Directory.Exists(thumbNailDirectory);
            if (!exists)
            {
                Directory.CreateDirectory(thumbNailDirectory);
            }
            var fileName = Path.GetFileName(thumbNailFileName);
            var thumbNailFileNameWithSize = Path.Join(thumbNailDirectory, fileName);
           
            if (System.IO.File.Exists(thumbNailFileNameWithSize) == false)
            {
                ConvertImage(pathFileName, thumbNailFileNameWithSize, targetWidthSize);
            }
            //}
            return false;
        }

        //public void ConvertImages(string path, string thumbnailPath, List<int> targetSizes)
        //{
        //    foreach (var size in targetSizes)
        //    {
        //        ConvertImage(path, thumbnailPath, size);
        //    }
        //}
        private void ConvertImage(string originalPhotoDirectory, string thumbnailPath, int targetWidthSize)
        {
            var image = NetVips.Image.NewFromFile(originalPhotoDirectory);

            NetVips.Image thumbnail = image.ThumbnailImage(targetWidthSize);
            //var targetThumbnailPath = Path.Join(thumbnailPath,)
            thumbnail.WriteToFile(thumbnailPath);
        }
    }
}