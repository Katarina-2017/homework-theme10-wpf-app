using HomeWorkTheme10WpfApp.Classes;
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
    /// Логика взаимодействия для PageManager.xaml
    /// </summary>
    public partial class PageManager : Page
    {
        public PageManager()
        {
            InitializeComponent();
        }
        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            //определяем какая кнопка нажата
            object tag = ((Button)e.OriginalSource).Tag;
            string tagTheButton = (string)tag;

            var currentClient = (sender as Button).DataContext as Manager;//формируем пустого клиента при нажатии на кнопку Добавить

            //открываем форму Добавления/Редактирования и передаем выбранного клиента и тэг нажатой кнопки
            NavigationService.Navigate(new AddEditClientInfoPage(currentClient, tagTheButton));
        }
        
        /// <summary>
        /// Метод нажания на кнопку Измененить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeTheNoteClient_Click(object sender, RoutedEventArgs e)
        {
            //определяем какая кнопка нажата
            object tag = ((Button)e.OriginalSource).Tag;
            string tagTheButton = (string)tag;

            var currentClient = dtgClients.SelectedItem as Consultant;//сохраняем выбранную строку с информацией о клиенте

            //открываем форму Добавления/Редактирования и передаем выбранного клиента и тэг нажатой кнопки
            NavigationService.Navigate(new AddEditClientInfoPage(currentClient, tagTheButton));
        }
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //формируем список клиентов и устанавливаем в качестве источника данных DataGrid
            var listOfClients = new Manager();
            dtgClients.ItemsSource = listOfClients.GetAllClients();
        }
    }
}
