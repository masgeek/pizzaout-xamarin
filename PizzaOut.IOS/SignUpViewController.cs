using System;
using System.Threading.Tasks;
using PizzaData.models;
using PizzaOut.IOS.DataManager;
using UIKit;

namespace PizzaOut.IOS
{
	partial class SignUpViewController : UIViewController
	{

	    private RestActions _restActions;

        public SignUpViewController (IntPtr handle) : base (handle)
		{
		   
		}

	    public override void ViewDidLoad()
	    {
	        base.ViewDidLoad();

            _restActions = new RestActions();

	        SignUpButton.TouchUpInside += async (e, s) =>
	        {
	            var canregister = CanRegister();
                
	            if (canregister)
	            {
                    //proceed to the registration logic
                    var user = await RegisterUserRest();

	                if (user?.USER_NAME != null)
	                {
	                    ShowAlert("Registration Successfull","Welcome " + user.SURNAME + " Please login and begin ordering");
	                    DismissViewController(true, null); //close the view controller
	                    return;
	                }
	                ShowAlert("Registration not Successfull", "Unable to register, please try again");
                }
	        };

	    }

	    //This assumes we have successfully create a new user account
        //Naturally, you'll add your logic here, but we're ignoring
        //that for simplicity.
        private bool CanRegister()
        {
            //let us initate the sign up process
            if (!IsUserNameValid())
            {
                ShowAlert("Invalid User Name","Invalid User Name");
                return false;
            }

            if (!IsSurnameValid())
            {
                ShowAlert("Invalid Surname", "Please provide a valid surname");
                return false;
            }

            if (!IsOtherNamesValid())
            {
                ShowAlert("Empty Other Names", "Please provide you other names");
                return false;
            }

            if (!IsEmailValid())
            {
                ShowAlert("Invalid Email", "Please enter correct email address");
                return false;
            }

   

            if (!IsPhoneValid())
            {
                ShowAlert("Invalid Phone Number", "PLease provide a valid phone number");
                return false;
            }

            if (!IsPasswordValid())
            {
                ShowAlert("Empty Password", "Empty Passwords are not allowed");
                return false;
            }

            if (!IsPasswordConfirmed())
            {
                ShowAlert("Password Do Not Match", "The Passwords do not match, please try again");
                return false;
            }

            return true;
        }

        partial void CancelButton_TouchUpInside(UIButton sender)
        {
            DismissViewController(true, null);
        }


        #region Field Validation
	
        private bool IsUserNameValid()
	    {
	        return !String.IsNullOrEmpty(UserNameTextView.Text.Trim());
	    }

	    private bool IsSurnameValid()
	    {
	        return !String.IsNullOrEmpty(SurNameTextView.Text.Trim());
	    }
	    private bool IsOtherNamesValid()
	    {
	        return !String.IsNullOrEmpty(OtherNamesTextView.Text.Trim());
	    }
	    private bool IsEmailValid()
	    {
	        return !String.IsNullOrEmpty(EmailTextView.Text.Trim());
	    }
	    private bool IsPhoneValid()
	    {
	        return !String.IsNullOrEmpty(PhoneTextView.Text.Trim());
	    }
	    private bool IsPasswordValid()
	    {
	        return !String.IsNullOrEmpty(PasswordTextField.Text.Trim());
	    }
	    private bool IsPasswordConfirmed()
	    {
	        if (String.IsNullOrEmpty(ConfirmPasswordTextField.Text.Trim()))
	        {
	            return false;
	        }

	        string password = PasswordTextField.Text.Trim();
	        string confirmPassword = ConfirmPasswordTextField.Text.Trim();

	        return password.Equals(confirmPassword); //compare the two password fields to acertain they match
	    }
        #endregion

	    public async Task<User> RegisterUserRest()
	    {
	        var user = new User
	        {
	            USER_NAME = UserNameTextView.Text.Trim(),
	            OTHER_NAMES = OtherNamesTextView.Text.Trim(),
	            SURNAME = SurNameTextView.Text.Trim(),
	            USER_STATUS = true, //(bool)User.ACCOUNT_STATUS.ACTIVE, //user is active or not
	            USER_TYPE = "1", //1 indicates it is a customer
	            LOCATION_ID = "1", //just a random location id
	            ADDRESS = "N/A",
	            EMAIL = EmailTextView.Text.Trim(),
	            MOBILE = PhoneTextView.Text.Trim(),
	            PASSWORD = PasswordTextField.Text.Trim(),
	            RESET_TOKEN = Guid.NewGuid().ToString() //random guid
	        };


	        var userModel = await _restActions.RegisterUser(user);
	        if (userModel.HAS_ERRORS)
	        {
	            var error = userModel.ERROR_LIST[0];//get only the first error
                //show the errors
	            if (error.message != null)
	            {
	                ShowAlert("Unable to register", error.message);
	                return null;
	            }
	        }

	        //userModel.HAS_ERRORS = false;
	        return userModel;
	    }
        private void ShowAlert(string title,string message)
	    {
#pragma warning disable 618
	        new UIAlertView(title, message, null, "OK", null).Show();
#pragma warning restore 618
        }

    }
}
