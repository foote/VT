using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDF.VehicleTracker.PL
{
    public class tblLeaderboard
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime GameTime { get; set; }
        public int Score { get; set; }
    }
}
