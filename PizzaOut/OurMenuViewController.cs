using Foundation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using PizzaData.models;
using PizzaOut.CustomViews;
using UIKit;

namespace PizzaOut
{
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public partial class OurMenuViewController : UITableViewController
    {
        private List<MenuCategory> _menuCategories;

        private TableSource _tableSource;
        public OurMenuViewController (IntPtr handle) : base (handle)
        {
            Title = "Our Menu";

            // Custom initialization
            _menuCategories = new List<MenuCategory>
            {
                new MenuCategory() {MENU_CAT_NAME = "Pizza",MENU_CAT_ID = 1,MENU_CAT_IMAGE = "pizza.png",ACTIVE = 1},
                new MenuCategory() {MENU_CAT_NAME = "Drinks",MENU_CAT_ID = 2,MENU_CAT_IMAGE = "drinks.png",ACTIVE = 1},
            };
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _tableSource = new TableSource(_menuCategories);
            menuTableView.Source = _tableSource; //assign teh table data source
        }
    }
}