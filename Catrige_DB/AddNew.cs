using System;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;
using static Catrige_DB.GlobalVariables;

namespace Catrige_DB
{
    /// <inheritdoc>
    ///     <cref></cref>
    /// </inheritdoc>
    /// <summary>
    ///     Логика взаимодействия для AddNew.xaml
    /// </summary>
    public partial class AddNew
    {
        public AddNew()
        {
            InitializeComponent();
            DataLoad();
        }

        //private readonly string _sql =
        //    "INSERT INTO [Cartridges] (Seal, Model, Organization, Data) VALUES (@Seal, @Model, @Organization, @Data)";

        private void BtAddNew_Click(object sender, EventArgs e)
        {
            if (int.TryParse(TextBoxSeal.Text, out var i) == false || TextBoxModel.Text.Length < 3 ||
                CbOrganization.Text == "")
                MessageBox.Show("Заполните все поля!");
            else
                using (var connection = new MySqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();
                    var command = new MySqlCommand(
                        $"insert into `catridgesonbalance`(`Seal`,`Model`, `Organization`, `DataIn`) values('{i}', '{TextBoxModel.Text}', '{CbOrganization.Text}', '{DpShosCatriges.Text}')",
                        connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Картридж успешно добавлен!");
                    DataLoad();
                }
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
            //adp.Fill(ds, "LoadDataBinding");
            adp.Fill(ds);
            var dv = new DataView(ds.Tables[0]);
            DgCatriges.ItemsSource = dv;
            connection.Close();
        }

        private void BtShowCartridges_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
    }
}