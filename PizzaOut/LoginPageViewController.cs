using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaData.Helpers;
using PizzaData.models;
using PizzaData.Rest;
using PizzaOut.DataManager;
using RestSharp;
using UIKit;

namespace PizzaOut
{
	partial class LoginPageViewController : UIViewController
	{
        //Create an event when a authentication is successful
        public event EventHandler OnLoginSuccess;
	    private string username, password;

	    private RestActions _restActions;
	    private User _userModel;


        public LoginPageViewController(IntPtr handle) : base(handle)
	    {
	  
        }


        public override void ViewDidLoad()
	    {
	        base.ViewDidLoad();

	        _restActions = new RestActions();
	        _userModel = new User();

            UserNameTextView.Text = "fatelord";
	        PasswordTextView.Text = "andalite6";

    
            //set buton click actions
	        BtnLogin.TouchUpInside += async (object sender, EventArgs e) => { await BtnLogin_TouchUpInside(sender); };
	    }

        private async Task BtnLogin_TouchUpInside(object sender)
        {
            //Validate our Username & Password.
            //This is usually a web service call.
            try
            {
                if (IsUserNameValid() && IsPasswordValid())
                {
                    username = UserNameTextView.Text.Trim();
                    password = PasswordTextView.Text.Trim();

                    //We have successfully authenticated a the user,
                    //Now fire our OnLoginSuccess Event.

                    _userModel = await _restActions.LoginUser(username, password);

                    if (_userModel != null)
                    {
                        //new UIAlertView("Login Successful", "Welcome back human", null, "OK", null).Show();
                        OnLoginSuccess?.Invoke(sender, new EventArgs());
                    }

    
                }
                else
                {
#pragma warning disable 618
                    new UIAlertView("Login Error", "Bad user name or password", null, "OK", null).Show();
#pragma warning restore 618
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }



        private bool IsUserNameValid()
        {
            return !String.IsNullOrEmpty(UserNameTextView.Text.Trim());
        }

        private bool IsPasswordValid()
        {
            return !String.IsNullOrEmpty(PasswordTextView.Text.Trim());
        }
	}
}
