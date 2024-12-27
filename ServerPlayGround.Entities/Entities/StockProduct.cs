using Karluna.Entities.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Karluna.Entities.Entities
{
    public class StockProduct : BaseEntity<int>
    {
        [StringLength(150, ErrorMessage ="Product.Code Cannot Exceed 150")]
        public string? Code { get; set; }
        
        [StringLength(1000, ErrorMessage = "Product.Name Cannot Exceed 1000")]
        public string Name { get; set; }

        [StringLength(1500, ErrorMessage = "Product.PhoneNumber Cannot Exceed 1500")]
        public string Notes { get; set; }
        [StringLength(150, ErrorMessage = "Product.Price Cannot Exceed 1500")]
        public string Price { get; set; }
        public int? TotalCount { get; set; }

        public int? VersionId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        
        public virtual StockCategory? StockCategory { get; set; }
        public virtual StockSubCategory? StockSubCategory { get; set; }
        public virtual StockProductVersion? StockProductVersion { get; set; }
        public virtual StockBrand? StockBrand { get; set; }
    }
}
