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
            //формируем список пользователей
            Users usersList = new Users();

            cmbUsers.ItemsSource = usersList.CreateUser();//устанавливаем список пользователей для Combobox
            
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            //Если выбрали роль Консультант
            if (cmbUsers.SelectedIndex == 0)
            {
                App.CurrentUser = 0;
                NavigationService.Navigate(new PageСonsultant()); //Откываем страницу Консультанта
                cmbUsers.SelectedIndex = -1;
            }

            //Если выбрали роль Менеджер 
            else if (cmbUsers.SelectedIndex == 1)
            {
                App.CurrentUser = 1;
                NavigationService.Navigate(new PageManager()); //Открываем страницу Менеджера
                cmbUsers.SelectedIndex = -1;
            } else
            {
                MessageBox.Show("Вы не выбрали роль для входа в систему!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
