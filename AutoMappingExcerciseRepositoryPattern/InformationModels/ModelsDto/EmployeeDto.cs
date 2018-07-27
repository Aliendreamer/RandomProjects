namespace InformationCenterApp.ModelsDto
{ 

    public class EmployeeDto
    {

        public EmployeeDto()
        {
            
        }

        public EmployeeDto(string firstName, string lastName, decimal salary,string jobTitle)
        {
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
            JobTitle = jobTitle;
        }

        public int  EmployeeId { get; set; }

        public string FirstName  { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public string  JobTitle { get; set; }
    }
}

