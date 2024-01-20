﻿using HomeWorkTheme10WpfApp.Classes;
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var listOfClients = new Manager();
            dtgClients.ItemsSource = listOfClients.GetAllClients();
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            var currentClient = (sender as Button).DataContext as Clients;
            NavigationService.Navigate(new AddEditClientInfoPage(currentClient));
        }
    }
}
