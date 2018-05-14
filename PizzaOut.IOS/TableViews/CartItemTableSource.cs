using System;
using System.Collections.Generic;
using System.Globalization;
using Foundation;
using PizzaData.models;
using UIKit;

namespace PizzaOut.IOS.TableViews
{
    class CartItemTableSource:UITableViewSource
    {
        // there is NO database or storage of Tasks in this example, just an in-memory List<>
        private readonly List<CartItem> _cartItems;
        private string cellIdentifier = "CartItemCell"; // set in the Storyboard

        readonly CartItemsViewController _owner;

        private string controllerName = "CartItemsViewController";

        public CartItemTableSource(List<CartItem> cartItems, CartItemsViewController owner)
        {
            _cartItems = cartItems;
            _owner = owner;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // in a Storyboard, Dequeue will Always return a cell,
            //var cell = tableView.DequeueReusableCell(cellIdentifier) as MenuCatCell;
            var cartItem = GetItem(indexPath.Row);

            //MenuCatCell cell = tableView.DequeueReusableCell(cellIdentifier) as MenuCatCell;

            //cell.UpdateCell(menuCat);
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            // if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellIdentifier);
            }

            double subTotal = cartItem.ITEM_PRICE * cartItem.QUANTITY;
            string itemInfo = cartItem.QUANTITY + " x " + cartItem.MENU_CAT_ITEM.MENU_ITEM_NAME + "(" + cartItem.ITEM_TYPE_SIZE + ")";

            cell.TextLabel.Text = itemInfo;
            cell.DetailTextLabel.Text = "Total "+subTotal.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
        
            //--- add accessory ---//
            //cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _cartItems.Count;
        }


        public CartItem GetItem(int id)
        {
            return _cartItems[id];
        }


        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var selectedItem = GetItem(indexPath.Row);

            tableView.DeselectRow(indexPath, true);

            // create the view controller for your initial view - using storyboard, code, etc
            ItemDetailsViewController itemDetailsViewController = null;//new ItemDetailsViewController();//_owner.Storyboard.InstantiateViewController(controllerName) as ItemDetailsViewController;
            if (itemDetailsViewController != null)
            {

                //Here you pass the data from the registerViewController to the secondViewController
                itemDetailsViewController.SetSelectedItem(selectedItem.MENU_CAT_ITEM);
                //itemDetailsViewController.SetSelectedItem(menuCat);
                _owner.NavigationController.PushViewController(itemDetailsViewController, true);
            }
        }
    }
}