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
        public Pointer<Inspector> InspectorPtr { get; private set; } = new();

        private readonly DOM.Window _browserWindow;

        static MainWindow()
        {
            InstanceManager = new InstanceManager();
        }

        public MainWindow(string url)
        {
            InstanceManager.RegisterInstance(this);
            _browserWindow = new(InspectorPtr);

            InitializeComponent();
            _browserWindow.Location = url;
        }

        public MainWindow() : this("http://duckduckgo.com/") { }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F5:
                {
                    _browserWindow.Location = _browserWindow.Location;
                    break;
                }

                case Key.F12:
                {
                    if (InspectorPtr.Null)
                    {
                        InspectorPtr.Object = new(_browserWindow);
                        InspectorPtr.Object.Closing += (sender, e) => InspectorPtr.Object = null;
                    }

                    try
                    {
                        Inspector inspectorWindow = InspectorPtr.Object!;

                        inspectorWindow.Show();
                        inspectorWindow.Focus();
                    }
                    catch (Exception ex)
                    {
                        InspectorPtr.Object = null;

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
