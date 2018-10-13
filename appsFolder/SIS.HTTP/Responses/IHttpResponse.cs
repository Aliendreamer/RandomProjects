namespace SIS.HTTP.Responses
{
    using Cookies;
    using Enums;
    using Headers;

    public interface IHttpResponse
    {
        HttpResponseStatusCode StatusCode { get; set; }

        IHttpCookieCollection Cookies { get; }

        IHttpHeaderCollection   Headers { get;}

        byte[] Content { get; set;}

        void AddHeader(HttpHeader header);

        byte[] GetBytes();

        void AddCookie(HttpCookie cookie);
    }
}