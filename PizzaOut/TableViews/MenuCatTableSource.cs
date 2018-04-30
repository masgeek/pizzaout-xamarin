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



        public MenuCatTableSource(List<MenuCategory> items)
        {
            _menuCategories = items;
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

       /* public override void AccessoryButtonTapped(UITableView tableView, NSIndexPath indexPath)
        {
            UIAlertController okAlertController = UIAlertController.Create("DetailDisclosureButton Touched", _menuCategories[indexPath.Row].MENU_CAT_NAME, UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            owner.PresentViewController(okAlertController, true, null);

            tableView.DeselectRow(indexPath, true);
        }*/
        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            //new UIAlertView("Row Selected", _menuCategories[indexPath.Row].MENU_CAT_NAME, null, "OK", null).Show();

            //tableView.DeselectRow(indexPath, true); // iOS convention is to remove the highlight

            //launch the selected menu categories view
            //Create an instance of our AppDelegate
            //var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;

            //Get an instance of our MainStoryboard.storyboard
           // var mainStoryboard = appDelegate.MainStoryboard;

            //Get an instance of our MainTabBarViewController
           // var mainTabBarViewController = appDelegate.GetViewController(mainStoryboard, "SignUpViewController");

            //Set the MainTabBarViewController as our RootViewController
           // appDelegate.SetRootViewController(mainTabBarViewController, true);


        }
    }
}