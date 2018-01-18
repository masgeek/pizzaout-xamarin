using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;

namespace RestService.models
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Orders
    {
      
        public int ORDER_ID { get; set; }
        public int USER_ID { get; set; }
        public int LOCATION_ID { get; set; }
        public int KITCHEN_ID { get; set; }
        public int CHEF_ID { get; set; }
        public int RIDER_ID { get; set; }
        public double ORDER_TOTAL { get; set; }

        public DateTime ORDER_DATE { get; set; }
        public DateTime CREATED_AT { get; set; }
        public DateTime UPDATED_AT { get; set; }
        public string PAYMENT_METHOD { get; set; }
        public string ORDER_STATUS { get; set; }
        public string NOTES { get; set; }
        //public Rider riderModel;
        //public AddressModel addressModelModel;
        //public Location locationModel;
        // public Payment paymentModel;

        //public OrderItems OrderItems { get; set; }

        //public string ORDER_ITEMS { get; set; }
        //public List<OrderItems> LOCATION { get; set; }
        //public List <OrderItems> ORDER_ITEMS { get; set; }

        public List<TimeLine> ORDER_TIMELINE { get; set; }
    }
}