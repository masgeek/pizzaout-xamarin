using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RestService.models
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
        public int USER_ID { get; set; }
        public int ACCOUNT_STATUS { get; set; }

        public int USER_TYPE { get; set; }
        public int LOCATION_ID { get; set; }

        public String SURNAME { get; set; }
        public String OTHER_NAMES { get; set; }
        public String EMAIL { get; set; }
        public String MOBILE_NO { get; set; }
        public String USERNAME { get; set; }
        public String ADDRESS { get; set; }

        public String RESET_TOKEN { get; set; }
        public String PASSWORD { get; set; }
    }
}
