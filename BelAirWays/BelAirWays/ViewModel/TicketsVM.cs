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
    class TicketsVM : INotifyPropertyChanged
    {
        private ListView _flightList;

        private TicketsModel models;
        public TicketsModel Models
        {
            get { return models; }
            set
            {
                models = value;
                OnPropertyChanged("Models");
            }
        }

        private RelayCommand bueTicket;
        public RelayCommand BuyTicket
        {
            get
            {
                return bueTicket ??
                  (bueTicket = new RelayCommand(obj =>
                  {
                      models.BuyTicket();
                  }));
            }
        }
        private RelayCommand _search;
        public RelayCommand Search
        {
            get
            {
                return _search ??
                  (_search = new RelayCommand(obj =>
                  {
                      models.SearchFlight(_flightList);
                  }));
            }
        }

        private RelayCommand _reset;
        public RelayCommand Reset
        {
            get
            {
                return _reset ??
                  (_reset = new RelayCommand(obj =>
                  {
                      models.ShowAllFlights(_flightList);
                  }));
            }
        }
        public TicketsVM(ListView flightList)
        {
            models = new TicketsModel();
            models.ShowAllFlights(flightList);
            _flightList = flightList;        
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
