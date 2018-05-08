using Foundation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaData.models;
using PizzaOut.DataManager;
using PizzaOut.TableViews;
using UIKit;

namespace PizzaOut
{
    public partial class OrdersViewTableController : UITableViewController
    {
        private string _orderType;
        private string _orderUrl;
        private RestActions restActions;
        private OrderTablesSource _tableSource;

        public OrdersViewTableController (IntPtr handle) : base (handle)
        {
        }

        public void SetOrderType(string orderType,string orderUrl)
        {
            _orderType = orderType;
            _orderUrl = orderUrl;
            restActions = new RestActions();
        }


        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            List<Order> orders = await LoadOrders(_orderUrl, _orderType);

            //set the orders table
            if (orders != null)
            {
                _tableSource = new OrderTablesSource(orders, this,_orderType);
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

        private async Task<List<Order>> LoadOrders(string orderUrl, string orderType)
        {
            var ordersList = await restActions.LoadOrders(orderUrl, orderType);

            return ordersList;
        }
    }
}