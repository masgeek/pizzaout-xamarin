using System;
using System.Collections.Generic;
using System.Text;

namespace RestService.models
{

    public class Rider
    {
        public int RIDER_ID { get; set; }

        public int ASSIGNED_KITCHEN_ID { get; set; }

        public String RIDER_NAME { get; set; }
        public String RIDER_MOBILE { get; set; }

        public bool RIDER_STATUS { get; set; }
    }
}
