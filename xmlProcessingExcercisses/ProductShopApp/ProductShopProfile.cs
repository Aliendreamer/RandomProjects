namespace ProductShopApp
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Dtos.export;
    using Dtos.import;
    using productShopDatabase.Models;
 


    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Product, ProductInRangeDto>()
                .ForMember(cfg=>cfg.Buyer,m=>
                    m.MapFrom(src=>src.Byer.FirstName!=null?src.Byer.FirstName+" "+src.Byer.LastName:src.Byer.LastName));

            CreateMap<Product, SoldProductsDto>().ReverseMap();

            CreateMap<User, UserDtoSoldItems>()
                .ForMember(opt => opt.SoldProducts, x => x.MapFrom(cfg => cfg.SoldProducts));

            CreateMap<Category, CountByCategoryDto>()
                .ForMember(opt => opt.ProductCount, cfg => cfg.MapFrom(x => x.Products.Count))
                .ForMember(opt => opt.AveragePrice, cfg => cfg.MapFrom(p => Math.Round(p.Products.Average(x => x.Product.Price),6)))
                .ForMember(opt => opt.TotalRevenue, cfg => cfg.MapFrom(p =>Math.Round(p.Products.Sum(x => x.Product.Price),2)));

            CreateMap<User, UserDtoUserInfoOnly>().ReverseMap();
            CreateMap<Product, ProductDtoFourthProblem>().ReverseMap();
        }
    }
}
