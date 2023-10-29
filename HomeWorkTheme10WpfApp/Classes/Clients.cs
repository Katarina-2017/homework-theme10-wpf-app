using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkTheme10WpfApp.Classes
{
    internal class Clients
    {
        string surname;
        string name;
        string patronymic;
        string phoneNumber;
        string seriesOfPassport;
        string numberOfPassport;

        public string Surname
        {
            get { return this.surname; }
            set { this.surname = value; }
        }
        public string Name
        {
            get { return this.name; } 
            set { this.name = value; }
        }
        public string Patronymic
        {
            get { return this.patronymic; }
            set { this.patronymic = value;}
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { this.phoneNumber = value; }
        }

        public string SeriesOfPassport
        {
            get { return this.seriesOfPassport; }
            set
            {
                this.seriesOfPassport = value;
            }
        }

        public string NumberOfPassport
        {
            get { return this.numberOfPassport; }
            set { this.numberOfPassport = value;}
        }

        public Clients(string Surname, string Name, string Patronymic, string PhoneNumber, string SeriesOfPassport, string NumberOfPassport)
        {
            this.surname = Surname;
            this.name = Name;
            this.patronymic = Patronymic;
            this.phoneNumber = PhoneNumber;
            this.seriesOfPassport = SeriesOfPassport;
            this.numberOfPassport = NumberOfPassport;
        }



    }
}
