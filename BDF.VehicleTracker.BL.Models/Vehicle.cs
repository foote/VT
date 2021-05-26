using System;
using System.Collections.Generic;
using System.Text;

namespace BDF.VehicleTracker.BL.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public Guid ColorId { get; set; }
        public Guid MakeId { get; set; }
        public Guid ModelId { get; set; }

        public string VIN { get; set; }
        public int Year { get; set; }

        /// <summary>
        /// Vehicle Color Name
        /// </summary>
        public string ColorName { get; set; }
        /// <summary>
        /// Vehicle Make
        /// </summary>
        public string MakeName { get; set; }
        /// <summary>
        /// Vehicle Model
        /// </summary>
        public string ModelName { get; set; }

    }
}
