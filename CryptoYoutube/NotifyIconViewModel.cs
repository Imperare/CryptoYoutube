using System;

namespace CryptoYoutube
{
    public class NotifyIconViewModel
    {
        /// <summary>
        /// Singleton
        /// </summary>
        public static NotifyIconViewModel Instance { get { return m_instance.Value; } }
        private static readonly Lazy<NotifyIconViewModel> m_instance = new Lazy<NotifyIconViewModel>(() => new NotifyIconViewModel());
    }
}
