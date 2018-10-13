namespace IRunesAplication
{
    using Controllers;
    using IRunes.Data;
    using SIS.HTTP.Enums;
    using SIS.WebServer;
    using SIS.WebServer.Results;
    using SIS.WebServer.Routing;

    public class StartUp
    {
        public static void Main()
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            AppRoutes(serverRoutingTable);

            InitializeDb();

            Server server = new Server(8000, serverRoutingTable);

            server.Run();
        }

        private static void InitializeDb()
        {
            var db = new IRunesDbContext();
            db.Database.EnsureCreated();
        }

        private static void AppRoutes(ServerRoutingTable serverRoutingTable)
        {
            // {controller}/{action}/{id}
            //Get
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] = request =>
              new RedirectResult("/home/index");

            //get
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/home/index"] = request =>
                new HomeController().Index(request);

            //Get
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/register"] = request =>
                new UsersController().Register();

            //post
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/register"] = request =>
                new UsersController().Register(request);

            //get
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/login"] = request =>
                new UsersController().Login();

            //post
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/login"] = request =>
                new UsersController().Login(request);

            //get log out users
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/logout"] = request =>
                new UsersController().Logout(request);

            //get all albums per user
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/all"] = request =>
                  new AlbumsController().All();

            //get
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/create"] = request =>
                  new AlbumsController().Create();
            //post
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/albums/create"] = request =>
                 new AlbumsController().Create(request);

            //get
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/albums/details"] = request =>
                new AlbumsController().Details(request.QueryData["id"].ToString());

            //get
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/tracks/create"] = request =>
        new TracksController().Create(request.QueryData["albumId"].ToString());

            //post
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/tracks/create"] = request =>
        new TracksController().Create(request);

            //get track details
            //(route=”/Tracks/Details?albumId={albumId}&trackId={trackId}”)
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/tracks/details"] = request =>
                new TracksController()
                    .Details(request.QueryData["albumId"].ToString(), request.QueryData["trackId"].ToString());
        }
    }
}