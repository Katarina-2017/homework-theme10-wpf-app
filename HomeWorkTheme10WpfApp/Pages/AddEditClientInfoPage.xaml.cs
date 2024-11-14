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
        private Consultant _currentClient = null;
        private Manager _currentManagerClient =  new Manager();

        public AddEditClientInfoPage()
        {
            InitializeComponent();
        }

        public AddEditClientInfoPage(Object client, string tagButton)
        {
            InitializeComponent();


            if (App.CurrentUser == 0 && tagButton== "ChangePhoneNumber")
            {
                _currentClient = (Consultant)client;

                Title = "Изменение телефона клиента";

                tbSurname.Text = _currentClient.Surname;
                tbSurname.IsReadOnly = true;
                tbName.Text = _currentClient.Name;
                tbName.IsReadOnly = true;
                tbPatronymic.Text = _currentClient.Patronymic;
                tbPatronymic.IsReadOnly = true;

                tbPhoneNumber.Text = _currentClient.PhoneNumber;

                tbSeriesOfPassport.Text = System.Text.RegularExpressions.Regex.Replace(_currentClient.SeriesOfPassport.ToString(), ".", "*");
                tbSeriesOfPassport.IsReadOnly = true;
                tbNumberOfPassport.Text = System.Text.RegularExpressions.Regex.Replace(_currentClient.NumberOfPassport.ToString(), ".", "*");
                tbNumberOfPassport.IsReadOnly = true;
            }

            else if (App.CurrentUser == 1 && tagButton == "AddClient")
            {
                Title = "Добавление нового клиента";

            }
            else if (App.CurrentUser == 1 && tagButton == "ChangeTheNoteClient")
            {
                _currentClient = (Consultant)client;

                Title = "Изменение данных клиента";

                tbSurname.Text = _currentClient.Surname;
                
                tbName.Text = _currentClient.Name;
                
                tbPatronymic.Text = _currentClient.Patronymic;
                
                tbPhoneNumber.Text = _currentClient.PhoneNumber;

                tbSeriesOfPassport.Text = _currentClient.SeriesOfPassport;
               
                tbNumberOfPassport.Text = _currentClient.NumberOfPassport;
                
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

                    _currentClient.PhoneNumber = tbPhoneNumber.Text;


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
                    Manager newClient = new Manager(tbSurname.Text, tbName.Text, tbPatronymic.Text, tbPhoneNumber.Text, tbSeriesOfPassport.Text, tbNumberOfPassport.Text);

                    var listClients = new Manager();
                    listClients.AddNewClient(newClient);
                    NavigationService.GoBack();

                }
            }
            else if (App.CurrentUser == 1 && _currentClient != null) 
            {
                var errorMessage = CheckErrorsManager();
                if (errorMessage.Length > 0)
                {
                    MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    _currentManagerClient.Surname = tbSurname.Text;
                    _currentManagerClient.Name = tbName.Text;
                    _currentManagerClient.Patronymic = tbPatronymic.Text;
                    _currentManagerClient.PhoneNumber = tbPhoneNumber.Text;
                    _currentManagerClient.SeriesOfPassport = tbSeriesOfPassport.Text;
                    _currentManagerClient.NumberOfPassport = tbNumberOfPassport.Text;


                    var listClients = new Manager();
                    listClients.UpdateClientInfo(_currentClient, _currentManagerClient);

                    NavigationService.GoBack();

                }
            }
        }

        public static string CurrentUser(int role)
        {
            string currentUser;

            if (role == 0)
            {
                currentUser = "Консультант";
            }
            else if (role == 1)
            {
                currentUser = "Менеджер";
            }
            else currentUser = "Неизвестный пользователь";
            return currentUser;
        }
    }
}
