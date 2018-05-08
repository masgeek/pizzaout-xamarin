using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using PizzaData.Helpers;
using PizzaData.models;
using PizzaData.Rest;
using RestService.Helpers;
using RestSharp;
using UIKit;

namespace PizzaOut.DataManager
{
    public class RestActions
    {
        private static RestServiceFactory _rest;

        private RestActions _restActions;

       // private static object myObject = new object();

        private static IRestResponse _restResponse;


        public RestActions()
        {
            _rest = new RestServiceFactory();
        }

        public async Task<User> RegisterUser(User user)
        {
            Dictionary<string, object> userRegisterPost = new Dictionary<string, object>
            {
                {"SURNAME", user.SURNAME},
                {"OTHER_NAMES", user.OTHER_NAMES},
                {"MOBILE", user.MOBILE_NO},
                {"EMAIL", user.EMAIL},
                {"LOCATION_ID", user.LOCATION_ID},
                {"USER_NAME", user.USER_NAME},
                {"USER_TYPE", user.USER_TYPE},
                {"PASSWORD", user.PASSWORD},
                {"RESET_TOKEN",user.RESET_TOKEN},
                {"USER_STATUS",user.USER_STATUS},
                {"RETURN_MODEL","YES"},
            };


            IRestResponse restResponse = await _rest.PostRequest("v1/users/register", userRegisterPost);

            var userModel = ObjectBuilder.BuildUserObject(restResponse);
            return userModel;

        }

        /// <summary>
        /// Log in the user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> LoginUser(string username, string password)
        {
            User user = new User
            {
                USER_NAME = username,

                PASSWORD = password,
            };

            Dictionary<string, object> loginPost = new Dictionary<string, object>
            {
                {"USER_NAME", user.USER_NAME},
                {"PASSWORD", user.PASSWORD},
            };


            _restResponse = await _rest.PostRequest("v1/users/login", loginPost);

            User userModel =  ObjectBuilder.BuildUserObject(_restResponse);

           return userModel;

        }

        public async Task<List<MenuCategory>> GetMenuCategories()
        {
            _restResponse = await _rest.GetRequest("v1/menucategories");

            List<MenuCategory> menuCategoryList = ObjectBuilder.BuildMenuCategoryList(_restResponse);

            return menuCategoryList;
        }

        public async Task<List<MenuCategoryItem>> GetMenuCategoryItems(int menu_cat_id)
        {
            _restResponse = await _rest.GetRequest("v1/menuitems/" + menu_cat_id + "/cat-item");

            List<MenuCategoryItem> menuCategoryList = ObjectBuilder.BuildCategoryItemList(_restResponse);

            return menuCategoryList;
        }


        public async Task<CartItem> AddCartItem(CartItem cartItem)
        {
            Dictionary<string, object> cartPost = new Dictionary<string, object>
            {
                {"USER_ID", cartItem.USER_ID},
                {"ITEM_TYPE_ID",cartItem.ITEM_TYPE_ID},
                {"QUANTITY", cartItem.QUANTITY},
                {"ITEM_PRICE", cartItem.ITEM_PRICE},
                {"ITEM_TYPE_SIZE",cartItem.ITEM_SIZE},
                {"CART_TIMESTAMP", cartItem.CART_TIMESTAMP},
            };


            _restResponse = await _rest.PostRequest("v1/my-cart", cartPost);

            CartItem cartItemObject = ObjectBuilder.BuildCartItemObject(_restResponse);

            return cartItemObject;

        }

        public async Task<CartItem> UpdateCartItem(CartItem cartItem, int cartItemId)
        {
            Dictionary<string, object> cartPost = new Dictionary<string, object>
            {
                {"USER_ID", cartItem.USER_ID},
                {"ITEM_TYPE_ID",cartItem.ITEM_TYPE_ID},
                {"QUANTITY", cartItem.QUANTITY},
                {"ITEM_PRICE", cartItem.ITEM_PRICE},
                {"ITEM_TYPE_SIZE",cartItem.ITEM_SIZE},
                {"CART_TIMESTAMP", cartItem.CART_TIMESTAMP},
            };


            _restResponse = await _rest.PostRequest("v1/my-cart/"+cartItemId+"/update-cart", cartPost);

            CartItem cartItemObject = ObjectBuilder.BuildCartItemObject(_restResponse);

            return cartItemObject;

        }



        public async Task<CartItem> ItemAlreadyInCart(CartItem queryCartItem)
        {
 
        
            Dictionary<string, object> cartPost = new Dictionary<string, object>
            {
                {"ITEM_TYPE_SIZE",queryCartItem.ITEM_SIZE},
            };

            _restResponse = await _rest.PostRequest("v1/my-cart/" + queryCartItem.ITEM_TYPE_ID + "/in-cart/"+ queryCartItem.USER_ID,cartPost);

            //var j = _restResponse;
            CartItem cartItemObject = ObjectBuilder.BuildCartItemObject(_restResponse);

            return cartItemObject;
        }

        public async Task<List<CartItem>> GetCartItems(int userId)
        {
        
            _restResponse = await _rest.GetRequest("v1/my-cart/" + userId + "/items");
            var cartItemList = ObjectBuilder.BuildCartItemList(_restResponse);

            return cartItemList;
        }

        public async Task<Order> CreateOrderFromCart(Dictionary<string, object> orderDictionary)
        {
            _restResponse = await _rest.PostRequest("v1/my-cart/create-order",orderDictionary);

            var cartItemList = ObjectBuilder.BuildOrderObject(_restResponse);

            return cartItemList;
        }

        public async Task<List<Location>> GetDeliveryLocations()
        {
            _restResponse = await _rest.GetRequest("v1/locations");
            var locationList = ObjectBuilder.BuildLocationList(_restResponse);
            return locationList;
        }

        public async Task<List<DeliveryTime>> GetDeliveryTime()
        {
            _restResponse = await _rest.GetRequest("v1/delivery");
            var deliveryTimeList = ObjectBuilder.BuildDeliveryTimeList(_restResponse);
            return deliveryTimeList;
        }
    }
}