namespace Information.Data.Repositories
{
    using InformationModels;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class EmployeeProjectRepository : Repository<EmployeeProject>, IEmployeeProjectRepository
    {
        public EmployeeProjectRepository(DbContext context) : base(context)
        {

        }

        private  InformationDbContext InformationDbContext => Context as InformationDbContext;
    }
}

