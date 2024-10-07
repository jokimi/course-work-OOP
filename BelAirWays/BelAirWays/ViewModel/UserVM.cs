using BelAirWays.Command;
using BelAirWays.Models;
using BelAirWays.Pages;
using BelAirWays.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BelAirWays.ViewModel
{
    class UserVM : INotifyPropertyChanged
    {
        private Page AllFlights;
        private Page History;
        private Page UpdatePassword;

        private MainWindow _mainWindow;
        private UserView _userWindow;
        
        private UserModel _user;
        public UserModel User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }


        private Page _selectPage;
        public Page SelectPage
        {
            get { return _selectPage; }
            set
            {
                _selectPage = value;
                OnPropertyChanged("SelectPage");
            }
        }
        private double _frameOpacity;
        public double FrameOpacity
        {
            get { return _frameOpacity; }
            set
            {
                _frameOpacity = value;
                OnPropertyChanged("FrameOpacity");
            }
        }

        public UserVM(UserView userWindow, MainWindow mainWindow)
        {
            AllFlights = new AllFlights();
            History = new History();
            UpdatePassword = new UpdatePassword();
            FrameOpacity = 1;
            SelectPage = AllFlights;
            _userWindow = userWindow;
            _mainWindow = mainWindow;
            _user = new UserModel();
        }
        
        private RelayCommand _changePassword;
        public RelayCommand ChangePassword
        {
            get
            {
                return _changePassword ??
                  (_changePassword = new RelayCommand(obj =>
                  {
                      UpdatePassword.Visibility = Visibility.Visible;
                      SlowOpacity(UpdatePassword);
                  }));
            }
        }
        private RelayCommand _exit;
        public RelayCommand Exit
        {
            get
            {
                return _exit ??
                  (_exit = new RelayCommand(obj =>
                  {
                      _user.Exit(_mainWindow, _userWindow);
                  }));
            }
        }

        private RelayCommand lookAllFlights;
        public RelayCommand LookAllFlights
        {
            get
            {
                return lookAllFlights ??
                  (lookAllFlights = new RelayCommand(obj =>
                  {
                      AllFlights.Visibility = Visibility.Visible;
                      SlowOpacity(AllFlights);
                  }));
            }
        }

        private RelayCommand _lookHistory;
        public RelayCommand LookHistory
        {
            get
            {
                return _lookHistory ??
                  (_lookHistory = new RelayCommand(obj =>
                  {
                      History.Visibility = Visibility.Visible;
                      SlowOpacity(History);
                  }));
            }
        }

        private async void SlowOpacity(Page page)
        {
            await Task.Factory.StartNew(() =>
            {
                for (double i = 1.0; i > 0.0; i -= 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(50);

                }
                SelectPage = page;
                for (double i = 0.0; i < 1.1; i += 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(50);
                }

            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
