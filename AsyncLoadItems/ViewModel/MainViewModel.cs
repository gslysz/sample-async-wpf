using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace AsyncLoadItems.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    ///     </para>
    ///     <para>
    ///         You can also use Blend to data bind with the tool's support.
    ///     </para>
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}


            Cars = new ObservableCollection<CarVm>();
            InitializeCarsMultipleTasks();

            InitializeCarsWithSeparateTasks();
        }

        public ObservableCollection<CarVm> Cars { get; set; }

        private async void InitializeCarsWithSeparateTasks()
        {
            var car1 = CreateCar("Chevy", "Chevette");
            var car2 = CreateCar("Ford", "F750");
            var car3 = CreateCar("El", "Camino");

            Cars.Add(car1);
            Cars.Add(car2);
            Cars.Add(car3);

            await car1.InitializeEngine();
            await car2.InitializeEngine();
            await car3.InitializeEngine();
        }


        private async void InitializeCarsMultipleTasks()
        {
            var car1 = CreateCar("Nissan", "Sentra");
            var car2 = CreateCar("Honda", "Odyssey");
            var car3 = CreateCar("Ford", "Pinto");

            Cars.Add(car1);
            Cars.Add(car2);
            Cars.Add(car3);

            var task1 = car1.InitializeEngine();
            var task2 = car2.InitializeEngine();
            var task3 = car3.InitializeEngine();

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