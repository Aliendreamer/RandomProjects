﻿namespace SIS.HTTP.Headers
{
  

    public class HttpHeader
    {

        public const string Cookie = "Cookie";

        public const string ContentType = "Content-Type";

        public const string ContentLength = "Content-Length";

        public const string ContentDisposition = "Content-Disposition";

        public const string Authorization = "Authorization";

        public const string Host = "Host";

        public const string Location = "Location";


        public HttpHeader(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string  Key { get; set; }

        public string  Value { get; set; }

        public override string ToString()
        {
            return $"{this.Key}: {this.Value}";
        }

    }
}
