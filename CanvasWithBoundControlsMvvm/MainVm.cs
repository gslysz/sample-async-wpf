using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CanvasWithBoundControlsMvvm
{
    public class MainVm:ObservableObject
    {

        public MainVm()
        {
            Cars = new ObservableCollection<CarVm>
            {
                new CarVm {X = 20, Y = 60},
                new CarVm {X = 20, Y = 160},
                new CarVm {X = 20, Y = 260}
            };

        }

        public ObservableCollection<CarVm> Cars { get; set; }

    }

    public class CarVm:ObservableObject
    {
        private double _y;
        private double _x;

        public double X
        {
            get { return _x; }
            set
            {
                _x = value; 
                RaisePropertyChanged();
            }
        }

        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                RaisePropertyChanged();
            }
        }
    }
}
