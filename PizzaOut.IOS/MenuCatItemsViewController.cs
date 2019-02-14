using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using PizzaData.models;
using PizzaOut.IOS.DataManager;
using PizzaOut.IOS.TableViews;
using PizzaOut.IOS.UIHelpers;
using UIKit;

namespace PizzaOut.IOS
{
    public partial class MenuCatItemsViewController : UITableViewController
    {
        private MenuCategory _selectedCategory;
        private LoadingOverlay _loadingOverlay;
        private RestActions restActions;
        private List<MenuCategoryItem> _menuCategoriesItems;
        private TextInfo textInfo;
        private MenuCatItemTableSource _menuCatItemTableSource;
        //private UITableView menuCatItemTableView;
        public MenuCatItemsViewController (IntPtr handle) : base (handle)
        {
        }

        public void SetSelectedItem(MenuCategory category)
        {
            this._selectedCategory = category;
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            textInfo = cultureInfo.TextInfo;
            restActions = new RestActions();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            string menuTitle = _selectedCategory.MENU_CAT_NAME + " Menu";

            Title = textInfo.ToTitleCase(textInfo.ToLower(menuTitle));
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewDidLoad();
            try
            {
                var bounds = UIScreen.MainScreen.Bounds;
                _loadingOverlay = new LoadingOverlay(bounds, $"Loading {_selectedCategory.MENU_CAT_NAME} items...");
                View.Add(_loadingOverlay);

                _menuCategoriesItems = await LoadMenuCatItems(_selectedCategory.MENU_CAT_ID);

                _loadingOverlay.Hide();//hide the overlay
                if (_menuCategoriesItems != null)
                {
                    _menuCatItemTableSource = new MenuCatItemTableSource(_menuCategoriesItems, this);
                    if (menuCatItemTableView != null)
                    {
                        menuCatItemTableView.Source = _menuCatItemTableSource; //assign the table data source

                        //reload the data
                        //menuCatItemTableView.RowHeight = UITableView.AutomaticDimension;
                        //menuCatItemTableView.EstimatedRowHeight = 40f;
                        menuCatItemTableView.ReloadData();
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                _loadingOverlay.Hide();
            }

        }

        private async Task<List<MenuCategoryItem>> LoadMenuCatItems(int selectedCategoryMenuCatId)
        {
            var menuCategoriesList = await restActions.GetMenuCategoryItems(selectedCategoryMenuCatId);

            return menuCategoriesList;
        }
    }
}