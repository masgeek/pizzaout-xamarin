using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PizzaData.models
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Order
    {
        [Key]
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
        public DateTime ORDER_DATE_TIME { get; set; }

        public string ORDER_TIME { get; set; }

        public string PAYMENT_METHOD { get; set; }
        public string ORDER_STATUS { get; set; }

        public string USSD_NUMBER { get; set; }

        public bool ORDER_CREATED { get; set; }
        public bool PAY_ORDER { get; set; }

        public string NOTES { get; set; }

        public Location LOCATION { get; set; }

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
                    STATUS = (string) x["STATUS"],
                    USER_VISIBLE = (bool)x["USER_VISIBLE"]
                }).ToList();
            }

            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrderItem> GetOrderItems(JArray orderItemsJArray, string filterName = "ORDER_ITEMS")
        {
            List<OrderItem> items = new List<OrderItem>();

            if (orderItemsJArray == null) return null;

            JEnumerable<JToken> children = orderItemsJArray.Children();

            List<JToken> properties = GetSubArray(children, filterName);

                //loop through the arrray list
                foreach (var property in properties)
                {
                    OrderItem item = property.ToObject<OrderItem>();
                    //add to list array
                    items.Add(item);
                }

                Console.WriteLine(items.Count);
           
            return items;
        }

        public List<OrderItem> GetOrderDetails(JArray orderItemsJArray)
        {
            List<OrderItem> items = new List<OrderItem>();
            if (orderItemsJArray == null) return null;

            items = (orderItemsJArray).Select(x => new OrderItem
            {
                ORDER_ITEM_ID = (int) x["ORDER_ITEM_ID"],
                ORDER_ID = (int) x["ORDER_ID"]
            }).ToList();


            return items;
        }

        /// <summary>
        /// Get the total of all order items
        /// </summary>
        /// <returns>double orderTotal</returns>
        public double ComputeOrderTotal()
        {
            double orderTotal = 0.0;
            if (this.ORDER_DETAILS != null)
            {
                List<OrderItem> orderItem = GetOrderItems(ORDER_DETAILS);

                foreach (var item in orderItem)
                {
                    double subtotal = item.QUANTITY * item.PRICE;
                    orderTotal = orderTotal + subtotal;
                }
            }

            return orderTotal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="children"></param>
        /// <param name="filterName"></param>
        /// <returns></returns>
        protected List<JToken> GetSubArray(JEnumerable<JToken> children, string filterName)
        {
            List<JToken> properties = new List<JToken>();
        
            foreach (var child in children)
            {
                var itemProperties = child.Children<JProperty>();

                var jToken = itemProperties
                    .Where(f => f.Name.Equals(filterName))
                    .Select(o => o.Value)
                    .FirstOrDefault();

                properties.Add(jToken);
            }

            Console.WriteLine("List count is "+properties.Count());
            return properties;
        }
    }
}