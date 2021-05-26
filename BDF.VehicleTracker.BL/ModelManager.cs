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
    public static class ModelManager
    {

        public async static Task<int> Insert(Model model, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (VehicleEntities dc = new VehicleEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblModel newrow = new tblModel();
                    newrow.Id = Guid.NewGuid();
                    newrow.Description = model.Description;

                    model.Id = newrow.Id;

                    dc.tblModels.Add(newrow);
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
                Model model = new Model { Description = description };
                return await Insert(model, rollback);
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

                    tblModel row = dc.tblModels.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblModels.Remove(row);
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

        public async static Task<int> Update(Model model, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblModel row = dc.tblModels.FirstOrDefault(c => c.Id == model.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();
                        row.Description = model.Description;

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

        public async static Task<Model> LoadById(Guid id)
        {
            try
            {
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblModel tblModel = dc.tblModels.Where(c => c.Id == id).FirstOrDefault();
                    Model model = new Model();

                    if (tblModel != null)
                    {
                        // Put the table row values into the object.
                        model.Id = tblModel.Id;
                        model.Description = tblModel.Description;
                        return model;
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

        public async static Task<IEnumerable<Model>> Load()
        {
            try
            {
                List<Model> models = new List<Model>();
                using (VehicleEntities dc = new VehicleEntities())
                {
                    dc.tblModels
                        .ToList()
                        .ForEach(c => models.Add(new Model
                        {
                            Id = c.Id,
                            Description = c.Description
                        }));
                }
                return models;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
