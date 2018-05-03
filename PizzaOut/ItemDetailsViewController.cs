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
        private bool itemExists;

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
            sizeStepper.MinimumValue = 0;
            sizeStepper.MaximumValue = _sizes.Count - 1; //set the maximum value based on sizes list decrease by one

            quantityStepper.MinimumValue = 1;
            quantityStepper.MaximumValue = _categoryItem.MAX_QTY; //set the maximum value

            ComputeSizeAndCost(0, _sizes[_sizeIndex]);

            //sizeStepper.Value = _selectedQuantity;
            quantityStepper.Value = _selectedQuantity;

            sizeStepper.ValueChanged += async (object sender, EventArgs e) =>
                {
                    await SizeStepperValueChanged(sender, e);
                };

            quantityStepper.ValueChanged += async (object sender, EventArgs e) =>
            {
                await QuantityStepperValueChanged(sender, e);
            };
            //click action for add to cart
            btnAddToCart.TouchUpInside += async (object sender, EventArgs e) =>
                {
                    
                    var itemAdded = await AddItemToCart();

                    if (itemAdded)
                    {
                        //close the view and go back
                        //this.NavigationController.PopViewController(true);
                    }
                };
        }

        private async Task<bool> AddItemToCart()
        {
            //v1/my-cart
            CartItem cart;
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
            if (itemExists)
            {
                //let us update teh cart
                cart = await restActions.UpdateCartItem(cartItem);
            }
            else
            {
                cart = await restActions.AddCartItem(cartItem);
            }

            return cart != null;
        }


        private async Task SizeStepperValueChanged(object sender, EventArgs e)
        {
       
            sizeValue.Text = _selectedSize;
            _sizeIndex = (int)sizeStepper.Value;


            //check if the item is already in the cart
            if (_sizeIndex < 0)
            {
                _sizeIndex = 0;
            }
            var size = _sizes[_sizeIndex];

            itemExists = false; //set the item exosts flag to false so we can recheck the cart
            await ComputeSizeAndCost(_sizeIndex, size);
        }

        private async Task QuantityStepperValueChanged(object sender, EventArgs e)
        {
            _selectedQuantity = (int) quantityStepper.Value;
            quantityValue.Text = _selectedQuantity.ToString();

            if (_sizeIndex < 0)
            {
                _sizeIndex = 0;
            }
            var size = _sizes[_sizeIndex];

            await ComputeSizeAndCost(_sizeIndex, size);
        }


        private async Task ComputeSizeAndCost(int index, ItemSize size)
        {
            _itemPrice = size.PRICE;
            _itemTypeId = size.ITEM_TYPE_ID;
            //compute the cost
            _itemsSubTotal = _itemPrice * _selectedQuantity;

            //totalCost.Text = String.Format("{0:C}", totalItemCost);//totalItemCost.ToString();
            totalCost.Text = _itemsSubTotal.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));

            _selectedSize = size.ITEM_TYPE_SIZE;
            sizeValue.Text = _selectedSize;


            await ItemAlreadyInCart(size);


        }


        /// <summary>
        /// Checks if the cart already has items
        /// </summary>
        private async Task ItemAlreadyInCart(ItemSize item)
        {
            CartItem queryCartItem = new CartItem
            {
                ITEM_TYPE_ID = item.ITEM_TYPE_ID,
                ITEM_SIZE = item.ITEM_TYPE_SIZE,
                USER_ID = _userId
            };


            if (!itemExists)
            {
                var cartItems = await restActions.ItemAlreadyInCart(queryCartItem);
                if (cartItems != null)
                {
                    itemExists = true;

                    _selectedQuantity = cartItems.QUANTITY;
                    _itemPrice = cartItems.ITEM_PRICE;
                    _itemTypeId = cartItems.ITEM_TYPE_ID;
                    //compute the cost
                    _itemsSubTotal = _itemPrice * _selectedQuantity;

                    //totalCost.Text = String.Format("{0:C}", totalItemCost);//totalItemCost.ToString();
                    totalCost.Text = _itemsSubTotal.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));

                    // _selectedSize = cartItems.ITEM_SIZE;
                    // sizeValue.Text = _selectedSize;

                    quantityValue.Text = cartItems.QUANTITY.ToString();
                    quantityStepper.Value = _selectedQuantity;

                    btnAddToCart.SetTitle("Update Cart", UIControlState.Normal);
                }
            }
            else
            {
                btnAddToCart.SetTitle("Add to Cart", UIControlState.Normal);
            }
        }
    }
}