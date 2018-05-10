using System;
using System.Collections.Generic;
using FFImageLoading;
using Foundation;
using PizzaData.models;
using PizzaOut.DataManager;
using SDWebImage;
using UIKit;

namespace PizzaOut.TableViews
{
    public class MenuCatTableSource:UITableViewSource
    {
        // there is NO database or storage of Tasks in this example, just an in-memory List<>
        private readonly List<MenuCategory> _menuCategories;
        private string cellIdentifier = "MenuCatCell"; // set in the Storyboard

        readonly OurMenuViewController _owner;

        private string controllerName = "MenuCatItemsViewController";

        public MenuCatTableSource(List<MenuCategory> items,OurMenuViewController _owner)
        {
            _menuCategories = items;
            this._owner = _owner;
        }


        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // in a Storyboard, Dequeue will Always return a cell,
            //var cell = tableView.DequeueReusableCell(cellIdentifier) as MenuCatCell;
            var menuCat = _menuCategories[indexPath.Row];

            //MenuCatCell cell = tableView.DequeueReusableCell(cellIdentifier) as MenuCatCell;

            //cell.UpdateCell(menuCat);
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            // if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellIdentifier);
            }

            cell.TextLabel.Text = menuCat.MENU_CAT_NAME;
            //MonkeyImage.Image = UIImage.FromBundle ("PurpleMonkey");
            cell.ImageView.SetImage(
                url: new NSUrl(menuCat.MENU_CAT_IMAGE),
                placeholder: UIImage.FromBundle("placeholder")
            );

            //ImageLoader.LoadImage(menuCat.MENU_CAT_IMAGE).Into(cell.ImageView); //load the url image into the image view

            //--- add accessory ---//
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _menuCategories.Count;
        }

        public MenuCategory GetItem(int id)
        {
            return _menuCategories[id];
        }

      
        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var menuCat = GetItem(indexPath.Row);

            tableView.DeselectRow(indexPath, true);

            // create the view controller for your initial view - using storyboard, code, etc
            MenuCatItemsViewController catItemsViewController = _owner.Storyboard.InstantiateViewController(controllerName) as MenuCatItemsViewController;

            //Here you pass the data from the registerViewController to the secondViewController
            if (catItemsViewController == null) return;

            catItemsViewController.SetSelectedItem(menuCat);
            _owner.NavigationController.PushViewController(catItemsViewController, true);
        }

    }
}