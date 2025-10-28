using Google.Protobuf.Compiler;
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
    /// Логика взаимодействия для lawyersList.xaml
    /// </summary>
    public partial class lawyersList : Window
    {
        string connectionString = DataModel.connection();
        public lawyersList()
        {
            InitializeComponent();
            LoadLawyers(string.Empty);
        }
        private void LoadLawyers(string filter)
        {
            string connectionString = "server=localhost;user=root;password=12345;database=law_office;";
            var lawyers = new List<lawyers>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var query = $"SELECT id, full_name, experience, license, contact_data, type_of_case FROM lawyers {filter}";

                using (var cmd = new MySqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lawyers.Add(new lawyers
                        {
                            Id = reader.GetInt32("id"),
                            FullName = reader.GetString("full_name"),
                            Experience = reader.GetInt32("experience"),
                            License = reader.GetString("license"),
                            ContactData = reader.GetString("contact_data"),
                            TypeOfCase = reader.GetString("type_of_case")
                        });
                    }
                }
            }
            LawyersGrid.ItemsSource = lawyers;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            adminWindow adminWindow = new adminWindow();
            adminWindow.Show();
            this.Close();
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            founder();
        }
        private void founder()
        {
            List<string> whereConditions = new List<string>();

            if (!string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                string safeName = SearchBox.Text.Trim().Replace("'", "''");
                whereConditions.Add($"full_name LIKE '%{safeName}%'");
            }

            if (!string.IsNullOrWhiteSpace(SearchBoxService.Text))
            {
                string safeService = SearchBoxService.Text.Trim().Replace("'", "''");
                whereConditions.Add($"type_of_case LIKE '%{safeService}%'");
            }

            string condition = whereConditions.Any()
                ? "WHERE " + string.Join(" AND ", whereConditions)
                : "";

            LoadLawyers(condition);
        }
        private void SearchButtonService(object sender, RoutedEventArgs e)
        {
            founder();
        }

        private void Clear_filters(object sender, RoutedEventArgs e)
        {
            SearchBoxService.Clear();
            SearchBox.Clear();
            founder();
        }
    }
}
//string condition = $" where type_of_case LIKE '%{filter}%'";