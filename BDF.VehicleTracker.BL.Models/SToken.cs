﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDF.VehicleTracker.BL.Models
{
    public class SToken
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
        public string expires_at { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
    }
}