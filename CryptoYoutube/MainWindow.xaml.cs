using CryptoYoutube.Onglets;
using CryptoYoutube.Services;
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
        private int pressCounter;
        private DateTime pressDateTime;

        public MainWindow()
        {
            InitializeComponent();

            ServiceRaccourcis.OnKeyPress += PressionF4;
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

		private void buttonHome_Click(object sender, RoutedEventArgs e)
		{
            (tabControl.SelectedItem as IOnglet)?.HomePage();
		}

		private void buttonOptiMinimale_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Normal;
			Height = 180;
			Width = 280;
		}

		private void buttonOpti_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Normal;
			Height = 510;
			Width = 875;
		}

		private void buttonMinimiser_Click(object sender, RoutedEventArgs e)
        {
			WindowState = WindowState.Minimized;
		}

		private void buttonMaximiser_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Maximized;
		}

		private void buttonFermer_Click(object sender, RoutedEventArgs e)
        {
			Close();
		}

        private void buttonTaskBarStart_Click(object sender, EventArgs e)
		{
			//if (tabItemYoutube.IsSelected)
			//{
			//	browserYoutube.GetBrowser().FocusedFrame.ExecuteJavaScriptAsync("document.getElementsByClassName('video-stream')[0].play();");
			//}
			//else
			//{
			//	browserAudio.GetBrowser().FocusedFrame.ExecuteJavaScriptAsync("window.sm2BarPlayers[0].actions.play();");
			//}
		}

        private void buttonTaskBarPause_Click(object sender, EventArgs e)
		{
			//if (tabItemYoutube.IsSelected)
			//{
			//	browserYoutube.GetBrowser().FocusedFrame.ExecuteJavaScriptAsync("document.getElementsByClassName('video-stream')[0].pause();");
			//}
			//else
			//{
			//	browserAudio.GetBrowser().FocusedFrame.ExecuteJavaScriptAsync("window.sm2BarPlayers[0].actions.pause();");
			//}
		}

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var ctrlPresse = (Keyboard.Modifiers & ModifierKeys.Control) != 0;
            if (ctrlPresse || e.ChangedButton == MouseButton.Middle)
            {
                this.WindowState = WindowState.Minimized;
                e.Handled = true;
            }
        }

        public void PressionF4()
        {
            var date = DateTime.Now;
            this.pressCounter++;

            // Première pression
            if (this.pressCounter <= 1 || (this.pressCounter > 1 && date > this.pressDateTime.AddMilliseconds(400)))
            {
                this.pressCounter = 1;
                this.pressDateTime = DateTime.Now;

                this.WindowState = WindowState.Minimized;
            }

            // Seconde pression
            else if (this.pressCounter == 2)
            {
                this.pressDateTime = DateTime.Now;

                this.WindowState = WindowState.Normal;
            }
        }
    }
}
