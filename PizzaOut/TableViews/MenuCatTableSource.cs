using System;
using System.Collections.Generic;
using FFImageLoading;
using Foundation;
using PizzaData.models;
using PizzaOut.DataManager;
using UIKit;

namespace PizzaOut.TableViews
{
    public class MenuCatTableSource:UITableViewSource
    {
        // there is NO database or storage of Tasks in this example, just an in-memory List<>
        private readonly List<MenuCategory> _menuCategories;
        private string cellIdentifier = "MenuCatCell"; // set in the Storyboard

        OurMenuViewController owner;

        public MenuCatTableSource(List<MenuCategory> items,OurMenuViewController _owner)
        {
            _menuCategories = items;
            owner = _owner;
        }


        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // in a Storyboard, Dequeue will ALWAYS return a cell,
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
            ImageLoader.LoadImage(menuCat.MENU_CAT_IMAGE).Into(cell.ImageView); //load the url image into the image view

            //add accessory
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
            var menuCat = _menuCategories[indexPath.Row];

            tableView.DeselectRow(indexPath, true);
            owner.NavigationController.PushViewController(new MenuCatItemsController(menuCat), true);
        }

    }
}