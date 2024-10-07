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
    class AirportVM : INotifyPropertyChanged
    {
        private AirportModel models;
        public AirportModel Models
        {
            get { return models; }
            set
            {
                models = value;
                OnPropertyChanged("Models");
            }
        }
        private RelayCommand addAirport;
        public RelayCommand AddAirport
        {
            get
            {
                return addAirport ??
                  (addAirport = new RelayCommand(obj =>
                  {
                      models.Add();
                 

                  }));
            }
        }

        public AirportVM()
        {
            models = new AirportModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
