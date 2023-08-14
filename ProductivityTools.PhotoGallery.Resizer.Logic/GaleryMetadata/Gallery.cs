using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PhotoGallery.Resizer.Logic.GaleryMetadata
{

    internal class Gallery
    {
        public Gallery()
        {
            this.ImageList = new List<Image>();
            this.ImageSizes = new List<int>();
        }
        public List<string> ReadGmails { get; set; }

        public List<Image> ImageList { get; set; }
        public List<int> ImageSizes { get; set; }
    } 
}
