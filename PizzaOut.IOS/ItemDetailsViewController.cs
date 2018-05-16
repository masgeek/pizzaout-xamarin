using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Foundation;
using PizzaData.Helpers;
using PizzaData.models;
using PizzaOut.IOS.DataManager;
using SDWebImage;
using UIKit;

namespace PizzaOut.IOS
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
        private int _cartItemId;
        private bool itemExists;

        public ItemDetailsViewController (IntPtr handle) : base(handle) { }

        public ItemDetailsViewController()
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
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _sizes = _categoryItem.GetSizes(_categoryItem.SIZES);

            activityIndicator.StartAnimating();

            lblItemName.Text = _categoryItem.MENU_ITEM_NAME;
            //lblItemDesc.Text = _categoryItem.MENU_ITEM_DESC;
            itemImage.SetImage(
                url: new NSUrl(_categoryItem.MENU_ITEM_IMAGE),
                placeholder: UIImage.FromBundle("placeholder")
            );

            //lblItemDesc.Editable = false;
            quantityValue.Text = _selectedQuantity.ToString();

            //set the default values
            sizeStepper.MinimumValue = 0;
            sizeStepper.MaximumValue = _sizes.Count - 1; //set the maximum value based on sizes list decrease by one

            quantityStepper.MinimumValue = 1;
            quantityStepper.MaximumValue = _categoryItem.MAX_QTY; //set the maximum value

            await ComputeSizeAndCost(0, _sizes[_sizeIndex]);

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
                    activityIndicator.StartAnimating();
                    var itemAdded = await AddItemToCart();

                    activityIndicator.StopAnimating();
                    if (!itemAdded) return;
                    //resest the exists flag
                    itemExists = false;
                    //close the view and go back
                    this.NavigationController.PopViewController(true);
                };

            activityIndicator.StopAnimating();
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
                //let us update the cart
            
                cart = await restActions.UpdateCartItem(cartItem, _cartItemId);
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

            if (!itemExists)
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


            if (!itemExists)
            {
                var cartItem = await restActions.ItemAlreadyInCart(queryCartItem);
                if (cartItem != null)
                {
                    itemExists = true;

                    _cartItemId = cartItem.CART_ITEM_ID;
                    _selectedQuantity = cartItem.QUANTITY;
                    _itemPrice = cartItem.ITEM_PRICE;
                    _itemTypeId = cartItem.ITEM_TYPE_ID;
                    //compute the cost
                    _itemsSubTotal = _itemPrice * _selectedQuantity;

                    //totalCost.Text = String.Format("{0:C}", totalItemCost);//totalItemCost.ToString();
                    totalCost.Text = _itemsSubTotal.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));

                    // _selectedSize = cartItems.ITEM_SIZE;
                    // sizeValue.Text = _selectedSize;

                    quantityValue.Text = cartItem.QUANTITY.ToString();
                    quantityStepper.Value = _selectedQuantity;

                    btnAddToCart.SetTitle("Update Cart", UIControlState.Highlighted);
                }
                else
                {
                    btnAddToCart.SetTitle("Add to Cart", UIControlState.Normal);
                }
            }
            else
            {
                btnAddToCart.SetTitle("Add to Cart", UIControlState.Normal);
            }
        }
    }
}