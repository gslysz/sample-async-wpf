using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GwsUtils;

namespace DragAndDropToWindow.ViewModel
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
            Persons = new ObservableCollection<PersonVm>();

            AddPerson("B", "Bubba");
            AddPerson("D", "Dootlyee");
            AddPerson("Big", "Bertha");
            AddPerson("H", "Hoojigotadudu");

      MyDropHandler= new CustomDropHandler();

        }

        private void AddPerson(string fname, string lname)
        {
            PersonVm person=new PersonVm(fname,lname);
            person.NotifyPersonMoveHandler += OnPersonMove;

            Persons.Add(person);
        }

        public EventHandler<PersonVm> NotifyOpenNewWindow;

        private void OnPersonMove(object sender, PersonVm personVm)
        {   
            DebugUtils.Write("OnPersonMove fired");

            var handler = NotifyOpenNewWindow;
            handler?.Invoke(this, personVm);


        }


        public ObservableCollection<PersonVm> Persons { get; set; }

        public CustomDropHandler MyDropHandler { get; set; }
    }
}