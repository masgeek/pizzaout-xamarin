using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using PizzaData.models;
using PizzaOut.IOS.DataManager;
using PizzaOut.IOS.TableViews;
using PizzaOut.IOS.UIHelpers;
using UIKit;

namespace PizzaOut.IOS
{
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public partial class OurMenuViewController : UITableViewController
    {
        private List<MenuCategory> _menuCategories;

        private MenuCatTableSource _tableSource;
        private RestActions _restActions;
        private LoadingOverlay _loadingOverlay;
        public OurMenuViewController (IntPtr handle) : base (handle)
        {
 
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            try
            {
    
                var bounds = UIScreen.MainScreen.Bounds;
                _loadingOverlay = new LoadingOverlay(bounds, "Loading menu items...");
                View.Add(_loadingOverlay);


                //tableView.RegisterNibForCellReuse(CustomCell.Nib, CustomCell.Key);

                _menuCategories = await LoadMenuCategories();

                _loadingOverlay.Hide();//hide the overlay after loading
                if (_menuCategories != null)
                {

                    _tableSource = new MenuCatTableSource(_menuCategories, this);
                    menuTableView.Source = _tableSource; //assign the table data source

                    //reload the data
                    //menuTableView.RowHeight = UITableView.AutomaticDimension;
                    //menuTableView.EstimatedRowHeight = 40f;
                    menuTableView.ReloadData();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                _loadingOverlay.Hide();
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Our Menu";
            _restActions = new RestActions();
        }

        /// <summary>
        /// Get the menu categories
        /// </summary>
        /// <returns></returns>
        private async Task<List<MenuCategory>> LoadMenuCategories()
        {
            var menuCategoriesList = await _restActions.GetMenuCategories();

            return menuCategoriesList;

        }
    }
}