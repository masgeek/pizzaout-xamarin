using System;
using System.Text;

namespace PizzaData.models
{
    public class ItemSize
    {
        public int ITEM_TYPE_ID { get; set; }

        public int MENU_ITEM_ID { get; set; }

        public string ITEM_TYPE_SIZE { get; set; }

        public double PRICE { get; set; }

        public string AVAILABLE { get; set; }

        public bool IsItemAvailable()
        {
            bool itemAvailable = false;
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(this.AVAILABLE);
                itemAvailable = Convert.ToBoolean(bytes[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return itemAvailable;
        }

    }
}
