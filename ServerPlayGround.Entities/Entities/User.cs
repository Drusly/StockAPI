using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Entities.Entities
{
    public class User : IdentityUser
    {
        [NotMapped]
       public List<string>? RoleList { get; set; }
    }
}
