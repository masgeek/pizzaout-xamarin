using Foundation;
using System;
using FFImageLoading;
using PizzaData.models;
using UIKit;

namespace PizzaOut
{
    public partial class MenuCatCell : UITableViewCell
    {


        public MenuCatCell(IntPtr handle) : base(handle)
        {
            SelectionStyle = UITableViewCellSelectionStyle.Gray;
        }
        public void UpdateCell(MenuCategory menuCat)
        {
            //loadImage(menuCat.MENU_CAT_IMAGE, itemImage);
            //itemName.Text = menuCat.MENU_CAT_NAME;
        }
    }
}