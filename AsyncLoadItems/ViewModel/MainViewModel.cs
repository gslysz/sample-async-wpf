using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace AsyncLoadItems.ViewModel
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
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}


            Cars = new ObservableCollection<CarVm>();

            InitializeCars();

        }

        private void InitializeCars()
        {
            var car1 = CreateCar("Nissan", "Sentra");
            var car2 = CreateCar("Honda", "Oddysey");
            var car3 = CreateCar("Ford", "Pinto");

            Cars.Add(car1);
            Cars.Add(car2);
            Cars.Add(car3);

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

        public ObservableCollection<CarVm> Cars { get; set; }
    }
}