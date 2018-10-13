namespace SIS.HTTP.Responses
{
    using System;
    using System.Linq;
    using System.Text;
    using Common;
    using Cookies;
    using Enums;
    using Extensions;
    using Headers;

    public class HttpResponse : IHttpResponse
    {

        public HttpResponse(HttpResponseStatusCode statusCode)
        {
            this.Cookies=new HttpCookieCollection();
            this.Headers = new HttpHeaderCollection();
            this.Content = new byte[0];
            this.StatusCode = statusCode;
        }

        public HttpResponseStatusCode StatusCode { get; set; }
        public IHttpHeaderCollection Headers { get; set; }
        public IHttpCookieCollection Cookies { get; set; }
        public byte[] Content { get; set; }


        public void AddHeader(HttpHeader header)
        {
            this.Headers.Add(header);
        }

        public byte[] GetBytes()
        {
         

            var bytesTransform = Encoding.ASCII.GetBytes(ToString()).Concat(this.Content).ToArray();

            return bytesTransform;
        }

        public void AddCookie(HttpCookie cookie)
        {
           this.Cookies.Add(cookie);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb
                .AppendLine($"{GlobalConstants.HttpOneProtocolFragment} {this.StatusCode.GetResponseLine()}")
                .Append($"{this.Headers}")
                .Append(Environment.NewLine);

            if (this.Cookies.HasCookies())
            {
                foreach (var cookie in Cookies)
                {
                    sb.Append($"Set-Cookie: {cookie}").Append(Environment.NewLine);
                }
                
            }
            
            sb.Append(Environment.NewLine);

            return sb.ToString();
        }
    }
}
