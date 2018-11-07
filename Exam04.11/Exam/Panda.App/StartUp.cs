namespace Panda.App
{
    using Data;
    using Services;
    using AutoMapper;
    using System.Linq;
    using Domain.Enums;
    using Domain.Models;
    using SIS.Framework.Api;
    using Services.Interfaces;
    using SIS.Framework.Services;
    using Services.PandaAutomapper;

    public class StartUp : MvcApplication
    {
        public override void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<PandaProfile>();
            });
            InitializeDb();
        }

        public override void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<IUserService, UserService>();
            dependencyContainer.RegisterDependency<IReceiptsService, ReceiptsService>();
            dependencyContainer.RegisterDependency<IHomeService, HomeService>();
            dependencyContainer.RegisterDependency<IPackageService, PackageService>();
        }

        private void InitializeDb()
        {
            using (var db = new PandaDb())
            {
                db.Database.EnsureCreated();

                if (db.Users.Any()) return;
                db.Users.Add(new User
                {
                    Role = UserRole.Admin,
                    Username = "Admin",
                    Password = "admin",
                    Email = "admin@admin.com"
                });
                db.SaveChanges();
            }
        }
    }
}