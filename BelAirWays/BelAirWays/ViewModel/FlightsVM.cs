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
    class FlightsVM : INotifyPropertyChanged
    {
        private FlightsModel models;
        public FlightsModel Models
        {
            get { return models; }
            set
            {
                models = value;
                OnPropertyChanged("Models");
            }
        }

        private RelayCommand addFlights;
        public RelayCommand AddFlights
        {
            get
            {
                return addFlights ??
                  (addFlights = new RelayCommand(obj =>
                  {
                      models.Add();
                  }));
            }
        }

        private RelayCommand deleteRoute;
        public RelayCommand DeleteRoute
        {
            get
            {
                return deleteRoute ??
                  (deleteRoute = new RelayCommand(obj =>
                  {
                      models.DeleteRoute();
                  }));
            }
        }

        private RelayCommand deleteCompany;
        public RelayCommand DeleteCompany
        {
            get
            {
                return deleteCompany ??
                  (deleteCompany = new RelayCommand(obj =>
                  {
                      models.DeleteCompany();
                  }));
            }
        }

        private RelayCommand deleteAircraft;
        public RelayCommand DeleteAircraft
        {
            get
            {
                return deleteAircraft ??
                  (deleteAircraft = new RelayCommand(obj =>
                  {
                      models.DeleteAircraft();
                  }));
            }
        }

        public FlightsVM()
        {
            models = new FlightsModel();
            models.TakeAllRouts();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
