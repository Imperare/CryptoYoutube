using System;
using System.Windows;
using System.Windows.Controls;

namespace CryptoYoutube.Onglets
{
    /// <summary>
    /// Logique d'interaction pour GoogleTabItem.xaml
    /// </summary>
    public partial class GoogleTabItem : TabItem, IOnglet
    {
        public GoogleTabItem()
        {
            InitializeComponent();

            browser.IsBrowserInitializedChanged += Browser_IsBrowserInitializedChanged;
        }

        public void HomePage()
        {
            browser.Load("https://www.google.com/");
        }

        private void Browser_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool && (bool)e.NewValue)
            {
                browser.Load("https://www.google.com/");
            }
        }
    }
}
