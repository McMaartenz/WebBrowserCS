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
using WebBrowser.DOM;
using DOMWindow = WebBrowser.DOM.Window;
using Window = System.Windows.Window;

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
        public bool ShouldClose { get; set; }

        private DOMWindow _browserWindow;

        public Inspector(DOMWindow browserWindow)
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

        public void ObserveRequest(Request request)
        {
            TextBlock? tb = null;
            NetworkHistory.Dispatcher.Invoke(() =>
            {
                tb = new() { Text = request.ToString() };
                NetworkHistory.Children.Add(tb);
            });

            request.StatusChanged += (sender, e) =>
            {
                NetworkHistory.Dispatcher.Invoke(() =>
                {
                    if (tb is not null)
                    {
                        tb.Text = request.ToString();
                    }
                });
            };
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ShouldClose;
            Hide();
        }
    }
}
