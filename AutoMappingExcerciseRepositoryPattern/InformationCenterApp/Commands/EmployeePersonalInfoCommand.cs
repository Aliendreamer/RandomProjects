namespace InformationCenterApp.Commands
{
    using System;
    using AutoMapper;
    using Information.Data;
    using InformationModels;
    using InformationModels.ModelsDto;
    
    public class EmployeePersonalInfoCommand:ICommand
    {
        public EmployeePersonalInfoCommand(UnitOfWork db, string[] info)
        {
            this.Db = db;
            this.Info = info;
        }

        public string[] Info { get; set; }
        public UnitOfWork Db { get; set; }

        public void Execute()
        {
            string format = "dd-MM-yyyy";
            int employeeId = int.Parse(this.Info[0]);
            Employee em = this.Db.Employees.Get(employeeId);
            var info = Mapper.Map<EmployeeDetailsDto>(em);
            string birtday = "[no birtday set!]";

            if (info.Birthday.HasValue)
            {
               birtday= info.Birthday?.ToString(format);
            }

            string address = info.Address ?? "[no address set!]";

            string result = $"ID:{info.EmployeeId} - {info.FirstName} {info.LastName} - ${info.Salary:f2}" +
                            $"{Environment.NewLine}{birtday}{Environment.NewLine}" +
                            $"Address: {address}";

            Console.WriteLine(result);

        }
    }
}
