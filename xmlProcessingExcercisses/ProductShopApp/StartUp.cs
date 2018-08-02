namespace ProductShopApp
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
    using Dtos.export;
    using Dtos.import;
    using productShopDatabase.Models;
    using productShopDatabaseData;
    using DataAnotations=System.ComponentModel.DataAnnotations;


    public class StartUp
    {
        static void Main()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>());
            IMapper mapper = config.CreateMapper();
            ProductShopDatabase db = new ProductShopDatabase();
            Run(mapper,db);



        }

        public static void Run(IMapper mapper, ProductShopDatabase db)
        {
            InitializeData(mapper, db);

            Console.WriteLine("Database is initialized!");

            DesirializeObjects(mapper, db);

            Console.WriteLine("xml files are created!");
        }

        public static void InitializeData(IMapper mapper, ProductShopDatabase db)
        {
            InitializeDb(db);
            ReadUsers(mapper, db);
            ReadProducts(mapper, db);
            ReadCategories(mapper, db);
            FillCategoryProducts(db);
        }

        public static void DesirializeObjects(IMapper mapper, ProductShopDatabase db)
        {
            // categoriesbyproductcount I know it does not show the nulls but id does not make sense
            // to show averages of null category 
            ProductsInRange(mapper, db);
            SoldProducts(mapper, db);
            CategoriesByProductCount(db, mapper);
            UsersandProducts(mapper, db);


        }

        public static bool IsValid(object obj)
        {
            var validationContext = new DataAnotations.ValidationContext(obj);
            var validationResults = new List<DataAnotations.ValidationResult>();
            return DataAnotations.Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }

        public static void CategoriesByProductCount(ProductShopDatabase db, IMapper mapper)
        {
            var categories = db.Categories.Where(x=>x.Products.Any()).ProjectTo<CountByCategoryDto>(mapper.ConfigurationProvider)
                .ToArray().OrderBy(x=>x.ProductCount);

            StringBuilder sb=new StringBuilder();
            var serializer=new XmlSerializer(typeof(CountByCategoryDto[]),new XmlRootAttribute("categories"));
            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), categories, xmlNamespaces);


            File.WriteAllText("../../../Xml/categories-by-products.xml", sb.ToString());
        }

        public static void FillCategoryProducts(ProductShopDatabase db)
        {
            IList<int> products = db.Products.Select(p=>p.Id).ToList();
            List<int> categories = db.Categories.Select(c => c.Id).ToList();
            List<CategoryProduct>categoryProducts=new List<CategoryProduct>();
            Random rng=new Random();

            for(int i=0;i<products.Count;i++)
            {
                int randomCategory = rng.Next(0, categories.Count - 1);
                CategoryProduct cp = new CategoryProduct()
                {
                    ProductId = products[i],
                    CategoryId = categories[randomCategory]
                };
                categoryProducts.Add(cp);
            }

            db.CategoryProducts.AddRange(categoryProducts);

            db.SaveChanges();
        }

        public static void ReadCategories(IMapper mapper, ProductShopDatabase db)
        {
  
            List<Category> categories = new List<Category>();

            var xmlStream = File.ReadAllText("../../../Xml/categories.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));
            var categoriesFromFile = (CategoryDto[]) serializer.Deserialize(new StringReader(xmlStream));

            foreach (CategoryDto categoryDto in categoriesFromFile)
            {
                if (!IsValid(categoryDto))
                {
                    continue;
                }

                Category category = mapper.Map<Category>(categoryDto);
                categories.Add(category);

            }

            db.Categories.AddRange(categories);
            db.SaveChanges();
        }

        public static void ReadProducts(IMapper mapper, ProductShopDatabase db)
        {
            List<User> currentUsers = db.Users.ToList();
            Random rng = new Random();
            List<Product> products = new List<Product>();
            var xmlStream = File.ReadAllText("../../../Xml/products.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));
            var productsFromFile = (ProductDto[])serializer.Deserialize(new StringReader(xmlStream));

            foreach (ProductDto productDto in productsFromFile)
            {
                if (!IsValid(productDto))
                {
                    continue;
                }

                int randomSeller= rng.Next(0, currentUsers.Count - 1);
                int randomByer = rng.Next(0, currentUsers.Count - 1);

                Product product = mapper.Map<Product>(productDto);
                product.Seller = currentUsers[randomSeller];
                product.Byer = randomByer % 7 != 0 ? currentUsers[randomByer] : null;
                products.Add(product);
            }

            db.Products.AddRange(products);
            db.SaveChanges();
        }

        public static void ReadUsers(IMapper mapper, ProductShopDatabase db)
        {
            List<User> users = new List<User>();
            var xmlStream = File.ReadAllText("Xml/users.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));
            var usersFromFile = (UserDto[]) serializer.Deserialize(new StringReader(xmlStream));

            foreach (UserDto userDto in usersFromFile)
            {
                if (!IsValid(userDto))
                {
                    continue;
                }

                User user = mapper.Map<User>(userDto);
                users.Add(user);
            }
            db.Users.AddRange(users);
            db.SaveChanges();
        }

        public static void InitializeDb(ProductShopDatabase db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        public static void ProductsInRange(IMapper mapper,ProductShopDatabase db)
        {
            var productsDto = db.Products.Where(p => p.Price >= 1000 && p.Price <= 2000 && p.Byer!=null)
                .OrderByDescending(p=>p.Price)
                .ProjectTo<ProductInRangeDto>(mapper.ConfigurationProvider).ToArray();

            StringBuilder sb=new StringBuilder();
            var xmlNamespaces=new XmlSerializerNamespaces(new []{XmlQualifiedName.Empty});
            var serializer= new XmlSerializer(typeof(ProductInRangeDto[]),new XmlRootAttribute("products"));
            serializer.Serialize(new StringWriter(sb), productsDto,xmlNamespaces);
            
            File.WriteAllText("../../../Xml/products-in-range.xml",sb.ToString());
            Console.WriteLine("done!");
        }

        public static void SoldProducts(IMapper mapper,ProductShopDatabase db)
        {
            var users = db.Users
                .Where(x => x.SoldProducts.Any())                
                .ProjectTo<UserDtoSoldItems>(mapper.ConfigurationProvider)
                .OrderBy(x => x.FirstName)
                .ThenBy(u => u.LastName)
                .ToArray();


            StringBuilder sb = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(UserDtoSoldItems[]), new XmlRootAttribute("products"));
            serializer.Serialize(new StringWriter(sb),users, xmlNamespaces);

            File.WriteAllText("../../../Xml/sold-products2.xml", sb.ToString());
        }

        public static void UsersandProducts(IMapper mapper, ProductShopDatabase db)
        {
            var users = new UsersDto
            {
                Count = db.Users.Count(),
                Users = db.Users.Where(x => x.SoldProducts.Any())
                    .Select(x => new UserDtoUserInfoOnly
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Age = x.Age.ToString(),
                        SoldProduct = new UsersandProductsProductDto
                        {
                            Count = x.SoldProducts.Count,
                            SoldProductsDtos = x.SoldProducts
                            .Select(s => new ProductDtoFourthProblem
                            {
                                Name = s.Name,
                                Price = s.Price

                            }).ToArray()
                        }

                    }).ToArray()
            };

            StringBuilder sb = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(UsersDto), new XmlRootAttribute("users"));
            serializer.Serialize(new StringWriter(sb), users, xmlNamespaces);

            File.WriteAllText("../../../Xml/users-and-products.xml", sb.ToString());

        }
    }
}
