using System;
using GalaSoft.MvvmLight;

namespace WpfApplication1.ViewModel
{
    public class BaseViewModel : ViewModelBase
    {
        #region Public Methods

        public void RaiseErrorNotice(Exception exception, int? timeDelay=null)
        {
            var errorNotice = NoticeEvent;
            if (errorNotice != null)
            {
                errorNotice(this,new NotificationEventArgs<Exception>
                    {
                        Data = exception,
                        Message = exception.GetBaseException().Message,
                        TimeDelay = timeDelay
                        
                    });
            }
        }

        #endregion

        public event EventHandler<NotificationEventArgs<Exception>> NoticeEvent;
    }
}