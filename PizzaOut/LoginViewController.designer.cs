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
    [Register ("LoginViewController")]
    partial class LoginViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnBackButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnLogin { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSignUp { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtUserName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnBackButton != null) {
                btnBackButton.Dispose ();
                btnBackButton = null;
            }

            if (btnLogin != null) {
                btnLogin.Dispose ();
                btnLogin = null;
            }

            if (btnSignUp != null) {
                btnSignUp.Dispose ();
                btnSignUp = null;
            }

            if (txtPassword != null) {
                txtPassword.Dispose ();
                txtPassword = null;
            }

            if (txtUserName != null) {
                txtUserName.Dispose ();
                txtUserName = null;
            }
        }
    }
}