namespace SIS.HTTP.Headers
{
    using System;
    using System.Collections.Generic;
  
    public class HttpHeaderCollection:IHttpHeaderCollection
    {

        private readonly IDictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers=new Dictionary<string, HttpHeader>();
        }

        public void Add(HttpHeader header)
        {
           

            var headerKey = header.Key;

            if (!this.headers.ContainsKey(headerKey))
            {
                this.headers.Add(headerKey,null);
                
            }
              this.headers[headerKey] = header;
            
        }

        public bool ContainsHeader(string key)
        {           

            return this.headers.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {
            return this.headers[key]??null;
        }

        public override string ToString()
        {
            string result = string.Join(Environment.NewLine, this.headers.Values);

            return result;
        }


    }
}
