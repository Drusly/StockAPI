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
    public class StockSubCategory:BaseEntity<int>
    {
        [StringLength(1000, ErrorMessage = "Category.Name Cannot Exceed 1000")]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        [StringLength(1500, ErrorMessage = "Brand.PhoneNumber Cannot Exceed 100")]
        public string Notes { get; set; }
        public virtual StockCategory? StockCategory { get; set; }
        
        [IgnoreDataMember]
        [JsonIgnore]
        public virtual List<StockProduct>? StockProducts { get; set; }
        [JsonIgnore]
        public virtual List<StockProductVersion>? StockVersions { get; set; }
    }
}
