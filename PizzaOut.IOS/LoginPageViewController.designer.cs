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
    [Register ("LoginPageViewController")]
    partial class LoginPageViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnLogin { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField PasswordTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SignUpButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField UserNameTextView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BtnLogin != null) {
                BtnLogin.Dispose ();
                BtnLogin = null;
            }

            if (PasswordTextView != null) {
                PasswordTextView.Dispose ();
                PasswordTextView = null;
            }

            if (SignUpButton != null) {
                SignUpButton.Dispose ();
                SignUpButton = null;
            }

            if (UserNameTextView != null) {
                UserNameTextView.Dispose ();
                UserNameTextView = null;
            }
        }
    }
}