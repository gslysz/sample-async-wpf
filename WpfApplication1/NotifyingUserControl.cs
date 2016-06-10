using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using WpfApplication1.ViewModel;

namespace WpfApplication1
{
    public class NotifyingUserControl : UserControl
    {
        public NotifyingUserControl()
        {
            InitializeDatacontext();
        }

        #region Private Methods

        private void ChildView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vm = DataContext as BaseViewModel;
            if (vm != null)
            {
                vm.NoticeEvent += VmNoticeEvent;
            }
        }

        private void InitializeDatacontext()
        {
            DataContextChanged += ChildView_DataContextChanged;
        }


        private async void VmNoticeEvent(object sender, NotificationEventArgs<Exception> e)
        {
            MetroWindow metroWindow = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();

            if (e.TimeDelay != null)
            {
                ProgressDialogController result = await metroWindow.ShowProgressAsync("Error", e.Message, true);
                await Task.Delay(2000);
                await result.CloseAsync();    
            }
            else
            {
                await metroWindow.ShowMessageAsync("Error", e.Message,MessageDialogStyle.Affirmative,null);
                
            }
            
            
        }

        #endregion
    }
}