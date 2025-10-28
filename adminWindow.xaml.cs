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
    /// Логика взаимодействия для adminWindow.xaml
    /// </summary>
    public partial class adminWindow : Window
    {
        public adminWindow()
        {
            InitializeComponent();
        }

        private void Button_lawyers(object sender, RoutedEventArgs e)
        {
            lawyersList lawyersList = new lawyersList();
            lawyersList.Show();
            this.Close();
        }

        private void Button_clients(object sender, RoutedEventArgs e)
        {
            clientsList clientsList = new clientsList();
            clientsList.Show();
            this.Close();
        }

        private void Button_sales(object sender, RoutedEventArgs e)
        {
            salesList salesList = new salesList();
            salesList.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            service serviceWindow = new service();
            serviceWindow.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            userChoise userChoise = new userChoise();
            userChoise.Show();
            this.Close();
        }
    }
}
