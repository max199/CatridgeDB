using System.Windows;

namespace Catrige_DB
{
    /// <inheritdoc>
    ///     <cref></cref>
    /// </inheritdoc>
    /// <summary>
    ///     Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings
    {
        public Settings()
        {
            InitializeComponent();
            //if (!File.Exists("Settings.ini")) return;
            //LoadSettings();
            //TbServerIp.Text = ServerIp;
            //TbServerPort.Text = ServerPort.ToString();
            //TbServerUserId.Text = UserId;
            //TbServerPassword.Password = Password;
            //TbServerDatabase.Text = Database;
        }

        private void BtSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            //SaveSettings(TbServerIp.Text, TbServerPort.Text, TbServerUserId.Text, TbServerPassword.Password,
            //    TbServerDatabase.Text);
            //Close();
        }
    }
}