using Foundation;
using System;
using FFImageLoading;
using PizzaData.models;
using PizzaOut.DataManager;
using UIKit;

namespace PizzaOut
{
    public partial class ItemDetailsViewController : UIViewController
    {
        private MenuCategoryItem _categoryItem;
        private int selectedQuantity = 1;
        public ItemDetailsViewController (IntPtr handle) : base (handle)
        {
        }

        public void SetSelectedItem(MenuCategoryItem categoryItem)
        {
            this._categoryItem = categoryItem;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = _categoryItem.MENU_ITEM_NAME;

            lblItemName.Text = _categoryItem.MENU_ITEM_NAME;
            lblItemDesc.Text = _categoryItem.MENU_ITEM_DESC;
            ImageLoader.LoadImage(_categoryItem.MENU_ITEM_IMAGE).Into(itemImage);

            lblItemDesc.Editable = false;
            quantityValue.Text = selectedQuantity.ToString();

        }

        partial void QuantityChangedEvent(UIStepper sender)
        {
            quantityStepper.MinimumValue = 1;
            quantityStepper.MaximumValue = _categoryItem.MAX_QTY; //set the maximum value
            selectedQuantity = (int) quantityStepper.Value;
            quantityValue.Text = selectedQuantity.ToString();
        }
    }
}