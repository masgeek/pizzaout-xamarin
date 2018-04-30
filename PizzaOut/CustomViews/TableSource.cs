using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PizzaData.models;
using UIKit;

namespace PizzaOut.CustomViews
{
    public class TableSource:UITableViewSource
    {
        // there is NO database or storage of Tasks in this example, just an in-memory List<>
        private readonly List<MenuCategory> _menuCategories;
        private string cellIdentifier = "taskcell"; // set in the Storyboard



        public TableSource(List<MenuCategory> items)
        {
            _menuCategories = items;
        }

 
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // in a Storyboard, Dequeue will ALWAYS return a cell,
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);
            }

            // now set the properties as normal
            MenuCategory menuCat = _menuCategories[indexPath.Row];

            cell.TextLabel.Text = menuCat.MENU_CAT_NAME;
            if (menuCat.ACTIVE == 1)
            {
                cell.Accessory = UITableViewCellAccessory.Checkmark;
            }
            else
            {
                cell.Accessory = UITableViewCellAccessory.None;
            }

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
    }
}