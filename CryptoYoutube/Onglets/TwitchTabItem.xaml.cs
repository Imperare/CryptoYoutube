using System;
using System.Windows;
using System.Windows.Controls;

namespace CryptoYoutube.Onglets
{
    /// <summary>
    /// Logique d'interaction pour TwitchTabItem.xaml
    /// </summary>
    public partial class TwitchTabItem : TabItem, IOnglet
    {
        public TwitchTabItem()
        {
            InitializeComponent();

            browser.IsBrowserInitializedChanged += Browser_IsBrowserInitializedChanged;
        }

        public void HomePage()
        {
            browser.Load("https://www.twitch.tv/");
        }

        private void Browser_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool && (bool)e.NewValue)
            {
                browser.Load("https://www.twitch.tv/");
            }
        }
    }
}
