// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace PizzaOut.IOS
{
    [Register ("ItemDetailsViewController")]
    partial class ItemDetailsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnAddToCart { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView itemImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblItemName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblSelectedQuantity { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblSelectedSize { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblTotal { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStepper QuantityPicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStepper SizePicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView TxtItemDesc { get; set; }

        [Action ("ItemSizeChangedEvent:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ItemSizeChangedEvent (UIKit.UIStepper sender);

        void ReleaseDesignerOutlets ()
        {
            if (BtnAddToCart != null) {
                BtnAddToCart.Dispose ();
                BtnAddToCart = null;
            }

            if (itemImage != null) {
                itemImage.Dispose ();
                itemImage = null;
            }

            if (LblItemName != null) {
                LblItemName.Dispose ();
                LblItemName = null;
            }

            if (LblSelectedQuantity != null) {
                LblSelectedQuantity.Dispose ();
                LblSelectedQuantity = null;
            }

            if (LblSelectedSize != null) {
                LblSelectedSize.Dispose ();
                LblSelectedSize = null;
            }

            if (LblTotal != null) {
                LblTotal.Dispose ();
                LblTotal = null;
            }

            if (QuantityPicker != null) {
                QuantityPicker.Dispose ();
                QuantityPicker = null;
            }

            if (SizePicker != null) {
                SizePicker.Dispose ();
                SizePicker = null;
            }

            if (TxtItemDesc != null) {
                TxtItemDesc.Dispose ();
                TxtItemDesc = null;
            }
        }
    }
}