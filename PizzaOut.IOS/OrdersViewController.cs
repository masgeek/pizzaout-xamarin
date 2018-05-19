using System;
using PizzaOut.IOS.DataManager;
using UIKit;

namespace PizzaOut.IOS
{
    public partial class OrdersViewController : UIViewController
    {
        private string controllerName = "OrdersViewTableController";
        private string orderUrl;
        private OrdersViewTableController _ordersViewTableController;

        public OrdersViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Orders";

            _ordersViewTableController = Storyboard.InstantiateViewController(controllerName) as OrdersViewTableController;
            int userId = UserSession.GetUserId();

            /*BtnUnpaidOrders.TouchUpInside += (e, s) =>
            {
                //open the table and show the unpaid orders
                // create the view controller for your initial view - using storyboard, code, etc
                if (_ordersViewTableController != null)
                {
                    orderUrl = $"v1/orders/{userId}/active-orders";
                    //Here you pass the data from the registerViewController to the secondViewController
                    _ordersViewTableController.SetOrderType("unpaid",orderUrl);

                    NavigationController.PushViewController(_ordersViewTableController, true);
                }
            };

            BtnConfirmedOrders.TouchUpInside += (e, s) =>
            {
                //open the table and show the confirmed orders
                // create the view controller for your initial view - using storyboard, code, etc
                if (_ordersViewTableController != null)
                {
                    orderUrl = $"v1/orders/{userId}/my-orders";
                    //Here you pass the data from the registerViewController to the secondViewController
                    _ordersViewTableController.SetOrderType("confirmed", orderUrl);

                    NavigationController.PushViewController(_ordersViewTableController, true);
                }
            };

            BtnOrderHistory.TouchUpInside += (e, s) =>
            {
                //open the table and show the confirmed orders
                // create the view controller for your initial view - using storyboard, code, etc
                if (_ordersViewTableController != null)
                {
                    orderUrl = $"v1/orders/{userId}/active-orders";
                    //Here you pass the data from the registerViewController to the secondViewController
                    _ordersViewTableController.SetOrderType("active", orderUrl);

                    NavigationController.PushViewController(_ordersViewTableController, true);
                }
            };*/
        }
    }
}