namespace Information.Data.Interfaces
{
    using InformationCenterApp.ModelsDto;
    using InformationModels;
    using InformationModels.ModelsDto;

    public interface IProjectRepository:IRepository<Project>
    {
        ProjectDto[] GetUnfinishedProjects();

        ProjectDto[] GetProjetsByManagerId(int id);
    }
}