﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace PizzaData.models
{
    public class MenuCategoryItem
    {
        public int MENU_ITEM_ID { get; set; }
        public int MENU_CAT_ID { get; set; }
        public int MAX_QTY { get; set; }

        public String MENU_ITEM_NAME { get; set; }
        public String MENU_ITEM_DESC { get; set; }
        public String MENU_ITEM_IMAGE { get; set; }
        public String CAT_NAME { get; set; }




        public bool HOT_DEAL { get; set; }
        public bool VEGETARIAN { get; set; }

        public JArray SIZES { get; set; }
        /// <summary>
        /// "ITEM_TYPE_ID": "1",
        /// "MENU_ITEM_ID": "1",
        ///  "ITEM_TYPE_SIZE": "LARGE",
        /// "PRICE": "15.00",
        /// "AVAILABLE": "1"
        /// </summary>
        /// <param name="sizesJArray"></param>
        /// <returns></returns>
        public List<ItemSize> GetSizes(JArray sizesJArray)
        {
            List<ItemSize> itemSizes = null;
            if (sizesJArray != null)
            {
                itemSizes = (sizesJArray).Select(x => new ItemSize
                {
                    ITEM_TYPE_ID = (int)x["ITEM_TYPE_ID"],
                    MENU_ITEM_ID = (int)x["MENU_ITEM_ID"],
                    ITEM_TYPE_SIZE = (string)x["ITEM_TYPE_SIZE"],
                    PRICE = (double)x["PRICE"],
                    AVAILABLE = (string)x["AVAILABLE"],
                }).ToList();
            }

            return itemSizes;
        }

        /// <summary>
        /// Create a dropdown of teh max quantity a single customer can order
        /// </summary>
        /// <param name="maxQty"></param>
        /// <returns></returns>
        public List<int> GetMaxQuantity(int maxQty)
        {
            List<int> maxQuantityList = null;
            for (int qty = 0; qty < maxQty; qty = qty + 1)
            {
               maxQuantityList.Add(qty);
            }
            return maxQuantityList;
        }
    }
}
