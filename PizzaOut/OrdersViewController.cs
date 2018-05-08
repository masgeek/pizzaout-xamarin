using Foundation;
using System;
using PizzaOut.DataManager;
using UIKit;

namespace PizzaOut
{
    public partial class OrdersViewController : UIViewController
    {
        private string controllerName = "OrdersViewTableController";
        private string orderUrl;
        public OrdersViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Orders";

            int userId = UserSession.GetUserId();

            BtnUnpaidOrders.TouchUpInside += (e, s) =>
            {
                //open the table and show the unpaid orders
                // create the view controller for your initial view - using storyboard, code, etc
                OrdersViewTableController ordersViewTableController = Storyboard.InstantiateViewController(controllerName) as OrdersViewTableController;
                if (ordersViewTableController != null)
                {
                    orderUrl = $"v1/orders/{userId}/active-orders";
                    //Here you pass the data from the registerViewController to the secondViewController
                    ordersViewTableController.SetOrderType("unpaid",orderUrl);

                    NavigationController.PushViewController(ordersViewTableController, true);
                }
            };
        }
    }
}