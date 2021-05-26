using System;
using System.Collections.Generic;
using System.Linq;
using BDF.VehicleTracker.PL;
using BDF.VehicleTracker.BL.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BDF.VehicleTracker.BL
{
    public static class MakeManager
    {

        public async static Task<int> Insert(Make make, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (VehicleEntities dc = new VehicleEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMake newrow = new tblMake();
                    newrow.Id = Guid.NewGuid();
                    newrow.Description = make.Description;

                    make.Id = newrow.Id;

                    dc.tblMakes.Add(newrow);
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

        public async static Task<int> Insert(int code, string description, bool rollback = false)
        {
            try
            {
                Make make = new Make { Description = description };
                return await Insert(make, rollback);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<int> Delete(Guid id, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (VehicleEntities dc = new VehicleEntities())
                {

                    tblMake row = dc.tblMakes.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblMakes.Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();

                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                    return results;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<int> Update(Make make, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblMake row = dc.tblMakes.FirstOrDefault(c => c.Id == make.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();
                        row.Description = make.Description;

                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();

                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                    return results;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<Make> LoadById(Guid id)
        {
            try
            {
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblMake tblMake = dc.tblMakes.Where(c => c.Id == id).FirstOrDefault();
                    Make make = new Make();

                    if (tblMake != null)
                    {
                        // Put the table row values into the object.
                        make.Id = tblMake.Id;
                        make.Description = tblMake.Description;
                        return make;
                    }
                    else
                    {
                        throw new Exception("Could not find the row");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<IEnumerable<Make>> Load()
        {
            try
            {
                List<Make> makes = new List<Make>();
                using (VehicleEntities dc = new VehicleEntities())
                {
                    dc.tblMakes
                        .ToList()
                        .ForEach(c => makes.Add(new Make
                        {
                            Id = c.Id,
                            Description = c.Description
                        }));
                }
                return makes;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
