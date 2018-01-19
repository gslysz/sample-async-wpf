using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace AsyncProcessItems1.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {

                // Spin up a Task to populate the BlockingCollection 
                using (Task t1 = Task.Factory.StartNew(() =>
                {
                    bc.Add(1);
                    bc.Add(2);
                    bc.Add(3);
                    bc.CompleteAdding();
                }))
                {

                    // Spin up a Task to consume the BlockingCollection
                    using (Task t2 = Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            // Consume consume the BlockingCollection
                            while (true) Console.WriteLine(bc.Take());
                        }
                        catch (InvalidOperationException)
                        {
                            // An InvalidOperationException means that Take() was called on a completed collection
                            Console.WriteLine("That's All!");
                        }
                    }))

                        Task.WaitAll(t1, t2);
                }
            }
        }
    }
}