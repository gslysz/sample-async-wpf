using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApplication1.ViewModel;

namespace SampleAsyncWpfApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            var rect = Rect.Empty;

            Console.WriteLine(rect.Top);


        }
    }
}
