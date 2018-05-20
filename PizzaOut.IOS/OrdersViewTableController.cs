using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaData.models;
using PizzaOut.IOS.DataManager;
using PizzaOut.IOS.TableViews;
using PizzaOut.IOS.UIHelpers;
using UIKit;

namespace PizzaOut.IOS
{
    public partial class OrdersViewTableController : UITableViewController
    {
        private string _orderType;
        private string _orderUrl;
        private RestActions restActions;
        private OrderTablesSource _tableSource;
        private LoadingOverlay _loadingOverlay;

        public OrdersViewTableController (IntPtr handle) : base (handle)
        {
        }

        public void SetOrderType(string orderType,string orderUrl)
        {
            _orderType = orderType;
            _orderUrl = orderUrl;
            restActions = new RestActions();
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            var bounds = UIScreen.MainScreen.Bounds;
            _loadingOverlay = new LoadingOverlay(bounds,"Loading Orders...");
            View.Add(_loadingOverlay);
            //set the orders table

            List<Order> orders = await LoadOrders(_orderUrl, _orderType);
            _loadingOverlay.Hide();
            if (orders != null)
            {
                _tableSource = new OrderTablesSource(orders, this, _orderType);
                if (ordersTable != null)
                {
                    ordersTable.Source = _tableSource; //assign the table data source

                    //reload the data
                    //menuCatItemTableView.RowHeight = UITableView.AutomaticDimension;
                    //menuCatItemTableView.EstimatedRowHeight = 40f;
                    ordersTable.ReloadData();
                }
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            ordersTable.Source = null;
            ordersTable.ReloadData();
        }

        private async Task<List<Order>> LoadOrders(string orderUrl, string orderType)
        {
            var ordersList = await restActions.LoadOrders(orderUrl, orderType);

            return ordersList;
        }
    }
}