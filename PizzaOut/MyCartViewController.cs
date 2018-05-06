using Foundation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using PizzaData.models;
using PizzaOut.DataManager;
using RestService.Helpers;
using UIKit;

namespace PizzaOut
{
    public partial class MyCartViewController : UIViewController
    {
        UIActionSheet normal;
        UIActionSheet _deliveryAddressActionSheet,_deliveryTimeActionSheet;

        private RestActions restActions;
        private List<CartItem> cartItemList;
        private string[] locationStingList,deliveryTimeList;

        private List<Location> locationList;
        private string controllerName = "CartItemsViewController";

        private string deliveryLocation, deliveryTime, deliveryDate;

        private int selectedLocationId;

        private bool unapidOrder = false;
        public MyCartViewController (IntPtr handle) : base (handle)
        {
        }

        public override async void ViewDidLoad()
        {
       
            base.ViewDidLoad();

            Title = "My Cart";
            restActions = new RestActions();

            cartItemList = await LoadCartItems(UserSession.GetUserId());
            locationStingList = await LoadLocationList();
            deliveryTimeList = await LoadDeliveryTimeList();

            _deliveryAddressActionSheet = new UIActionSheet("Select Delivery Address", null, cancelTitle: "Cancel", destroy: null, other: locationStingList);
            _deliveryTimeActionSheet = new UIActionSheet("Select Delivery Time", null, cancelTitle: "Cancel", destroy: null, other: deliveryTimeList);

            //set minimum date
            DateTime date = DateTime.Now;
            NSDate nsDate = (NSDate)DateTime.SpecifyKind(date, DateTimeKind.Local);
            dtDeliveryDate.MinimumDate = nsDate;
            deliveryDate = nsDate.ToString(); //set as default date

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

            btnDeliveryAddress.TouchUpInside += (e, s) => { _deliveryAddressActionSheet.ShowInView(View); };

            btnDeliveryTime.TouchUpInside += (e, s) => { _deliveryTimeActionSheet.ShowInView(View); };

            dtDeliveryDate.ValueChanged += (e, s) => { deliveryDate = dtDeliveryDate.Date.ToString(); };

            _deliveryAddressActionSheet.Clicked += (btn_sender, args) =>
            {
       
                try
                {
                    if (args.ButtonIndex >= 0)
                    {
                        var selectedIndex = args.ButtonIndex;

                        deliveryLocation = locationStingList[selectedIndex];

                        btnDeliveryAddress.SetTitle(deliveryLocation, UIControlState.Normal);

                       selectedLocationId = locationList
                            .Where(item => item.LOCATION_NAME == deliveryLocation)
                            .Select(item => item.LOCATION_ID)
                            .FirstOrDefault();

        
                    }

                    Console.WriteLine("{0} Clicked", args.ButtonIndex);
                }
                catch (Exception e)
                { 
                    AppCenterLog.Error("ERROR",e.Message,e);
                }
            };

            _deliveryTimeActionSheet.Clicked += (btn_sender, args) =>
            {

                try
                {
                    if (args.ButtonIndex >= 0)
                    {
                        var selectedIndex = args.ButtonIndex;

                        deliveryTime = deliveryTimeList[selectedIndex];
                        btnDeliveryTime.SetTitle(deliveryTime, UIControlState.Normal);
                    }

                    Console.WriteLine("{0} Clicked", args.ButtonIndex);
                }
                catch (Exception e)
                {
                    AppCenterLog.Error("ERROR", e.Message, e);
                }
            };

            btnPay.TouchUpInside += (e, s) =>
            {
                //let us validate the data
                if (IsLocationSelected()&&IsTimeSelected()&&IsDateSelected())
                {
                    if (unapidOrder)
                    {
                        //edit teh unpaid order
                    }
                    else
                    {
                        //create new order
                    }
                }
                else
                {
#pragma warning disable 618
                    new UIAlertView("Incomplete Info", "Ensure delivery location,date and time are specified", null, "OK", null).Show();
#pragma warning restore 618
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

        private async Task<string[]> LoadLocationList()
        {

            try
            {
                locationList = await restActions.GetDeliveryLocations();

                string[] locations = new string[locationList.Count];
                int i = 0;
                foreach (var location in locationList)
                {
                    locations[i] = location.LOCATION_NAME;
                    i++;
                }

                return locations;
            }
            catch (Exception e)
            {
                AppCenterLog.Error("ERROR", e.Message, e);
            }

            return null;
        }

        private async Task<string[]> LoadDeliveryTimeList()
        {

            try
            {
                var timeList = await restActions.GetDeliveryTime();

                string[] time = new string[timeList.Count];
                int i = 0;

                foreach (var deliveryTimeObj in timeList)
                {
                    time[i] = deliveryTimeObj.DELIVERY_TIME;
                    i++;
                }


                return time;
            }
            catch (Exception e)
            {
                AppCenterLog.Error("ERROR", e.Message, e);
            }

            return null;
        }

        private bool IsLocationSelected()
        {
            return !String.IsNullOrEmpty(deliveryLocation.Trim());
        }

        private bool IsTimeSelected()
        {
            return !String.IsNullOrEmpty(deliveryTime);
        }

        private bool IsDateSelected()
        {
            return !String.IsNullOrEmpty(deliveryDate);
        }

        private void CreateOrderFromCart()
        {
            /**
             *   delivery_date_time = String.format("%s %s:00", delivery_date, delivery_time);
               paramHash = new HashMap<>();
               paramHash.put("USER_ID", user_id);
               paramHash.put("LOCATION_ID", String.valueOf(location_id));
               paramHash.put("CURRENCY", getString(R.string.currency));
               paramHash.put("ORDER_TIME", delivery_date_time);
               paramHash.put("CART_TIMESTAMP", String.valueOf(cart_timestamp));
               paramHash.put("PAYMENT_CHANNEL", "MOBILE");
             */
            //let us load the items for teh signed in user from the cart
            string order_time = deliveryDate + deliveryTime;
            Dictionary<string, object> orderDictionary = new Dictionary<string, object>
            {
                {"USER_ID",UserSession.GetUserId()},
                {"LOCATION_ID",selectedLocationId},
                {"CURRENCY",Helper.Currency()},
                {"ORDER_TIME",deliveryTime},
                {"CART_TIMESTAMP",UserSession.GetUserId()},
                {"PAYMENT_CHANNEL","MOBILE"},
            };
            //var cartItemList = await restActions.CreateOrderFromCart(orderDictionary);
        }
    }
}