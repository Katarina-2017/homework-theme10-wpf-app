using HomeWorkTheme10WpfApp.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        /// <summary>
        /// Метод AddEditClientInfoPage(Object client, string tagButton) - страница с изменениями информации о клиенте
        /// </summary>
        /// <param name="client"></param>
        /// <param name="tagButton"></param>
        public AddEditClientInfoPage(Object client, string tagButton)
        {
            InitializeComponent();

            //Если выбрали пользователя консультант и нажата кнопки Изменить
            if (App.CurrentUser == 0 && tagButton== "ChangePhoneNumber")
            {
                _currentClient = (Consultant)client;

                Title = "Изменение телефона клиента"; //меняем заголовок у формы

                //передаем данные о клиенте в текст боксы
                tbSurname.Text = _currentClient.Surname;
                tbSurname.IsReadOnly = true;
                tbName.Text = _currentClient.Name;
                tbName.IsReadOnly = true;
                tbPatronymic.Text = _currentClient.Patronymic;
                tbPatronymic.IsReadOnly = true;

                tbPhoneNumber.Text = _currentClient.PhoneNumber;

                //скрываем серию и номер паспорта
                tbSeriesOfPassport.Text = System.Text.RegularExpressions.Regex.Replace(_currentClient.SeriesOfPassport.ToString(), ".", "*");
                tbSeriesOfPassport.IsReadOnly = true;
                tbNumberOfPassport.Text = System.Text.RegularExpressions.Regex.Replace(_currentClient.NumberOfPassport.ToString(), ".", "*");
                tbNumberOfPassport.IsReadOnly = true;
            }

            //Если выбрали пользователя менеджер и нажата кнопка Добавить клиента
            else if (App.CurrentUser == 1 && tagButton == "AddClient")
            {
                Title = "Добавление нового клиента"; //меняем заголовок у формы

            }

            //Если выбрали пользователя менеджер и нажата кнопка Изменить
            else if (App.CurrentUser == 1 && tagButton == "ChangeTheNoteClient")
            {
                _currentClient = (Consultant)client;

                Title = "Изменение данных клиента"; //меняем заголовок у формы

                //передаем данные о клиенте в текст боксы
                tbSurname.Text = _currentClient.Surname;
                tbName.Text = _currentClient.Name;
                tbPatronymic.Text = _currentClient.Patronymic;
                tbPhoneNumber.Text = _currentClient.PhoneNumber;
                tbSeriesOfPassport.Text = _currentClient.SeriesOfPassport;
                tbNumberOfPassport.Text = _currentClient.NumberOfPassport;
                
            }
        }

        /// <summary>
        /// Метод CheckErrorsManager() - проверяет заполнение полей для менеджера
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Метод CheckErrorsConsultant() - проверяет заполнение полей для консультанта
        /// </summary>
        /// <returns></returns>
        private string CheckErrorsConsultant()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(tbPhoneNumber.Text))
            {
                errorBuilder.AppendLine("Номер телефона обязательно для заполнения");
            }


            return errorBuilder.ToString();
        }

        /// <summary>
        /// Метод btnSaveChanges_Click - сохранение информации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            //Сохраняем новый номер телефона, если зашли под консультантом
            if (App.CurrentUser == 0 && _currentClient != null)
            {
                var errorMessage = CheckErrorsConsultant();
                if (errorMessage.Length > 0)
                {
                    MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string oldPhoneNumber = _currentClient.PhoneNumber;

                    _currentClient.PhoneNumber = tbPhoneNumber.Text;


                    var listClients = new Consultant();
                    listClients.UpdateClientInfo(_currentClient);//Обновляем номер телефона у выбранного клиента

                    //заполняем информацию по сделанным изменениям и записываем ее в строку temp 
                    _currentClient.DateTimeUpdateClientNote = DateTime.Now;
                    _currentClient.ListOfChange = $"Номер телефона {oldPhoneNumber} изменен на {_currentClient.PhoneNumber}";
                    _currentClient.TypeOfChangeNote = "Изменение";
                    _currentClient.WhoChangedTheNote = CurrentUser(App.CurrentUser);

                    string temp = String.Format("{0} # {1} # {2} # {3} # у клиента {4} {5} {6} ",
                        _currentClient.DateTimeUpdateClientNote,
                        _currentClient.ListOfChange,
                        _currentClient.TypeOfChangeNote,
                        _currentClient.WhoChangedTheNote,
                        _currentClient.Surname,
                        _currentClient.Name,
                        _currentClient.Patronymic);

                    listClients.SaveListOfChange(temp); // сохраняем строку с изменениями в текстовый файл

                    NavigationService.GoBack();

                }
            }
            //Сохраняем информацию о новом клиенте, если зашли под менеджером
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
                    listClients.AddNewClient(newClient); //Добавляем информацию о новом клиенте

                    //заполняем информацию по сделанным изменениям и записываем ее в строку temp 
                    newClient.DateTimeUpdateClientNote = DateTime.Now;
                    newClient.ListOfChange = $"Добавлен новый клиент {tbSurname.Text} {tbName.Text} {tbPatronymic.Text} {tbPhoneNumber.Text}";
                    newClient.TypeOfChangeNote = "Добавление";
                    newClient.WhoChangedTheNote = CurrentUser(App.CurrentUser);

                    string temp = String.Format("{0} # {1} # {2} # {3}",
                        newClient.DateTimeUpdateClientNote,
                        newClient.ListOfChange,
                        newClient.TypeOfChangeNote,
                        newClient.WhoChangedTheNote);
                    listClients.SaveListOfChange(temp); // сохраняем строку с изменениями в текстовый файл

                    NavigationService.GoBack();

                }
            }
            //Сохраняем внесенные изменения в информацию о клиенте, если зашли под менеджером
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
                    listClients.UpdateClientInfo(_currentClient, _currentManagerClient);//обновляем информацию у выбранного клиента

                    //заполняем информацию по сделанным изменениям и записываем ее в строку temp
                    _currentManagerClient.DateTimeUpdateClientNote = DateTime.Now;
                    _currentManagerClient.ListOfChange = $"Изменена основная информация о клиенте: {_currentClient.Surname} {_currentClient.Name} " +
                        $"{_currentClient.Patronymic} {_currentClient.PhoneNumber} {_currentClient.SeriesOfPassport} {_currentClient.NumberOfPassport} на " +
                        $"{_currentManagerClient.Surname} {_currentManagerClient.Name} {_currentManagerClient.Patronymic} {_currentManagerClient.PhoneNumber} " +
                        $"{_currentManagerClient.SeriesOfPassport} {_currentManagerClient.NumberOfPassport}";
                    _currentManagerClient.TypeOfChangeNote = "Изменение";
                    _currentManagerClient.WhoChangedTheNote = CurrentUser(App.CurrentUser);

                    string temp = String.Format("{0} # {1} # {2} # {3}",
                        _currentManagerClient.DateTimeUpdateClientNote,
                        _currentManagerClient.ListOfChange,
                        _currentManagerClient.TypeOfChangeNote,
                        _currentManagerClient.WhoChangedTheNote);
                    listClients.SaveListOfChange(temp); // сохраняем строку с изменениями в текстовый файл

                    NavigationService.GoBack();

                }
            }
        }

        /// <summary>
        /// Переводим роль, которую выбрали в текстовое значение
        /// </summary>
        /// <param name="role">Роль пользователя</param>
        /// <returns></returns>
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
