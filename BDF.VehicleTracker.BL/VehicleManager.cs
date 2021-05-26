using System;
using System.Collections.Generic;
using System.Linq;
using BDF.VehicleTracker.PL;
using BDF.VehicleTracker.BL.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BDF.Utilities.Reporting;

namespace BDF.VehicleTracker.BL
{
    public static class VehicleManager
    {

        public async static Task<int> Insert(Vehicle vehicle, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (VehicleEntities dc = new VehicleEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblVehicle newrow = new tblVehicle();
                    newrow.Id = Guid.NewGuid();
                    newrow.ColorId = vehicle.ColorId;
                    newrow.ModelId = vehicle.ModelId;
                    newrow.MakeId = vehicle.MakeId;
                    newrow.VIN = vehicle.VIN;
                    newrow.Year = vehicle.Year;

                    vehicle.Id = newrow.Id;

                    dc.tblVehicles.Add(newrow);
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

        public async static Task<int> Insert(Guid colorId,
                                             Guid makeId,
                                             Guid modelId,
                                             string vin,
                                             int year,
                                             bool rollback = false)
        {
            try
            {
                Vehicle vehicle = new Vehicle { ColorId = colorId, MakeId = makeId, ModelId = modelId, VIN = vin, Year = year };
                return await Insert(vehicle, rollback);
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

                    tblVehicle row = dc.tblVehicles.FirstOrDefault(c => c.Id == id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        dc.tblVehicles.Remove(row);
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

        public async static Task<int> Update(Vehicle vehicle, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblVehicle row = dc.tblVehicles.FirstOrDefault(c => c.Id == vehicle.Id);
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();
                        row.ColorId = vehicle.ColorId;
                        row.ModelId = vehicle.ModelId;
                        row.MakeId = vehicle.MakeId;
                        row.VIN = vehicle.VIN;
                        row.Year = vehicle.Year;

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

        public static void Export(List<Vehicle> vehicles)
        {
            try
            {
                string[,] data = new string[vehicles.Count + 1, 5];
                int counter = 0;

                data[counter, 0] = "VIN";
                data[counter, 1] = "Model";
                data[counter, 2] = "Make";
                data[counter, 3] = "Color";
                data[counter, 4] = "Year";
                counter++;

                foreach (Vehicle v in vehicles)
                {
                    data[counter, 0] = v.VIN;
                    data[counter, 1] = v.ModelName;
                    data[counter, 2] = v.MakeName;
                    data[counter, 3] = v.ColorName;
                    data[counter, 4] = v.Year.ToString();
                    counter++;
                }

                Excel.Export("Vehicles.xlsx", data);
            }
            catch (Exception ex)
            {

                //throw ex;
            }
        }

        public async static Task<Vehicle> LoadById(Guid id)
        {
            try
            {
                using (VehicleEntities dc = new VehicleEntities())
                {
                    tblVehicle tblVehicle = dc.tblVehicles.Where(c => c.Id == id).FirstOrDefault();
                    Vehicle vehicle = new Vehicle();

                    if (tblVehicle != null)
                    {
                        // Put the table row values into the object.
                        vehicle.Id = tblVehicle.Id;
                        vehicle.ColorId = tblVehicle.ColorId;
                        vehicle.MakeId = tblVehicle.MakeId;
                        vehicle.ModelId = tblVehicle.ModelId;
                        vehicle.VIN = tblVehicle.VIN;
                        vehicle.Year = tblVehicle.Year;
                        vehicle.ColorName = tblVehicle.Color.Description;
                        vehicle.MakeName = tblVehicle.Make.Description;
                        vehicle.ModelName = tblVehicle.Model.Description;
                        return vehicle;
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

        public async static Task<IEnumerable<Vehicle>> Load()
        {
            try
            {
                List<Vehicle> vehicles = new List<Vehicle>();
                using (VehicleEntities dc = new VehicleEntities())
                {
                    dc.tblVehicles
                        .ToList()
                        .ForEach(c => vehicles.Add(new Vehicle
                        {
                            Id = c.Id,
                            ColorId = c.ColorId,
                            MakeId = c.MakeId,
                            ModelId = c.ModelId,
                            VIN = c.VIN,
                            Year = c.Year,
                            ColorName = c.Color.Description,
                            MakeName = c.Make.Description,
                            ModelName = c.Model.Description
                        }));
                }
                return vehicles;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async static Task<IEnumerable<Vehicle>> Load(string colorName)
        {
            try
            {
                List<Vehicle> vehicles = new List<Vehicle>();
                using (VehicleEntities dc = new VehicleEntities())
                {
                    dc.tblVehicles
                        .Where(v => v.Color.Description.Contains(colorName))
                        .ToList()
                        .ForEach(c => vehicles.Add(new Vehicle
                        {
                            Id = c.Id,
                            ColorId = c.ColorId,
                            MakeId = c.MakeId,
                            ModelId = c.ModelId,
                            VIN = c.VIN,
                            Year = c.Year,
                            ColorName = c.Color.Description,
                            MakeName = c.Make.Description,
                            ModelName = c.Model.Description
                        }));
                }
                return vehicles;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
