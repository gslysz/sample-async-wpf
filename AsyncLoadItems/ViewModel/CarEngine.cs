using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncLoadItems.ViewModel
{
    public class CarEngine
    {
        public int NumCCs { get; set; }

        public int NumCylinders { get; set; }

        public async Task InitializeEngine(int engineStartTime)
        {
            //any long running task
            await Task.Run(() =>
            {
                Thread.Sleep(engineStartTime);
            });

        }
    }
}
