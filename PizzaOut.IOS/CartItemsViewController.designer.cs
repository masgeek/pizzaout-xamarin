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
    [Register ("CartItemsViewController")]
    partial class CartItemsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView cartItemsTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (cartItemsTable != null) {
                cartItemsTable.Dispose ();
                cartItemsTable = null;
            }
        }
    }
}