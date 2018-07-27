namespace InformationCenterApp.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using AutoMapper.QueryableExtensions;
    using Information.Data;
    using InformationModels;
    using ModelsDto;

    public  class ManagerInfoCommand:ICommand
    {
        public ManagerInfoCommand(UnitOfWork db,string[] info)
        {
            Info = info;
            Db = db;
        }
        public string[] Info { get; set; }
        public UnitOfWork Db { get; set; }

        public void Execute()
        {
            int managerInfo = int.Parse(this.Info[0]);
            var managers = this.Db.Employees.Find(e => e.JobTitle == "Manager").AsQueryable().ProjectTo<ManagerDto>()
                .OrderByDescending(x => x.EmployeeCount).ToList();

          ManagerDto  manager = managers.Any(x => x.EmployeeId == managerInfo) ? managers.FirstOrDefault(x => x.EmployeeId == managerInfo) : managers.First();

            if (manager == null) return;
            {
                StringBuilder sb=new StringBuilder();
                sb.AppendLine($"{manager?.FirstName} {manager?.LastName} | Employees: {manager?.EmployeeCount}");

                foreach (var e in manager.Employees)
                {
                    sb.AppendLine($"    - {e.FirstName} {e.LastName} - ${e.Salary:f2}");
                }

                Console.WriteLine(sb.ToString());
            }
        }
    }
}
