using System;
using System.Diagnostics.CodeAnalysis;

namespace PizzaData.models
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class CartItem
    {
        public int CART_ITEM_ID { get; set; }

        public int USER_ID { get; set; }

        public int ITEM_TYPE_ID { get; set; }

        public int QUANTITY { get; set; }


        public long CART_TIMESTAMP { get; set; }
        public double ITEM_PRICE { get; set; }

        public string ITEM_SIZE { get; set; }

        public DateTime CREATED_AT { get; set; }

        public DateTime UPDATED_AT { get; set; }

        public MenuCategoryItem MENU_CAT_ITEM { get; set; }
    }
}
