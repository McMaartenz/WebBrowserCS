﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebBrowser
{
    public enum RequestType
    {
        GET
    }

    public enum RequestStatus
    {
        Blank,
        InProgress,
        Resolved
    }

    public class Request
    {
        public static readonly HttpClient client = new();

        private RequestStatus _status;
        public RequestStatus Status
        {
            get
            {
                return _status;
            }

            private set
            {
                _status = value;
                StatusChanged?.Invoke(this, value);
            }
        }

        public RequestType Type { get; set; }
        public HttpResponseMessage? Response { get; private set; }

        public Uri Uri { get; private set; }
    
        public event EventHandler<RequestStatus>? StatusChanged;
        public event EventHandler<HttpResponseMessage>? Resolved;

        public Request(RequestType type, string url)
        {
            Type = type;
            Uri = new(url);
            Status = RequestStatus.Blank;
        }

        public async Task<HttpResponseMessage> Resolve()
        {
            Status = RequestStatus.InProgress;

            Response = Type switch
            {
                RequestType.GET => await client.GetAsync(Uri),
                _ => throw new NotImplementedException()
            };
            
            Status = RequestStatus.Resolved;
            Resolved?.Invoke(this, Response);

            return Response;
        }

        public override string ToString()
        {
            string responseCode = ((int?)Response?.StatusCode)?.ToString("000") ?? "   ";

            return $"{responseCode} {Type} {Uri.Host} {Uri.PathAndQuery} ({Status})";
        }
    }
}
