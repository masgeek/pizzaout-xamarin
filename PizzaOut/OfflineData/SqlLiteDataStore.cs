using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace PizzaOut.OfflineData
{
    public class SqlLiteDataStore
    {
        private static SqlLiteDataStore offlineStorage;
        private static object myObject = new object();

        public static string DatabaseFilePath
        {
            get
            {
                const string dbName = "db_pizzaout.db";
                var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var dbPath = Path.Combine(documents, dbName);
                return dbPath;
            }
        }
    }
}