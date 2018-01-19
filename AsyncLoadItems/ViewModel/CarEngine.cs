using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace AsyncLoadItems.ViewModel
{
    public class CarEngine : ObservableObject
    {
        private bool _hasTurbo;
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

        public EngineResult PullEngine(int pullEngineTime)
        {
            Thread.Sleep(pullEngineTime);
            return new EngineResult { Engine = this, Status = "Pulled" };
        }


        public void UpgradeEngine(int upgradeEngineTime)
        {
            Thread.Sleep(upgradeEngineTime);
            HasTurbo = true;

        }

        public bool HasTurbo
        {
            get { return _hasTurbo; }
            set
            {
                _hasTurbo = true;
                RaisePropertyChanged();
            }
        }
    }
}
