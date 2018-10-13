namespace SIS.HTTP.Requests
{
    
    using System.Collections.Generic;
    using Cookies;
    using Enums;
    using Headers;
    using Sessions;

    public interface IHttpRequest
    {

        string Url { get; }

        string Path { get; }

        IDictionary<string, object> FormData { get; }

        IDictionary<string, object> QueryData { get; }

        IHttpHeaderCollection Headers { get; }

        HttpRequestMethod RequestMethod { get; }

        IHttpCookieCollection Cookies { get; }

        IHttpSession Session { get; set;}
    }
}