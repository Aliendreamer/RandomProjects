namespace IRunesApplication.ViewModels
{
    using AutoMapper;
    using IRunes.Domain;

    public class IRunesProfile : Profile
    {
        public IRunesProfile()
        {
            CreateMap<User, RegisterViewModel>().ReverseMap();

            CreateMap<LoginViewModel, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForAllOtherMembers(dest => dest.Ignore());

            CreateMap<AlbumCreateViewModel, Album>()
                .ForMember(dest => dest.Cover, opt => opt.MapFrom(src => src.AlbumCover))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AlbumName))
                .ForAllOtherMembers(dest => dest.Ignore());

            CreateMap<Album, AlbumDetailsViewModel>()
                .ForMember(dest => dest.AlbumUsers, opt => opt.Ignore()).ReverseMap();

            CreateMap<Track, TrackViewModel>().ReverseMap()
                .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.trackLink))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.trackName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.trackPrice));
        }
    }
}