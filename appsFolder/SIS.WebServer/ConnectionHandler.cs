namespace SIS.WebServer
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using HTTP.Cookies;
    using HTTP.Enums;
    using HTTP.Requests;
    using HTTP.Responses;
    using HTTP.Sessions;
    using Results;
    using Routing;

    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly ServerRoutingTable serverRoutingTable;

        public ConnectionHandler(Socket client, ServerRoutingTable serverRoutingTable)
        {
            this.client = client;
            this.serverRoutingTable = serverRoutingTable;
        }

        private async Task<IHttpRequest> ReadRequest()
        {
            var result = new StringBuilder();
            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                int numberOfBytesRead = await this.client.ReceiveAsync(data.Array, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
                result.Append(bytesAsString);

                if (numberOfBytesRead < 1023)
                {
                    break;
                }
            }

            return result.Length != 0 ? new HttpRequest(result.ToString()) : null;
        }

        private string SetRequestSession(IHttpRequest httpRequest)
        {
            string sessionId = null;

            bool sessionActive = httpRequest.Cookies.ContainsCookie(HttpSessionStorage.SessionCookieKey);
            if (sessionActive)
            {
                var cookie = httpRequest.Cookies.GetCookie(HttpSessionStorage.SessionCookieKey);

                sessionId = cookie.Value;

                httpRequest.Session = HttpSessionStorage.GetSession(sessionId);
            }
            else
            {
                sessionId = Guid.NewGuid().ToString();
                httpRequest.Session = HttpSessionStorage.GetSession(sessionId);
            }

            return sessionId;
        }

        private void SetResponseSession(IHttpResponse httpResponse, string sessionId)
        {
            string cookieValue = $"{sessionId}; HttpOnly";

            if (sessionId != null)
            {
                httpResponse.AddCookie(
                    new HttpCookie(HttpSessionStorage.SessionCookieKey, cookieValue));
            }
        }

        private IHttpResponse HandleRequest(IHttpRequest request)
        {
            if (!this.serverRoutingTable.Routes.ContainsKey(request.RequestMethod)
                || !this.serverRoutingTable.Routes[request.RequestMethod].ContainsKey(request.Path.ToLower()))
            {
                return ReturnIfResource(request.Path);
            }

            var response = this.serverRoutingTable.Routes[request.RequestMethod][request.Path].Invoke(request);

            return response;
        }

        //TODO check if it is correct
        //it works at least does not break my site keep an eye on how
        //it will be implemented on lectures!
        private IHttpResponse ReturnIfResource(string path)
        {
            string pathTofile = $"../../..{path}";
            bool fileExist = File.Exists(pathTofile);
            if (!fileExist) return new HttpResponse(HttpResponseStatusCode.NotFound);

            string fileToBytes = File.ReadAllText(pathTofile);
            var bytesTransform = Encoding.UTF8.GetBytes(fileToBytes);
            return new InlineResourceResult(bytesTransform, HttpResponseStatusCode.Found);
        }

        private async Task PrepareResponse(IHttpResponse httpResponse)
        {
            byte[] byteSegments = httpResponse.GetBytes();

            await this.client.SendAsync(byteSegments, SocketFlags.None);
        }

        public async Task ProcessRequestAsync()
        {
            var httpRequest = await this.ReadRequest();

            if (httpRequest != null)
            {
                string sessionId = this.SetRequestSession(httpRequest);

                var httpResponse = this.HandleRequest(httpRequest);

                this.SetResponseSession(httpResponse, sessionId);

                await this.PrepareResponse(httpResponse);
            }

            this.client.Shutdown(SocketShutdown.Both);
        }
    }
}