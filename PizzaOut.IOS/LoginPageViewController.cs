using System;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using PizzaData.models;
using PizzaOut.IOS.DataManager;
using PizzaOut.IOS.UIHelpers;
using UIKit;

namespace PizzaOut.IOS
{
	partial class LoginPageViewController : UIViewController
	{
        //Create an event when a authentication is successful
        public event EventHandler OnLoginSuccess;
	    private string username, password;

	    private RestActions _restActions;
	    private User _userModel;
	    LoadingOverlay _loadPop;


        public LoginPageViewController(IntPtr handle) : base(handle)
	    {
	  
        }


        public override void ViewDidLoad()
	    {
	        base.ViewDidLoad();

	        _restActions = new RestActions();
	        _userModel = new User();

            //UserNameTextView.Text = "fatelord";
	        //PasswordTextView.Text = "andalite6";

    
            //set buton click actions
	        BtnLogin.TouchUpInside += async (object sender, EventArgs e) => { await BtnLogin_TouchUpInside(sender); };

	        UserNameTextView.EditingDidEnd += HandleEditingDidEnd;
	        UserNameTextView.Delegate = new CatchEnterDelegate();

            PasswordTextView.EditingDidEnd += HandleEditingDidEnd;
            PasswordTextView.Delegate = new CatchEnterDelegate();

	        /*UserNameTextView.ShouldReturn =(textField) => {
	            textField.ResignFirstResponder();
	            return true;
	        };

	        UserNameTextView.ShouldReturn = (textField) => {
	            PasswordTextView.ResignFirstResponder();
	            return true;
	        };*/
        }


	    private async Task BtnLogin_TouchUpInside(object sender)
        {
            //Validate our Username & Password.
            //This is usually a web service call.
            try
            {
                var bounds = UIScreen.MainScreen.Bounds;

                // show the loading overlay on the UI thread using the correct orientation sizing
                _loadPop = new LoadingOverlay(bounds,"Logging you in..."); // using field from step 2
                View.Add(_loadPop);

                if (IsUserNameValid() && IsPasswordValid())
                {
                    username = UserNameTextView.Text.Trim();
                    password = PasswordTextView.Text.Trim();

                    //We have successfully authenticated a the user,
                    //Now fire our OnLoginSuccess Event.
                    _userModel = await _restActions.LoginUser(username, password);

                    if (_userModel != null)
                    {
                        UserSession.SetUserSession(_userModel);
                        if (UserSession.IsLoggedIn())
                        {
                            _loadPop.Hide();
                            OnLoginSuccess?.Invoke(sender, new EventArgs());
                        }
                        else
                        {
                            var message = "Unable to log you in, please try again";
                            if (_userModel.message != null)
                            {
                                message = _userModel.message;
                            }
                            _loadPop.Hide();
                            MessagingActions.ShowAlert("Unable to login",message);
                        }
                    }
                    else
                    {
                        _loadPop.Hide();
                        MessagingActions.ShowAlert("Login Error", "Incorrect user name or password");
                    }
                }
                else
                {
                    _loadPop.Hide();
                    MessagingActions.ShowAlert("Unable to login", "Incorrect user name or password");
                }


            }
            catch (Exception ex)
            {
              Crashes.TrackError(ex);
                _loadPop.Hide(0); //hide immediatelly
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

	    void HandleEditingDidEnd(object sender, EventArgs e)
	    {
	        //do what you need to do with the value of the textfield here
	    }

	    public override void ViewDidUnload()
	    {
	        base.ViewDidUnload();

	        // Clear any references to subviews of the main view in order to
	        // allow the Garbage Collector to collect them sooner.
	        //
	        // e.g. myOutlet.Dispose (); myOutlet = null;

	        ReleaseDesignerOutlets();
	    }

	    public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
	    {
	        // Return true for supported orientations
	        return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
	    }
    }
}

