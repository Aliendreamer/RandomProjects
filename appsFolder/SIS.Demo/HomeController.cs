namespace SIS.Demo
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using HTTP.Enums;
    using HTTP.Responses;
    using WebServer.Results;

    public class HomeController
    {

        public IHttpResponse Index()
        {

            string content = "<h1>Hello</h1>";

            return  new HtmlResult(content,HttpResponseStatusCode.Ok);
        }

        
        public IHttpResponse Register()
        {
           string result= File.ReadAllText(@"F:\csharp\CsharpWeb\csharpweb092018\appsFolder\SIS.Demo\add.html");

            string content = result;


            return new HtmlResult(content,HttpResponseStatusCode.Ok);
        }

        public IHttpResponse Register(IDictionary<string,object>data)
        {
            string result = File.ReadAllText(@"F:\csharp\CsharpWeb\csharpweb092018\appsFolder\SIS.Demo\add.html");

            string content = result;
            foreach (var d in data)
            {
                content = content.Replace($"{{{{{{{d.Key}}}}}}}", d.Value.ToString());

            }

            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }
    }
}
