﻿namespace SIS.WebServer.Results
{
    using HTTP.Enums;
    using HTTP.Headers;
    using HTTP.Responses;

    public class RedirectResult:HttpResponse
    {
        public RedirectResult(string location) 
            : base(HttpResponseStatusCode.Found)
        {

            this.Headers.Add(new HttpHeader("Location",location));

        }
    }
}
