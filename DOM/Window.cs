using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebBrowser.DOM.Nodes;

namespace WebBrowser.DOM
{
    public class Window
    {
        private readonly Pointer<Inspector> _inspectorPtr;
        private readonly Pointer<IRenderer> _renderPtr;
        public Document Document { get; set; }
        public IConsole Console => _inspectorPtr.Object!;
        public IRenderer Renderer => _renderPtr.Object!;

        public Window(Pointer<IRenderer> renderPtr, Pointer<Inspector> inspectorPtr)
        {
            _renderPtr = renderPtr;
            _inspectorPtr = inspectorPtr;

            Screen = new()
            {
                Height = SystemParameters.FullPrimaryScreenHeight,
                Width = SystemParameters.FullPrimaryScreenWidth
            };

            Document = new(this);
        }

        private string _location = "about:blank";
        public string Location
        {
            get => _location;

            set
            {
                _location = value;
                _ = Navigate();
            }
        }

        public Screen Screen { get; set; }

        public double OuterWidth => Renderer.OuterWidth();
        public double OuterHeight => Renderer.OuterHeight();

        public double InnerWidth => Renderer.InnerWidth();
        public double InnerHeight => Renderer.InnerHeight();

        private async Task Navigate()
        {
            HttpResponseMessage response = await GET(Location);
            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            string body = await response.Content.ReadAsStringAsync();
            Document = new(this);
            Document.LoadHTML(body);
        }

        private async Task<HttpResponseMessage> GET(string url)
        {
            Request request = new(RequestType.GET, url);
            _inspectorPtr.Object?.ObserveRequest(request);
            return await request.Resolve();
        }
    }
}
