namespace TryNhibernate
{
	using System;
	using Classes;
	using FluentNHibernate.Cfg;
	using FluentNHibernate.Cfg.Db;
	using NHibernate;
	using NHibernate.Tool.hbm2ddl;

	public static class NhibernateHelper
	{

		public static ISession OpenSession()

		{
			try
			{



				string connectionString =
					"Server=(LocalDb)\\.;Database=TestNhibernate;Trusted_Connection=True;MultipleActiveResultSets=true;";

				ISessionFactory sessionFactory = Fluently.Configure()

					.Database(MsSqlConfiguration.MsSql2012

						.ConnectionString(connectionString).ShowSql()

					)

					.Mappings(m =>

						m.FluentMappings

							.AddFromAssemblyOf<Student>())

					.ExposeConfiguration(cfg => new SchemaExport(cfg)

						.Create(false, false))

					.BuildSessionFactory();

				return sessionFactory.OpenSession();


			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				throw;
			}
		}
	}
}
