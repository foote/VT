using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BDF.VehicleTracker.BL.Test
{
    [TestClass]
    public class utMake
    {
        [TestMethod]
        public void LoadTest()
        {
            //var task = MakeManager.Load();
            //task.Wait();
            //IEnumerable<Models.Make> makes = task.Result;
            //Assert.IsNotNull(makes);
            //Assert.AreEqual(3, makes.ToList().Count);

            Task.Run(async () =>
            {
                var task = await MakeManager.Load();
                IEnumerable<Models.Make> makes = task;
                Assert.AreEqual(3, makes.ToList().Count);
            }).GetAwaiter().GetResult();

        }
        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                int results = await MakeManager.Insert(new Models.Make { Description = "NewMake" }, true);
                Assert.IsTrue(results > 0);

            });

        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = await MakeManager.Load();
                IEnumerable<Models.Make> makes = task;
                Models.Make make = makes.FirstOrDefault(c => c.Description == "Ford");
                make.Description = "Updated Make";
                int results = await MakeManager.Update(make, true);
                Assert.IsTrue(results > 0);

            });

        }
        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await MakeManager.Load();
                IEnumerable<Models.Make> makes = task;
                Models.Make make = makes.FirstOrDefault(c => c.Description == "Ford");
                int results = await MakeManager.Delete(make.Id, true);
                Assert.IsTrue(results > 0);

            });
        }
    }
}
