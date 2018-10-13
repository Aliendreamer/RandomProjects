namespace SIS.HTTP.Cookies
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HttpCookieCollection:IHttpCookieCollection
    { 
        private readonly IDictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies=new Dictionary<string, HttpCookie>();
        }


        public void Add(HttpCookie cookie)
        {
            var cookieKey = cookie.Key;
           
            if (!this.cookies.ContainsKey(cookieKey))
            {
                this.cookies.Add(cookie.Key, cookie);
            }
        }
                   

        public bool ContainsCookie(string key)
        {
            return this.cookies.ContainsKey(key);
        }

        public HttpCookie GetCookie(string key)
        {
            return this.cookies.FirstOrDefault(x => x.Key == key).Value;
        }

        public bool HasCookies()
        {
            return this.cookies.Any();
        }


        public IEnumerator<HttpCookie> GetEnumerator()
        {
            return this.cookies.Select(cookie => cookie.Value).GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join("; ",this.cookies.Values);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
