using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace DragAndDropToWindow.ViewModel
{
    public class PersonVm:ViewModelBase
    {
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged();
            }
        }

        public PersonVm(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

        }


        public EventHandler<PersonVm> NotifyPersonMoveHandler;
    }
}
