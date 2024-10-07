using BelAirWays.Command;
using BelAirWays.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BelAirWays.ViewModel
{
    class RouteVM : INotifyPropertyChanged
    {
        private RouteModel models;
        public RouteModel Models
        {
            get { return models; }
            set
            {
                models = value;
                OnPropertyChanged("Models");
            }
        }
        private RelayCommand addRoute;
        public RelayCommand AddRoute
        {
            get
            {
                return addRoute ??
                  (addRoute = new RelayCommand(obj =>
                  {                   
                      models.Add();
                  }));
            }
        }
        private RelayCommand deleteRouteFrom;
        public RelayCommand DeleteRouteFrom
        {
            get
            {
                return deleteRouteFrom ?? (deleteRouteFrom = new RelayCommand(obj =>
                {
                    models.DeleteAirportFrom();
                }));
            }
        }
        public RouteVM()
        {
            models = new RouteModel();
            models.TakeAllAirports();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}