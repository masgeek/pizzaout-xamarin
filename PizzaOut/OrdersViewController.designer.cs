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
    [Register ("OrdersViewController")]
    partial class OrdersViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnClosedOrders { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnPaidOrders { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnPendingOrders { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView OrdersViewController { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnClosedOrders != null) {
                btnClosedOrders.Dispose ();
                btnClosedOrders = null;
            }

            if (btnPaidOrders != null) {
                btnPaidOrders.Dispose ();
                btnPaidOrders = null;
            }

            if (btnPendingOrders != null) {
                btnPendingOrders.Dispose ();
                btnPendingOrders = null;
            }

            if (OrdersViewController != null) {
                OrdersViewController.Dispose ();
                OrdersViewController = null;
            }
        }
    }
}