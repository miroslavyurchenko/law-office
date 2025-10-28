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
    /// Логика взаимодействия для clientsList.xaml
    /// </summary>
    public partial class clientsList : Window
    {
        public clientsList()
        {
            InitializeComponent();
            LoadClients(string.Empty);
        }

        private void LoadClients(string filter)
        {
            string connectionString = "server=localhost;user=root;password=12345;database=law_office;";
            var clients = new List<Client>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT id, full_name, date_of_birth, rnokpp, contact_data FROM clients {filter}";

                using (var cmd = new MySqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(new Client
                        {
                            Id = reader.GetInt32("id"),
                            FullName = reader.GetString("full_name"),
                            DateOfBirth = reader.IsDBNull(reader.GetOrdinal("date_of_birth"))
                                ? (DateTime?)null
                                : reader.GetDateTime("date_of_birth"),
                            Rnokpp = reader.GetString("rnokpp"),
                            ContactData = reader.IsDBNull(reader.GetOrdinal("contact_data"))
                                ? null
                                : reader.GetString("contact_data")
                        });
                    }
                }
            }

            ClientsGrid.ItemsSource = clients;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            adminWindow adminWindow = new adminWindow();
            adminWindow.Show();
            this.Close();
        }
        private void founder()
        {
            List<string> whereConditions = new List<string>();

            if (!string.IsNullOrWhiteSpace(SearchBoxClient.Text))
            {
                string safeName = SearchBoxClient.Text.Trim().Replace("'", "''");
                whereConditions.Add($"full_name LIKE '%{safeName}%'");
            }

            if (!string.IsNullOrWhiteSpace(SearchBoxRNOKPP.Text))
            {
                string safeRNKOPP = SearchBoxRNOKPP.Text.Trim().Replace("'", "''");
                whereConditions.Add($"rnokpp LIKE '%{safeRNKOPP}%'");
            }

            string condition = whereConditions.Any()
                ? "WHERE " + string.Join(" AND ", whereConditions)
                : "";

            LoadClients(condition);
        }

        private void SearchButtonClient(object sender, RoutedEventArgs e)
        {
            founder();
        }

        private void SearchBoxRNKOPP(object sender, RoutedEventArgs e)
        {
            founder();
        }
        private void Clear_filters(object sender, RoutedEventArgs e)
        {
            SearchBoxClient.Clear();
            SearchBoxRNOKPP.Clear();
            founder();
        }
    }
}