using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BDF.VehicleTracker.BL.Test
{
    [TestClass]
    public class utModel
    {
        [TestMethod]
        public void LoadTest()
        {
            //var task = ModelManager.Load();
            //task.Wait();
            //IEnumerable<Models.Model> models = task.Result;
            //Assert.IsNotNull(models);
            //Assert.AreEqual(3, models.ToList().Count);

            Task.Run(async () =>
            {
                var task = await ModelManager.Load();
                IEnumerable<Models.Model> models = task;
                Assert.AreEqual(3, models.ToList().Count);
            }).GetAwaiter().GetResult();

        }
        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                int results = await ModelManager.Insert(new Models.Model {Description = "NewModel" }, true);
                Assert.IsTrue(results > 0);

            });

        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = await ModelManager.Load();
                IEnumerable<Models.Model> models = task;
                Models.Model model = models.FirstOrDefault(c => c.Description == "Mustang");
                model.Description = "Updated Model";
                int results = await ModelManager.Update(model, true);
                Assert.IsTrue(results > 0);

            });

        }
        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await ModelManager.Load();
                IEnumerable<Models.Model> models = task;
                Models.Model model = models.FirstOrDefault(c => c.Description == "Mustang");
                int results = await ModelManager.Delete(model.Id, true);
                Assert.IsTrue(results > 0);

            });
        }
    }
}
