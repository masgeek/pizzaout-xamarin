using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PizzaData.models;
using UIKit;

namespace PizzaOut.TableViews
{
    public class MenuCatItemTableSource:UITableViewSource
    {
        public List<MenuCategoryItem> CategoryItems;
        private string cellIdentifier = "MenuCatItemCell"; // set in the Storyboard

        public MenuCatItemTableSource(List<MenuCategoryItem> _categoryItems)
        {
            this.CategoryItems = _categoryItems;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            //cell.UpdateCell(menuCat);

            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            // if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellIdentifier);
            }

            cell.TextLabel.Text = "Test me";
            //ImageLoader.LoadImage(menuCat.MENU_CAT_IMAGE).Into(cell.ImageView); //load the url image into the image view

            //add accessory
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return CategoryItems.Count();
        }
    }
}