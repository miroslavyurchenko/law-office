using MySql.Data.MySqlClient;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для service.xaml
    /// </summary>
    public partial class service : Window
    {
        public service()
        {
            InitializeComponent();
            LoadSales(string.Empty);
        }

        private void LoadSales(string filter)
        {
            string connectionString = "server=localhost;user=root;password=12345;database=law_office;";
            var sales = new List<Service>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $@"select s.*, c.full_name as `client` , l.full_name as `lawyear`, s_s.status, se.type_of_service, se.deadline
                    from sale s join clients c on c.id = s.clients_id join lawyers l on l.id = s.lawyers_id join service se on l.type_of_case = se.type_of_service 
                    join status_s s_s on s_s.id = s.status_id {filter}";

                using (var cmd = new MySqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    { 
                        DateTime saleDate = reader.GetDateTime("sale_date");
                        DateTime dueDate;
                        if (reader.GetString("status")=="Закрита")
                        {
                            dueDate = DateTime.Now;
                        }else{
                            dueDate = saleDate.AddDays(reader.GetInt32("deadline"));
                        }
                        
                        sales.Add(new Service
                        {
                            Id = reader.GetInt32("id"),
                            Clients = reader.GetString("client"),
                            Lawyers = reader.GetString("lawyear"),
                            SaleDate = saleDate,
                            EndDate = dueDate,
                            TypeOfCase = reader.GetString("type_of_service"),
                            Status = reader.GetString("status")
                        });
                    }
                }
            }
            SalesGrid.ItemsSource = sales;
        }
        /*private void founder(string filter)
        {
            string condition = $" where c.full_name LIKE '%{filter}%'";
            LoadSales(condition);
        }

        private void SearchBoxClient_Click(object sender, RoutedEventArgs e)
        {
            string filter = SearchBoxClient.Text.Trim();
            founder(filter);
        }
        private void founder2(string filter)
        {
            string condition = $" where l.full_name LIKE '%{filter}%'";
            LoadSales(condition);
        }
        private void SearchBoxLawyer_Click(object sender, RoutedEventArgs e)
        {
            string filter = SearchBoxLawyer.Text.Trim();
            founder(filter);
        }*/
        private void founder()
        {
            List<string> whereConditions = new List<string>();

            if (!string.IsNullOrWhiteSpace(SearchBoxClient.Text))
            {
                string safeName = SearchBoxClient.Text.Trim().Replace("'", "''");
                whereConditions.Add($"c.full_name LIKE '%{safeName}%'");
            }

            if (!string.IsNullOrWhiteSpace(SearchBoxLawyer.Text))
            {
                string safeService = SearchBoxLawyer.Text.Trim().Replace("'", "''");
                whereConditions.Add($"l.full_name LIKE '%{safeService}%'");
            }

            string condition = whereConditions.Any()
                ? "WHERE " + string.Join(" AND ", whereConditions)
                : "";

            LoadSales(condition);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            adminWindow adminWindow = new adminWindow();
            adminWindow.Show();
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            var selected = SalesGrid.SelectedItem as Service;
            if (selected != null)
            {
                UpdateSaleStatus(selected.Id);
            }
            LoadSales(string.Empty);
        }


    public void UpdateSaleStatus(int saleId)
    {
        string connectionString = "server=localhost;user=root;password=12345;database=law_office;";
        string query = "UPDATE sale SET status_id = 2 WHERE id = @sale_id";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@sale_id", saleId);

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Status updated successfully.");
            }
            else
            {
                Console.WriteLine("No sale found with the specified ID.");
            }
        }
    }

        private void SearchBoxClient_Click(object sender, RoutedEventArgs e)
        {
            founder();
        }

        private void SearchBoxLawyer_Click(object sender, RoutedEventArgs e)
        {
            founder();
        }

        private void Clear_filters(object sender, RoutedEventArgs e)
        {
            SearchBoxClient.Clear();
            SearchBoxLawyer.Clear();
            founder();
        }
    }
}