namespace InformationModels
{
    using System;
    using System.Collections.Generic;

    public class Employee
    {
        public Employee()
        {
            this.Projects=new HashSet<EmployeeProject>();
            this.ManagerProjects=new HashSet<Project>();
            this.Employees=new HashSet<Employee>();
        }

        public int EmployeeId { get; set; }
        public string  FirstName { get; set; }

        public string LastName { get; set; }

        public decimal  Salary { get; set; }

        public DateTime? BirthDay { get; set; }

        public string JobTitle { get; set; }

        public string  Address { get; set; }        

        public virtual ICollection<EmployeeProject> Projects { get; set; }

        public int? ManagerId { get; set; }
        public virtual Employee Manager { get; set; }

        public virtual ICollection<Project> ManagerProjects { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
