using System;
using System.Windows;
using MySql.Data.MySqlClient;
using static Catrige_DB.GlobalVariables;

namespace Catrige_DB
{
    /// <inheritdoc>
    ///     <cref></cref>
    /// </inheritdoc>
    /// <summary>
    ///     Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login
    {
        private string _login;
        private string _password;

        public Login()
        {
            InitializeComponent();
            LastLogin(false, true);
            if (DebugMode)
            {
                var win2 = new MainWindow();
                win2.Show();
                Close();
            }

            //if (File.Exists("Settings.ini"))
            //{
            //    LoadSettings();
            //}
        }

        private void BtLogin_Click(object sender, RoutedEventArgs e)
        {
            _login = TbLogin.Text;
            _password = TbPassword.Password;
            GetAccess();
        }

        private void GetIsAdmin()
        {
            var conn = new MySqlConnection(ConnectionString.ToString());
            try
            {
                var sql = "Select IsAdmin from users where UserId='" + TbLogin.Text + "'";
                conn.Open();
                var command = new MySqlCommand(sql, conn);
                var isAdmin = command.ExecuteScalar().ToString();
                GetUserStatus(Convert.ToBoolean(isAdmin));
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void GetAccess()
        {
            var conn = new MySqlConnection(ConnectionString.ToString());
            try
            {
                //myConnection.Open(); // Открываем соединение
                //// --- код запроса и т.п. --- //
                //MessageBox.Show("Подключение прошло успешно!");
                //myConnection.Close(); // Закрываем соединение
                conn.Open();
                var cmd =
                    new MySqlCommand(
                        $"Select * from users where UserId='{_login}' and UserPassword='{_password}'", conn);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                if (_login == (string) cmd.ExecuteScalar())
                {
                    conn.Close();
                    CurrentUser = _login;
                    //MessageBox.Show("OK");
                    GetIsAdmin();
                    LastLogin(true, false);
                    var win2 = new MainWindow();
                    win2.Show();
                    Close();
                }
                else
                {
                    conn.Close();
                    MessageBox.Show("Проверьте данные");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void LastLogin(bool hasLogin, bool init)
        {
            var iniFile = new IniFile("Settings.ini");
            if (hasLogin) iniFile.Write("UserID", TbLogin.Text);
            if (init) TbLogin.Text = iniFile.Read("UserID");
        }
    }
}