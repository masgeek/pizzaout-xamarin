using Foundation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using PizzaData.models;
using PizzaOut.DataManager;
using PizzaOut.TableViews;
using UIKit;

namespace PizzaOut
{
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public partial class OurMenuViewController : UITableViewController
    {
        private List<MenuCategory> _menuCategories;

        private MenuCatTableSource _tableSource;
        private RestActions restActions;

        public OurMenuViewController (IntPtr handle) : base (handle)
        {
  
            Title = "Our Menu";

            // Custom initialization
            /*_menuCategories = new List<MenuCategory>
            {
                new MenuCategory() {MENU_CAT_NAME = "Pizza",MENU_CAT_ID = 1,MENU_CAT_IMAGE = "pizza.png",ACTIVE = 1},
                new MenuCategory() {MENU_CAT_NAME = "Drinks",MENU_CAT_ID = 2,MENU_CAT_IMAGE = "drinks.png",ACTIVE = 1},
            };*/

            restActions = new RestActions();
        }


        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            //tableView.RegisterNibForCellReuse(CustomCell.Nib, CustomCell.Key);

            _menuCategories = await LoadMenuCategories();
            if (_menuCategories != null)
            {
                _tableSource = new MenuCatTableSource(_menuCategories);
                menuTableView.Source = _tableSource; //assign the table data source

                //reload the data
                //menuTableView.RowHeight = UITableView.AutomaticDimension;
                //menuTableView.EstimatedRowHeight = 40f;
                menuTableView.ReloadData();
            }
        }

        /// <summary>
        /// Get eh menu categories
        /// </summary>
        /// <returns></returns>
        private async Task<List<MenuCategory>> LoadMenuCategories()
        {
            var menuCategoriesList = await restActions.GetMenuCategories();

            return menuCategoriesList;

        }
    }
}