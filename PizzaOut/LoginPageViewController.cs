using System;
using System.Runtime.Remoting.Channels;
using System.Threading.Tasks;
using PizzaData.models;
using PizzaOut.DataManager;
using UIKit;

namespace PizzaOut
{
	partial class LoginPageViewController : UIViewController
	{
        //Create an event when a authentication is successful
        public event EventHandler OnLoginSuccess;

	    private string username, password;
		public LoginPageViewController (IntPtr handle) : base (handle)
		{
        }

	    public override void ViewDidLoad()
	    {
            base.ViewDidLoad();

            //set default test values
	        UserNameTextView.Text = "fatelord";
	        PasswordTextView.Text = "andalite6";

            //set buton click actions
            btnLogin.TouchUpInside += async (object sender, EventArgs e) => { await LoginButton_TouchUpInside(); };
        }



        private async Task LoginButton_TouchUpInside()
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

                    User userModel = await RestActions.LoginUserRest(username, password);

                    if (userModel != null)
                    {
                        new UIAlertView("Login Successful", "Welcome back human", null, "OK", null).Show();
                   }

                    if (OnLoginSuccess != null)
                    {

                        //OnLoginSuccess(sender, new EventArgs());
                    }
                }
                else
                {
                    new UIAlertView("Login Error", "Bad user name or password", null, "OK", null).Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
