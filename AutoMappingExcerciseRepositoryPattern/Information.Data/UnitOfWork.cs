namespace Information.Data
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using InformationModels;
    using Interfaces;
    using Repositories;

    public class UnitOfWork:IUnitOfWork
    {

        public UnitOfWork(InformationDbContext context)
        {
            Context = context;
            Projects =new ProjectRepository(Context);
            Employees = new EmployeeRepository(Context);
            EmployeeProjects=new EmployeeProjectRepository(context);
        }


        public void Dispose()
        {
            Context.Dispose();
        }

        protected InformationDbContext Context { get; private set; }

        public IProjectRepository Projects { get; private set; }
        public IEmployeeRepository Employees { get; private set; }
        public IEmployeeProjectRepository EmployeeProjects { get; private set; }

        public int Complete()
        {
            return Context.SaveChanges();
        }


        public void Seed()
        {
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
            SeedEmployees();
            SeedProjects();
            SeedEmployeeProjects();
        }

        private void SeedEmployees()
        {
            List<Employee> employees = new List<Employee>();
            Random rng = new Random();
            string[] firstNames = new[]
            {
                "Ivan", "Iliana", "Todor", "Pesho", "Gosho", "Qna", "Vasilena", "Elin", "Ilina", "Anton"
            };
            string[] lastNames = new[]
            {
                "Ivanov", "Todorov", "Qvorov", "Peshev", "Topalov", "Vasilev", "Topuzakov", "Modrich", "Mbape", "Smith"
            };
            string[] jobTitle = new[]
            {
                "Researcher", "Tehnician", "Manager", "It guru", "QA dosadnik", "Trader", "Cleaner", "Lab rat",
                "Field agent", "Security"
            };
            decimal[] salaries = new[]
            {
                3000M, 2999M, 999M, 599M, 9999M, 3333M, 4444M, 8888M, 777M, 666M
            };
            DateTime[] dates=new []
            {
                new DateTime(2000,01,10),
                new DateTime(1970,12,10),
                new DateTime(2005,07,22),
                new DateTime(1993,06,10),
                new DateTime(1988,11,20),
                new DateTime(2001,01,18),
                new DateTime(1985,02,10),
                new DateTime(2011,12,02),
                new DateTime(2002,01,10),
                new DateTime(1978,07,10),
            };
            for (int i = 0; i < 100; i++)
            {
                int next = rng.Next(0, 9);
                int othernext = rng.Next(0, 9);
                int thirdnext = rng.Next(0, 9);
                int fourthnext = rng.Next(0, 9);

                Employee emp = new Employee()
                {
                    FirstName = firstNames[fourthnext],
                    LastName = lastNames[next],
                    JobTitle = jobTitle[othernext],
                    Salary = salaries[thirdnext],
                    BirthDay = dates[next]
                };
                employees.Add(emp);
            }

            while (employees.Count(x => x.JobTitle=="Manager")>10)
            {
                int next = rng.Next(0,99);
                var list = Enumerable.Range(0,9).Where(a => a !=2).ToArray();
                int job = rng.Next(0, list.Length - 1);
                if (employees[next].JobTitle == "Manager")
                employees[next].JobTitle = jobTitle[list[job]];
                
            }


            var temporary = employees.Where(x => x.JobTitle != "Manager").ToArray();
            var managers = employees.Where(x => x.JobTitle == "Manager").ToList();

            while (temporary.Any(x => x.Manager == null))
            {

                foreach (var e in temporary)
                {
                    int next = rng.Next(1, managers.Count);
                    e.Manager = managers[next];
                }
            }

            List<Employee> final = temporary.Concat(managers).ToList();
            Context.Employees.AddRange(final);
            Complete();
        }

        private void SeedProjects()
        {
            string format = "dd-MM-yyyy";
            DateTime date = DateTime.ParseExact("01-01-2010", format, CultureInfo.InvariantCulture);
            var managers = Context.Employees.Where(x => x.JobTitle == "Manager").ToList();
            Random rng = new Random();
            Project[] projects = new[]
            {
                new Project()
                {
                    Name = "Save the www",Manager = managers[rng.Next(0,managers.Count-1)],Goal = "Come up with www 2.0",StartDate =date,EndDate = null,
                },

                new Project()
                {
                    Name = "Save the Planet",Manager = managers[rng.Next(0,managers.Count-1)],Goal = "Get less chinese and indians living?",StartDate =date,EndDate = DateTime.Now,
                },

                new Project()
                {
                    Name = "Goal why?",Manager = managers[rng.Next(0,managers.Count-1)],Goal = "Kill all the sheeps!",StartDate =date,EndDate = date.AddYears(6),
                },

                new Project()
                {
                    Name = "Pandas",Manager = managers[rng.Next(0,managers.Count-1)],Goal = "they are cute!",StartDate =date.AddYears(8),EndDate = date.AddYears(3),
                },

                new Project()
                {
                    Name = "ww3",Manager = managers[rng.Next(0,managers.Count-1)],Goal = "Get the nukes rolling",StartDate =DateTime.Now,EndDate = null,
                },

            };

            Context.Projects.AddRange(projects);
            Complete();
        }

        private void SeedEmployeeProjects()
        {
            Employee[] employees = Context.Employees.Where(x => x.JobTitle != "Manager").ToArray();
            Project[] projects = Context.Projects.ToArray();
            List<EmployeeProject> connections = new List<EmployeeProject>();
            Random rng = new Random();
            foreach (var e in employees)
            {
                int next = rng.Next(0, projects.Length + 1);
                if (next > projects.Length - 1) continue;
                EmployeeProject ep = new EmployeeProject() { EmployeeId = e.EmployeeId, ProjectId = projects[next].ProjectId };
                if (connections.Any(n => n.Equals(ep))) continue;
                connections.Add(ep);
            }

            Context.EmployeeProjects.AddRange(connections);
            Complete();
        }
    }
}
