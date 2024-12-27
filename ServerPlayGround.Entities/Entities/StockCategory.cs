using Karluna.Entities.Entities.Base;
using Karluna.Entities.Enums;
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
    public class StockCategory:BaseEntity<int>
    {
        [StringLength(1000, ErrorMessage = "Category.Name Cannot Exceed 1000")]
        public string Name { get; set; }
        public StockEnum.DomainName DomainName { get; set; }
        public StockEnum.MaterialStatus MaterialStatus { get; set; }
        [StringLength(1500, ErrorMessage = "Brand.PhoneNumber Cannot Exceed 100")]
        public string Notes { get; set; }
        public int SubCategoryId { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public virtual List<StockSubCategory>? StockSubCategories { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public virtual List<StockProduct>? StockProducts { get; set; }

    }
}
