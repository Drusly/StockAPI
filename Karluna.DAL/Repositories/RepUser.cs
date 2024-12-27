using Karluna.DAL.Generic;
using Karluna.DAL.Interface;
using Karluna.Data.DbContext;
using Karluna.Entities.Entities;
using Karluna.Entities.Models.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.DAL.Repositories
{
    public class RepUser : BaseRepository<User>, IRepUser
    {
        public RepUser(KtsDbContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<User> GetUsers(ReqUser req)
        {
            var Query = KtsDbContext.Set<User>().AsQueryable();

            if(string.IsNullOrEmpty(req.User.UserName) == false)
                Query = Query.Where(x => x.UserName == req.User.UserName);

            return Query;
        }
    }
}
