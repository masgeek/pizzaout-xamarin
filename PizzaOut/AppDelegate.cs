using System;
using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using UIKit;

namespace PizzaOut
{
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        private bool isAuthenticated = true;

        public override UIWindow Window
        {
            get;
            set;
        }
		
        //Public property to access our MainStoryboard.storyboard file
        public UIStoryboard MainStoryboard => UIStoryboard.FromName("MainStoryboard", NSBundle.MainBundle);

        //Creates an instance of viewControllerName from storyboard
        public UIViewController GetViewController(UIStoryboard storyboard, string viewControllerName)
        {
            return storyboard.InstantiateViewController(viewControllerName);
        }

        //Sets the RootViewController of the Apps main window with an option for animation.
        public void SetRootViewController(UIViewController rootViewController, bool animate)
        {
            if(animate)
            {
                var transitionType = UIViewAnimationOptions.TransitionFlipFromRight;

                Window.RootViewController = rootViewController;
                UIView.Transition(Window, 0.5, transitionType,
                                  () => Window.RootViewController = rootViewController,
                                  null);
            }
            else
            {
                Window.RootViewController = rootViewController;
            }
        }

        //Override FinishedLaunching. This executes after the app has started.
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            //set the crash analytics
            AppCenter.Start("cfa8f1ba-443f-4136-a786-1b8ceabf07d8", typeof(Analytics), typeof(Crashes));
            AppCenter.LogLevel = LogLevel.Verbose;
            //isAuthenticated can be used for an auto-login feature, you'll have to implement this
            //as you see fit or get rid of the if statement if you want.
            if (isAuthenticated)
            {
                //We are already authenticated, so go to the main tab bar controller;
                var tabBarController = GetViewController(MainStoryboard, "MainTabBarController");
                SetRootViewController(tabBarController, false);
            }
            else
            {
                //User needs to log in, so show the Login View Controlller
                var loginViewController = GetViewController(MainStoryboard, "LoginPageViewController") as LoginPageViewController;
                loginViewController.OnLoginSuccess += LoginViewController_OnLoginSuccess;
                SetRootViewController(loginViewController, false);
            }

            //Analytics.TrackEvent("Application started logged in status is "+isAuthenticated);
            return true;
        }

        void LoginViewController_OnLoginSuccess (object sender, EventArgs e)
        {
            //We have successfully Logged In
            var tabBarController = GetViewController(MainStoryboard, "MainTabBarController");
            SetRootViewController(tabBarController, true);
        }
    }
}

