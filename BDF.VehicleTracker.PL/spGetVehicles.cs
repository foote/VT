using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDF.VehicleTracker.PL
{
    public class spGetVehiclesResult
    {
       
        public Guid Id { get; set; }
        public Guid ColorId { get; set; }
        public Guid MakeId { get; set; }
        public Guid ModelId { get; set; }

        public int Year { get; set; }

        public string VIN { get; set; }
       
        public string ColorName { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
    }
}
