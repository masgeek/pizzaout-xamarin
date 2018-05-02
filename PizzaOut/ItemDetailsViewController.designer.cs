// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace PizzaOut
{
    [Register ("ItemDetailsViewController")]
    partial class ItemDetailsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnAddToCart { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView itemImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView lblItemDesc { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblItemName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStepper quantityStepper { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel quantityValue { get; set; }

        [Action ("QuantityChangedEvent:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void QuantityChangedEvent (UIKit.UIStepper sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnAddToCart != null) {
                btnAddToCart.Dispose ();
                btnAddToCart = null;
            }

            if (itemImage != null) {
                itemImage.Dispose ();
                itemImage = null;
            }

            if (lblItemDesc != null) {
                lblItemDesc.Dispose ();
                lblItemDesc = null;
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
        }
    }
}