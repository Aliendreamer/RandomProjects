namespace CarDealerClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CarDealer.Data;
    using CarDealer.Models;
    using Dtos;
    using Dtos.exportDtos;
    using Dtos.importDtos;
    using Microsoft.EntityFrameworkCore;

    class StartUp
    {
        static void Main()
        {
            var context = new CarDealerContext();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());
            IMapper mapper = config.CreateMapper();

            // Run(mapper,context);
            SalesWithAppliedDiscount(mapper, context);
        }

        public static void Run(IMapper mapper, CarDealerContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            InitializedData(mapper, context);

            Console.WriteLine("Data and Db initialized!");

            GenerateXmlQueries(mapper, context);

            Console.WriteLine("Xmls generated");
        }

        public static void InitializedData(IMapper mapper, CarDealerContext context)
        {
            LoadSuppliers(mapper, context);
            LoadParts(mapper, context);
            LoadCars(mapper, context);
            LoadCustomers(mapper, context);
            CreateSales(mapper, context);
        }

        public static void LoadSuppliers(IMapper mapper, CarDealerContext context)
        {
            var xmlStream = File.ReadAllText("Xml/suppliers.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportSuppliersDto[]), new XmlRootAttribute("suppliers"));
            var suppliersfromFile = (ImportSuppliersDto[])serializer.Deserialize(new StringReader(xmlStream));

            List<Supplier> suppliers = suppliersfromFile.Select(mapper.Map<Supplier>).ToList();
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

        }

        public static void LoadParts(IMapper mapper, CarDealerContext context)
        {
            var xmlStream = File.ReadAllText("Xml/parts.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(ImportPartDto[]), new XmlRootAttribute("parts"));
            var partsFromFile = (ImportPartDto[])serializer.Deserialize(new StringReader(xmlStream));

            List<Part> parts = partsFromFile.Select(mapper.Map<Part>).ToList();

            context.SaveChanges();
            var suppliers = context.Suppliers.Select(x => x.SupplierId).ToList();
            Random rng = new Random();
            for (int i = 0; i < parts.Count; i++)
            {
                int supplierId = rng.Next(0, suppliers.Count - 1);
                parts[i].SupplierId = suppliers[supplierId];
            }
            context.Parts.AddRange(parts);
            context.SaveChanges();

        }

        public static void LoadCars(IMapper mapper, CarDealerContext context)
        {

            var xmlstring = File.ReadAllText("Xml/cars.xml");
            var serializer = new XmlSerializer(typeof(ImportCarDto[]), new XmlRootAttribute("cars"));
            var cars = (ImportCarDto[])serializer.Deserialize(new StringReader(xmlstring));
            context.Cars.AddRange(cars.Select(mapper.Map<Car>).ToList());
            context.SaveChanges();

            Random rng = new Random();
            var parts = context.Parts.AsNoTracking().ToList();
            var carsFromDbb = context.Cars.AsNoTracking().ToList();

            foreach (var car in carsFromDbb)
            {
                List<PartCar> conn = new List<PartCar>();

                int numberOfParts = rng.Next(10, 20);
                Console.WriteLine("I know it takes time 2mins on average pc but God only thing " +
                                  "I came up with! to lie ef" +
                                  "Maybe playing with detach attach modified state can simplify things");

                for (int i = 0; i < numberOfParts; i++)
                {
                    int part = rng.Next(0, parts.Count - 1);
                    PartCar pc = new PartCar()
                    {
                        CarId = car.CarId,
                        PartId = parts[part].PartId
                    };
                    if (conn.Any(x => x.PartId == pc.PartId))
                    {
                        i--;
                        continue;

                    }
                    conn.Add(pc);
                    context.PartCars.Add(pc);
                    context.SaveChanges();
                }

            }

        }

        public static void LoadCustomers(IMapper mapper, CarDealerContext context)
        {


            var xmlstring = File.ReadAllText("Xml/customers.xml");
            var serializer = new XmlSerializer(typeof(ImportCustomerDto[]), new XmlRootAttribute("customers"));
            var customersFromXml = (ImportCustomerDto[])serializer.Deserialize(new StringReader(xmlstring));

            List<Customer> customers = customersFromXml.Select(mapper.Map<Customer>).ToList();

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        public static void CreateSales(IMapper mapper, CarDealerContext context)
        {
            double[] discounts = new[] { 0.00, 0.05, 0.1, 0.15, 0.20, 0.30, 0.4, 0.50 };
            IList<Sale> sales = new List<Sale>();
            var cars = context.Cars.Select(x => x.CarId).ToList();
            var customers = context.Customers.Select(x => x.CustomerId).ToList();
            Random rng = new Random();
            for (int i = 0; i < 100; i++)
            {
                int carRandom = rng.Next(0, cars.Count - 1);
                int customerRandom = rng.Next(0, customers.Count - 1);
                int discountRandom = rng.Next(0, discounts.Length - 1);
                var sale = new Sale
                {
                    CarId = cars[carRandom],
                    CustomerId = customers[customerRandom],
                    Discount = discounts[discountRandom]
                };
                sales.Add(sale);
            }
            context.Sales.AddRange(sales);
            context.SaveChanges();
        }

        public static void GenerateXmlQueries(IMapper mapper, CarDealerContext context)
        {
            GetCarsWithDistance(mapper, context);
            GetCarsWithFerariMake(mapper, context);
            GetLocalSuppliers(mapper, context);
            GetCarsAndParts(mapper, context);
            TotalSaleByCustomer(mapper, context);
            SalesWithAppliedDiscount(mapper, context);

        }

        public static void GetCarsWithDistance(IMapper mapper, CarDealerContext context)
        {
            var cars = context.Cars.Where(x => x.Distance > 2_000_000)
                .ProjectTo<ImportCarDto>(mapper.ConfigurationProvider).ToArray();

            StringBuilder sb = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(ImportCarDto[]), new XmlRootAttribute("cars"));
            serializer.Serialize(new StringWriter(sb), cars, xmlNamespaces);
            File.WriteAllText("../../../Xml/cars-with-distance.xml", sb.ToString());
        }

        public static void GetCarsWithFerariMake(IMapper mapper, CarDealerContext context)
        {
            var cars = context.Cars.Where(x => x.Make == "Ferrari")
                .ProjectTo<ExportFerariCars>(mapper.ConfigurationProvider).ToArray();

            StringBuilder sb = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(ExportFerariCars[]), new XmlRootAttribute("cars"));
            serializer.Serialize(new StringWriter(sb), cars, xmlNamespaces);
            File.WriteAllText("../../../Xml/ferari-cars.xml", sb.ToString());
        }

        public static void GetLocalSuppliers(IMapper mapper, CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(x => x.IsImporter == false)
                .ProjectTo<ExportLocalSuppliers>(mapper.ConfigurationProvider)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(ExportLocalSuppliers[]), new XmlRootAttribute("suppliers"));
            serializer.Serialize(new StringWriter(sb), suppliers, xmlNamespaces);
            File.WriteAllText("../../../Xml/local-suppliers.xml", sb.ToString());
        }

        public static void GetCarsAndParts(IMapper mapper, CarDealerContext context)
        {
            var carsParts = context.Cars
                .Include(x => x.Parts)
                .ThenInclude(x => x.Part)
                .Select(x => new ExportCarDto
                {
                    Make = x.Make,
                    Model = x.Model,
                    Distance = x.Distance,
                    Parts = x.Parts.Select(z => new ExportPartDto
                    {
                        Price = z.Part.Price,
                        Name = z.Part.Name

                    }).ToList()

                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(ExportCarDto[]), new XmlRootAttribute("cars"));
            var xmlNameSpaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), carsParts, xmlNameSpaces);
            File.WriteAllText("../../../Xml/cars-and-parts.xml", sb.ToString());


        }

        public static void TotalSaleByCustomer(IMapper mapper, CarDealerContext context)
        {
            var customers = context.Customers.Include(x => x.Sales)
                .ThenInclude(x => x.Car)
                .ThenInclude(x => x.Parts)
                .Where(x => x.Sales.Any())
                .ProjectTo<TotalSalesPerCustomerDto>(mapper.ConfigurationProvider)
                .OrderByDescending(x => x.SumPaid).ThenBy(x => x.CarDeals)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            var xmlnamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(TotalSalesPerCustomerDto[]), new XmlRootAttribute("customers"));
            serializer.Serialize(new StringWriter(sb), customers, xmlnamespaces);
            File.WriteAllText("../../../Xml/customers-total-sales.xml", sb.ToString());
        }

        public static void SalesWithAppliedDiscount(IMapper mapper, CarDealerContext context)
        {
            var sales = context.Sales.ProjectTo<ExportSaleDto>(mapper.ConfigurationProvider).ToArray();

            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(ExportSaleDto[]), new XmlRootAttribute("sales"));
            var xmlNameSpaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), sales, xmlNameSpaces);
            File.WriteAllText("../../../Xml/sales-discounts.xml", sb.ToString());

        }
    }
}
