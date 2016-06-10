using System;

namespace WpfApplication1.ViewModel
{
    public class NotificationEventArgs : EventArgs
    {
        #region Public Properties

        public string Message { get; set; }

        #endregion
    }

    public class NotificationEventArgs<TOutgoing> : NotificationEventArgs
    {
        #region Public Properties

        public TOutgoing Data { get; set; }

        public int? TimeDelay { get; set; }

        #endregion
    }
}