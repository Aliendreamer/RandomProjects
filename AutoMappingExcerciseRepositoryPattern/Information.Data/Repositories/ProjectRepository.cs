namespace Information.Data.Repositories
{
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using InformationModels;
    using InformationModels.ModelsDto;
    using Interfaces;


    public class ProjectRepository:Repository<Project>,IProjectRepository
    {

        public ProjectRepository(InformationDbContext context) 
            : base(context)
        {

        }

        private  InformationDbContext InformationDbContext => Context as  InformationDbContext;

        public ProjectDto[] GetUnfinishedProjects()
        {
           var pDto= this.InformationDbContext.Projects.Where(p => p.EndDate == null).ProjectTo<ProjectDto>().ToArray();

            return pDto;

        }

        public ProjectDto[] GetProjetsByManagerId(int id)
        {
            var pDto = this.InformationDbContext.Projects.Where(p => p.ManagerId==id).ProjectTo<ProjectDto>().ToArray();

            return pDto;
        }
    }
}
