using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkTheme10WpfApp.Classes
{
    //Интерфейс для менеджера
    interface IManager
    {
        void UpdateClientInfo(Consultant client, Manager currentClient);
        void AddNewClient(Manager newClient);
    }
    /// <summary>
    /// Класс Менеджер
    /// </summary>
    public class Manager : Consultant, IManager
    {
        //Свойства
        public new string Surname { get; set; }
       
        public new string Name { get; set; }
       
        public new string Patronymic { get; set; }
       
        public new string PhoneNumber { get; set; }
       
        public new string SeriesOfPassport 
        {
            get { return this.seriesOfPassport.ToString(); }
            set { this.seriesOfPassport = value;}
        }
       
        public new string NumberOfPassport 
        {
            get { return this.numberOfPassport.ToString(); }
            set { this.numberOfPassport = value;}
        }

        //Констуркторы
        public Manager (string Surname, string Name, string Patronymic, string PhoneNumber, string SeriesOfPassport, string NumberOfPassport) 
        {
            this.surname = Surname;
            this.name = Name;
            this.patronymic = Patronymic;
            this.phoneNumber = PhoneNumber;
            this.seriesOfPassport = SeriesOfPassport;
            this.numberOfPassport = NumberOfPassport;
        }

        public Manager()
        {

        }
        /// <summary>
        /// Метод UpdateClientInfo(Consultant client, Manager currentClient) - изменение всей информации о клиенте
        /// </summary>
        /// <param name="client">клиент, со старой информацией</param>
        /// <param name="currentClient">клиент, с новой информацией</param>
        public void UpdateClientInfo(Consultant client, Manager currentClient)
        {
            var oldClient = _clients.IndexOf(_clients.FirstOrDefault(c => c.Surname == client.Surname && c.Name == client.Name &&
           c.Patronymic == client.Patronymic &&
           c.NumberOfPassport == client.NumberOfPassport &&
           c.SeriesOfPassport == client.SeriesOfPassport)); //ищем клиента в списке клиентов

            _clients.RemoveAt(oldClient); //удаляем этого клиента

            //обновляем все данные
            string noteSurname = currentClient.Surname;
            string noteName = currentClient.Name;
            string notePatronymic = currentClient.Patronymic;
            string notePhoneNumber = currentClient.PhoneNumber;
            string noteSeriesPassportClient = currentClient.SeriesOfPassport;
            string noteNumberPassportClient = currentClient.NumberOfPassport;

            _clients.Insert(oldClient, new Manager(noteSurname, noteName, notePatronymic, notePhoneNumber,
                noteSeriesPassportClient, noteNumberPassportClient)); //вставляем в список клиента, с обновленной информацией на место удаленного 

            SaveClientsInJsonFile(_clients); //сохранем список клиентов
        }

        /// <summary>
        /// AddNewClient(Manager newClient) - добавление нового клиента
        /// </summary>
        /// <param name="newClient">новый клиент</param>
        public void AddNewClient(Manager newClient)
        {
            _clients.Add(newClient);

            SaveClientsInJsonFile(_clients);
        }
    }
}
