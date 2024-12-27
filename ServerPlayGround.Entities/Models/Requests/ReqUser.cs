using Karluna.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Entities.Models.Requests
{
    public class ReqUser
    {
        public ReqUser()
        { 
            this.User = new User();
        }

        public User User { get; set; }
        public string PasswordInput {  get; set; }
        public UserRole? Role { get; set; }
        public string? strRole {  get; set; }
    }
}
