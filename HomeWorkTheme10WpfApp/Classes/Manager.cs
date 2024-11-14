using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkTheme10WpfApp.Classes
{
    interface IManager
    {
        void UpdateClientInfo(Consultant client, Manager currentClient);
        void AddNewClient(Manager newClient);
    }
    public class Manager : Consultant, IManager
    {
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

        public void UpdateClientInfo(Consultant client, Manager currentClient)
        {
            var oldClient = _clients.IndexOf(_clients.FirstOrDefault(c => c.Surname == client.Surname && c.Name == client.Name &&
           c.Patronymic == client.Patronymic &&
           c.NumberOfPassport == client.NumberOfPassport &&
           c.SeriesOfPassport == client.SeriesOfPassport));

            _clients.RemoveAt(oldClient);
            string noteSurname = currentClient.Surname;
            string noteName = currentClient.Name;
            string notePatronymic = currentClient.Patronymic;
            string notePhoneNumber = currentClient.PhoneNumber;
            string noteSeriesPassportClient = currentClient.SeriesOfPassport;
            string noteNumberPassportClient = currentClient.NumberOfPassport;

            _clients.Insert(oldClient, new Manager(noteSurname, noteName, notePatronymic, notePhoneNumber,
                noteSeriesPassportClient, noteNumberPassportClient));

            SaveClientsInJsonFile(_clients);
        }

        public void AddNewClient(Manager newClient)
        {
            _clients.Add(newClient);

            SaveClientsInJsonFile(_clients);
        }
    }
}
