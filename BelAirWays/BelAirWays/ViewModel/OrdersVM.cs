using BelAirWays.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BelAirWays.Models;
using System.Windows.Controls;

namespace BelAirWays.ViewModel
{
    class OrdersVM : INotifyPropertyChanged
    {

        private ListView _flightList;

        private OrderModel models;
        public OrderModel Models
        {
            get { return models; }
            set
            {
                models = value;
                OnPropertyChanged("Models");
            }

        }
        private RelayCommand _deleteOrder;
        public RelayCommand DeleteOrder
        {
            get
            {
                return _deleteOrder ??
                  (_deleteOrder = new RelayCommand(obj =>
                  {
                      models.Delete_click(_flightList);


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


                      models.ShowAllOrders(_flightList);

                  }));
            }
        }

        public OrdersVM(ListView flightList)
        {
            models = new OrderModel();
            models.ShowAllOrders(flightList);
            _flightList = flightList;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
