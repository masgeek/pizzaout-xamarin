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

             CallOrderRest();
             CallMenuCategoriesRest();
            //RegisterUserRest();
            //Console.WriteLine("Hello sammy i am here");
            Console.ReadLine();
        }

        private static async void CallMenuCategoriesRest()
        {
            IRestResponse restResponse = await rest.GetRequest("v1/menucategories");
            if (restResponse != null)
            {
                List<MenuCategories> menuCategories = ObjectBuilder.BuildMenuCategoryList(restResponse);

                if (menuCategories != null)
                {
                    foreach (var menuCategory in menuCategories)
                    {
                        Console.WriteLine(menuCategory.MENU_CAT_NAME);
                        // Console.WriteLine(menuCategory.MENU_CAT_IMAGE);
                        //order.GetOrderItems(order.ORDER_DETAILS);
                        //get teh items under this menu
                        Console.WriteLine("----------------------------------");
                        CallMenuCategoryItemsRest(menuCategory.MENU_CAT_ID);
                    }
                }

                Console.WriteLine("Press any key to exit");
            }
        }

        private static async void CallMenuCategoryItemsRest(int menuCategoryMenuCatId)
        {
            IRestResponse restResponse = await rest.GetRequest("v1/menuitems/"+menuCategoryMenuCatId+"/cat-item");
            if (restResponse != null)
            {
                List<MenuCategoryItems> categoryItemList = ObjectBuilder.BuildCategoryItemList(restResponse);

                if (categoryItemList != null)
                {
                    foreach (var categoryItem in categoryItemList)
                    {
                        Console.WriteLine(categoryItem.MENU_ITEM_NAME);
                        //Console.WriteLine(categoryItem.MENU_ITEM_DESC);

                      var sizes =  categoryItem.GetSizes(categoryItem.SIZES);
                       //order.GetOrderItems(order.ORDER_DETAILS);
                    }
                }

                Console.WriteLine("------------------------------");
            }
        }

        private static async void RegisterUserRest()
        {
            var user = new User
            {
                USERNAME = "fatelordb",
                OTHER_NAMES = "Sammy",
                SURNAME = "Barasa",
                USER_STATUS = (int)User.ACCOUNT_STATUS.ACTIVE, //user is active or not
                USER_TYPE = 1,
                LOCATION_ID = 1,
                ADDRESS = "TEST",
                EMAIL = "sammy@gmail.com",
                MOBILE_NO = "0733333",
                PASSWORD = "123456",
                RESET_TOKEN = Guid.NewGuid().ToString()
            };

            Dictionary<string, object> userRegisterPost = new Dictionary<string, object>
            {
                {"SURNAME", user.SURNAME},
                {"OTHER_NAMES", user.OTHER_NAMES},
                {"MOBILE", user.MOBILE_NO},
                {"EMAIL", user.EMAIL},
                {"LOCATION_ID", user.LOCATION_ID},
                {"USER_NAME", user.USERNAME},
                {"USER_TYPE", user.USER_TYPE},
                {"PASSWORD", user.PASSWORD},
                {"RESET_TOKEN",user.RESET_TOKEN},
                {"USER_STATUS",user.USER_STATUS}
            };


            IRestResponse restResponse = await rest.PostRequest("v1/users/register",userRegisterPost);

           var userModel = ObjectBuilder.BuildUserList(restResponse);
            if (userModel != null)
            {
                //Console.WriteLine(userModel[0].USERNAME);
            }

        }
        private static async void CallOrderRest()
        {
            IRestResponse restResponse = await rest.GetRequest("v1/orders");
            if (restResponse != null)
            {
                List<Orders> orders = ObjectBuilder.BuildOrdersList(restResponse);

                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        //Console.WriteLine(order.GetOrderTimeline(order.ORDER_TIMELINE));
                       order.GetOrderItems(order.ORDER_DETAILS);
                    }
                }

                Console.WriteLine("Press any key to exit");
            }
        }
    }
}
