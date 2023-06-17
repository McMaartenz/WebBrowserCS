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

using DOMWindow = WebBrowser.DOM.Window;

namespace WebBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IRenderer
    {
        public readonly static InstanceManager InstanceManager;
        public Pointer<Inspector> InspectorPtr { get; private set; } = new();
        public Pointer<IRenderer> RendererPtr { get; init; } = new();

        private readonly DOMWindow _browserWindow;

        static MainWindow()
        {
            InstanceManager = new InstanceManager();
        }

        public MainWindow(string url)
        {
            InstanceManager.RegisterInstance(this);

            RendererPtr.Object = this;

            _browserWindow = new(RendererPtr, InspectorPtr);
            InspectorPtr.Object = new(_browserWindow);

            InitializeComponent();
            _browserWindow.Location = url;
        }

        public MainWindow() : this("http://localhost:7357/webbrowser/index.html") { }

        public void Render(UIElement? element)
        {
            RenderField.Children.Clear();

            if (element is not null)
            {
                RenderField.Children.Add(element);
            }
        }

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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (InspectorPtr.Object is not null)
            {
                Inspector inspector = InspectorPtr.Object;
                inspector.ShouldClose = true;
                inspector.Close();
            }
        }
    }
}
