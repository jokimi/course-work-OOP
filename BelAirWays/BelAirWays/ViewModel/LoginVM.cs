using BelAirWays.Command;
using BelAirWays.Models;
using BelAirWays.View;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.Mvvm;

namespace BelAirWays.ViewModel
{
    class LoginVM : ViewModelBase, INotifyPropertyChanged
    {
        private MainWindow _mainWindow;
        private LoginForm _loginform;
        private LoginModel log;
        public LoginModel Log
        {
            get { return log; }
            set
            {
                log = value;
                OnPropertyChanged("Log");
            }
        }
        private RelayCommand _exit;
        public RelayCommand Exit
        {
            get
            {
                return _exit ?? (_exit = new RelayCommand (obj =>
                {
                    log.Exit(_mainWindow, _loginform);
                }));
            }
        }
        private RelayCommand _sendPassword;
        public RelayCommand SendPassword
        {
            get
            {
                return _sendPassword ?? (_sendPassword = new RelayCommand (obj =>
                {
                    log.SendNewPassword();
                }));
            }
        }

        private RelayCommand login;
        public RelayCommand Login
        {
            get
            {
                return login ?? (login = new RelayCommand (obj =>
                {
                    var passwordBox = obj as PasswordBox;
                    string passwordEnter = log.GetHashString(passwordBox.Password);
                    if (log.Check(passwordEnter) == 0)
                    {
                        UserView userView = new UserView(_mainWindow);
                        _loginform.Hide();
                        userView.ShowDialog();
                    }
                    else if (log.Check(passwordEnter) == 1)
                    {
                        AdminView adminView = new AdminView(_mainWindow);
                        _loginform.Hide();
                        adminView.ShowDialog();
                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox mbox = new Xceed.Wpf.Toolkit.MessageBox();
                        FontFamily fontFam = new FontFamily("Trebuchet MS");
                        string hexColor1 = "#d7ecfd";
                        Color color1 = (Color)ColorConverter.ConvertFromString(hexColor1);
                        SolidColorBrush backgroundBrush = new SolidColorBrush(color1);
                        mbox.OkButtonStyle = (Style)Application.Current.Resources["OkButtonStyle"];
                        mbox.Background = backgroundBrush;
                        mbox.Caption = "Ошибка входа";
                        mbox.Text = "Неверный логин или пароль!";
                        mbox.FontFamily = fontFam;
                        mbox.ShowDialog();
                    }
                }));
            }
        }
        public LoginVM( LoginForm loginForm, MainWindow mainWindow)
        {
            _loginform = loginForm;
            _mainWindow = mainWindow;
            log = new LoginModel();
        }
        public LoginVM() { }
        public new event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}