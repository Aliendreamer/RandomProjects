namespace SIS.HTTP.Cookies
{
    using System;
  

    public class HttpCookie
    {
        private const int HttpDefaultExpirationDays = 3;

        public HttpCookie(string key, string value,int expires=HttpDefaultExpirationDays)
        {
            Key = key;
            Value = value;
            Expires = DateTime.UtcNow.AddDays(expires);
            IsNew = true;
           
        }

        public HttpCookie(string key, string value,bool isNew, int expires=HttpDefaultExpirationDays)
            :this(key,value,expires)
        {
           
            IsNew = isNew;
        }

       
        public string Key { get;}

        public string Value { get;}

        public DateTime Expires { get;}

        public bool IsNew { get;}


        public override string ToString()
            => $"{this.Key}={this.Value}; Expires={this.Expires.ToLongDateString()}";

    }
}
