﻿// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//

using System.CodeDom.Compiler;
using Foundation;

namespace PizzaOut.IOS
{
    [Register ("OurMenuViewController")]
    partial class OurMenuViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView menuTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (menuTableView != null) {
                menuTableView.Dispose ();
                menuTableView = null;
            }
        }
    }
}