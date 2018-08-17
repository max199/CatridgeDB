using System;
using System.Data;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using MySql.Data.MySqlClient;
using static Catrige_DB.GlobalVariables;

namespace Catrige_DB
{
    /// <inheritdoc>
    ///     <cref></cref>
    /// </inheritdoc>
    /// <summary>
    ///     Логика взаимодействия для DeleteCatriges.xaml
    /// </summary>
    public partial class DeleteCatriges
    {
        private int _i;

        public DeleteCatriges()
        {
            InitializeComponent();
            var timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1) //in Hour, Minutes, Second.
            };
            timer.Tick += Timer_Tick;
            timer.Start();
            DataLoad();
            //DgCatriges.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //DgCatriges.SelectionMode = DgCatriges.SelectionMode.FullRowSelect;
            //DgCatriges.SelectionMode = (DataGridSelectionMode) DataGridSelectionUnit.FullRow;
        }

        private void DataLoad()
        {
            var connection = new MySqlConnection(ConnectionString.ToString());
            connection.Open();
            var cmd =
                new MySqlCommand(
                    "Select * from catridgesonbalance", connection);
            var adp = new MySqlDataAdapter(cmd);
            var ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding");
            DgCatriges.DataContext = ds.Tables[0];
            var dt = new DataTable("cartridgestorefill");
            adp.Fill(dt);
            DgCatriges.ItemsSource = dt.DefaultView;
            connection.Close();
        }

        private void BtDelete_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(TbSeal.Text))
            //{
            //    using (var connection = new MySqlConnection(ConnectionString.ToString()))
            //    {
            //        connection.Open();
            //        var cmd =
            //            new MySqlCommand(
            //                "DELETE FROM catridgesonbalance Where Seal = @Seal", connection);
            //        var adp = new MySqlDataAdapter(cmd);
            //        var ds = new DataSet();
            //        adp.Fill(ds, "LoadDataBinding");
            //        DgCatriges.DataContext = ds.Tables[0];
            //        connection.Close();
            //        MessageBox.Show("Удаление завершено!");
            //        DataLoad();
            //    }
            //}
            //else
            //{
            //    LbErrorMessage.Visibility = Visibility.Visible;
            //    //timer.Enabled = true;
            //    //timer.Start();
            //    _i = 0;
            //}
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_i < 25)
                if (_i % 2 == 1)
                {
                    LbErrorMessage.Foreground = Brushes.Black; /* ErrorMessage.Visible = false;*/
                    _i++;
                }
                else
                {
                    LbErrorMessage.Foreground = Brushes.Red; /*ErrorMessage.Visible = true;*/
                    _i++;
                }
            else
                LbErrorMessage.Visibility = Visibility.Hidden;
        }
    }
}