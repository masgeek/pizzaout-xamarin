﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace PizzaData.models
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class OrderItem
    {
        public int ORDER_ITEM_ID { get; set; }
        public int ORDER_ID { get; set; }
        public int ITEM_TYPE_ID { get; set; }
        public int  QUANTITY { get; set; }
        public double PRICE{ get; set; }
        public double SUBTOTAL{ get; set; }

        public string OPTIONS{ get; set; }
        public string  NOTES { get; set; }
        public DateTime CREATED_AT{ get; set; }
        public string UPDATED_AT{ get; set; }
}
}
