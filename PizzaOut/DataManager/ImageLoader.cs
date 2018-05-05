using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FFImageLoading;
using FFImageLoading.Work;
using Foundation;
using UIKit;

namespace PizzaOut.DataManager
{
    public class ImageLoader
    {

        public static TaskParameter LoadImage(string imageUrl)
        {
            return ImageService.Instance.LoadUrl(imageUrl);
        }
    }
}