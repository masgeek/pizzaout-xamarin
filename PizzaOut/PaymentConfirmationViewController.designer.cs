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
    [Register ("PaymentConfirmationViewController")]
    partial class PaymentConfirmationViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnPayOrder { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblOrderHeader { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblPaymentNumber { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtOrderNumber { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtOrderTotal { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnPayOrder != null) {
                btnPayOrder.Dispose ();
                btnPayOrder = null;
            }

            if (lblOrderHeader != null) {
                lblOrderHeader.Dispose ();
                lblOrderHeader = null;
            }

            if (lblPaymentNumber != null) {
                lblPaymentNumber.Dispose ();
                lblPaymentNumber = null;
            }

            if (txtOrderNumber != null) {
                txtOrderNumber.Dispose ();
                txtOrderNumber = null;
            }

            if (txtOrderTotal != null) {
                txtOrderTotal.Dispose ();
                txtOrderTotal = null;
            }
        }
    }
}