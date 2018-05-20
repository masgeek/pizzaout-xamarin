using System;
using System.Collections.Generic;
using Foundation;
using PizzaData.models;
using PizzaOut.IOS.DataManager;
using UIKit;

namespace PizzaOut.IOS.TableViews
{
    public class OrderTablesSource : UITableViewSource
    {
        readonly OrdersViewTableController _owner;
        private readonly List<Order> _orderItems;
        private string _cellIdentifier = "OrderItemCell"; // set in the Storyboard
        private string _controllerName = "OrdersViewTableController";

        public OrderTablesSource(List<Order> orderItems, OrdersViewTableController owner,string orderType)
        {
            this._orderItems = orderItems;
            this._owner = owner;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {

            // in a Storyboard, Dequeue will Always return a cell,
            var orderItem = GetItem(indexPath.Row);
            UITableViewCell cell = tableView.DequeueReusableCell(_cellIdentifier);
            // if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, _cellIdentifier);
            }

            cell.TextLabel.Text = orderItem.ORDER_ID.ToString();
            cell.DetailTextLabel.Text = orderItem.ORDER_STATUS;

            //--- add accessory ---//
            cell.Accessory =  UITableViewCellAccessory.None;
            //cell.Accessory = orderItem.PAY_ORDER ? UITableViewCellAccessory.DisclosureIndicator : UITableViewCellAccessory.DetailButton;
        
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _orderItems.Count;
        }

        public Order GetItem(int id)
        {
            return _orderItems[id];
        }


        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            Order order = GetItem(indexPath.Row);


            tableView.DeselectRow(indexPath, true);

            // create the view controller for your initial view - using storyboard, code, etc
            if (order.PAY_ORDER)
            {
                MyCartViewController myCartViewController =_owner.Storyboard.InstantiateViewController("MyCartViewController") as MyCartViewController;

                //Here you pass the data from the registerViewController to the secondViewController
                if (myCartViewController == null) return;

                myCartViewController.SetOrderItems(order);
                _owner.NavigationController.PushViewController(myCartViewController, true);
            }
            else
            {
                //show the items in the order
                MessagingActions.ShowAlert("Order Confirmed","This order has been confirmed, please await delivery from vendor");
            }
        }
    }
}