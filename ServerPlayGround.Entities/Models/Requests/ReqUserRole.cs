using Karluna.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Entities.Models.Requests
{
    public class ReqUserRole
    {
        public ReqUserRole() 
        {
            Role = new UserRole(); 
        }

        public UserRole Role { get; set; }
    }
}
