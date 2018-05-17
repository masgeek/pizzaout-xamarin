using System;
using System.Threading.Tasks;
using PizzaData.models;
using PizzaOut.IOS.DataManager;
using PizzaOut.IOS.UIHelpers;
using UIKit;

namespace PizzaOut.IOS
{
	partial class SignUpViewController : UIViewController
	{

	    private RestActions _restActions;
	    LoadingOverlay _loadPop;

        public SignUpViewController (IntPtr handle) : base (handle)
		{
		   
		}

	    public override void ViewDidLoad()
	    {
	        base.ViewDidLoad();

	        //set teh width and content size
	        // var contentWidth = registerScrollView.Bounds.Width;

	        //var contentHeight = registerScrollView.Bounds.Height * 3;

	        //registerScrollView.ContentSize = CGSizeMake(contentWidth, contentHeight);
	        //registerScrollView.ContentSize = new CGSize(contentWidth, contentHeight);

	        //var subviewHeight =120;
	        //var currentViewOffset = 0;

	        //create subviews
	        //while (currentViewOffset < contentHeight)
	        //{
	        //var frame = new CGRect(0, currentViewOffset, contentWidth, subviewHeight).Inset(dx: 5, dy: 5);
	        //var hue = currentViewOffset / contentHeight;
	        //var subview = new UIView(frame: frame)
	        //{
	        //BackgroundColor = new UIColor(red: hue, green: 1, blue: 1, alpha: 1)
	        //};

	        //registerScrollView.AddSubview(subview);


	        //currentViewOffset = currentViewOffset + subviewHeight;

	        //  }
            _restActions = new RestActions();

	        SignUpButton.TouchUpInside += async (e, s) =>
	        {
	            var bounds = UIScreen.MainScreen.Bounds;

	            // show the loading overlay on the UI thread using the correct orientation sizing
	            _loadPop = new LoadingOverlay(bounds, "Signing you up..."); // using field from step 2
	            View.Add(_loadPop);
                var canregister = CanRegister();
                
	            if (canregister)
	            {
                    //proceed to the registration logic
                    var user = await RegisterUserRest();

	                if (user?.USER_NAME != null)
	                {
	                    _loadPop.Hide();

                        MessagingActions.ShowAlert("Registration Successfull","Welcome " + user.SURNAME + " Please login and begin ordering");
	                    DismissViewController(true, null); //close the view controller
	                    return;
	                }
	                _loadPop.Hide();
                    MessagingActions.ShowAlert("Registration not Successfull", "Unable to register, please try again");
                }
	            _loadPop.Hide();
            };

            #region  handle the return actions to clear the on screen keyboard properly
            UserNameTextView.ShouldReturn = (textField) => {
	            textField.ResignFirstResponder();
	            return true;
	        };
	        SurNameTextView.ShouldReturn = (textField) => {
	            textField.ResignFirstResponder();
	            return true;
	        };
	        OtherNamesTextView.ShouldReturn = (textField) => {
	            textField.ResignFirstResponder();
	            return true;
	        };
	        EmailTextView.ShouldReturn = (textField) => {
	            textField.ResignFirstResponder();
	            return true;
	        };
	        PhoneTextView.ShouldReturn = (textField) => {
	            textField.ResignFirstResponder();
	            return true;
	        };
	        PasswordTextField.ShouldReturn = (textField) => {
	            textField.ResignFirstResponder();
	            return true;
	        };
	        ConfirmPasswordTextField.ShouldReturn = (textField) => {
	            textField.ResignFirstResponder();
	            return true;
	        };
            #endregion
        }

	    //This assumes we have successfully create a new user account
        //Naturally, you'll add your logic here, but we're ignoring
        //that for simplicity.
        private bool CanRegister()
        {
            //let us initate the sign up process
            if (!IsUserNameValid())
            {
                MessagingActions.ShowAlert("Invalid User Name","Invalid User Name");
                return false;
            }

            if (!IsSurnameValid())
            {
                MessagingActions.ShowAlert("Invalid Surname", "Please provide a valid surname");
                return false;
            }

            if (!IsOtherNamesValid())
            {
                MessagingActions.ShowAlert("Empty Other Names", "Please provide you other names");
                return false;
            }

            if (!IsEmailValid())
            {
                MessagingActions.ShowAlert("Invalid Email", "Please enter correct email address");
                return false;
            }

   

            if (!IsPhoneValid())
            {
                MessagingActions.ShowAlert("Invalid Phone Number", "PLease provide a valid phone number");
                return false;
            }

            if (!IsPasswordValid())
            {
                MessagingActions.ShowAlert("Empty Password", "Empty Passwords are not allowed");
                return false;
            }

            if (!IsPasswordConfirmed())
            {
                MessagingActions.ShowAlert("Password Do Not Match", "The Passwords do not match, please try again");
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
	                MessagingActions.ShowAlert("Unable to register", error.message);
	                return null;
	            }
	        }
	        return userModel;
	    }

    }
}
