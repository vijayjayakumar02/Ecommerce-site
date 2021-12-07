using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Data.ScaffoldedEntity
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string CoverFileName { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}
