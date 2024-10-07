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
using DevExpress.Mvvm;

namespace BelAirWays.ViewModel
{
    class AdminVM : ViewModelBase, INotifyPropertyChanged 
    {
        private Page AddAirportForm;
        private Page AddAircraftForm;
        private Page AddCompanyForm;
        private Page AddFlightsForm;
        private Page AddRouteForm;
        private Page AllOrders;
        private Page AllFlightsAdm;
        

        private MainWindow _mainWindow;
        private AdminView _adminWindow;


        private AdminModel _admin;
        public AdminModel Admin
        {
            get { return _admin; }
            set
            {
                _admin = value;
                OnPropertyChanged("Admin");
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

        public AdminVM(AdminView adminWindow,MainWindow mainWindow)
        {
            AddAirportForm = new AddAirportForm();
            AddAircraftForm = new AddAircraft();
            AddCompanyForm = new AddCompanyForm();
            AddFlightsForm = new AddFlightsForm();
            AddRouteForm = new AddRouteForm();
            AllOrders = new AllOrders();
            AllFlightsAdm = new AllFlightsAdm();
           
            FrameOpacity = 1;
            SelectPage = AddAirportForm;
            _adminWindow = adminWindow;
            _mainWindow = mainWindow;
            _admin = new AdminModel();
        }

        private RelayCommand addAirport;
        public RelayCommand AddAiraport
        {
            get
            {
                return addAirport ??
                  (addAirport = new RelayCommand(obj =>
                  {
                      AddAirportForm.Visibility = Visibility.Visible;
                      SlowOpacity(AddAirportForm);
                  }));
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
                      AddAircraftForm.Visibility = Visibility.Visible;
                      SlowOpacity(AddAircraftForm);
                  }));
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

                      AddCompanyForm.Visibility = Visibility.Visible;
                      SlowOpacity(AddCompanyForm);
                  }));
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
                      
                      AddFlightsForm.Visibility = Visibility.Visible;
                      SlowOpacity(AddFlightsForm);
                  }));
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

                      AddRouteForm.Visibility = Visibility.Visible;
                      SlowOpacity(AddRouteForm);
                  }));
            }
        }
        private RelayCommand lookAllOrders;
        public RelayCommand LookAllOrders
        {
            get
            {
                return lookAllOrders ??
                  (lookAllOrders = new RelayCommand(obj =>
                  {
                      
                      AddAirportForm.Visibility = Visibility.Visible;
                      SlowOpacity(AllOrders);
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

                      AllFlightsAdm.Visibility = Visibility.Visible;
                      SlowOpacity(AllFlightsAdm);

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


                      _admin.Exit(_mainWindow, _adminWindow);



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

        public new event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}