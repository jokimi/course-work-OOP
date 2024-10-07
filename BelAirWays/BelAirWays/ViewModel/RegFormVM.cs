using BelAirWays.Command;
using BelAirWays.Models;
using BelAirWays.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BelAirWays.ViewModel
{
    class RegFormVM : INotifyPropertyChanged
    {
        private RegistrationForm _registration_form;
        private MainWindow _main_window;


        private RegFormModel reg;
        public RegFormModel Reg
        {
            get { return reg; }
            set
            {
                reg = value;
                OnPropertyChanged("Reg");
            }
        }
        
        private RelayCommand _registration;
        public RelayCommand Registration
        {
            get
            {
                return _registration ??
                  (_registration = new RelayCommand(obj =>
                  {
                      var passwordBox = obj as PasswordBox;
                      string passwordEnter = reg.GetHashString(passwordBox.Password);
                      reg.Add(passwordEnter, _registration_form, _main_window);
                  }));
            }
        }
        private RelayCommand _sendCode;
        public RelayCommand SendCode
        {
            get
            {
                return _sendCode ??
                  (_sendCode = new RelayCommand(obj =>
                  {
                      reg.SendCode();
                  }));
            }
        }
        private RelayCommand _exit;
        public RelayCommand Exit
        {
            get
            {
                return _exit ??
                  (_exit = new RelayCommand(obj =>
                  {                  
                      reg.Exit( _main_window, _registration_form);
                  }));
            }
        }

        public RegFormVM(RegistrationForm regform, MainWindow mainWindow)
        {
            _registration_form = regform;
            _main_window = mainWindow;
            reg = new RegFormModel();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}