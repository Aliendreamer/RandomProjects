namespace CarDealerClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CarDealer.Models;
    using Dtos;
    using Dtos.exportDtos;
    using Dtos.importDtos;

    public class CarDealerProfile:Profile
    {
        public CarDealerProfile()
        {

            CreateMap<Supplier,ImportSuppliersDto>().ReverseMap();

            CreateMap<Part, ImportPartDto>().ReverseMap();

            CreateMap<Car, ImportCarDto>().ReverseMap();

            CreateMap<Customer, ImportCustomerDto>().ReverseMap();

            CreateMap<Car, ExportFerariCars>().ReverseMap();

            CreateMap<Supplier, ExportLocalSuppliers>()
                .ForMember(opt => opt.PartCount, cfg => cfg.MapFrom(x => x.Parts.Count));

            CreateMap<Part, ExportPartDto>().ReverseMap();

            CreateMap<Car, ExportCarDto>();

            CreateMap<Customer, TotalSalesPerCustomerDto>()
                .ForMember(x => x.CarDeals, opt => opt.MapFrom(x => x.Sales.Count))
                .ForMember(а => а.SumPaid, opt => opt.MapFrom(x => x.Sales.
                                                                Select(xd => xd.Car.Parts
                                                                .Sum(xa=>xa.Part.Price)).Sum()));

            CreateMap<Sale, ExportSaleDto>()
                .ForMember(cfg => cfg.Discount, opt => opt.MapFrom(x => Math.Round(x.Discount,2)))
                .ForMember(cfg => cfg.CustomerName, opt => opt.MapFrom(x => x.Customer.Name))
                .ForMember(cfg=>cfg.ExportFerariCars,opt=>opt.MapFrom(x=>x.Car))
                .ForMember(cfg => cfg.Price,
                    opt => opt.MapFrom(x => Math.Round(x.Car.Parts.Sum(p => p.Part.Price),2)));

        }
    }
}
