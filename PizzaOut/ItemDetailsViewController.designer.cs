// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using System.Threading.Tasks;
using UIKit;

namespace PizzaOut
{
    [Register ("ItemDetailsViewController")]
    partial class ItemDetailsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView activityIndicator { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnAddToCart { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView itemImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblItemName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStepper quantityStepper { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel quantityValue { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStepper sizeStepper { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel sizeValue { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel totalCost { get; set; }

        [Action ("ItemSizeChangedEvent:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ItemSizeChangedEvent (UIKit.UIStepper sender);

        [Action ("SizeChangedEvent:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SizeChangedEvent (UIKit.UIStepper sender);

        void ReleaseDesignerOutlets ()
        {
            if (activityIndicator != null) {
                activityIndicator.Dispose ();
                activityIndicator = null;
            }

            if (btnAddToCart != null) {
                btnAddToCart.Dispose ();
                btnAddToCart = null;
            }

            if (itemImage != null) {
                itemImage.Dispose ();
                itemImage = null;
            }

            if (lblItemName != null) {
                lblItemName.Dispose ();
                lblItemName = null;
            }

            if (quantityStepper != null) {
                quantityStepper.Dispose ();
                quantityStepper = null;
            }

            if (quantityValue != null) {
                quantityValue.Dispose ();
                quantityValue = null;
            }

            if (sizeStepper != null) {
                sizeStepper.Dispose ();
                sizeStepper = null;
            }

            if (sizeValue != null) {
                sizeValue.Dispose ();
                sizeValue = null;
            }

            if (totalCost != null) {
                totalCost.Dispose ();
                totalCost = null;
            }
        }
    }
}