using Karluna.DAL.Generic;
using Karluna.DAL.Interface;
using Karluna.Data.DbContext;
using Karluna.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.DAL.Repositories
{
    public class RepUserRole : BaseRepository<UserRole>, IRepUserRole
    {
        public RepUserRole(KtsDbContext dbContext) : base(dbContext)
        {
        }
    }
}
