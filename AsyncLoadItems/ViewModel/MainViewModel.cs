using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace AsyncLoadItems.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Constructors And Destructors

        public MainViewModel()
        {
            Cars = new ObservableCollection<CarVm>();

            var car1 = CreateCar("Chevy", "Chevette");
            var car2 = CreateCar("Ford", "F750");
            var car3 = CreateCar("El", "Camino");

            Cars.Add(car1);
            Cars.Add(car2);
            Cars.Add(car3);

            StartEngineCommand = AsyncCommand.Create(InitializeCarsWithSeparateTasks);

            StartEnginesSameTimeCommand = AsyncCommand.Create(InitializeCarsMultipleTasks);

            UpgradeCarsAsyncCommand = AsyncCommand.Create(UpgradeCarsAsync);

            UpgradeCarsCommand = new RelayCommand(UpgradeCars);

            var carsForUpgrading = CreateLotsOfCars(8);
            CarsForUpgrading = new ObservableCollection<CarVm>(carsForUpgrading);
        }

        #endregion

        #region Commands

        public IAsyncCommand StartEngineCommand { get; set; }

        public IAsyncCommand StartEnginesSameTimeCommand { get; set; }

        public IAsyncCommand UpgradeCarsAsyncCommand { get; set; }

        public ICommand UpgradeCarsCommand { get; set; }

        #endregion

        #region Properties

        public ObservableCollection<CarVm> Cars { get; set; }

        public ObservableCollection<CarVm> CarsForUpgrading
        {
            get => _carsForUpgrading;
            set
            {
                _carsForUpgrading = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Non-Public Methods

        private List<CarVm> CreateLotsOfCars(int numCarsToCreate)
        {
            var cars = new List<CarVm>();

            for (var i = 0; i < numCarsToCreate; i++)
            {
                var car = CreateCar(i);
                cars.Add(car);
            }

            return cars;
        }

        private CarVm CreateCar(int i)
        {
            return new CarVm {Make = $"Car{i}"};
        }


        private void UpgradeCars()
        {
            var carsForUpgrading = CreateLotsOfCars(8);
            CarsForUpgrading = new ObservableCollection<CarVm>(carsForUpgrading);

            UpgradeCarsWithBlockingCollection();
        }


        private async Task UpgradeCarsAsync()
        {
            var carsForUpgrading = CreateLotsOfCars(8);
            CarsForUpgrading = new ObservableCollection<CarVm>(carsForUpgrading);

            await Task.Run(() => { UpgradeCarsWithBlockingCollection(); });
        }

        private void UpgradeCarsWithBlockingCollection()
        {
            //We have two steps of processing that need to be done. 1) pull out engine  then   2) Upgrade engine   (ignore reinstalling engine!)

            //We want to do it as fast as possible

            //So if we pull an engine, can start upgrading it
            //While that engine is being upgraded, can pull another, but the second one cannot be upgraded until the engine before it is done.

            var boundedCapacity =
                3; //if the bounded capacity is reached, further items are not added until they are taken off by the consumer
            using (var blockingCollection = new BlockingCollection<EngineResult>(boundedCapacity))
            {
                //This is the producer
                using (var t1 = Task.Run(() =>
                    {
                        foreach (var car in CarsForUpgrading)
                        {
                            var pullEngineTime = 1000; //can play with this 
                            var result = car.PullEngine(pullEngineTime);

                            blockingCollection.Add(result);
                        }

                        blockingCollection.CompleteAdding();
                    }))

                    //This is the consumer.  It will stay on a thread and will constantly monitor the blockingCollection until 
                    //'blockingCollection.IsAddingCompleted' is set to true (using the 'CompleteAdding' method)  
                {
                    foreach (var engineResult in blockingCollection.GetConsumingEnumerable())
                    {
                        var upgradeTime = 3000;
                        engineResult.Upgrade(upgradeTime);
                    }
                }
            }
        }


        private async Task InitializeCarsWithSeparateTasks()
        {
            await Task.Run(async () =>
            {
                await Cars[0].StartEngine(2000);
                await Cars[1].StartEngine(1000);
                await Cars[2].StartEngine(3000);
            });
        }


        private async Task InitializeCarsMultipleTasks()
        {
            var task1 = Cars[0].StartEngine(2000);
            var task2 = Cars[1].StartEngine(1000);
            var task3 = Cars[2].StartEngine(3000);

            var tasks = new List<Task> {task1, task2, task3};

            await Task.WhenAll(tasks);
        }


        private CarVm CreateCar(string make, string model)
        {
            var car = new CarVm
            {
                Make = make,
                Model = model
            };

            return car;
        }

        #endregion

        #region Fields

        private ObservableCollection<CarVm> _carsForUpgrading;
        private Timer _upgradeCarsTimer;

        #endregion
    }
}