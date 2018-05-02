using Foundation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Emit;
using System.Threading.Tasks;
using FFImageLoading;
using PizzaData.models;
using PizzaOut.DataManager;
using UIKit;
using Valore.IOSDropDown;

namespace PizzaOut
{
    public partial class ItemDetailsViewController : UIViewController
    {
        private RestActions restActions;
        private MenuCategoryItem _categoryItem;
        private int _selectedQuantity = 1, _sizeIndex = 0;
        private string _selectedSize = null;
        private List<ItemSize> _sizes;
        private double _totalItemCost = 0.0;

        public ItemDetailsViewController (IntPtr handle) : base (handle)
        {
        }

        public void SetSelectedItem(MenuCategoryItem categoryItem)
        {
            this._categoryItem = categoryItem;
            restActions = new RestActions();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = _categoryItem.MENU_ITEM_NAME;
            _sizes = _categoryItem.GetSizes(_categoryItem.SIZES);

            lblItemName.Text = _categoryItem.MENU_ITEM_NAME;
            //lblItemDesc.Text = _categoryItem.MENU_ITEM_DESC;
            ImageLoader.LoadImage(_categoryItem.MENU_ITEM_IMAGE).Into(itemImage);

            //lblItemDesc.Editable = false;
            quantityValue.Text = _selectedQuantity.ToString();

            //set the default values
            ComputeSizeAndCost(0, _sizes);

            //sizeStepper.Value = _selectedQuantity;
            quantityStepper.Value = _selectedQuantity;

            sizeStepper.ValueChanged += SizeStepper_ValueChanged;
        }

        private void SizeStepper_ValueChanged(object sender, EventArgs e)
        {
        
            sizeStepper.MinimumValue = 0;
            sizeStepper.MaximumValue = _sizes.Count-1; //set the maximum value based on sizes list decrease by one
            sizeValue.Text = _selectedSize;
            _sizeIndex = (int)sizeStepper.Value;
            ComputeSizeAndCost(_sizeIndex,_sizes);
        }

        partial void QuantityChangedEvent(UIStepper sender)
        {
            quantityStepper.MinimumValue = 1;
            quantityStepper.MaximumValue = _categoryItem.MAX_QTY; //set the maximum value
            _selectedQuantity = (int) quantityStepper.Value;
            quantityValue.Text = _selectedQuantity.ToString();
            ComputeSizeAndCost(_sizeIndex, _sizes);
        }


        private void ComputeSizeAndCost(int index, List<ItemSize> itemSizes)
        {
            if (index < 0)
            {
                index = 0;
            }
            var size = itemSizes[index];

            //compute the cost
            _totalItemCost = size.PRICE * _selectedQuantity;

            //totalCost.Text = String.Format("{0:C}", totalItemCost);//totalItemCost.ToString();
            totalCost.Text = _totalItemCost.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));

            _selectedSize = size.ITEM_TYPE_SIZE;
            sizeValue.Text = _selectedSize;
        }

        /// <summary>
        /// Checks if the cart already has items
        /// </summary>
        private async Task itemAlreadyInCart(int itemTypeId,int userId)
        {
            var cartItems = await restActions.ItemAlreadyInCart(itemTypeId,userId);

        }
    }
}