using System.Windows;
using System.Windows.Controls;

namespace CryptoYoutube.Onglets
{
    /// <summary>
    /// Logique d'interaction pour AudioTabItem.xaml
    /// </summary>
    public partial class AudioTabItem : TabItem, IOnglet
    {
        public AudioTabItem()
        {
            InitializeComponent();

            browser.IsBrowserInitializedChanged += Browser_IsBrowserInitializedChanged;
        }

        public void HomePage()
        {
            browser.Load("http://baudin.pro/aud/");
        }

        private void Browser_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool && (bool)e.NewValue)
            {
                browser.Load("http://baudin.pro/aud/");
            }
        }
    }
}
