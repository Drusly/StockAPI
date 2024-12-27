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
    public class RepStockProductVersion : BaseRepository<StockProductVersion>, IRepStockProductVersion
    {
        public RepStockProductVersion(KtsDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<StockProductVersion> GetStockProductVersion(ReqStockProductVersion req)
        {
            var Query = KtsDbContext.Set<StockProductVersion>().AsQueryable();

            if (req.StockProductVersion.Id > 0)
                Query = Query.Where(c => c.Id > req.StockProductVersion.Id);
            if (req.StockProductVersion.SubCategoryId > 0)
                Query = Query.Where(c => c.SubCategoryId > req.StockProductVersion.SubCategoryId);

            return Query;
        }

        public StockProductVersion GetLast(ReqStockProductVersion req)
        {
            var query = KtsDbContext.Set<StockProductVersion>().AsQueryable();
            query.Where(c => c.SubCategoryId == req.StockProductVersion.SubCategoryId);
            return query.OrderBy(c => c.CreatedDate).LastOrDefault();
        }
    }
}
