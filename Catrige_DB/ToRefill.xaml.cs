using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MySql.Data.MySqlClient;
using static Catrige_DB.GlobalVariables;

namespace Catrige_DB
{
    /// <inheritdoc>
    ///     <cref></cref>
    /// </inheritdoc>
    /// <summary>
    ///     Логика взаимодействия для ToRefill.xaml
    /// </summary>
    public partial class ToRefill
    {
        private bool _tbRepairhasText;
        //public static int Seal;
        //public static string Model;
        //public static string Organization;
        //public static string Owner;
        //public static string DataIn;
        //public static string Repair;
        //public static bool Status;

        public ToRefill()
        {
            InitializeComponent();
            CheckBoxForRepairFix();
            DataLoad();
            LbTodayData.Content = DateTime.Today.ToShortDateString();
            UpdateItems();
            //Status = CheckBoxRefill.IsChecked.Value;
        }

        private void CheckBoxForRepairFix()
        {
            if (CbForRepair.IsChecked != false) return;
            CbForRepair.IsChecked = true;
            CbForRepair.IsChecked = false;
        }

        private void CbForRepair_Checked(object sender, RoutedEventArgs e)
        {
            CbForRepair.Content = "Нужен ремонт";
            TbRepairNeed.IsEnabled = true;
            TbRepairNeed.Text = "Введите деффект";
        }

        private void CbForRepair_Unchecked(object sender, RoutedEventArgs e)
        {
            CbForRepair.Content = "Ремон не нужен";
            TbRepairNeed.IsEnabled = false;
        }

        private void DataLoad()
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
                        "Select Seal,Model,Organization,Owner,Repair,DataIn,DataOut,Status,WhoRefill from cartridgestorefill",
                        conn);
                var adp = new MySqlDataAdapter(cmd);
                var ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                var dt = new DataTable("cartridgestorefill");
                adp.Fill(dt);
                DgRefillLog.ItemsSource = dt.DefaultView;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }


        private void UpdateItems()
        {
            //CbOrganization.Items.Clear();
            try
            {
                var con = new MySqlConnection(ConnectionString.ToString());
                var cmd = new MySqlCommand("select Organization from organizations", con);
                con.Open();
                var adapter = new MySqlDataAdapter(cmd);
                var dt = new DataTable();
                adapter.Fill(dt);
                CbOrganization.DataContext = dt;
                CbOrganization.DisplayMemberPath = "Organization";
                CbOrganization.SelectedValuePath = "Organization";
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка - UpdateItems");
            }
        }

        private void TbRepairNeed_MouseEnter(object sender, MouseEventArgs e)
        {
            TbRepairNeed.Text = string.Empty;
        }

        private void TbRepairNeed_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_tbRepairhasText == false)
                TbRepairNeed.Text = "Введите деффект";
        }

        private void TbRepairNeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbRepairNeed.Text != "Введите деффект")
                _tbRepairhasText = true;
        }

        private void BtAddToRefill_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TbSeal.Text, out var i) == false || TbModel.Text.Length < 3 ||
                CbOrganization.Text == "")
                MessageBox.Show("Заполните все поля!");
            else
                using (var connection = new MySqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();
                    var command = new MySqlCommand(
                        $"insert into `cartridgestorefill`(`Seal`,`Model`, `Organization`, `Owner`, `DataIn`, `Status`, `WhoRefill`) values('{i}', '{TbModel.Text}', '{CbOrganization.Text}', '{TbOwner.Text}', '{DateTime.Today.ToShortDateString()}', '{false}', '{"none"}')",
                        connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    DataLoad();
                    MessageBox.Show("Картридж успешно добавлен");
                }
            //const string sql = "inster into cartridgestorefill(Seal,Model,Organization,Owner,DataIn,Repair,Status)" +
            //                   "VALUEs(:Seal, :Model, :Organization, :Owner, :DataIn, :Repair, :Status)";




            //const string sql =
            //    "INSERT INTO cartridgestorefill(Seal, Model, Organization, Owner, DataIn,Repair,Status) " +
            //    "VALUES(:Seal, :Model, :Organization, :Owner, :DataIn, :Repair, :Status)";

            //AddUpdateData(sql, 0);

            //BtAddToRefill.IsEnabled = false;
            //BtUpdateRefill.IsEnabled = true;
            //BtDellFromRefill.IsEnabled = true;
        }

        public void AddUpdateData(string sqlStm, int state)
        {
            //OnlyNumber1(Seal.ToString());
            //OnlyNumber(TbSeal.Text)


            var conn = new MySqlConnection(ConnectionString.ToString());
            try
            {
                //var msg = string.Empty;
                //conn.Open();
                //var cmd = new MySqlCommand
                //{
                //    CommandText = sqlStm,
                //    CommandType = CommandType.Text
                //};

                var msg = string.Empty;
                var cmd = conn.CreateCommand();
                cmd.CommandText = sqlStm;
                cmd.CommandType = CommandType.Text;

                switch (state)
                {
                    case 0:
                        msg = "Картридж успешно добавлен";
                        //cmd.Parameters.Add($"insert into `cartridgestorefill`(`Seal`,`Model`, `Organization`, `Owner`, `DataIn`,'Repair', 'Status') values('{seal}', '{model}', '{organization}','{owner}', '{DateTime.Today.ToShortDateString()}' , '{repair}', '{status}')");
                        cmd.Parameters.Add("Seal", MySqlDbType.String, 4).Value = int.Parse(TbSeal.ToString());
                        cmd.Parameters.Add("Model", MySqlDbType.String, 10).Value = TbModel;
                        cmd.Parameters.Add("Organization", MySqlDbType.String, 7).Value = CbOrganization;
                        cmd.Parameters.Add("Owner", MySqlDbType.String, 20).Value = TbOwner;
                        cmd.Parameters.Add("DataIn", MySqlDbType.String, 10).Value =
                            DateTime.Today.ToShortDateString();
                        //cmd.Parameters.Add("DataOut", MySqlDbType.String, 10).Value =
                        //    DateTime.Today.ToShortDateString();
                        cmd.Parameters.Add("Repair", MySqlDbType.String, 255).Value =
                            CbForRepair.IsChecked != null && CbForRepair.IsChecked.Value;
                        cmd.Parameters.Add("Status", MySqlDbType.String, 5).Value =
                            CbRefillStatus.IsChecked != null && CbRefillStatus.IsChecked.Value;
                        //cmd.Parameters.Add("WhoRefill", MySqlDbType.String, 5).Value = seal;
                        break;
                    case 1:
                        msg = "Картридж успешно обновлен";
                        cmd.Parameters.Add("Seal", MySqlDbType.String, 4).Value = int.Parse(TbSeal.ToString());
                        cmd.Parameters.Add("Model", MySqlDbType.String, 10).Value = TbModel;
                        cmd.Parameters.Add("Organization", MySqlDbType.String, 7).Value = CbOrganization;
                        cmd.Parameters.Add("Owner", MySqlDbType.String, 20).Value = TbOwner;
                        cmd.Parameters.Add("DataIn", MySqlDbType.String, 10).Value =
                            DateTime.Today.ToShortDateString();
                        //cmd.Parameters.Add("DataOut", MySqlDbType.String, 10).Value =
                        //    DateTime.Today.ToShortDateString();
                        cmd.Parameters.Add("Repair", MySqlDbType.String, 255).Value =
                            CbRefillStatus.IsChecked != null && CbRefillStatus.IsChecked.Value;
                        cmd.Parameters.Add("Status", MySqlDbType.String, 5).Value =
                            CbRefillStatus.IsChecked != null && CbRefillStatus.IsChecked.Value;
                        //cmd.Parameters.Add("WhoRefill", MySqlDbType.String, 5).Value = seal;
                        break;
                    case 2:
                        msg = "Картридж успешно удалён";
                        cmd.Parameters.Add("Seal", MySqlDbType.Int32, 6).Value = int.Parse(TbSeal.Text);
                        break;
                }

                try
                {
                    var n = cmd.ExecuteNonQuery();
                    if (n <= 0) return;
                    MessageBox.Show(msg);
                    DataLoad();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка - AddUpdateData");
            }
        }

        private void BtDellFromRefill_Click(object sender, RoutedEventArgs e)
        {
            const string sql = "DELETE FROM cartridgestorefill " +
                               "WHERE Seal = :Seal";
            AddUpdateData(sql, 2);
            ResetAll();
        }

        private void DgRefillLog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ResetAll();
            if (!(sender is DataGrid dg)) return;
            if (!(dg.SelectedItem is DataRowView dr)) return;
            TbSeal.Text = dr["Seal"].ToString();
            TbModel.Text = dr["Model"].ToString();
            CbOrganization.Text = dr["Organization"].ToString();
            CbOrganization.DisplayMemberPath = dr["Organization"].ToString();
            CbOrganization.SelectedValuePath = dr["Organization"].ToString();
            TbOwner.Text = dr["Owner"].ToString();
            if (dr["Repair"].ToString() != string.Empty)
            {
                TbRepairNeed.IsEnabled = true;
                CbForRepair.IsChecked = true;
                TbRepairNeed.Text = dr["Repair"].ToString();
            }
            else
            {
                CbForRepair.IsChecked = false;
                TbRepairNeed.IsEnabled = false;
            }

            CbRefillStatus.IsChecked = Convert.ToBoolean(dr["Status"]);
            //TbSeal.Text = dr["WhoRefill"].ToString();

            BtAddToRefill.IsEnabled = false;
            BtUpdateRefill.IsEnabled = true;
            BtDellFromRefill.IsEnabled = true;
        }

        private void ResetAll()
        {
            TbSeal.Text = "";
            TbModel.Text = "";
            CbOrganization.Text = "";
            TbOwner.Text = "";
            TbRepairNeed.Text = "";
            CbForRepair.IsChecked = false;

            BtAddToRefill.IsEnabled = true;
            BtDellFromRefill.IsEnabled = false;
            BtDellFromRefill.IsEnabled = false;
        }

        private void BtUpdateRefill_Click(object sender, RoutedEventArgs e)
        {
            const string sql = "UPDATE cartridgestorefill SET Model = :Model," +
                               "Model = :Model, Organization = :Organization, Owner = :Owner, Repair = :Repair, Status = :Status, WhoRefill = :WhoRefill" +
                               "WHERE Seal = :Seal";
            AddUpdateData(sql, 1);
        }
    }
}