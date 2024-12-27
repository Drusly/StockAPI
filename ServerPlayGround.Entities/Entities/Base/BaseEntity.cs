using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Entities.Entities.Base
{
    public abstract class BaseEntity<T>: IEntity
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public T Id { get; set; }

        object IEntity.Id
        {
            get { return Id; }
            set { }
        }

        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } 

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOnUtc { get; set; }

        public bool IsDeleted { get; set; }
    }
}
