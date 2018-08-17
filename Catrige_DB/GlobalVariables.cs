using System;
using MySql.Data.MySqlClient;

namespace Catrige_DB
{
    internal class GlobalVariables
    {
        public static bool IsAdmin;
        public static string UserStatus;
        public static string CurrentUser;
        public static string[] Org = new string[10];
        public static bool DebugMode = true;

        //public static string ServerIp = "localhost";
        public static string ServerIp = "172.27.0.96";
        public static int ServerPort = 3306;
        public static string UserId = "root";
        public static string Password = "";
        public static string Database = "CatrigesDB";
        public static string CharSet = "Charset=utf8";

        //System settings
        //public static string FrameWidth;
        //public static string FrameHeight;

        public static MySqlConnectionStringBuilder ConnectionString = new MySqlConnectionStringBuilder
        {
            Server = ServerIp,
            Port = Convert.ToUInt32(ServerPort),
            UserID = UserId,
            Password = Password,
            Database = Database
        };
        //System settings

        //public static string ServerIp;
        //public static string ServerPort;
        //public static string UserId;
        //public static string Password;
        //public static string Database;

        //TextBox data
        //public static int Seal;
        //public static string Model;
        //public static string Organization;
        //public static string Owner;
        //public static string DataIn;
        //public static string Repair;
        //public static bool Status;
        //TextBox data

        //public static void OnlyNumber1(string num)
        //{
        //    if (int.TryParse(num, out var i) == false)
        //    Seal = i;
        //}

        public static int OnlyNumber(string num)
        {
            if (int.TryParse(num, out var i) == false)
            { num = i.ToString();}
            return i;
        }

        public static void GetUserStatus(bool admin)
        {
            if (admin) // подключение к базе данных и получение статуса пользователя "[Администратор/Пользователь]"
            {
                IsAdmin = true;
                UserStatus = ": " + CurrentUser + "- режим - [Администратор]";
            }
            else
            {
                IsAdmin = false;
                UserStatus = ": " + CurrentUser + "- режим - [Пользователь]";
            }
        }

        //public static void AddUpdateData_Old(string sqlStm, int state)
        //{
        //    OnlyNumber(Seal.ToString());
        //    var conn = new MySqlConnection(ConnectionString.ToString());
        //    try
        //    {
        //        var msg = string.Empty;
        //        conn.Open();
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandText = sqlStm;
        //        cmd.CommandType = CommandType.Text;
        //        //var adp = new MySqlDataAdapter(cmd);
        //        //var ds = new DataSet();
        //        //adp.Fill(ds, "LoadDataBinding");
        //        //var dt = new DataTable("cartridgestorefill");
        //        //adp.Fill(dt);
        //        //DgRefillLog.ItemsSource = dt.DefaultView;
        //        //conn.Close();
        //        switch (state)
        //        {
        //            case 0:
        //                msg = "Картридж успешно добавлен";
        //                cmd.Parameters.Add(
        //                    $"insert into `cartridgestorefill`(`Seal`,`Model`, `Organization`, `Owner`, `DataIn`,'Repair', 'Status') values('{Seal}', '{Model}', '{Organization}', '{Repair}', '{Owner}','{Status}')");
        //                break;
        //            case 1:
        //                msg = "Картридж успешно обновлен";
        //                break;
        //            case 2:
        //                msg = "Картридж успешно удалён";
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Ошибка");
        //    }
        //}


//internal static void SaveSettings(string serverIp, string serverPort, string userId, string userPassword,
        //    string dbName)
        //{
        //    var myIni = new IniFile("Settings.ini");
        //    myIni.Write("ServerIp", serverIp);
        //    myIni.Write("ServerPort", serverPort);
        //    myIni.Write("UserID", userId);
        //    myIni.Write("UserPassword", userPassword);
        //    myIni.Write("DataBaseName", dbName);
        //}

        internal static void LoadSettings()
        {
            //if (!File.Exists("Settings.ini")) return;
            //var iniFile = new IniFile("Settings.ini");
            //ServerIp = iniFile.Read("ServerIp");
            //ServerPort = Convert.ToUInt32(iniFile.Read("ServerPort"));
            //UserId = iniFile.Read("UserID");
            //Password = iniFile.Read("UserPassword") ?? "";
            //Database = iniFile.Read("DataBaseName");
            //MessageBox.Show("IP сервера: " + ServerIp + "   Порт сервера: "
            //                + ServerPort + "   ID пользователя: "
            //                + UserId + "   Пароль пользователя: " + Password
            //                + "   Имя таблицы БД: " + Database);
            ////MessageBox.Show("Порт сервера: " + ServerPort.ToString());
            ////MessageBox.Show("ID пользователя: " + UserId);
            ////MessageBox.Show("Пароль пользователя: " + Password);
            ////MessageBox.Show("Имя таблицы БД: " + Database);
        }
    }
}