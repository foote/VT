using BDF.VehicleTracker.BL.Models;
using BDF.VehicleTracker.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDF.VehicleTracker.BL
{
    public static class StravaManager
    {
        public async static Task<int> Insert(StravaEvent stravaNewEvent, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (VehicleEntities dc = new VehicleEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblStravaEvent newrow = new tblStravaEvent();
                    newrow.Id = Guid.NewGuid();
                    newrow.AspectType = stravaNewEvent.aspect_type;
                    newrow.EventTime = stravaNewEvent.event_time;
                    newrow.ObjectId = stravaNewEvent.object_id;
                    newrow.ObjectType = stravaNewEvent.object_type;
                    newrow.OwnerId = stravaNewEvent.owner_id;
                    newrow.SubscriptionId = stravaNewEvent.subscription_id;
                    
                    dc.tblStravaEvents.Add(newrow);
                    int results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return results;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
