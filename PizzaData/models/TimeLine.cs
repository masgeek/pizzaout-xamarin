using System;
using System.Diagnostics.CodeAnalysis;

namespace PizzaData.models
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class TimeLine
    {
        public int TRACKING_ID { get; set; }
        public int ORDER_ID { get; set; }
        public String COMMENTS { get; set; }
        public String STATUS { get; set; }
        public String TRACKING_DATE { get; set; }
        public bool USER_VISIBLE { get; set; }
    }
}
