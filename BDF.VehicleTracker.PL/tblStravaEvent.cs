using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDF.VehicleTracker.PL
{
    public class tblStravaEvent
    {
        public Guid Id { get; set; }
        public string AspectType { get; set; }
        public long EventTime { get; set; }
        public long ObjectId { get; set; }
        public string ObjectType { get; set; }
        public long OwnerId { get; set; }
        public long SubscriptionId { get; set; }
    }
}
