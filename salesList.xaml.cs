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
    /// Логика взаимодействия для salesList.xaml
    /// </summary>
    public partial class salesList : Window
    {
        public salesList()
        {
            InitializeComponent();
            LoadTypes(string.Empty);
        }
        private void LoadTypes(string filter)
        {
            var types = new List<TypeOfService>();
            string connectionString = DataModel.connection();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT id, type_of_service, name, cost FROM type_of_service {filter}";

                using (var cmd = new MySqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        types.Add(new TypeOfService
                        {
                            Id = reader.GetInt32("id"),
                            Type = reader.GetString("type_of_service"),
                            Name = reader.GetString("name"),
                            Cost = reader.GetDecimal("cost")
                        });
                    }
                }
            }

            TypeGrid.ItemsSource = types;
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

            if (!string.IsNullOrWhiteSpace(SearchBoxName.Text))
            {
                string safeName = SearchBoxName.Text.Trim().Replace("'", "''");
                whereConditions.Add($"name LIKE '%{safeName}%'");
            }

            if (!string.IsNullOrWhiteSpace(SearchBoxType.Text))
            {
                string safeRNKOPP = SearchBoxType.Text.Trim().Replace("'", "''");
                whereConditions.Add($"type_of_service LIKE '%{safeRNKOPP}%'");
            }

            string condition = whereConditions.Any()
                ? "WHERE " + string.Join(" AND ", whereConditions)
                : "";

            LoadTypes(condition);
        }

        private void SearchBoxClient_Click(object sender, RoutedEventArgs e)
        {
            founder();
        }

        private void SearchBoxType_Click(object sender, RoutedEventArgs e)
        {
            founder();
        }
        private void SearchBoxClient_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void SearchBoxType_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Clear_filters(object sender, RoutedEventArgs e)
        {
            SearchBoxName.Clear();
            SearchBoxType.Clear();
            founder();
        }
    }
}