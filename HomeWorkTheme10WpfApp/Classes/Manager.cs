using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkTheme10WpfApp.Classes
{
    internal class Manager : Consultant
    {
        public Manager() 
        { 

        }

        public void AddNewClient(Clients newClient)
        {

            _clients.Add(newClient);

            SaveClientsInJsonFile(_clients);

        }
    }
}
