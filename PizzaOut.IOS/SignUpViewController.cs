using System;
using System.Threading.Tasks;
using PizzaData.models;
using PizzaOut.IOS.DataManager;
using PizzaOut.IOS.UIHelpers;
using UIKit;

namespace PizzaOut.IOS
{
	partial class SignUpViewController : UIViewController
	{

	    private RestActions _restActions;
	    LoadingOverlay _loadPop;

        public SignUpViewController (IntPtr handle) : base (handle)
		{
		   
		}

	    public override void ViewDidLoad()
	    {
	        base.ViewDidLoad();

	        //set the width and content size
	        // var contentWidth = registerScrollView.Bounds.Width;

	        //var contentHeight = registerScrollView.Bounds.Height * 3;

	        //registerScrollView.ContentSize = CGSizeMake(contentWidth, contentHeight);
	        //registerScrollView.ContentSize = new CGSize(contentWidth, contentHeight);

	        //var subviewHeight =120;
	        //var currentViewOffset = 0;

	        //create subviews
	        //while (currentViewOffset < contentHeight)
	        //{
	        //var frame = new CGRect(0, currentViewOffset, contentWidth, subviewHeight).Inset(dx: 5, dy: 5);
	        //var hue = currentViewOffset / contentHeight;
	        //var subview = new UIView(frame: frame)
	        //{
	        //BackgroundColor = new UIColor(red: hue, green: 1, blue: 1, alpha: 1)
	        //};

	        //registerScrollView.AddSubview(subview);


	        //currentViewOffset = currentViewOffset + subviewHeight;

	        //  }
            _restActions = new RestActions();
        }

	    public override void ViewWillAppear(bool animated)
	    {
	        base.ViewWillAppear(animated);
	        //registerScrollView.SubscribeKeyboardManager();
        }

    }
}
