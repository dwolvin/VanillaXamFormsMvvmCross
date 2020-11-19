using System;
using System.Threading.Tasks;
using MvvmCross.Logging;
using NUnit.Framework;
using vanilla.Core.Services;

namespace UnitTest
{
    public class TaskTests : MvvmCross.Tests.MvxIoCSupportingTest
    {
        protected override void AdditionalSetup()
        {
            Ioc.RegisterSingleton<IMvxLogProvider>(base.CreateLogProvider());            
        }

        [Test]
        public void LongTaskCompletes() {

            base.Setup();

            var service = Ioc.IoCConstruct<SimpleService>();

            Task task = Task.Run(() => service.SleepTask());
            task.Wait();

            Assert.IsTrue(task.IsCompleted);
            Assert.IsTrue(task.IsCompletedSuccessfully);
            Assert.IsFalse(task.IsFaulted);
        }

        [Test]
        public void ThrowingTaskThrows() {
            base.Setup();
            var service = Ioc.IoCConstruct<SimpleService>();

            Task task = Task.Run(() => service.BoomTask());

            //Keep the test alive with try/catch
            try
            {
                task.Wait();
            }
            catch (Exception ex) {

                // I knew this would happen.
            }

            Assert.IsTrue(task.IsCompleted);
            Assert.IsFalse(task.IsCompletedSuccessfully);
            Assert.IsTrue(task.IsFaulted);
        }

        [Test]
        public void TaskReturnsObject()
        {
            base.Setup();

            var service = Ioc.IoCConstruct<SimpleService>();

            var task = Task.Run(() => service.CreateStation(10.00f, 20.00f, "TestName", "TestCode"));
            task.Wait();

            var station = task.Result;

            Assert.IsNotNull(station);
            Assert.AreEqual(10.00f, station.Lat);
            Assert.AreEqual(20.00f, station.Lon);
            Assert.AreEqual("TestCode", station.StationCode);
            Assert.IsTrue(station.DateCreated > DateTime.Now.AddMinutes(-1));

        }

        [Test]
        public async Task TaskReturnsObjectAsync()
        {
            base.Setup();

            var service = Ioc.IoCConstruct<SimpleService>();

            var station =  await service.CreateStation(10.00f, 20.00f, "TestName", "TestCode");

            Assert.IsNotNull(station);
            Assert.AreEqual(10.00f, station.Lat);
            Assert.AreEqual(20.00f, station.Lon);
            Assert.AreEqual("TestCode", station.StationCode);
            Assert.IsTrue(station.DateCreated > DateTime.Now.AddMinutes(-1));

        }


    }
}