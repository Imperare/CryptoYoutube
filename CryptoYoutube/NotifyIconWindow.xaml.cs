using System;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;

namespace CryptoYoutube
{
    /// <summary>
    /// Logique d'interaction pour NotifyIconWindow.xaml
    /// </summary>
    public partial class NotifyIconWindow : Window
    {
        private MainWindow m_ecranPrincipal;
        private readonly NotifyIcon NotifIcon;
        private readonly ContextMenuStrip NotifMenu;

        private bool IsOpen;

        public NotifyIconWindow()
        {
            IsOpen = true;

            NotifMenu = new ContextMenuStrip();
            NotifMenu.Items.Add("Ouvrir l'interface", null, OuvrirInterface);
            NotifMenu.Items.Add("Quitter", null, this.ExitAction);

            NotifIcon = new NotifyIcon
            {
                Icon = Properties.Resources.Icone_16,
                Text = "CryptoYTB",
                ContextMenuStrip = NotifMenu,
                Visible = true
            };
            NotifIcon.MouseClick += OnClick;

            DataContext = new NotifyIconViewModel();

            m_ecranPrincipal = new MainWindow();
            m_ecranPrincipal.Show();
        }

        private void OuvrirInterface(object sender, EventArgs e)
        {
            if (m_ecranPrincipal != null && m_ecranPrincipal.IsVisible)
                return;

            m_ecranPrincipal = new MainWindow();
            m_ecranPrincipal.Show();
        }

        private void ExitAction(object sender, EventArgs e)
        {
            NotifMenu.Close();
            NotifIcon.Visible = false;

            Environment.Exit(1);
        }

        // Display menu's icon by left click (right click default)
        private void OnClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                OpenNotifyIconLeftClick();
        }

        private void OpenNotifyIconLeftClick()
        {
            var action = IsOpen ? "HideContextMenu" : "ShowContextMenu";
            if (IsOpen)
                IsOpen = false;

            var context = typeof(NotifyIcon).GetMethod(action, BindingFlags.Instance | BindingFlags.NonPublic);
            context?.Invoke(NotifIcon, null);
        }
    }
}
