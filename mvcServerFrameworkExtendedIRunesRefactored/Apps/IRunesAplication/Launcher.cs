namespace IRunesApplication
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using IRunes.Services;
    using IRunes.Services.Contracts;
    using SIS.Framework;
    using SIS.Framework.Routers;
    using SIS.Framework.Services;
    using SIS.Framework.Services.Contracts;
    using SIS.WebServer;
    using ViewModels;

    public class Launcher
    {
        public static void Main(string[] args)
        {
            var dependencyMap = new Dictionary<Type, Type>();
            var dependencyContainer = new DependencyContainer(dependencyMap);
            dependencyContainer.RegisterDependency<IHashService, HashService>();
            dependencyContainer.RegisterDependency<IUserService, UserService>();
            dependencyContainer.RegisterDependency<IAlbumService, AlbumService>();
            dependencyContainer.RegisterDependency<ITrackService, TrackService>();
            dependencyContainer.RegisterDependency<IUserCookieService, UserCookieService>();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<IRunesProfile>();
            });

            var handlingContext = new HttpRouteHandlingContext(
                new ControllerRouter(dependencyContainer),
                new ResourceRouter());

            Server server = new Server(8000, handlingContext);
            var engine = new MvcEngine();
            engine.Run(server);
        }
    }
}