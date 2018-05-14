using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using PizzaData.models;
using SDWebImage;
using UIKit;

namespace PizzaOut.IOS.TableViews
{
    public class MenuCatItemTableSource:UITableViewSource
    {
        readonly MenuCatItemsViewController _owner;
        public List<MenuCategoryItem> CategoryItems;
        private string cellIdentifier = "MenuCatItemCell"; // set in the Storyboard
        private string controllerName = "ItemDetailsViewController";
        public MenuCatItemTableSource(List<MenuCategoryItem> categoryItems, MenuCatItemsViewController owner)
        {
            this.CategoryItems = categoryItems;
            this._owner = owner;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {

            var menuCat = CategoryItems[indexPath.Row];

            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            // if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellIdentifier);
            }

            cell.TextLabel.Text = menuCat.MENU_ITEM_NAME;
            cell.DetailTextLabel.Text = menuCat.MENU_ITEM_DESC;
            cell.ImageView.SetImage(
                url: new NSUrl(menuCat.MENU_ITEM_IMAGE),
                placeholder: UIImage.FromBundle("placeholder")
            );

            //add accessory
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            return cell;
        }

        public override nfloat GetHeightForFooter(UITableView tableView, nint section)
        {
            //return base.GetHeightForFooter(tableView, section);
            return 0;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return CategoryItems.Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexPathRow"></param>
        /// <returns></returns>
        private MenuCategoryItem GetItem(int indexPathRow)
        {
            return CategoryItems[indexPathRow];
        }

        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var selectedItem = GetItem(indexPath.Row);

            tableView.DeselectRow(indexPath, true);

            // create the view controller for your initial view - using storyboard, code, etc
            ItemDetailsViewController itemDetailsViewController = _owner.Storyboard.InstantiateViewController(controllerName) as ItemDetailsViewController;
            if (itemDetailsViewController != null)
            {

                //Here you pass the data from the registerViewController to the secondViewController
                itemDetailsViewController.SetSelectedItem(selectedItem);
                //itemDetailsViewController.SetSelectedItem(menuCat);
                _owner.NavigationController.PushViewController(itemDetailsViewController, true);
            }
        }


    }

}