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
using System.Xml.Linq;

namespace HomeWorkTheme10WpfApp.Classes
{
    internal class Consultant
    {
        private ObservableCollection<Clients> _clients { get; set; }

        private string path;

        public Consultant (string Path)
        {
            this.path = Path;
           
        }

        public ObservableCollection<Clients> GetClientsFromJson()
        {
            FileInfo jsonFileName = new FileInfo(this.path);

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

                foreach (var client in clients)
                {
                    if (client.SeriesOfPassport != null)
                    {
                        string oldSeriesOfPassport = client.SeriesOfPassport;
                        string hiddenSeriesOfPassport = HiddenString(oldSeriesOfPassport);
                        client.SeriesOfPassport = hiddenSeriesOfPassport;
                    }

                    if (client.NumberOfPassport != null)
                    {
                        string oldNumberOfPassport = client.NumberOfPassport;
                        string hiddenNumberOfPassport = HiddenString(oldNumberOfPassport);
                        client.NumberOfPassport = hiddenNumberOfPassport;
                    }
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

        

        private string HiddenString(string text)
        {
            string newText = System.Text.RegularExpressions.Regex.Replace(text, ".", "*");
            return newText;
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
    }
}
