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
    [Register ("MyCartViewController")]
    partial class MyCartViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnDeliveryAddress { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnDeliveryTime { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnPay { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnViewItems { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker dtDeliveryDate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTotal { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnDeliveryAddress != null) {
                btnDeliveryAddress.Dispose ();
                btnDeliveryAddress = null;
            }

            if (btnDeliveryTime != null) {
                btnDeliveryTime.Dispose ();
                btnDeliveryTime = null;
            }

            if (btnPay != null) {
                btnPay.Dispose ();
                btnPay = null;
            }

            if (btnViewItems != null) {
                btnViewItems.Dispose ();
                btnViewItems = null;
            }

            if (dtDeliveryDate != null) {
                dtDeliveryDate.Dispose ();
                dtDeliveryDate = null;
            }

            if (lblTotal != null) {
                lblTotal.Dispose ();
                lblTotal = null;
            }
        }
    }
}