using BelAirWays.Command;
using BelAirWays.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelAirWays.ViewModel
{
    class UpdatePasswordVM : INotifyPropertyChanged
    {

        private UpdatePasswordModel _models;
        public UpdatePasswordModel Models
        {
            get { return _models; }
            set
            {
                _models = value;
                OnPropertyChanged("Models");
            }

        }

        private RelayCommand _changePassword;
        public RelayCommand ChangePassword
        {
            get
            {
                return _changePassword ??
                  (_changePassword = new RelayCommand(obj =>
                  {
                      var passwordBox = obj as PasswordBox;
                      string passwordEnter = _models.GetHashString(passwordBox.Password);
                      _models.ChangePassword(passwordEnter);
                  }));
            }
        }

        public UpdatePasswordVM()
        {
            _models = new UpdatePasswordModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
