namespace InformationCenterApp.Commands
{
    using System;
    using Information.Data;
    using ModelsDto;


    public class AddEmployeeCommand:ICommand
    {
        public AddEmployeeCommand(UnitOfWork db,string[]info)
        {
            this.Db = db;
            this.Info = info;
        }

        public string[] Info { get; set; }

        public UnitOfWork Db {get; set;}

        public void Execute()
        {
            string firstName = this.Info[0];
            string lastName = this.Info[1];
            decimal salary = decimal.Parse(this.Info[2]);
            string jobTitle = this.Info[3];

            var employee=new  EmployeeDto(firstName,lastName,salary,jobTitle);
            this.Db.Employees.AddEmployee(employee);
            Db.Complete();
            Console.WriteLine("Sucessfuly passed the changes");

        }
    }
}
