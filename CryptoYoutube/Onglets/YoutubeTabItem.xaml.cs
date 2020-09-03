using System;
using System.Windows;
using System.Windows.Controls;

namespace CryptoYoutube.Onglets
{
    /// <summary>
    /// Logique d'interaction pour YoutubeTabItem.xaml
    /// </summary>
    public partial class YoutubeTabItem : TabItem, IOnglet
    {
        public YoutubeTabItem()
        {
            InitializeComponent();

            browser.IsBrowserInitializedChanged += Browser_IsBrowserInitializedChanged;
        }

        private void Browser_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool && (bool)e.NewValue)
            {
                browser.FrameLoadEnd += BrowserYoutube_FrameLoadEnd;
                browser.Load("https://www.youtube.com/");
            }
        }

        private void BrowserYoutube_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            Application.Current.Dispatcher?.Invoke(() =>
            {
                if (browser.Address.Equals("https://www.youtube.com/"))
                {
                    browser.GetBrowser().FocusedFrame.ExecuteJavaScriptAsync("document.getElementById('remind-me-later-button').click();");
                    browser.GetBrowser().FocusedFrame.ExecuteJavaScriptAsync("document.getElementById('dismiss-button').click();");
                    return;
                }
            });
        }

        public void HomePage()
        {
            browser.Load("https://www.youtube.com/");
        }
    }
}
