﻿using System;
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

            browserYoutube.IsBrowserInitializedChanged += BrowserYoutube_IsBrowserInitializedChanged;
            browserAudio.IsBrowserInitializedChanged += BrowserAudio_IsBrowserInitializedChanged;
		}

        private void BrowserYoutube_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue is bool && (bool)e.NewValue)
			{
				browserYoutube.FrameLoadEnd += BrowserYoutube_FrameLoadEnd;
				browserYoutube.Load("https://www.youtube.com/");
			}
		}

		private void BrowserAudio_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue is bool && (bool)e.NewValue)
			{
				browserAudio.Load("http://baudin.pro/aud/");
			}
		}

        private void BrowserYoutube_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
		{
			Application.Current.Dispatcher?.Invoke(() =>
			{
				if (browserYoutube.Address.Equals("https://www.youtube.com/"))
				{
					browserYoutube.GetBrowser().FocusedFrame.ExecuteJavaScriptAsync("document.getElementById('remind-me-later-button').click();");
					browserYoutube.GetBrowser().FocusedFrame.ExecuteJavaScriptAsync("document.getElementById('dismiss-button').click();");
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

		private void buttonHome_Click(object sender, RoutedEventArgs e)
		{
			if (tabItemYoutube.IsSelected)
			{
				browserYoutube.Load("https://www.youtube.com/");
			}
			else
			{
				browserAudio.Load("http://baudin.pro/aud/");
			}
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
			if (tabItemYoutube.IsSelected)
			{
				browserYoutube.GetBrowser().FocusedFrame.ExecuteJavaScriptAsync("document.getElementsByClassName('video-stream')[0].play();");
			}
			else
			{
				browserAudio.GetBrowser().FocusedFrame.ExecuteJavaScriptAsync("window.sm2BarPlayers[0].actions.play();");
			}
		}

        private void buttonTaskBarPause_Click(object sender, EventArgs e)
		{
			if (tabItemYoutube.IsSelected)
			{
				browserYoutube.GetBrowser().FocusedFrame.ExecuteJavaScriptAsync("document.getElementsByClassName('video-stream')[0].pause();");
			}
			else
			{
				browserAudio.GetBrowser().FocusedFrame.ExecuteJavaScriptAsync("window.sm2BarPlayers[0].actions.pause();");
			}
		}
    }
}
