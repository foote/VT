using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDF.VehicleTracker.BL.Models
{
    public class Map
    {
        public string id { get; set; }
        public string summary_polyline { get; set; }
        public int resource_state { get; set; }
        public IEnumerable<CoordinateEntity> points { get; set; }
    }
}
