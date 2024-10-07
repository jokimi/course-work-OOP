using BelAirWays.Command;
using BelAirWays.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelAirWays.ViewModel
{
    class CompanyVM : INotifyPropertyChanged
    {

        private CompanyModel models;
        public CompanyModel Models
        {
            get { return models; }
            set
            {
                models = value;
                OnPropertyChanged("Models");
            }

        }

        private RelayCommand addCompany;
        public RelayCommand AddCompany
        {
            get
            {
                return addCompany ??
                  (addCompany = new RelayCommand(obj =>
                  {
                      models.Add();
                  }));
            }
        }
        public CompanyVM()
        {
            models = new CompanyModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
