using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.BindingModel
{
    public class AddProductBindingModel
    {
        public string ProductName { get; set; }

        public string Createdby { get; set; }

        public DateTime Createdon { get; set; }

        public int StockIn  { get; set; }

        public string Modifiedby { get; set; }

        public DateTime Modifiedon { get; set; }
        
        public string Description { get; set; }

        public decimal price { get; set; }

        public DateTime Expireon { get; set; }

        public decimal Discount { get; set; }

        public int BrandId { get; set; }

        public int categoryId { get; set; }
    }
}
