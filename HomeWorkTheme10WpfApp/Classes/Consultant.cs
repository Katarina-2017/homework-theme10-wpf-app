using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HomeWorkTheme10WpfApp.Classes
{
    public class Consultant
    {
        string surname;
        string name;
        string patronymic;
        string phoneNumber;
        string seriesOfPassport;
        string numberOfPassport;

        string path; // Путь к файлу с данными

        ObservableCollection<Consultant> _clients { get; set; }

        public string Surname
        {
            get { return this.surname; }

        }
        public string Name
        {
            get { return this.name; }

        }
        public string Patronymic
        {
            get { return this.patronymic; }

        }
        public string SeriesOfPassport
        {
            get
            {
                if (seriesOfPassport != "")
                {
                    return System.Text.RegularExpressions.Regex.Replace(this.seriesOfPassport.ToString(), ".", "*");
                }
                return this.seriesOfPassport.ToString();
            }

        }

        public string NumberOfPassport
        {
            get
            {
                if (numberOfPassport != "")
                {
                    return System.Text.RegularExpressions.Regex.Replace(this.numberOfPassport.ToString(), ".", "*");
                }
                return this.numberOfPassport.ToString();
            }

        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { if (phoneNumber != null) phoneNumber = value; }
        }

        public Consultant(string Surname, string Name, string Patronymic, string PhoneNumber, string SeriesOfPassport, string NumberOfPassport)
        {
            this.surname = Surname;
            this.name = Name;
            this.patronymic = Patronymic;
            this.phoneNumber = PhoneNumber;
            this.seriesOfPassport = SeriesOfPassport;
            this.numberOfPassport = NumberOfPassport;
        }

        public Consultant()
        {
            this.path = System.IO.Path.GetFullPath("clients.json"); ;
            _clients = new ObservableCollection<Consultant>();

            _clients = GetClientsFromJson();

        }

        public ObservableCollection<Consultant> GetAllClients()
        {
            return _clients;
        }

        private ObservableCollection<Consultant> GetClientsFromJson()
        {
            FileInfo jsonFileName = new FileInfo(path);

            if (jsonFileName.Exists)
            {
                var clients = new ObservableCollection<Consultant>();

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
                var clientsNull = new ObservableCollection<Consultant>();
                MessageBox.Show($"Файл с именем {this.path} не найден.");
                return clientsNull;
            }

        }

        private Consultant GetClientFromJsonElement(JToken clientJsonElement)
        {
            string surnameClient = clientJsonElement["surname"].ToString();
            string nameClient = clientJsonElement["name"].ToString();
            string patronymicClient = clientJsonElement["patronymic"].ToString();
            string seriesPassportClient = clientJsonElement["passport"]["series"].ToString();
            string numberPassportClient = clientJsonElement["passport"]["number"].ToString();

            string phoneNumberClient = clientJsonElement["phoneNumber"].ToString();
            var client = new Consultant(
                        surnameClient,
                        nameClient,
                        patronymicClient,
                        phoneNumberClient,
                        seriesPassportClient,
                        numberPassportClient
                        );

            return client;
        }

        public virtual void UpdateClientInfo(Consultant currentClient)
        {
            var oldClient = _clients.IndexOf(_clients.FirstOrDefault(c => c.Surname == currentClient.Surname && c.Name == currentClient.Name &&
            c.Patronymic == currentClient.Patronymic &&
            c.numberOfPassport == currentClient.numberOfPassport &&
            c.seriesOfPassport == currentClient.seriesOfPassport));

            _clients.RemoveAt(oldClient);
            string noteSurname = currentClient.Surname;
            string noteName = currentClient.Name;
            string notePatronymic = currentClient.Patronymic;
            string notePhoneNumber = currentClient.PhoneNumber;
            string noteSeriesPassportClient = currentClient.seriesOfPassport;
            string noteNumberPassportClient = currentClient.numberOfPassport;

            _clients.Insert(oldClient, new Consultant(noteSurname, noteName, notePatronymic, notePhoneNumber,
                noteSeriesPassportClient, noteNumberPassportClient));

            SaveClientsInJsonFile(_clients);

        }

        public void SaveClientsInJsonFile(ObservableCollection<Consultant> clients)
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
                        ["series"] = clients[i].seriesOfPassport,
                        ["number"] = clients[i].numberOfPassport
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
