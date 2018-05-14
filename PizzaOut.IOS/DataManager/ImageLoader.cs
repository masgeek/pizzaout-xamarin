using FFImageLoading;
using FFImageLoading.Work;

namespace PizzaOut.IOS.DataManager
{
    public class ImageLoader
    {

        public static TaskParameter LoadImage(string imageUrl)
        {
            return ImageService.Instance.LoadUrl(imageUrl);
        }
    }
}