using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WebBrowser
{
    enum ConsoleWarningLevel
    {
        Log,
        Warn,
        Error
    }

    /// <summary>
    /// Interaction logic for Inspector.xaml
    /// </summary>
    public partial class Inspector : Window
    {
        private DOM.Window _browserWindow;

        public Inspector(DOM.Window browserWindow)
        {
            _browserWindow = browserWindow;
            InitializeComponent();
        }

        private void Message(ConsoleWarningLevel level, object obj)
        {
            ConsoleHistory.Dispatcher.BeginInvoke(() =>
            {
                TextBlock tb = new() { Text = $"{level}: {obj}"};
                ConsoleHistory.Children.Add(tb);
            });
        }

        public void Log(object obj) => Message(ConsoleWarningLevel.Log, obj);
        public void Warn(object obj) => Message(ConsoleWarningLevel.Warn, obj);
        public void Error(object obj) => Message(ConsoleWarningLevel.Error, obj);
    }
}
