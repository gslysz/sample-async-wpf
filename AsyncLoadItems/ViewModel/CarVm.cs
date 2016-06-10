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
        private bool _isInitializing;
        private bool _isInitialized;


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

        public bool IsInitializing
        {
            get { return _isInitializing; }
            set
            {
                _isInitializing = value;
                RaisePropertyChanged();
            }
        }

        public bool IsInitialized
        {
            get { return _isInitialized; }
            set
            {
                _isInitialized = value;
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
    }
}
