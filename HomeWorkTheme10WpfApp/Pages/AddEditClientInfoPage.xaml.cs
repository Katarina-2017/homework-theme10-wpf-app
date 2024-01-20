using HomeWorkTheme10WpfApp.Classes;
using Newtonsoft.Json.Linq;
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
    /// Логика взаимодействия для AddEditClientInfoPage.xaml
    /// </summary>
    public partial class AddEditClientInfoPage : Page
    {
        private Clients _currentClient = null;
        public AddEditClientInfoPage()
        {
            InitializeComponent();
        }

        public AddEditClientInfoPage (Clients client)
        {
            InitializeComponent();

           

            if (App.CurrentUser == 0)
            {
                _currentClient = client;

                Title = "Изменение телефона клиента";

                tbSurname.Text = _currentClient.Surname;
                tbSurname.IsReadOnly = true;
                tbName.Text = _currentClient.Name;
                tbName.IsReadOnly = true;
                tbPatronymic.Text = _currentClient.Patronymic;
                tbPatronymic.IsReadOnly = true;

                tbPhoneNumber.Text = _currentClient.PhoneNumber;

                tbSeriesOfPassport.Text = _currentClient.SeriesOfPassport;
                tbSeriesOfPassport.IsReadOnly = true;
                tbNumberOfPassport.Text = _currentClient.NumberOfPassport;
                tbNumberOfPassport.IsReadOnly = true;
            }

            else if (App.CurrentUser == 1)
            {
                Title = "Добавление нового клиента";

            }
        }

        private string CheckErrorsManager()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(tbSurname.Text))
            {
                errorBuilder.AppendLine("Фамилия клиента обязательно для заполнения");
            }

            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                errorBuilder.AppendLine("Имя клиента обязательно для заполнения");
            }

            if (string.IsNullOrWhiteSpace(tbPatronymic.Text))
            {
                errorBuilder.AppendLine("Отчество клиента обязательно для заполнения");
            }

            if (string.IsNullOrWhiteSpace(tbPhoneNumber.Text))
            {
                errorBuilder.AppendLine("Номер телефона обязательно для заполнения");
            }

            if (string.IsNullOrWhiteSpace(tbSeriesOfPassport.Text))
            {
                errorBuilder.AppendLine("Серия паспорта клиента обязательно для заполнения");
            }

            if (string.IsNullOrWhiteSpace(tbNumberOfPassport.Text))
            {
                errorBuilder.AppendLine("Номер паспорта клиента обязательно для заполнения");
            }

            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0,"Устраните следующие ошибки:\n");
            }
             return errorBuilder.ToString();
        }

        private string CheckErrorsConsultant()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(tbPhoneNumber.Text))
            {
                errorBuilder.AppendLine("Номер телефона обязательно для заполнения");
            }


            return errorBuilder.ToString();
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser == 0 && _currentClient != null)
            {
                var errorMessage = CheckErrorsConsultant();
                if (errorMessage.Length > 0)
                {
                    MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                        _currentClient.Surname = tbSurname.Text;
                        _currentClient.Name = tbName.Text;
                        _currentClient.Patronymic = tbPatronymic.Text;
                        _currentClient.PhoneNumber = tbPhoneNumber.Text;
                        _currentClient.SeriesOfPassport = tbSeriesOfPassport.Text;
                        _currentClient.NumberOfPassport = tbNumberOfPassport.Text;

                        var listClients = new Consultant();
                        listClients.UpdateClientInfo(_currentClient);

                        NavigationService.GoBack();

                }
            }
            else if (App.CurrentUser == 1 && _currentClient == null)
            {
                var errorMessage = CheckErrorsManager();
                if (errorMessage.Length > 0)
                {
                    MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                        Clients newClient = new Clients(tbSurname.Text, tbName.Text, tbPatronymic.Text, tbPhoneNumber.Text, tbSeriesOfPassport.Text, tbNumberOfPassport.Text);

                        var listClients = new Manager();
                        listClients.AddNewClient(newClient);
                        NavigationService.GoBack();
                    
                }
            }
            
        }
    }
}
