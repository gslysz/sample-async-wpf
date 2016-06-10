
using System;
using System.Threading.Tasks;
using SimpleMahappsMvvmLightApp.ViewModel;

namespace SimpleMahappsMvvmLightApp.Service
{
    public class DialogService : IDialogService
    {
        public Task ShowMessage(string message, string title)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessage(string message, string title, string buttonText, Action callBack)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessage(string message, string title, string confirmButtonText, string cancelButtonText, Action<bool> callBack)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessage(string message, string title, string confirmButtonText, string cancelButtonText,
            string alternateButtonText, Action<int> callBack)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessage(string message, string title, string confirmButtonText, string cancelButtonText,
            string alternateButtonText, Action<bool> callBack)
        {
            throw new NotImplementedException();
        }

        public Task StartProgressDialog(string message, string title, bool determinate)
        {
            throw new NotImplementedException();
        }

        public Task StopProgressDialog()
        {
            throw new NotImplementedException();
        }

        public Task UpdateProgressDialogStatus(decimal percentComplete)
        {
            throw new NotImplementedException();
        }

        public bool IsProgressDialogOpen { get; private set; }

        public Task ShowError(string message, string title, string buttonText, Action callBack)
        {
            throw new NotImplementedException();
        }

        public Task ShowError(Exception error, string title, string buttonText, Action callBack)
        {
            throw new NotImplementedException();
        }

        public Task ShowCustomDialog(DialogViewModelBase viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
