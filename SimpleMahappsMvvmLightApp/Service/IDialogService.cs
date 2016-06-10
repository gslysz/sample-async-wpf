using System;
using System.Threading.Tasks;
using SimpleMahappsMvvmLightApp.ViewModel;

namespace SimpleMahappsMvvmLightApp.Service
{
    public interface IDialogService
    {
        #region Confirmation Dialog

        /// <summary>
        ///     Dialog with 'Ok' button
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        Task ShowMessage(string message, string title);

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttonText"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        Task ShowMessage(string message, string title, string buttonText, Action callBack);

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="confirmButtonText"></param>
        /// <param name="cancelButtonText"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        Task ShowMessage(string message, string title, string confirmButtonText, string cancelButtonText,
            Action<bool> callBack);

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="confirmButtonText"></param>
        /// <param name="cancelButtonText"></param>
        /// <param name="alternateButtonText"></param>
        /// <param name="callBack"> 0 for alt, -1 for cancel, 1 for confirm </param>
        /// <returns></returns>
        Task ShowMessage(string message, string title, string confirmButtonText, string cancelButtonText,
            string alternateButtonText,
            Action<int> callBack); 

        #endregion

        #region Progress Dialog

        bool IsProgressDialogOpen { get; }

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="determinate"></param>
        /// <returns></returns>
        Task StartProgressDialog(string message, string title, bool determinate);

        /// <summary>
        /// </summary>
        /// <returns></returns>
        Task StopProgressDialog();

        /// <summary>
        /// </summary>
        /// <param name="percentComplete"></param>
        /// <returns></returns>
        Task UpdateProgressDialogStatus(decimal percentComplete);

        #endregion

        #region Error Dialog

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttonText"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        Task ShowError(string message, string title, string buttonText, Action callBack);

        /// <summary>
        /// </summary>
        /// <param name="error"></param>
        /// <param name="title"></param>
        /// <param name="buttonText"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        Task ShowError(Exception error, string title, string buttonText, Action callBack);

        #endregion

        #region Custum Dialogs

        Task ShowCustomDialog(DialogViewModelBase viewModel);

        #endregion
    }
}