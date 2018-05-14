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
    [Register ("OrdersViewController")]
    partial class OrdersViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnConfirmedOrders { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnOrderHistory { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnUnpaidOrders { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BtnConfirmedOrders != null) {
                BtnConfirmedOrders.Dispose ();
                BtnConfirmedOrders = null;
            }

            if (BtnOrderHistory != null) {
                BtnOrderHistory.Dispose ();
                BtnOrderHistory = null;
            }

            if (BtnUnpaidOrders != null) {
                BtnUnpaidOrders.Dispose ();
                BtnUnpaidOrders = null;
            }
        }
    }
}