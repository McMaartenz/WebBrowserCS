using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.DOM
{
    public class Window
    {

        private readonly Pointer<Inspector> _inspectorPtr;
        public Document Document { get; set; }

        public Window(Pointer<Inspector> inspectorPtr)
        {
            _inspectorPtr = inspectorPtr;
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

        public int Width { get; set; }
        public int Height { get; set; }

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
