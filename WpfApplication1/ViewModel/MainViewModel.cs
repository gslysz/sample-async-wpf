using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace WpfApplication1.ViewModel
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
    public class MainViewModel : BaseViewModel
    {
        private CancellationTokenSource _cancellationTokenSource;
        private ChildViewModel _child;
        private ProgressReport _currentProgressReport;
        private List<int> _randomNumList;

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Messages = new ObservableCollection<string>();

            DoItCommand = new RelayCommand(DoIt);
            DontDoItCommand = new RelayCommand(DontDoIt);
            ProcessSomeDataCommand = new RelayCommand(ProcessSomeData);
            CancelProcessingDataCommand = new RelayCommand(CancelProcessing);

            AvailableCarMakes = new ObservableCollection<CarMake>(Enum.GetValues(typeof (CarMake)).Cast<CarMake>());


            Cars = new ObservableCollection<CarViewModel>
            {
                new CarViewModel{Owner = "Big G",Make= CarMake.Nissan,Year = 2001},
                new CarViewModel{Owner = "Mimer",Make= CarMake.Honda,Year = 2015},
                
            };


            Child = new ChildViewModel();
        }

        #region Public Properties

        public ObservableCollection<CarMake> AvailableCarMakes { get; set; }


        public ObservableCollection<CarViewModel> Cars { get; set; } 

        public ChildViewModel Child
        {
            get { return _child; }
            set
            {
                _child = value;
                RaisePropertyChanged();
            }
        }


        public ProgressReport CurrentProgressReport
        {
            get { return _currentProgressReport; }
            set
            {
                _currentProgressReport = value;
                RaisePropertyChanged();
            }
        }


        public ObservableCollection<string> Messages { get; set; }

        #endregion

        #region ICommand Properties

        public ICommand CancelProcessingDataCommand { get; set; }

        public ICommand DoItCommand { get; set; }

        public ICommand DontDoItCommand { get; set; }
        public ICommand ProcessSomeDataCommand { get; set; }

        #endregion

        #region Public Methods

        public async Task<string> DoLongTaskAsync()
        {
            double val = 0;

            await Task.Run(() =>
            {
                for (int i = 1; i < 1000000; i++)
                {
                    val = Math.Sqrt(i);
                }

                Thread.Sleep(3000);
            });

            return string.Format("Ok... LongTask is finally done. Val= {0}", val);
        }

        #endregion

        #region Private Methods

        private void CancelProcessing()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        private async void DoIt()
        {
            Messages.Add("Started long task");

            Task<string> t = DoLongTaskAsync();

            Messages.Add("... moving on to other things...");

            string messageFromTask = await t;
            Messages.Add(messageFromTask);

            Messages.Add("This is another message");
        }

        private async Task DoProcessingTask(CancellationToken cancellationToken,
            IProgress<ProgressReport> progress = null)
        {
            int numRandoms = 100000;
            var generator = new Random();

            _randomNumList = new List<int>();
            for (int i = 0; i < numRandoms; i++)
            {
                int randomNum = generator.Next();
                _randomNumList.Add(randomNum);
            }

            int numSorts = 1000;
            double percentComplete = 0;

            await Task.Run(() =>
            {
                for (int i = 0; i < numSorts; i++)
                {
                    //TODO: this isn't working yet!!!
                    cancellationToken.ThrowIfCancellationRequested();

                    var copiedList = new List<int>(_randomNumList);
                    copiedList.Sort();

                    percentComplete = i/(double) numSorts*100;

                    string progressString;
                    if (percentComplete < 15)
                    {
                        progressString = "Starting...";
                    }
                    else if (percentComplete >= 15 && percentComplete <= 75)
                    {
                        progressString = "Sorting...";
                    }
                    else if (percentComplete > 75)
                    {
                        progressString = "Almost done.";
                    }
                    else if (Math.Abs(percentComplete - 100) < 1)
                    {
                        progressString = "COMPLETE!!! ";
                    }
                    else
                    {
                        progressString = "";
                    }

                    if (progress != null)
                        progress.Report(new ProgressReport
                        {
                            ProgressString = progressString,
                            ProgressValue = percentComplete
                        });
                }
            }, cancellationToken);

            CurrentProgressReport = new ProgressReport {ProgressValue = 100, ProgressString = "COMPLETE!!!"};
        }


        private void DontDoIt()
        {
            RaiseErrorNotice(new ApplicationException("Oh oh - you shouldn't have pressed that."));
        }

        private async void ProcessSomeData()
        {
            var progress = new Progress<ProgressReport>();
            progress.ProgressChanged += progress_ProgressChanged;

            _cancellationTokenSource = new CancellationTokenSource();


            try
            {
                await DoProcessingTask(_cancellationTokenSource.Token, progress);
            }
            catch (Exception exception)
            {
                if (CurrentProgressReport != null)
                {
                    CurrentProgressReport.ProgressString = "Oh my - you cancelled.";
                }
            }
        }


        private void progress_ProgressChanged(object sender, ProgressReport e)
        {
            CurrentProgressReport = e;
        }

        #endregion
    }
}