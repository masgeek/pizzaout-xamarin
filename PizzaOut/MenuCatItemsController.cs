using Foundation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using PizzaData.models;
using PizzaOut.DataManager;
using PizzaOut.TableViews;
using UIKit;

namespace PizzaOut
{
    public partial class MenuCatItemsController : UITableViewController
    {
        private RestActions restActions;
        private MenuCategory _selectedCategory;
        private List<MenuCategoryItem> _menuCategoriesItems;
        private TextInfo textInfo;
        private MenuCatItemTableSource _tableSource;

        public MenuCatItemsController(MenuCategory menuCat)
        {
            this._selectedCategory = menuCat;
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            textInfo = cultureInfo.TextInfo;
            restActions = new RestActions();
        }

        public MenuCatItemsController (IntPtr handle) : base (handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            string menuTitle = _selectedCategory.MENU_CAT_NAME+" Menu";

            Title = textInfo.ToTitleCase(textInfo.ToLower(menuTitle));

            _menuCategoriesItems = await LoadMenuCatItems(_selectedCategory.MENU_CAT_ID);

            if (_menuCategoriesItems != null)
            {
                 _tableSource = new MenuCatItemTableSource(_menuCategoriesItems);
                if (menuCatItemTableView != null)
                {
                    menuCatItemTableView.Source = _tableSource; //assign the table data source

                    //reload the data
                    //menuCatItemTableView.RowHeight = UITableView.AutomaticDimension;
                    //menuCatItemTableView.EstimatedRowHeight = 40f;
                    menuCatItemTableView.ReloadData();
                }
            }


        }

        private async Task<List<MenuCategoryItem>> LoadMenuCatItems(int selectedCategoryMenuCatId)
        {
            var menuCategoriesList = await restActions.GetMenuCategoryItems(selectedCategoryMenuCatId);

            return menuCategoriesList;
        }
    }
}