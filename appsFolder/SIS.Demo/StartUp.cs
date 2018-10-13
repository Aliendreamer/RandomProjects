namespace SIS.Demo
{
   
    using HTTP.Enums;
    using WebServer;
    using WebServer.Routing;

    public class StartUp
    {
        public static void Main()
        {
            
            ServerRoutingTable serverRoutingTable=new ServerRoutingTable();

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"]=request=>new HomeController().Index();
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/Register"]=request=>new HomeController().Register();
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/Register"]=request=>new HomeController().Register(request.FormData);

            Server server=new Server(8000,serverRoutingTable);

            server.Run();
        }
    }
}
