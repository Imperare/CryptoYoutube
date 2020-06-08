using System;
using System.Windows;
using System.Windows.Input;

namespace CryptoYoutube
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            browser.FrameLoadEnd += Browser_FrameLoadEnd; ;
			browser.Load("https://www.youtube.com/");
		}

        private void Browser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
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

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			Func<double> PasOpacite = () =>
			{
				if (Opacity > 0.41) return 0.2;
				if (Opacity > 0.11) return 0.1;
				return 0.01;
			};

			if (e.Key == Key.Add)
			{

				Opacity += PasOpacite();
				Opacity = Math.Max(0, Math.Min(1, Opacity));
			}
			else if (e.Key == Key.Subtract)
			{
				Opacity -= PasOpacite();
				Opacity = Math.Max(0, Math.Min(1, Opacity));
			}
			else if (e.Key == Key.NumPad1 && Keyboard.IsKeyDown(Key.LeftCtrl))
			{
				Opacity = 1;
			}
			else if (e.Key == Key.NumPad0 && Keyboard.IsKeyDown(Key.LeftCtrl))
			{
				Opacity = 0;
			}
		}

        private void buttonMinimiser_Click(object sender, RoutedEventArgs e)
        {
			WindowState = WindowState.Minimized;
        }

        private void buttonFermer_Click(object sender, RoutedEventArgs e)
        {
			Close();
        }

        private void buttonRedefinir_Click(object sender, RoutedEventArgs e)
        {
			Height = 510;
			Width = 875;
		}
    }
}
