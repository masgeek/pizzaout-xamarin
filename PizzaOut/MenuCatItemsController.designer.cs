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
    [Register ("MenuCatItemsController")]
    partial class MenuCatItemsController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView menuCatItemsTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (menuCatItemsTable != null) {
                menuCatItemsTable.Dispose ();
                menuCatItemsTable = null;
            }
        }
    }
}