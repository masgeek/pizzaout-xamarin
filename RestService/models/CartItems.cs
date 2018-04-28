using System;
using System.Collections.Generic;
using System.Text;

namespace RestService.models
{
    public class CartItems
    {
        public int CART_ITEM_ID, USER_ID, ITEM_TYPE_ID, QUANTITY;

        public long CART_TIMESTAMP { get; set; }
        public double ITEM_PRICE;
        public String CREATED_AT, UPDATED_AT, ITEM_SIZE;
        public MenuCategoryItem MENU_CAT_ITEM;
    }
}
