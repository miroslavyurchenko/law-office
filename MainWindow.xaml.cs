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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KursApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Sing_in(object sender, RoutedEventArgs e)
        {
            string connectionString = DataModel.connection();
            string login = TextBox_Login.Text.Trim();
            string pass = TextBox_Pass.Password.Trim();
            //MessageBox.Show("login: " + login + "pass: " + pass);
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", login);
                    cmd.Parameters.AddWithValue("@password", pass);

                    int userExists = Convert.ToInt32(cmd.ExecuteScalar());

                    if (userExists > 0)
                    {
                        adminWindow adminWindow = new adminWindow();
                        adminWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Невірний логін або пароль");
                    }
                }
            }
        }
        private void TextBox_TextChanged1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged2(object sender, TextChangedEventArgs e)
        {

        }
    }
}
