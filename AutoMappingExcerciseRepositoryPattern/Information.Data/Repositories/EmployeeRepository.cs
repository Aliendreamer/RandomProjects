namespace Information.Data.Repositories
{
    using System;
    using AutoMapper;
    using InformationCenterApp.ModelsDto;
    using InformationModels;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;


    public class EmployeeRepository: Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(InformationDbContext context)
            :base(context){}


        private  InformationDbContext InformationDbContext => Context as InformationDbContext;

        public void AddEmployee(EmployeeDto dto)
        {
            Employee emp = Mapper.Map<Employee>(dto);
            InformationDbContext.Employees.Add(emp);
           
        }

        public EmployeeDto ById(int employeeId)
        {
            var employee = InformationDbContext.Employees.Find(employeeId);
            EmployeeDto empDto = Mapper.Map<EmployeeDto>(employee);
            return empDto;
        }

        public void SetBirthday(int id, DateTime date)
        {
            Employee emp = InformationDbContext.Employees.Find(id);
            emp.BirthDay = date;
        }

        public void SetAddress(int id,string address)
        {
            Employee emp = InformationDbContext.Employees.Find(id);
            emp.Address= address;
        }
    }
}
