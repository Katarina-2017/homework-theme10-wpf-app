using HomeWorkTheme10WpfApp.Classes;
using HomeWorkTheme10WpfApp.ViewModels;
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
            var currentClient = dtgClients.SelectedItem as Consultant;
            NavigationService.Navigate(new AddEditClientInfoPage(currentClient));
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var listOfClients = new Consultant();

            dtgClients.ItemsSource = listOfClients.GetAllClients();
        }


    }
}
