using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleJson;

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

        //public List<TimeLine> ORDER_TIMELINE { get; set; }
        public JArray ORDER_DETAILS { get; set; }
        public JArray ORDER_TIMELINE { get; set; }
        public JArray ORDER_ITEMS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TimeLine> GetOrderTimeline(JArray orderTimelineJArray)
        {
            List<TimeLine> items = null;
            if (orderTimelineJArray != null)
            {
                items = (orderTimelineJArray).Select(x => new TimeLine
                {
                    TRACKING_ID = (int) x["TRACKING_ID"],
                    STATUS = (string) x["STATUS"]
                }).ToList();
            }

            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrderItems> GetOrderItems(JArray orderItemsJArray)
        {
            List<OrderItems> items = null;
            if (orderItemsJArray == null) return null;

            var children = orderItemsJArray.Children();

            foreach (var child in children)
            {
                var itemProperties = child.Children<JProperty>();

                List<JToken> properties = itemProperties.Select(o => o.Value).ToList();

                foreach (var itemProperty in itemProperties)
                {
                    var th = itemProperty.Name;
                    var t = itemProperties[th];
                        Console.WriteLine(t.ToString());
                }
            }

           
            return items;
        }

        public List<OrderItems> GetOrderDetails(JArray orderItemsJArray)
        {
            List<OrderItems> items = null;
            if (orderItemsJArray == null) return null;

            items = (orderItemsJArray).Select(x => new OrderItems
            {
                ORDER_ITEM_ID = (int) x["ORDER_ITEM_ID"],
                ORDER_ID = (int) x["ORDER_ID"]
            }).ToList();


            return items;
        }
    }
}