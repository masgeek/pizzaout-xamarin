using Foundation;
using System;
using UIKit;

namespace PizzaOut
{
    public partial class MenuViewController : UIViewController
    {
        public MenuViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "Our Menu";
        }
    }
}