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
    public static class ColorManager
    {

        public async static Task<int> Insert(Color color, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (VehicleEntities dc = new VehicleEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblColor newrow = new tblColor();
                    newrow.Id = Guid.NewGuid();
                    newrow.Code = color.Code;
                    newrow.Description = color.Description;

                    color.Id = newrow.Id;

                    dc.tblColors.Add(newrow);
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
                Color color = new Color { Code = code, Description = description };
                return await Insert(color, rollback);
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

                    tblColor row = dc.tblColors.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblColors.Remove(row);
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

        public async static Task<int> Update(Color color, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblColor row = dc.tblColors.FirstOrDefault(c => c.Id == color.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();
                        row.Code = color.Code;
                        row.Description = color.Description;

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

        public async static Task<Color> LoadById(Guid id)
        {
            try
            {
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblColor tblColor = dc.tblColors.Where(c => c.Id == id).FirstOrDefault();
                    Color color = new Color();

                    if (tblColor != null)
                    {
                        // Put the table row values into the object.
                        color.Id = tblColor.Id;
                        color.Code = tblColor.Code;
                        color.Description = tblColor.Description;
                        return color;
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

        public async static Task<IEnumerable<Color>> Load()
        {
            try
            {
                List<Color> colors = new List<Color>();
                using (VehicleEntities dc = new VehicleEntities())
                {
                    dc.tblColors
                        .ToList()
                        .ForEach(c => colors.Add(new Color
                        {
                            Id = c.Id,
                            Code = c.Code,
                            Description = c.Description
                        }));
                }
                return colors;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
