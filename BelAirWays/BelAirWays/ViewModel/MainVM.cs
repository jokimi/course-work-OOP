using BelAirWays.Command;
using BelAirWays.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelAirWays.ViewModel
{
    class MainVM
    {

        private MainWindow _main_window;

        private RelayCommand goToRegistation;
        public RelayCommand GoToRegistration
        {
            get
            {
                return goToRegistation ??
                  (goToRegistation = new RelayCommand(obj =>
                  {

                      RegistrationForm reg_form = new RegistrationForm(_main_window);

                      _main_window.Hide();
                      reg_form.ShowDialog();

                  }));
            }
        }

        private RelayCommand goToLogin;
        public RelayCommand GoToLogin
        {
            get
            {
                return goToLogin ??
                  (goToLogin = new RelayCommand(obj =>
                  {

                      LoginForm log_form = new LoginForm(_main_window);

                      _main_window.Hide();
                      log_form.ShowDialog();

                  }));
            }
        }

        public MainVM(MainWindow mainwindow)
        {
            _main_window = mainwindow;

        }
    }
}
