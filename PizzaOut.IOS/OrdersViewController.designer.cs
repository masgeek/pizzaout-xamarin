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
        UIKit.UIView orderItemsView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (orderItemsView != null) {
                orderItemsView.Dispose ();
                orderItemsView = null;
            }
        }
    }
}