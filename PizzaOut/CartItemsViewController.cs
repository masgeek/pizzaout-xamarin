using Foundation;
using System;
using System.Collections.Generic;
using PizzaData.models;
using PizzaOut.TableViews;
using UIKit;

namespace PizzaOut
{
    public partial class CartItemsViewController : UITableViewController
    {
        private List<CartItem> _cartItemList;
        public CartItemsViewController (IntPtr handle) : base (handle)
        {
        }

        public void SetCartItems(List<CartItem> cartItemList)
        {
            _cartItemList = cartItemList;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            LoadMenuItems();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            LoadMenuItems();
        }

        private void LoadMenuItems()
        {

            var tableSource = new CartItemTableSource(_cartItemList,this);
            if (cartItemsTable != null)
            {
                cartItemsTable.Source = tableSource; //assign the table data source

                //reload the data
                //menuCatItemTableView.RowHeight = UITableView.AutomaticDimension;
                //menuCatItemTableView.EstimatedRowHeight = 40f;
                cartItemsTable.ReloadData();
            }
        }
    }
}