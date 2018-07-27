namespace InformationCenterApp
{
    using System;
    using AutoMapper;
    using InformationModels;
    using InformationModels.ModelsDto;
    using ModelsDto;

    public class InformationDataProfile:Profile
    {
        public InformationDataProfile()
        {
            CreateMap<Employee,EmployeeDetailsDto>();
            CreateMap<EmployeeDetailsDto, Employee>();

            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();

            CreateMap<Employee, ManagerDto>();
            CreateMap<ManagerDto, Employee>();

            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();

           CreateMap<Employee,EmployeesBirthdayDto>()
               .ForMember(dto=>dto.Age,
                   opt=>opt.MapFrom(x=>Math.Abs(x.BirthDay.Value.Year - DateTime.Now.Year)))
                .ForMember(dto=>dto.ManagerName,
                   opt=>opt.MapFrom(m=>m.Manager.FirstName));
        }
    }
}
