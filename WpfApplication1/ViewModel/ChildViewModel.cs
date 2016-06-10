using System;

namespace WpfApplication1.ViewModel
{
    public class ChildViewModel : BaseViewModel
    {
        private string _name;

        #region Public Properties

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;

                RaisePropertyChanged();
                if (_name.Contains("0"))
                {
                    RaiseErrorNotice(new ApplicationException("No '0' allowed, dummy"));
                    
                }
            }
        }

        #endregion
    }
}