using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FFImageLoading;
using Foundation;
using PizzaData.models;
using PizzaOut.DataManager;
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

            var menuCat = CategoryItems[indexPath.Row];

            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            // if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellIdentifier);
            }

            cell.TextLabel.Text = menuCat.MENU_ITEM_NAME;
            ImageLoader.LoadImage(menuCat.MENU_ITEM_IMAGE).Into(cell.ImageView); //load the url image into the image view
            cell.DetailTextLabel.Text = menuCat.MENU_ITEM_DESC;
            
            //ImageLoader.LoadImage(menuCat.MENU_CAT_IMAGE).Into(cell.ImageView); //load the url image into the image view

            //add accessory
            cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
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
    }
}