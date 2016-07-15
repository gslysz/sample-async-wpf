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
            mainVm.StartEngineCommand.Execute(null);

            //We need to sleep to ensure the cars are initialized
            Thread.Sleep(4000);
            Assert.IsTrue(mainVm.Cars[0].IsEngineStarted);

            //NOTE: this next assert fails because not enough time was given to the Thread.Sleep. This highlights the problem of Thread.Sleep, in
            //that certain tasks might take more time than others on different computers and it is tricky to get the tests setup to ensure
            //they pass every time. Usually you would have to make it Sleep much more than you have too, but then that might affect something else.
            Assert.IsTrue(mainVm.Cars[1].IsEngineStarted);
            Assert.IsTrue(mainVm.Cars[2].IsEngineStarted);
        }



        [TestMethod]
        public async Task UseWithAsyncCommand1()
        {
            //NOTE: this is the better way to do it.

            MainViewModel mainVm = new MainViewModel();
            await mainVm.StartEngineCommand.ExecuteAsync(null);
            Assert.IsTrue(mainVm.Cars[0].IsEngineStarted);
            Assert.IsTrue(mainVm.Cars[1].IsEngineStarted);
            Assert.IsTrue(mainVm.Cars[2].IsEngineStarted);
        }


    }
}
