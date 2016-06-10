using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;
using WpfApplication1.ViewModel;

namespace WpfApplication1
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            InitializeDataContext();
        }

        #region Private Methods

        private void InitializeDataContext()
        {
            var mainVm = ServiceLocator.Current.GetInstance<MainViewModel>();
            mainVm.Messages.CollectionChanged += Messages_CollectionChanged;

            mainVm.NoticeEvent += MainVmNoticeEvent;
        }

        private async void Messages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            string newItem = e.NewItems.Cast<string>().FirstOrDefault();
            //var messageBoxResult = MessageBox.Show(this, newItem);

            ProgressDialogController temp = await this.ShowProgressAsync("progress", newItem, true);
            await Task.Delay(1000);
            await temp.CloseAsync();
        }

        private async void MainVmNoticeEvent(object sender, NotificationEventArgs<Exception> e)
        {
            string message = e.Message;
            ProgressDialogController temp = await this.ShowProgressAsync("progress", message, true);
            await Task.Delay(2000);
            await temp.CloseAsync();
        }

        #endregion
    }
}