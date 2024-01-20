using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using HomeWorkTheme10WpfApp.Pages;
using System.Windows.Markup;

namespace HomeWorkTheme10WpfApp.Classes
{
    internal class Consultant
    {
        public ObservableCollection<Clients> _clients { get; set; }

        public string path = System.IO.Path.GetFullPath("clients.json");

        public Consultant ()
        {
           
            _clients = new ObservableCollection<Clients> ();
            _clients = GetClientsFromJson();
        }

        public ObservableCollection<Clients> GetAllClients ()
        {
            return _clients;
        }


        private ObservableCollection<Clients> GetClientsFromJson()
        {
            FileInfo jsonFileName = new FileInfo(path);

            if (jsonFileName.Exists)
            {
                var clients = new ObservableCollection<Clients>();

                string jsonPath = jsonFileName.FullName;
                string json = File.ReadAllText(jsonPath);

                var clientsInJson = JObject.Parse(json)["clients"].ToArray();

                foreach (var item in clientsInJson)
                {
                    var client = GetClientFromJsonElement(item);
                    clients.Add(client);
                }

                return clients;
            }
            else
            {
                var clientsNull = new ObservableCollection<Clients> ();
                MessageBox.Show ($"Файл с именем {this.path} не найден.");
                return clientsNull;
            }

        }

        private Clients GetClientFromJsonElement(JToken clientJsonElement)
        {
            string surnameClient = clientJsonElement["surname"].ToString();
            string nameClient = clientJsonElement["name"].ToString();
            string patronymicClient = clientJsonElement["patronymic"].ToString();
            string seriesPassportClient = clientJsonElement["passport"]["series"].ToString();
            string numberPassportClient = clientJsonElement["passport"]["number"].ToString();    

            string phoneNumberClient = clientJsonElement["phoneNumber"].ToString();
            var client = new Clients(
                        surnameClient,
                        nameClient,
                        patronymicClient,
                        phoneNumberClient,
                        seriesPassportClient,
                        numberPassportClient
                        );

            return client;
        }

        public void UpdateClientInfo (Clients currentClient)
        {
            var oldClient = _clients.IndexOf(_clients.FirstOrDefault(c => c.Surname == currentClient.Surname && c.Name == currentClient.Name && 
            c.Patronymic == currentClient.Patronymic && 
            c.NumberOfPassport == currentClient.NumberOfPassport &&
            c.SeriesOfPassport == currentClient.SeriesOfPassport));

            _clients.RemoveAt(oldClient);
            string noteSurname = currentClient.Surname;
            string noteName = currentClient.Name;
            string notePatronymic = currentClient.Patronymic;
            string notePhoneNumber = currentClient.PhoneNumber;
            string noteSeriesPassportClient = currentClient.SeriesOfPassport;
            string noteNumberPassportClient = currentClient.NumberOfPassport;

            _clients.Insert(oldClient, new Clients(noteSurname, noteName, notePatronymic, notePhoneNumber, 
                noteSeriesPassportClient, noteNumberPassportClient));
            
            SaveClientsInJsonFile (_clients);
        }
        
        public void SaveClientsInJsonFile (ObservableCollection<Clients> clients)
        {
            JArray arrayClients = new JArray();
            
            JObject mainTree = new JObject();

            for (int i = 0; i < clients.Count; i++)
            {
                JObject obj = new JObject
                {
                    ["surname"] = clients[i].Surname,
                    ["name"] = clients[i].Name,
                    ["patronymic"] = clients[i].Patronymic,

                    ["passport"] = new JObject
                    {
                        ["series"] = clients[i].SeriesOfPassport,
                        ["number"] = clients[i].NumberOfPassport
                    },
                    ["phoneNumber"] = clients[i].PhoneNumber
                };
                arrayClients.Add(obj);
            }

            mainTree["clients"] = arrayClients;

            StreamWriter wr = new StreamWriter(path); //записываем информацию в файл
            wr.WriteLine(mainTree.ToString());
            wr.Close();

            MessageBox.Show("Операция выполнена успешно.");
        }

       
    }
}
