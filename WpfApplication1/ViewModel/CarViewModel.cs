using System.Collections.ObjectModel;

namespace WpfApplication1.ViewModel
{
    public class CarViewModel : BaseViewModel
    {
        private CarMake _make;
        private string _model;
        private int _year;
        private string _owner;

        #region Public Properties

        public string Owner
        {
            get { return _owner; }
            set
            {
                _owner = value; 
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<CarMake> AvailTypes { get; set; } 


        public CarMake Make
        {
            get { return _make; }
            set
            {
                _make = value;
                RaisePropertyChanged();
            }
        }
        
        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}