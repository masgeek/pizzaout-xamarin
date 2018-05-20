using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;

namespace PizzaOut.IOS.UIHelpers
{
    /// <summary>
    /// This class cannot be inherited
    ///
    ///var bounds = UIScreen.MainScreen.Bounds;
    /// 
    /// show the loading overlay on the UI thread using the correct orientation sizing
    /// loadPop = new LoadingOverlay (bounds); // using field from step 2
    /// View.Add (loadPop);
    /// </summary>
    public sealed class LoadingOverlay:UIView
    {
        // control declarations
        /// <summary>
        /// var bounds = UIScreen.MainScreen.Bounds;
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="message"></param>
        public LoadingOverlay(CGRect frame,string message = "Loading Items...") : base(frame)
        {
            // configurable bits
            BackgroundColor = UIColor.Black;
            Alpha = 0.75f;
            AutoresizingMask = UIViewAutoresizing.All;

            nfloat labelHeight = 22;
            nfloat labelWidth = Frame.Width - 20;

            // derive the center x and y
            nfloat centerX = Frame.Width / 2;
            nfloat centerY = Frame.Height / 2;

            // create the activity spinner, center it horizontall and put it 5 points above center x
            var activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
            activitySpinner.Frame = new CGRect(
                centerX - (activitySpinner.Frame.Width / 2),
                centerY - activitySpinner.Frame.Height - 20,
                activitySpinner.Frame.Width,
                activitySpinner.Frame.Height);
            activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
            AddSubview(activitySpinner);
            activitySpinner.StartAnimating();

            // create and configure the "Loading Data" label
            var loadingLabel = new UILabel(new CGRect(
                centerX - (labelWidth / 2),
                centerY + 20,
                labelWidth,
                labelHeight
            ))
            {
                BackgroundColor = UIColor.Clear,
                TextColor = UIColor.White,
                Text = message,
                TextAlignment = UITextAlignment.Center,
                AutoresizingMask = UIViewAutoresizing.All
            };

            AddSubview(loadingLabel);
        }

        /// <summary>
        /// Fades out the control and then removes it from the super view
        /// </summary>
        /// <param name="duration"></param>
        public void Hide(double duration = 0.5)
        {
            UIView.Animate(
                duration, // duration
                () => { Alpha = 0; },
                completion: () => { RemoveFromSuperview(); }
            );
        }
    }
}