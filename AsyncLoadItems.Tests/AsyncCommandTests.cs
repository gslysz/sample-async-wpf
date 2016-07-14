using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsyncLoadItems.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AsyncLoadItems.Tests
{
    [TestClass]
    public class AsyncCommandTests
    {
        [TestMethod]
        public void UseThreadSleepWithAsyncCode()
        {
            //NOTE: this is the bad way to do it!

            MainViewModel mainVm = new MainViewModel();
            mainVm.DoInitializationCommand.Execute(null);

            //We need to sleep to ensure the cars are initialized
            Thread.Sleep(4000);
            Assert.IsTrue(mainVm.Cars.First().IsInitialized);
        }



        [TestMethod]
        public async Task UseWithAsyncCommand1()
        {
            //NOTE: this is the better way to do it.

            MainViewModel mainVm = new MainViewModel();
            await mainVm.DoInitializationCommand.ExecuteAsync(null);
            Assert.IsTrue(mainVm.Cars.First().IsInitialized);
        }


    }
}
