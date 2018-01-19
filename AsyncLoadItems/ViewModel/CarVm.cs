using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;


namespace AsyncLoadItems.ViewModel
{
    public class CarVm : ObservableObject
    {
        private string _feedBack;
        private CarEngine _engine;
        private string _model;
        private string _make;
        private bool _isEngineStarting;
        private bool _isEngineStarted;

        public CarVm()
        {
            Engine = new CarEngine { NumCCs = 3800, NumCylinders = 6 };
        }


        public string Make
        {
            get { return _make; }
            set
            {
                _make = value;
                RaisePropertyChanged();
            }
        }

        public string Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChanged();
            }
        }

        public CarEngine Engine
        {
            get { return _engine; }
            set
            {
                _engine = value;
                RaisePropertyChanged();
            }
        }

        public bool IsEngineStarting
        {
            get { return _isEngineStarting; }
            set
            {
                _isEngineStarting = value;
                RaisePropertyChanged();
            }
        }

        public bool IsEngineStarted
        {
            get { return _isEngineStarted; }
            set
            {
                _isEngineStarted = value;
                RaisePropertyChanged();
            }
        }


        public string FeedBack
        {
            get { return _feedBack; }
            set
            {
                _feedBack = value;
                RaisePropertyChanged();
            }
        }

        public async Task StartEngine(int engineStartTime = 2000)
        {
            Engine = new CarEngine { NumCCs = 3800, NumCylinders = 6 };

            IsEngineStarting = true;
            FeedBack = "Starting engine...";
            Task task = Engine.InitializeEngine(engineStartTime);
            await task;

            IsEngineStarting = false;
            IsEngineStarted = true;
            FeedBack = "Bruuuuum!";
        }


        public EngineResult PullEngine(int pullEngineTime = 1000)
        {
            var result=  Engine.PullEngine(pullEngineTime);
            FeedBack = "Engine Pulled ";
            return result;

        }


        //public async Task UpgradeEngineTask(int upgradeEngineTime = 3000)
        //{
        //    FeedBack = "Upgrading engine...";
        //    Task task = Engine.UpgradeEngine(upgradeEngineTime);
        //    await task;


        //    FeedBack = "Upgraded engine";
        //}


    }

    public class EngineResult
    {
        public CarEngine Engine { get; set; }

        public string Status { get; set; }

        public void Upgrade(int upgradeTime= 2000)
        {
            Engine.UpgradeEngine(upgradeTime);
            Console.WriteLine($"Engine upgraded!");
        }
    }
}
