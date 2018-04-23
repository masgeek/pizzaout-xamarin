using Foundation;
using System;
using UIKit;

namespace PizzaOut
{
    public partial class SignUpViewController : UIViewController
    {
        public SignUpViewController (IntPtr handle) : base (handle)
        {
        }

        partial void BtnBack_TouchUpInside(UIButton sender)
        {
            UIStoryboard board = UIStoryboard.FromName("Main", null);
            UIViewController ctrl = (UIViewController)board.InstantiateViewController("TabViewController");
            ctrl.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
            this.PresentViewController(ctrl, true, null);
        }
    }
}