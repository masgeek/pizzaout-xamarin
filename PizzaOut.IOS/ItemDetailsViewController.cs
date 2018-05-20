using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Foundation;
using PizzaData.Helpers;
using PizzaData.models;
using PizzaOut.IOS.DataManager;
using PizzaOut.IOS.UIHelpers;
using SDWebImage;
using UIKit;

namespace PizzaOut.IOS
{
    public partial class ItemDetailsViewController : UIViewController
    {
        private RestActions _restActions;
        private MenuCategoryItem _categoryItem;
        private int _selectedQuantity = 1, _sizeIndex;
        private string _selectedSize;
        private List<ItemSize> _sizes;
        private double _itemsSubTotal, _itemPrice;
        private int _userId;
        private int _itemTypeId;
        private int _cartItemId;
        private bool _itemExists;

        private LoadingOverlay _loadingOverlay;
        public ItemDetailsViewController(IntPtr handle) : base(handle) { }

        public ItemDetailsViewController()
        {
        }

        public void SetSelectedItem(MenuCategoryItem categoryItem)
        {
            _categoryItem = categoryItem;
            _restActions = new RestActions();
            _userId = UserSession.GetUserId();
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = _categoryItem.MENU_ITEM_NAME;
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _sizes = _categoryItem.GetSizes(_categoryItem.SIZES);

            LblItemName.Text = _categoryItem.MENU_ITEM_NAME;
            TxtItemDesc.Text = _categoryItem.MENU_ITEM_DESC;

            TxtItemDesc.Editable = false; //make the textfield readonly

            itemImage.SetImage(
                url: new NSUrl(_categoryItem.MENU_ITEM_IMAGE),
                placeholder: UIImage.FromBundle("placeholder")
            );

            //lblItemDesc.Editable = false;
            LblSelectedQuantity.Text = _selectedQuantity.ToString();

            //set the default values
            SizePicker.MinimumValue = 0;
            SizePicker.MaximumValue = _sizes.Count - 1; //set the maximum value based on sizes list decrease by one

            QuantityPicker.MinimumValue = 1;
            QuantityPicker.MaximumValue = _categoryItem.MAX_QTY; //set the maximum value

            await ComputeSizeAndCost(_sizes[_sizeIndex]);

            //sizeStepper.Value = _selectedQuantity;
            QuantityPicker.Value = _selectedQuantity;

            SizePicker.ValueChanged += async (sender, e) =>
            {
                await SizeStepperValueChanged();
            };

            QuantityPicker.ValueChanged += async (sender, e) =>
            {
                await QuantityStepperValueChanged();
            };
            //click action for add to cart
            BtnAddToCart.TouchUpInside += async (sender, e) =>
            {
                var bounds = UIScreen.MainScreen.Bounds;
                _loadingOverlay = new LoadingOverlay(bounds, "Updating your cart...");
                View.Add(_loadingOverlay);
                var itemAdded = await AddItemToCart();

                _loadingOverlay.Hide();
                if (!itemAdded) return;
                //resest the exists flag
                _itemExists = false;
                //close the view and go back
                NavigationController.PopViewController(true);
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
            if (_itemExists)
            {
                //let us update the cart

                cart = await _restActions.UpdateCartItem(cartItem, _cartItemId);
            }
            else
            {
                cart = await _restActions.AddCartItem(cartItem);
            }


            return cart != null;
        }


        private async Task SizeStepperValueChanged()
        {

            LblSelectedSize.Text = _selectedSize;
            _sizeIndex = (int)SizePicker.Value;


            //check if the item is already in the cart
            if (_sizeIndex < 0)
            {
                _sizeIndex = 0;
            }
            var size = _sizes[_sizeIndex];

            _itemExists = false; //set the item exosts flag to false so we can recheck the cart
            await ComputeSizeAndCost(size);
        }

        private async Task QuantityStepperValueChanged()
        {
            _selectedQuantity = (int)QuantityPicker.Value;
            LblSelectedQuantity.Text = _selectedQuantity.ToString();

            if (_sizeIndex < 0)
            {
                _sizeIndex = 0;
            }
            var size = _sizes[_sizeIndex];

            await ComputeSizeAndCost(size);
        }


        private async Task ComputeSizeAndCost(ItemSize size)
        {
            _itemPrice = size.PRICE;
            _itemTypeId = size.ITEM_TYPE_ID;
            //compute the cost
            _itemsSubTotal = _itemPrice * _selectedQuantity;

            //totalCost.Text = String.Format("{0:C}", totalItemCost);//totalItemCost.ToString();
            LblTotal.Text = _itemsSubTotal.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));

            _selectedSize = size.ITEM_TYPE_SIZE;
            LblSelectedSize.Text = _selectedSize;

            if (!_itemExists)
            {
                await ItemAlreadyInCart(size);
            }


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


            if (!_itemExists)
            {
                var cartItem = await _restActions.ItemAlreadyInCart(queryCartItem);
                if (cartItem != null)
                {
                    _itemExists = true;

                    _cartItemId = cartItem.CART_ITEM_ID;
                    _selectedQuantity = cartItem.QUANTITY;
                    _itemPrice = cartItem.ITEM_PRICE;
                    _itemTypeId = cartItem.ITEM_TYPE_ID;
                    //compute the cost
                    _itemsSubTotal = _itemPrice * _selectedQuantity;

                    //totalCost.Text = String.Format("{0:C}", totalItemCost);//totalItemCost.ToString();
                    LblTotal.Text = _itemsSubTotal.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));

                    // _selectedSize = cartItems.ITEM_SIZE;
                    // sizeValue.Text = _selectedSize;

                    LblSelectedQuantity.Text = cartItem.QUANTITY.ToString();
                    QuantityPicker.Value = _selectedQuantity;

                    BtnAddToCart.SetTitle("Update Cart", UIControlState.Highlighted);
                }
                else
                {
                    BtnAddToCart.SetTitle("Add to Cart", UIControlState.Normal);
                }
            }
            else
            {
                BtnAddToCart.SetTitle("Add to Cart", UIControlState.Normal);
            }
        }
    }
}