namespace InformationCenterApp.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using AutoMapper.QueryableExtensions;
    using Information.Data;
    using ModelsDto;

    public class ListEmployeesOlderThanCommand:ICommand
    {
        public ListEmployeesOlderThanCommand(UnitOfWork db, string[] info)
        {
            Db = db;
            Info = info;
        }

        public UnitOfWork Db { get; set; }
        public string[] Info { get; set; }

        public void Execute()
        {
            int age = int.Parse(this.Info[0]);

            var employees = Db.Employees.GetAll().ProjectTo<EmployeesBirthdayDto>().ToList().Where(x => x.Age > age)
                .ToList();

           foreach (var e in employees)
            {
                Console.WriteLine("{0} {1} - ${2:f2} Manager:{3}", e.FirstName, e.LastName, e.Salary
                ,e.ManagerName ?? "[no manager]");
            }

          
        }
    }
}
