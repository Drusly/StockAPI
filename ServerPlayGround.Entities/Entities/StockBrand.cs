using Karluna.Entities.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Karluna.Entities.Entities
{
    public class StockBrand: BaseEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(1000, ErrorMessage = "Brand.Name Cannot Exceed 1000")]
        public string Name { get; set; }
        
        [StringLength(1000, ErrorMessage = "Brand.Address Cannot Exceed 1000")]
        public string Address { get; set; }

        [StringLength(100, ErrorMessage = "Brand.PhoneNumber Cannot Exceed 100")]
        public string PhoneNumber { get; set; }

        [StringLength(1500, ErrorMessage = "Brand.PhoneNumber Cannot Exceed 100")]
        public string Notes { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public virtual List<StockProduct>? StockProducts { get; set; }
    }
}
