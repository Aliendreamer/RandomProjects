namespace productShopDatabase.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public Category()
        {
            this.Products=new HashSet<CategoryProduct>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public HashSet<CategoryProduct> Products { get; set; }

    }
}