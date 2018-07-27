namespace InformationModels
{
    using System;
    using System.Collections.Generic;

    public class Project
    {
        public Project()
        {
            this.Employees=new HashSet<EmployeeProject>();
        }

        public int  ProjectId { get; set; }

        public string Name { get; set; }

        public string Goal { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public virtual ICollection<EmployeeProject> Employees { get; set; }

        public int ManagerId { get; set; }
        public virtual Employee Manager { get; set; }
    }
}
