using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для newUser.xaml
    /// </summary>
    public partial class newUser : Window
    {
        public newUser()
        {
            InitializeComponent();
        }
        string connectionString = DataModel.connection();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            adminWindow adminWindow = new adminWindow();
            adminWindow.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Insert(object sender, RoutedEventArgs e)
        {
          
            using (var connection = new MySqlConnection(connectionString))
            {
                string input = Contact.Text.Trim();
                string formatted;
                if (full_name.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Введіть ПІБ");
                    return;
                }
                if (Regex.IsMatch(input, @"^\d{9}$"))
                {
                    formatted = Regex.Replace(input, @"(\d{2})(\d{3})(\d{2})(\d{2})", "+380-$1-$2-$3-$4");
                    
                }
                else
                {
                    MessageBox.Show("Номер має складатися з 9 цифр (наприклад: 991234567)");
                    return;
                }
                if (!Regex.IsMatch(rnokpp.Text, @"^\d{10}$"))
                {
                    MessageBox.Show("РНОКПП має складатися з 10 цифр");
                    return;
                }
                connection.Open();
                string query = $"INSERT INTO clients (full_name, date_of_birth, rnokpp, contact_data) VALUES(@full_name, @date_of_birth, @rnokpp, @contact_data);";

                using (var cmd = new MySqlCommand(query, connection))
                {   
                    cmd.Parameters.AddWithValue("@full_name", full_name.Text);
                    cmd.Parameters.AddWithValue("@date_of_birth", date_of_birth.SelectedDate);
                    cmd.Parameters.AddWithValue("@rnokpp", rnokpp.Text);
                    cmd.Parameters.AddWithValue("@contact_data", formatted);


                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Новий користувач зареєстрований");
        }
    }
}
