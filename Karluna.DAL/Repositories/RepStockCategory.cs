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
    public class RepStockCategory : BaseRepository<StockCategory>, IRepStockCategory
    {
        public RepStockCategory(KtsDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<StockCategory> GetStockCategory(ReqStockCategory req)
        {
            var Query = KtsDbContext.Set<StockCategory>().AsQueryable();

            if (req.StockCategory.Id > 0)
                Query = Query.Where(c => c.Id == req.StockCategory.Id);

            else if (string.IsNullOrEmpty(req.StockCategory.Name) == false && req.StockCategory.Id > 0)
                Query = Query.Where(c => c.Name == req.StockCategory.Name);

            if (req.GetForCode)
                Query = Query.Where(c => c.MaterialStatus == req.StockCategory.MaterialStatus && c.DomainName == req.StockCategory.DomainName);

            return Query;
        }
    }
}
