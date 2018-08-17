using System;
using System.Windows;
using static Catrige_DB.GlobalVariables;

namespace Catrige_DB
{
    /// <inheritdoc>
    ///     <cref></cref>
    /// </inheritdoc>
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Title += UserStatus;
        }

        public void BtAdd_Click(object sender, EventArgs e)
        {
            MaiPageFrame.NavigationService.Navigate(new Uri("ToRefill.xaml", UriKind.Relative));
        }

        public void BtAddNew_Click(object sender, EventArgs e)
        {
            MaiPageFrame.NavigationService.Navigate(new Uri("AddNewCatriges.xaml", UriKind.Relative));
        }

        public void BtDelete_Click(object sender, EventArgs e)
        {
            MaiPageFrame.NavigationService.Navigate(new Uri("DeleteCatriges.xaml", UriKind.Relative));
        }

        public void BtCheck_Click(object sender, EventArgs e)
        {
        }

        public void BtShowToFilling_Click(object sender, EventArgs e)
        {
            MaiPageFrame.NavigationService.Navigate(new Uri("ShowRefillLog.xaml", UriKind.Relative));
        }

        private void BtSettings_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdmin)
            {
                var win3 = new Settings();
                win3.Show();
            }
            else
            {
                MessageBox.Show("Вы не администратор");
            }
        }
    }
}