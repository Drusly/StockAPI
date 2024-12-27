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
    public class StockProductVersion: BaseEntity<int>
    {
        [StringLength(3, ErrorMessage = "Version.Code Cannot Exceed 3")]
        public string? Code { get; set; }
        public int SubCategoryId {  get; set; }
        [StringLength(1500, ErrorMessage = "Brand.PhoneNumber Cannot Exceed 100")]
        public string Notes { get; set; }
        [JsonIgnore]
        public virtual StockSubCategory? StockSubCategory { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<StockProduct>? StockProducts { get; set; }
        [StringLength(150, ErrorMessage = "Version.Name Cannot Exceed 150")]
        public string? Name { get; set; }

    }
}
