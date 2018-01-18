using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RestService.models
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class TimeLine
    {
        private int TRACKING_ID { get; set; }
        private int ORDER_ID { get; set; }
        private String COMMENTS { get; set; }
        private String STATUS { get; set; }
        private String TRACKING_DATE { get; set; }
        private bool USER_VISIBLE { get; set; }
    }
}
