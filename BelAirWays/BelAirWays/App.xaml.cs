using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace BelAirWays
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Xceed.Wpf.Toolkit.Licenser.LicenseKey = "WTK46JWAYKZUWKN2AFA";
        }
    }
}