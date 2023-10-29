using HomeWorkTheme10WpfApp.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для PageСonsultant.xaml
    /// </summary>
    public partial class PageСonsultant : Page
    {
        public PageСonsultant()
        {
            InitializeComponent();

            string path = "C:\\Users\\Home\\source\\repos\\HomeWorkTheme10WpfApp\\clients.json";

            var listOfClients = new Consultant(path);

            dtgClients.ItemsSource = listOfClients.GetClientsFromJson();

            dtgClients.IsReadOnly= true;
        }
    }
}
