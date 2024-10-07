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
    class AllFlightsAdmVM : INotifyPropertyChanged
    {
        private ListView _flightList;

        private AllFlightsAdmModel models;
        public AllFlightsAdmModel Models
        {
            get { return models; }
            set
            {
                models = value;
                OnPropertyChanged("Models");
            }

        }


        private RelayCommand _delete;
        public RelayCommand Delete
        {
            get
            {
                return _delete ??
                  (_delete = new RelayCommand(obj =>
                  {
                      models.Delete(_flightList);

                  }));
            }
        }

        private RelayCommand reload;
        public RelayCommand Reload
        {
            get
            {
                return reload ??
                  (reload = new RelayCommand(obj =>
                  {


                      models.ShowAllFlights(_flightList);

                  }));
            }
        }

        public AllFlightsAdmVM(ListView flightList)
        {
            _flightList = flightList;
            models = new AllFlightsAdmModel(flightList);
            models.ShowAllFlights(flightList);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}