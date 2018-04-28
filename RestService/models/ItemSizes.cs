using System;
using System.Collections.Generic;
using System.Text;

namespace RestService.models
{
    public class ItemSizes
    {
        public int ITEM_TYPE_ID { get; set; }

        public int MENU_ITEM_ID { get; set; }

        public double PRICE { get; set; }

        public String ITEM_TYPE_SIZE { get; set; }

        public bool AVAILABLE { get; set; }
    }
}
