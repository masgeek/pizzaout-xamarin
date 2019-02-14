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
    [Register ("PaymentConfirmationViewController")]
    partial class PaymentConfirmationViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnClose { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnPayOrder { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblOrderSummary { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblUssdNumber { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TxtOrderNumber { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TxtOrderTotal { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BtnClose != null) {
                BtnClose.Dispose ();
                BtnClose = null;
            }

            if (BtnPayOrder != null) {
                BtnPayOrder.Dispose ();
                BtnPayOrder = null;
            }

            if (LblOrderSummary != null) {
                LblOrderSummary.Dispose ();
                LblOrderSummary = null;
            }

            if (LblUssdNumber != null) {
                LblUssdNumber.Dispose ();
                LblUssdNumber = null;
            }

            if (TxtOrderNumber != null) {
                TxtOrderNumber.Dispose ();
                TxtOrderNumber = null;
            }

            if (TxtOrderTotal != null) {
                TxtOrderTotal.Dispose ();
                TxtOrderTotal = null;
            }
        }
    }
}