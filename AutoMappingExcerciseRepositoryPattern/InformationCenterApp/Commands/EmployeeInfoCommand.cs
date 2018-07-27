namespace InformationCenterApp.Commands
{
    using System;
    using Information.Data;

    public class EmployeeInfoCommand:ICommand
    {
        public EmployeeInfoCommand(UnitOfWork db, string[] info)
        {
            Db = db;
            Info = info;
        }

        public UnitOfWork Db { get; set; }
        public string[] Info { get; set; }

        public void Execute()
        {
            int id = int.Parse(this.Info[0]);
            var info=Db.Employees.ById(id);

            string result = $"ID:{info.EmployeeId} - {info.FirstName} {info.LastName} - ${info.Salary:f2}";

            Console.WriteLine(result);

        }
    }
}
