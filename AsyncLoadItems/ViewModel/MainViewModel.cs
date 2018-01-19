using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace AsyncLoadItems.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
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
        }

        public ObservableCollection<CarVm> Cars { get; set; }

        public IAsyncCommand StartEngineCommand { get; set; }

        public IAsyncCommand StartEnginesSameTimeCommand { get; set; }

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
    }
}