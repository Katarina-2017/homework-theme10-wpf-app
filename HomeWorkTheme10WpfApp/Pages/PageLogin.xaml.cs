using HomeWorkTheme10WpfApp.Classes;
using HomeWorkTheme10WpfApp.Pages;
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

namespace HomeWorkTheme10WpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
            Users usersList = new Users();

            cmbUsers.ItemsSource = usersList.CreateUser();
            
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (cmbUsers.SelectedIndex == 0)
            {
                App.CurrentUser = 0;
                NavigationService.Navigate(new PageСonsultant());
                cmbUsers.SelectedIndex = -1;
            }
            else if (cmbUsers.SelectedIndex == 1)
            {
                App.CurrentUser = 1;
                NavigationService.Navigate(new PageManager());
                cmbUsers.SelectedIndex = -1;
            } else
            {
                MessageBox.Show("Вы не выбрали роль для входа в систему!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
