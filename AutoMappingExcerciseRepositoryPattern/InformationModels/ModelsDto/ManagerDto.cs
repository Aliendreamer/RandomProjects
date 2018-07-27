namespace InformationCenterApp.ModelsDto
{
    using System.Collections.Generic;
    using InformationModels;

    public class ManagerDto
    {
        public ManagerDto()
        {
            this.Employees = new HashSet<Employee>();
        }
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public int EmployeeCount => this.Employees.Count;
    }
}
