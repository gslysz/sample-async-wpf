using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using GalaSoft.MvvmLight;

namespace AsyncLoadItems.ViewModel
{
    public class UpgradingCarController:ObservableObject
    {
        private BlockingCollection<EngineResult> _blockingCollection;
        private Timer _timer;
        private int _carCounter;

        public UpgradingCarController()
        {
            Cars = new ObservableCollection<CarVm>();

            _blockingCollection = new BlockingCollection<EngineResult>();


            _timer = new Timer(1000);
            _timer.Elapsed += TimerOnElapsed;

            StartMonitoringBlockingCollection();
        }

        private void StartMonitoringBlockingCollection()
        {
            {
                foreach (var engineResult in _blockingCollection.GetConsumingEnumerable())
                {
                    var upgradeTime = 3000;
                    engineResult.Upgrade(upgradeTime);
                }
            }
        }

        public ObservableCollection<CarVm> Cars { get; set; }



        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            //create a new car
            CarVm car = new CarVm();
            car.Make = $"{_carCounter++}";

            Cars.Add(car);
            
            //pull engine
            var engineResult=  car.PullEngine(1000);
            _blockingCollection.Add(engineResult);
        }
    }
}
