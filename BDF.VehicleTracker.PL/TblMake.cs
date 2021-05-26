using System;
using System.Collections.Generic;

#nullable disable

namespace BDF.VehicleTracker.PL
{
    public partial class tblMake
    {
        public tblMake()
        {
            tblVehicles = new HashSet<tblVehicle>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<tblVehicle> tblVehicles { get; set; }
    }
}
