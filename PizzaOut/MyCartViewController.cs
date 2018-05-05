using Foundation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using PizzaData.models;
using PizzaOut.DataManager;
using UIKit;

namespace PizzaOut
{
    public partial class MyCartViewController : UIViewController
    {
        private RestActions restActions;
        private List<CartItem> cartItemList;

        private string controllerName = "CartItemsViewController";
        public MyCartViewController (IntPtr handle) : base (handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "My Cart";
            restActions = new RestActions();

            cartItemList = await LoadCartItems(UserSession.GetUserId());
    

            btnViewItems.TouchUpInside += (e, s) =>
            {
                // create the view controller for your initial view - using storyboard, code, etc
                CartItemsViewController cartItemsViewController = this.Storyboard.InstantiateViewController(controllerName) as CartItemsViewController;
                if (cartItemsViewController != null)
                {
                    cartItemsViewController.SetCartItems(cartItemList);
                   NavigationController.PushViewController(cartItemsViewController, true);
                }
            };
        }

        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            //reload the data
            cartItemList = await LoadCartItems(UserSession.GetUserId());
        }

        private void ComputeTotal(List<CartItem> cartItems)
        {
            double total = 0.0;
            foreach (CartItem cartItem in cartItems)
            {
                total = total + (cartItem.ITEM_PRICE * cartItem.QUANTITY);
            }

            lblTotal.Text = total.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
        }

        private async Task<List<CartItem>> LoadCartItems(int userId)
        {
            //let us load the items for teh signed in user from the cart
            var cartItemList = await restActions.GetCartItems(userId);

            if (cartItemList != null)
            {
                //load teh total and all that stuff
                ComputeTotal(cartItemList);
            }
            return cartItemList;
        }
    }
}