using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BDF.VehicleTracker.BL.Test
{
    [TestClass]
    public class utVehicle
    {
        [TestMethod]
        public void LoadTest()
        {
            //var task = VehicleManager.Load();
            //task.Wait();
            //IEnumerable<Models.Vehicle> vehicles = task.Result;
            //Assert.IsNotNull(vehicles);
            //Assert.AreEqual(3, vehicles.ToList().Count);

            Task.Run(async () =>
            {
                var task = await VehicleManager.Load();
                IEnumerable<Models.Vehicle> vehicles = task;
                Assert.AreEqual(2, vehicles.ToList().Count);
            }).GetAwaiter().GetResult();

        }

        [TestMethod]
        public void LoadTestByColorName()
        {
            Task.Run(async () =>
            {
                var task = await VehicleManager.Load("Rebecca");
                IEnumerable<Models.Vehicle> vehicles = task;
                Assert.AreEqual(1, vehicles.ToList().Count);
            }).GetAwaiter().GetResult();

        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                Models.Vehicle vehicle = new Models.Vehicle();
                
                var task1 = await ColorManager.Load();
                IEnumerable<Models.Color> colors = task1;

                var task2 = await ModelManager.Load();
                IEnumerable<Models.Model> models = task2;

                var task3 = await MakeManager.Load();
                IEnumerable<Models.Make> makes = task3;

                vehicle.ColorId = colors.LastOrDefault().Id;
                vehicle.MakeId = makes.LastOrDefault().Id;
                vehicle.ModelId = models.LastOrDefault().Id;
                vehicle.VIN = "NEWVIN";
                vehicle.Year = 1987;

                int results = await VehicleManager.Insert(vehicle, true);
                Assert.IsTrue(results > 0);

            });

        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = await VehicleManager.Load();
                IEnumerable<Models.Vehicle> vehicles = task;
                Models.Vehicle vehicle = vehicles.FirstOrDefault(c => c.Year == 1978);
                vehicle.Year = 1950;
                int results = await VehicleManager.Update(vehicle, true);
                Assert.IsTrue(results > 0);

            });

        }
        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await VehicleManager.Load();
                IEnumerable<Models.Vehicle> vehicles = task;
                Models.Vehicle vehicle = vehicles.FirstOrDefault(c => c.Year == 1978);
                int results = await VehicleManager.Delete(vehicle.Id, true);
                Assert.IsTrue(results > 0);

            });
        }
    }
}
