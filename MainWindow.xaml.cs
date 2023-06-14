using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly static InstanceManager InstanceManager;
        private Inspector? _inspectorWindow;

        private readonly DOM.Window _browserWindow;

        static MainWindow()
        {
            InstanceManager = new InstanceManager();
        }

        public MainWindow(string url)
        {
            InstanceManager.RegisterInstance(this);
            _browserWindow = new()
            {
                Url = url
            };

            InitializeComponent();
        }

        public MainWindow() : this("https://duckduckgo.com") { }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F12:
                {
                    if (_inspectorWindow is null)
                    {
                        _inspectorWindow = new(_browserWindow);
                        _inspectorWindow.Closing += (sender, e) => _inspectorWindow = null;
                    }

                    try
                    {
                        _inspectorWindow?.Show();
                        _inspectorWindow?.Focus();
                    }
                    catch (Exception ex)
                    {
                        _inspectorWindow = null;

                        MessageBox.Show(
                            $"Could not display inspector window: {ex}",
                            "Inspector Window",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                    
                    break;
                }

                default:
                {
                    break;
                }
            }
        }
    }
}
