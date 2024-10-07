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
    class HistoryVM : INotifyPropertyChanged
    {
        private HistoryModel models;
        public HistoryModel Models
        {
            get { return models; }
            set
            {
                models = value;
                OnPropertyChanged("Models");
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
                      models.InsertAllHistory();
                  }));
            }
        }

        public HistoryVM(ListView flightList)
        {
            models = new HistoryModel(flightList);
            models.InsertAllHistory();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
