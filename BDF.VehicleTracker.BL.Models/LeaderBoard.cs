using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDF.VehicleTracker.BL.Models
{
    public class Leaderboard
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime GameTime { get; set; }
        public int Score { get; set; }
    }
}
