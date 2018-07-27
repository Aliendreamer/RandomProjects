namespace InformationCenterApp.Commands
{
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Information.Data;
    using InformationModels;
    using ModelsDto;

    public class SetManagerCommand:ICommand
    {
        public SetManagerCommand(UnitOfWork db, string[] info)
        {
            Info = info;
            Db = db;
        }

        public string[] Info { get; set; }
        public UnitOfWork Db { get; set; }


        public void Execute()
        {
            int id = int.Parse(this.Info[0]);
            int managerId = int.Parse(this.Info[1]);

            Employee employeeToUpdate = Db.Employees
                .Find(e => e.EmployeeId == id).ToList().FirstOrDefault();

            var managerNeeded = Db.Employees.Find(x => x.JobTitle == "Manager" && x.EmployeeId == managerId).AsQueryable()
                .ProjectTo<EmployeeDto>().ToList().FirstOrDefault();

            if (employeeToUpdate != null) employeeToUpdate.ManagerId= managerNeeded!=null ?managerId :0;

            
            Db.Complete();
        }
    }
}
