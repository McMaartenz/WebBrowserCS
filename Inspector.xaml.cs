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
    public enum ConsoleWarningLevel
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

        private void ConsoleMessage(ConsoleWarningLevel level, object contents)
        {
            ConsoleHistory.Dispatcher.BeginInvoke(() =>
            {
                TextBlock tb = new()
                {
                    Text = contents.ToString(),
                    Foreground = level switch
                    {
                        ConsoleWarningLevel.Log => Brushes.SteelBlue,
                        ConsoleWarningLevel.Warn => Brushes.Yellow,
                        ConsoleWarningLevel.Error => Brushes.Crimson,
                        _ => Brushes.Black
                    }
                };

                ConsoleContext.TextBlocks.Add(tb, level);
                ConsoleContext.UpdateView();
            }, DispatcherPriority.Render);
        }

        public void Log(object contents) => ConsoleMessage(ConsoleWarningLevel.Log, contents);
        public void Warn(object contents) => ConsoleMessage(ConsoleWarningLevel.Warn, contents);
        public void Error(object contents) => ConsoleMessage(ConsoleWarningLevel.Error, contents);

        public void ObserveRequest(Request request)
        {
            TextBlock? tb = null;
            NetworkHistory.Dispatcher.Invoke(() =>
            {
                tb = new() { Text = request.ToString() };
                NetworkContext.TextBlocks.Add(request, tb);
                NetworkContext.UpdateView();
            });

            request.StatusChanged += (sender, e) =>
            {
                NetworkHistory.Dispatcher.Invoke(() =>
                {
                    if (tb is not null)
                    {
                        tb.Text = request.ToString();
                        NetworkContext.UpdateView();
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

        private void clearConsoleButton_Click(object sender, RoutedEventArgs e)
        {
            ConsoleContext.TextBlocks.Clear();
            ConsoleContext.UpdateView();
        }

        private void filterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox box)
            {
                return;
            }

            string? text = box.Text;
            if (box.Text == string.Empty)
            {
                text = null;
            }

            ConsoleContext.filterText = text;
            ConsoleContext.UpdateView();
        }
    }
}
