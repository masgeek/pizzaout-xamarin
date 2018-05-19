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
    [Register ("MyCartViewController")]
    partial class MyCartViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnDeliveryAddress { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnDeliveryTime { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnPay { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker deliveryDatePicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTotalAmount { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BtnDeliveryAddress != null) {
                BtnDeliveryAddress.Dispose ();
                BtnDeliveryAddress = null;
            }

            if (BtnDeliveryTime != null) {
                BtnDeliveryTime.Dispose ();
                BtnDeliveryTime = null;
            }

            if (BtnPay != null) {
                BtnPay.Dispose ();
                BtnPay = null;
            }

            if (deliveryDatePicker != null) {
                deliveryDatePicker.Dispose ();
                deliveryDatePicker = null;
            }

            if (lblTotalAmount != null) {
                lblTotalAmount.Dispose ();
                lblTotalAmount = null;
            }
        }
    }
}