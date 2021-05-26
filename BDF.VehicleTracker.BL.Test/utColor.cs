using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BDF.VehicleTracker.BL.Test
{
    [TestClass]
    public class utColor
    {
        [TestMethod]
        public void LoadTest()
        {
            //var task = ColorManager.Load();
            //task.Wait();
            //IEnumerable<Models.Color> colors = task.Result;
            //Assert.IsNotNull(colors);
            //Assert.AreEqual(3, colors.ToList().Count);
            
            Task.Run(async () =>
            {
                var task = await ColorManager.Load();
                IEnumerable<Models.Color> colors = task;
                Assert.AreEqual(3, colors.ToList().Count);
            }).GetAwaiter().GetResult();
            
        }
        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                int results = await ColorManager.Insert(new Models.Color { Code = -99, Description = "NewColor" }, true);
                Assert.IsTrue(results > 0);

            });

        }

        [TestMethod]
        public void UpdateTest()
        {
           
            var task = ColorManager.Load();
            IEnumerable<Models.Color> colors = task.Result;
            task.Wait();
            Models.Color color = colors.FirstOrDefault(c => c.Code == 6591981);
            color.Description = "Updated Color";
            var results = ColorManager.Update(color, true);
            Assert.IsTrue(results.Result > 0);

        }
        [TestMethod]
        public void DeleteTest()
        {
            //Task.Run(async () =>
            //{
            //    var task = await ColorManager.Load();
            //    IEnumerable<Models.Color> colors = task;
            //    Models.Color color = colors.FirstOrDefault(c => c.Code == 6591981);
            //    int results = await ColorManager.Delete(color.Id, true);
            //    Assert.IsTrue(results > 0);

            //});

            var task = ColorManager.Load();
            IEnumerable<Models.Color> colors = task.Result;
            task.Wait();
            Models.Color color = colors.FirstOrDefault(c => c.Code == 6591981);
            var results = ColorManager.Delete(color.Id, true);
            Assert.IsTrue(results.Result > 0);



        }
    }
}
