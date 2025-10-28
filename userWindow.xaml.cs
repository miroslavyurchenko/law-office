using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KursApp
{
    /// <summary>
    /// Логика взаимодействия для userWindow.xaml
    /// </summary>
    public partial class userWindow : Window
    {
        string connectionString = DataModel.connection();
        public userWindow()
        {
            InitializeComponent();
            show_lawyears(string.Empty);
            show_clients();
        }
        //first
        private void show_lawyears(string where)
        {
            laywersCombobox.Items.Clear();
            string connectionString = DataModel.connection();
            var sales = new List<Service>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT id, full_name FROM lawyers {where}";

                using (var cmd = new MySqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ComboBoxItem
                        {
                            Content = reader["full_name"].ToString(),
                            Tag = Convert.ToInt32(reader["id"])
                        };
                        laywersCombobox.Items.Add(item);
                    }
                }
            }
        }
        private void show_clients()
        {
            clientsCombobox.Items.Clear();
            string connectionString = DataModel.connection();
            var sales = new List<Service>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT id, full_name FROM clients";

                using (var cmd = new MySqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ComboBoxItem
                        {
                            Content = reader["full_name"].ToString(),
                            Tag = Convert.ToInt32(reader["id"])
                        };

                        clientsCombobox.Items.Add(item);
                    }
                }
            }
        }
        private void type_select(object sender, SelectionChangedEventArgs e)
        {
            ComboBox typeCombobox = sender as ComboBox;
            string type = typeCombobox.SelectedItem as string;
            show_lawyears($"where type_of_case = '{(typeCombobox.SelectedItem as ComboBoxItem)?.Content.ToString()}'");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            adminWindow adminWindow = new adminWindow();
            adminWindow.Show();
            this.Close();

        }

        private void Add_sales(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)laywersCombobox.SelectedItem;
            int? lawyers_id = Convert.ToInt32(selectedItem.Tag);
            ComboBoxItem selectedItem2 = (ComboBoxItem)clientsCombobox.SelectedItem;
            int? clients_id = Convert.ToInt32(selectedItem2.Tag);
            string connectionString = DataModel.connection();

            using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"INSERT INTO sale (lawyers_id, clients_id, status_id) values(@lawyers_id, @clients_id, @status_id)";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@lawyers_id", lawyers_id);
                        cmd.Parameters.AddWithValue("@clients_id", clients_id);
                        cmd.Parameters.AddWithValue("@status_id",1);

                    cmd.ExecuteNonQuery();
                    }
                }
            MessageBox.Show("Справа додана до реєстру");
        }

        private void clientsCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string connectionString = DataModel.connection();
            int where = (clientsCombobox.SelectedIndex)+1;
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT id, date_of_birth FROM clients where id = {where}";

                using (var cmd = new MySqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        date_of_birth.SelectedDate = Convert.ToDateTime(reader["date_of_birth"]);
                    }
                }
            }
        }
    }
}