namespace Information.Data.Interfaces
{
    using System;

    public interface IUnitOfWork:IDisposable
    {
       
        IProjectRepository Projects { get; }
        IEmployeeRepository Employees { get; }
        IEmployeeProjectRepository EmployeeProjects { get; }
        int Complete();
    }
}