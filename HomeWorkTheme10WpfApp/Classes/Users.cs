using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HomeWorkTheme10WpfApp.Classes
{
    /// <summary>
    /// Класс Users - управляет списком ролей пользователей, работающих в системе
    /// </summary>
    public class Users
    {
        public string Role { get; set; } = "";

        public ObservableCollection<Users> usersCollection = new ObservableCollection<Users>();
        
        public ObservableCollection<Users> CreateUser()
        {
            usersCollection.Add(new Users()
            {
                Role = "консультант"
            });

            usersCollection.Add(new Users()
            {
                Role = "менеджер"
            });
            
            return usersCollection;
        }
    }
}
