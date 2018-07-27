namespace Information.Data.Interfaces
{
    using System;
    using InformationCenterApp.ModelsDto;
    using InformationModels;
    public interface IEmployeeRepository:IRepository<Employee>
    {
        void AddEmployee(EmployeeDto dto);

        EmployeeDto ById(int employeeId);

        void SetBirthday(int id,DateTime date);

        void SetAddress(int id, string address);

    }
}