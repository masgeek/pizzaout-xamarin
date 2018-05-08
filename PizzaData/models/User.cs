using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PizzaData.models
{
    /// <summary>
 /*        "API_TOKEN": "VzVNfyVIurSz1tRCM7CrBptixeLPXClp",
"CLIENT_TOKEN": null,
"DATE_REGISTERED": "2017-10-09 07:06:51",
"EMAIL": "petchaloo@gmail.com345",
"LAST_UPDATED": "2018-01-16 11:54:33",
"LOCATION_ID": "1",
"MOBILE": "72480222034535",
"OTHER_NAMES": "PETER KYALO345345",
"RESET_TOKEN": "NONE",
"RIDER_ID": 0,
"SURNAME": "KINGOO B345",
"TOKEN_EXPPIRY": null,
"USER_ID": "5",
"USER_NAME": "pkyalo",
"USER_STATUS": true,
"USER_TYPE": "ADMIN"*/

    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class User
    {
        public enum ACCOUNT_STATUS
        {
            DEACTIVATED = 0,
            ACTIVE = 1,
        };

        public int USER_ID { get; set; }

        public int USER_STATUS { get; set; }

        public string USER_TYPE { get; set; }
        public string LOCATION_ID { get; set; }

        public string SURNAME { get; set; }
        public string OTHER_NAMES { get; set; }

        public string EMAIL { get; set; }
        public string MOBILE_NO { get; set; }
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string ADDRESS { get; set; }
        
        public string RESET_TOKEN { get; set; }

        public bool HAS_ERRORS { get; set; }

        public List<ErrorModel> ERROR_LIST { get; set; }


        public List<ErrorModel> GetErrors(JArray errorsJArray)
        {
            List<ErrorModel> items = null;
            if (errorsJArray != null)
            {
                items = (errorsJArray).Select(x => new ErrorModel
                {
                    field = (string)x["field"],
                    message = (string)x["message"],
      
                }).ToList();
            }

            return items;
        }
    }
}
