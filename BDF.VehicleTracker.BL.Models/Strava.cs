using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDF.VehicleTracker.BL.Models
{
    //public class StravaSubscriptionValidation
    //{
    //    //public string verify_token { get; set; }
    //    public string hub_challenge { get; set; }
    //    //public string mode { get; set; }
    //}

    public class StravaEvent
    {
        public string aspect_type { get; set; }
        public long event_time { get; set; }
        public long object_id { get; set; }
        public string object_type { get; set; }
        public long owner_id { get; set; }
        public long subscription_id { get; set; }
        //public string[] updates { get; set; }
    }
}
