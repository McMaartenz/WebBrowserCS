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
    public partial class Inspector : Window, IConsole
    {
        public bool ShouldClose { get; set; }

        private readonly DOMWindow _browserWindow;

        public Inspector(DOMWindow browserWindow)
        {
            _browserWindow = browserWindow;
            InitializeComponent();
        }

        private void Message(ConsoleWarningLevel level, object obj)
        {
            ConsoleHistory.Dispatcher.BeginInvoke(() =>
            {
                TextBlock tb = new()
                {
                    Text = $"{level}: {obj}",
                    Foreground = level switch
                    {
                        ConsoleWarningLevel.Log => Brushes.SteelBlue,
                        ConsoleWarningLevel.Warn => Brushes.Yellow,
                        ConsoleWarningLevel.Error => Brushes.Crimson,
                        _ => Brushes.Black
                    }
                };
                ConsoleHistory.Children.Add(tb);
            });
        }

        public void Log(object contents) => Message(ConsoleWarningLevel.Log, contents);
        public void Warn(object contents) => Message(ConsoleWarningLevel.Warn, contents);
        public void Error(object contents) => Message(ConsoleWarningLevel.Error, contents);

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

            request.Resolved += (sender, e) =>
            {
                currentDocument.Dispatcher.Invoke(() =>
                {
                    currentDocument.Text = _browserWindow.Document.ToString();
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
