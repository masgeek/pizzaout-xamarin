using Foundation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Emit;
using System.Threading.Tasks;
using FFImageLoading;
using PizzaData.models;
using PizzaOut.DataManager;
using RestService.Helpers;
using UIKit;

namespace PizzaOut
{
    public partial class ItemDetailsViewController : UIViewController
    {
        private RestActions restActions;
        private MenuCategoryItem _categoryItem;
        private int _selectedQuantity = 1, _sizeIndex = 0;
        private string _selectedSize = null;
        private List<ItemSize> _sizes;
        private double _itemsSubTotal = 0.0,_itemPrice = 0.0;
        private int _userId;
        private int _itemTypeId;
        public ItemDetailsViewController (IntPtr handle) : base (handle)
        {
        }

        public void SetSelectedItem(MenuCategoryItem categoryItem)
        {
            this._categoryItem = categoryItem;
            restActions = new RestActions();
            _userId = UserSession.GetUserId();
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

            //click action for add to cart
            btnAddToCart.TouchUpInside += async (object sender, EventArgs e) =>
                {
                    //this.NavigationController.PopViewController(true);
                    AddItemToCart();
                };
        }

        private async Task AddItemToCart()
        {
            //v1/my-cart
            var cartItem = new CartItem
            {
                USER_ID = _userId,
                ITEM_TYPE_ID = _itemTypeId,
                ITEM_SIZE = _selectedSize,
                ITEM_PRICE = _itemPrice,
                SUB_TOTAL = _itemsSubTotal,
                QUANTITY = _selectedQuantity,
                CART_TIMESTAMP = Helper.GetTimeStamp()
            };

            var cart = await restActions.AddCartItem(cartItem);

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

            _itemPrice = size.PRICE;
            //compute the cost
            _itemsSubTotal =_itemPrice * _selectedQuantity;

            //totalCost.Text = String.Format("{0:C}", totalItemCost);//totalItemCost.ToString();
            totalCost.Text = _itemsSubTotal.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));

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