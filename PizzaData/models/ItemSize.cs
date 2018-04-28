using System;

namespace PizzaData.models
{
    public class ItemSize
    {
        public int ITEM_TYPE_ID { get; set; }
        public int MENU_ITEM_ID { get; set; }

        public String ITEM_TYPE_SIZE { get; set; }

        public double PRICE { get; set; }

        public string AVAILABLE { get; set; }
    }
}
