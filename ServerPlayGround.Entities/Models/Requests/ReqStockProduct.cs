using Karluna.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Entities.Models.Requests
{
    public class ReqStockProduct
    {
        public ReqStockProduct() 
        {
            this.StockProduct = new StockProduct();
        }

        public StockProduct StockProduct { get; set; }
        public int? SubCategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public bool? IncludeSubCategory { get; set; }
        public bool? IncludeBrand { get; set; }
        public bool? IncludeCategory { get; set; }
    }
}
