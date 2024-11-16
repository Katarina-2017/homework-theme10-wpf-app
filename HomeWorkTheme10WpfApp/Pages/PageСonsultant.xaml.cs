using HomeWorkTheme10WpfApp.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для PageСonsultant.xaml
    /// </summary>
    public partial class PageСonsultant : Page
    {

        public PageСonsultant()
        {
            InitializeComponent();

        }

        private void btnChangePhoneNumber_Click(object sender, RoutedEventArgs e)
        {
            //определяет какая кнопка нажата
            object tag = ((Button)e.OriginalSource).Tag;
            string tagTheButton = (string)tag;

            var currentClient = dtgClients.SelectedItem as Consultant; //сохраняем выбранную строку с информацией о клиенте

            //открываем форму Добавления/Редактирования и передаем выбранного клиента и тэг нажатой кнопки
            NavigationService.Navigate(new AddEditClientInfoPage(currentClient, tagTheButton)); 
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //формируем список клиентов и устанавливаем в качестве источника данных DataGrid
            var listOfClients = new Consultant();

            dtgClients.ItemsSource = listOfClients.GetAllClients();
        }

    }
}
