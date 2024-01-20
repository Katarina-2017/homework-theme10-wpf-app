using HomeWorkTheme10WpfApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HomeWorkTheme10WpfApp.ViewModels
{
    public class ButtonViewModel : BaseViewModel
    {
        private string buttonVisibility = "Visible";
        
        public string ButtonVisibility
        {
            
            get { return buttonVisibility; }

            set
            {
                buttonVisibility = value;
                OnPropertyChanged("ButtonVisibility");
            }
        }


    }
}
