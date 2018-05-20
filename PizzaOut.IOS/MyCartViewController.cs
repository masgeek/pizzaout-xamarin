using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Microsoft.AppCenter;
using PizzaData.Helpers;
using PizzaData.models;
using PizzaOut.IOS.DataManager;
using PizzaOut.IOS.UIHelpers;
using UIKit;

namespace PizzaOut.IOS
{
    public partial class MyCartViewController : UIViewController
    {
        UIActionSheet normal;
        UIActionSheet _deliveryAddressActionSheet,_deliveryTimeActionSheet;

        private LoadingOverlay _loadingOverlay;
        private RestActions restActions;
        private Order _order;
        private List<CartItem> cartItemList;
        private string[] locationStingList,deliveryTimeList;

        private List<Location> locationList;
        private string controllerName = "CartItemsViewController";

        private string deliveryLocation=null, deliveryTime=null, deliveryDate = null;

        private int _selectedLocationId;
        private long _cartTimestamp;

        private NSDate currentNsDate;
        private bool unpaidOrder = false;

        private double total = 0.0;
        private double minprice;
        public MyCartViewController (IntPtr handle) : base (handle)
        {
     
        }

        public void SetOrderItems(Order order)
        {
            _order = order;
            unpaidOrder = true;
        }
        public override async void ViewDidLoad()
        {
       
            base.ViewDidLoad();

            Title = "My Cart";
            restActions = new RestActions();
            minprice = UserSession.MinPrice();
            var bounds = UIScreen.MainScreen.Bounds;

            _loadingOverlay = new LoadingOverlay(bounds,"Refreshing Cart...");
            View.Add(_loadingOverlay);

            cartItemList = await LoadCartItems(UserSession.GetUserId());
            locationStingList = await LoadLocationList();
            deliveryTimeList = await LoadDeliveryTimeList();

            _loadingOverlay.Hide();

#pragma warning disable 618
            _deliveryAddressActionSheet = new UIActionSheet("Select Delivery Address", null, cancelTitle: "Cancel", destroy: null, other: locationStingList);

            _deliveryTimeActionSheet = new UIActionSheet("Select Delivery Time", null, cancelTitle: "Cancel", destroy: null, other: deliveryTimeList);
#pragma warning restore 618

            //set minimum date
            DateTime date = DateTime.Now;
            currentNsDate = (NSDate) DateTime.SpecifyKind(date, DateTimeKind.Utc);
            deliveryDate = currentNsDate.ToString(); //set as default date
            if (unpaidOrder)
            {
                deliveryDate = _order.ORDER_DATE_TIME.ToString("dd/MM/yyyy"); //"09:35:37"

                deliveryTime = _order.ORDER_TIME;

                deliveryLocation = _order.LOCATION.LOCATION_NAME;
                _selectedLocationId = _order.LOCATION_ID;

                NSDate _orderNsDate = (NSDate) DateTime.SpecifyKind(_order.ORDER_DATE_TIME, DateTimeKind.Utc);
                ;

                //set the current values

                BtnDeliveryAddress.SetTitle(deliveryLocation, UIControlState.Normal);
                BtnDeliveryTime.SetTitle(deliveryTime, UIControlState.Normal);
                deliveryDatePicker.SetDate(_orderNsDate, true);
                deliveryDatePicker.MaximumDate = _orderNsDate;

                //btnViewItems.Hidden = true;
            }




            /*btnViewItems.TouchUpInside += (e, s) =>
            {
                // create the view controller for your initial view - using storyboard, code, etc
                CartItemsViewController cartItemsViewController = this.Storyboard.InstantiateViewController(controllerName) as CartItemsViewController;
                if (cartItemsViewController != null)
                {
                    cartItemsViewController.SetCartItems(cartItemList);
                    NavigationController.PushViewController(cartItemsViewController, true);
                }
            };*/

            BtnDeliveryAddress.TouchUpInside += (e, s) => { _deliveryAddressActionSheet.ShowInView(View); };

            BtnDeliveryTime.TouchUpInside += (e, s) => { _deliveryTimeActionSheet.ShowInView(View); };

            deliveryDatePicker.ValueChanged += (e, s) =>
            {
                deliveryDatePicker.MinimumDate = currentNsDate; //set minimum date to current date
                deliveryDate = deliveryDatePicker.Date.ToString();
            };

            _deliveryAddressActionSheet.Clicked += (btn_sender, args) =>
            {
       
                try
                {
                    if (args.ButtonIndex >= 0)
                    {
                        var selectedIndex = args.ButtonIndex;

                        deliveryLocation = locationStingList[selectedIndex];

                        BtnDeliveryAddress.SetTitle(deliveryLocation, UIControlState.Normal);

                        _selectedLocationId = GetLocationId(locationList,deliveryLocation);
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
                        BtnDeliveryTime.SetTitle(deliveryTime, UIControlState.Normal);
                    }

                    Console.WriteLine("{0} Clicked", args.ButtonIndex);
                }
                catch (Exception e)
                {
                    AppCenterLog.Error("ERROR", e.Message, e);
                }
            };

            BtnPay.TouchUpInside += async (e, s) =>
            {
                
                //let us validate the data
                if (IsLocationSelected()&&IsTimeSelected()&&IsDateSelected())
                {
                    //check minimum purchase price
                    if (total >= minprice)
                    {
                        //create new order
                        if (unpaidOrder)
                        {
                            //let us update the order and proceed
                            OpenCheckout(_order);
                        }
                        else
                        {
        
                            await CreateOrderFromCart();
                        }
                    }
                    else
                    {
                        var least = minprice.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
                        MessagingActions.ShowAlert("Minimum Price",
                            $"Please make a purchase of at least {least} to be eligible for delivery");
       
                    }
                }
                else
                {
                    MessagingActions.ShowAlert("Incomplete Info", "Ensure delivery location,date and time are specified");
                }
            };
        }

        private int GetLocationId(List<Location> locations,string _deliveryLocation)
        {
            int locationid = locationList
                .Where(item => item.LOCATION_NAME == _deliveryLocation)
                .Select(item => item.LOCATION_ID)
                .FirstOrDefault();

            return locationid;
        }

        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            //reload the data
            if (unpaidOrder)
            {
                total = _order.ComputeOrderTotal();
                lblTotalAmount.Text = total.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
            }
            else
            {
                cartItemList = await LoadCartItems(UserSession.GetUserId());
            }
        }

        private void ComputeTotal(List<CartItem> cartItems)
        {
            total = 0.0; //reset the amount
            foreach (CartItem cartItem in cartItems)
            {
                total = total + (cartItem.ITEM_PRICE * cartItem.QUANTITY);
                _cartTimestamp = cartItem.CART_TIMESTAMP;
            }

            lblTotalAmount.Text = total.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
        }

        private async Task<List<CartItem>> LoadCartItems(int userId)
        {
            //let us load the items for teh signed in user from the cart
            var cartItems = await restActions.GetCartItems(userId);

            if (cartItems != null)
            {
                //load the total and all that stuff
                ComputeTotal(cartItems);
            }
            return cartItems;
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
            return !String.IsNullOrEmpty(deliveryLocation);
        }

        private bool IsTimeSelected()
        {
            return !String.IsNullOrEmpty(deliveryTime);
        }

        private bool IsDateSelected()
        {
            return !String.IsNullOrEmpty(deliveryDate);
        }

        private async Task CreateOrderFromCart()
        {
            var innerRect = UIScreen.MainScreen.Bounds;

            _loadingOverlay = new LoadingOverlay(innerRect, "Creating Order...");
            //let us load the items for the signed in user from the cart
            DateTime myDate = DateTime.Parse(deliveryDate);
            string orderTime = myDate.ToShortDateString() +" "+ deliveryTime+":00";

            Dictionary<string, object> orderDictionary = new Dictionary<string, object>
            {
                {"USER_ID",UserSession.GetUserId()},
                {"LOCATION_ID",_selectedLocationId},
                {"CURRENCY",Helper.Currency()},
                {"ORDER_TIME",orderTime},
                {"CART_TIMESTAMP",_cartTimestamp},
                {"PAYMENT_CHANNEL","MOBILE"},
            };

            _order = await restActions.CreateOrderFromCart(orderDictionary);

            _loadingOverlay.Hide();
            OpenCheckout(_order);
        }

        private void OpenCheckout(Order order)
        {
            // create the view controller for your initial view - using storyboard, code, etc
            PaymentConfirmationViewController paymentConfirmationViewController = this.Storyboard.InstantiateViewController("PaymentConfirmationViewController") as PaymentConfirmationViewController;
            if (paymentConfirmationViewController != null)
            {
                paymentConfirmationViewController.SetOrderItems(order);
                //NavigationController.PushViewController(paymentConfirmationViewController, true);
                //NavigationController.PushViewController(paymentConfirmationViewController, true);

                paymentConfirmationViewController.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
                this.PresentViewController(paymentConfirmationViewController, true, null);

            }
        }
    }
}