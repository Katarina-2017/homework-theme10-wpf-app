using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkTheme10WpfApp.Classes
{
    public class Manager : Consultant
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

        public override void UpdateClientInfo(Consultant newClient)
        {
            _clients.Add(newClient);

            SaveClientsInJsonFile(_clients);
        }
    }
}
