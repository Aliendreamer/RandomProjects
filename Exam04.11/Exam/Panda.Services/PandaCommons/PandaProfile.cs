namespace Panda.Services.PandaCommons
{
    using AutoMapper;
    using Domain.Enums;
    using Domain.Models;
    using System.Globalization;
    using Infrastructure.ViewModels.InputModels;
    using Infrastructure.ViewModels.OutputModels;

    public class PandaProfile : Profile
    {
        public PandaProfile()
        {
            CreateMap<Receipt, ReceiptDisplayModel>()
                .ForMember(dest => dest.Recipient, conf => conf.MapFrom(a => a.Recipient.Username))
                .ForMember(dest => dest.IssuedOn, conf => conf.MapFrom(a => a.IssuedOn.ToShortDateString()))
                .ForMember(dest => dest.Fee, conf => conf.MapFrom(a => a.Fee))
                .ForMember(dest => dest.Id, conf => conf.MapFrom(a => a.Id))
                .ReverseMap();

            CreateMap<Receipt, ReceiptDetailsViewModel>()
                .ForMember(dest => dest.Recipient, conf => conf.MapFrom(a => a.Recipient.Username))
                .ForMember(dest => dest.IssuedOn, conf => conf.MapFrom(a => a.IssuedOn.ToShortDateString()))
                .ForMember(dest => dest.Fee, conf => conf.MapFrom(a => a.Fee))
                .ForMember(dest => dest.Id, conf => conf.MapFrom(a => a.Id))
                .ForMember(dest => dest.DeliveryAddress, conf => conf.MapFrom(a => a.Package.ShippingAddress))
                .ForMember(dest => dest.Description, conf => conf.MapFrom(a => a.Package.Description))
                .ForMember(dest => dest.Weight, conf => conf.MapFrom(a => a.Package.Weight)).ReverseMap();

            CreateMap<User, RegisterInputViewModel>().ReverseMap()
                .ForMember(dest => dest.Username, conf => conf.MapFrom(u => u.Username))
                .ForMember(dest => dest.Password, conf => conf.MapFrom(u => u.Password))
                .ForMember(dest => dest.Email, conf => conf.MapFrom(u => u.Email))
                .ForMember(dest => dest.Role, conf => conf.UseValue(UserRole.User));

            CreateMap<User, CreateDropDownMenuModel>()
                .ForMember(dest => dest.Username, conf => conf.MapFrom(a => a.Username));

            CreateMap<PackageCreateModel, Package>()
                .ForMember(dest => dest.Description, conf => conf.MapFrom(u => u.Description))
                .ForMember(dest => dest.ShippingAddress, conf => conf.MapFrom(u => u.ShippingAddress))
                .ForMember(dest => dest.Weight, conf => conf.MapFrom(u => u.Weight))
                .ForMember(dest => dest.Id, conf => conf.Ignore())
                .ForMember(dest => dest.EstimatedDeliveryDate, conf => conf.AllowNull())
                .ForMember(dest => dest.Recipient, conf => conf.Ignore())
                .ForMember(dest => dest.Status, conf => conf.UseValue(PackageStatus.Pending));

            CreateMap<Package, PackageDetailsViewModel>()
                .ForMember(dest => dest.Description, conf => conf.MapFrom(u => u.Description))
                .ForMember(dest => dest.Address, conf => conf.MapFrom(u => u.ShippingAddress))
                .ForMember(dest => dest.Weight, conf => conf.MapFrom(u => u.Weight))
                .ForMember(dest => dest.DeliveryDate, conf => conf.MapFrom(u => u.EstimatedDeliveryDate.Value.ToString("M/d/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.DeliveryDate, conf => conf.ResolveUsing<PackageDateResolver>())
                .ForMember(dest => dest.Recipient, conf => conf.MapFrom(а => а.Recipient.Username))
                .ForMember(dest => dest.Status, conf => conf.MapFrom(a => a.Status));

            CreateMap<Package, PackageViewModel>()
                .ForMember(dest => dest.Description, conf => conf.MapFrom(u => u.Description))
                .ForMember(dest => dest.Id, conf => conf.MapFrom(u => u.Id))
                .ForMember(dest => dest.Weight, conf => conf.MapFrom(u => u.Weight))
                .ForMember(dest => dest.ShippingAddress, conf => conf.MapFrom(u => u.ShippingAddress))
                .ForMember(dest => dest.Number, conf => conf.Ignore())
                .ForMember(dest => dest.Recipient, conf => conf.MapFrom(u => u.Recipient.Username));
        }
    }
}