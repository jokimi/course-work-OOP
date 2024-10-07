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
    class AircraftVM : INotifyPropertyChanged
    {

        private AircraftModel models;
        public AircraftModel Models
        {
            get { return models; }
            set
            {
                models = value;
                OnPropertyChanged("Models");
            }

        }

        private RelayCommand addAircraft;
        public RelayCommand AddAircraft
        {
            get
            {
                return addAircraft ??
                  (addAircraft = new RelayCommand(obj =>
                  {
                      models.Add();
                  }));
            }
        }
        public AircraftVM()
        {
            models = new AircraftModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
