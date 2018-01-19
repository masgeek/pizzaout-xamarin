using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestService;
using RestService.Helpers;
using RestService.models;
using RestSharp;

namespace TestConsole
{
    class Program
    {
        private static RestServiceFactory rest;
        static void Main()
        {
            rest = RestServiceFactory.Instance;

            CallRest();

            Console.ReadLine();
        }

        private static async void CallRest()
        {
            IRestResponse restResponse = await rest.GetRequest("v1/orders");
            if (restResponse != null)
            {
                List<Orders> orders = ObjectBuilder.BuildOrdersObjectArr(restResponse);

                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        //Console.WriteLine(order.GetOrderTimeline(order.ORDER_TIMELINE));
                        Console.WriteLine(order.GetOrderItems(order.ORDER_DETAILS));
                    }
                }

                Console.WriteLine("Press any key to exit");
            }
        }
    }
}
