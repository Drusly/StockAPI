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
    public class RepStockSubCategory : BaseRepository<StockSubCategory>, IRepStockSubCategory
    {
        public RepStockSubCategory(KtsDbContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<StockSubCategory> GetStockSubCategory(ReqStockSubCategory req)
        {
            var Query = KtsDbContext.Set<StockSubCategory>().AsQueryable();

            if (req.StockSubCategory.Id > 0)
                Query = Query.Where(c => c.Id == req.StockSubCategory.Id);
            else if (string.IsNullOrEmpty(req.StockSubCategory.Name) == false && req.StockSubCategory.CategoryId > 0)
                Query = Query.Where(c => c.Name == req.StockSubCategory.Name && c.CategoryId == req.StockSubCategory.CategoryId);
            if (req.CategoryId > 0)
                Query = Query.Where(c => c.CategoryId == req.CategoryId);
            if (req.IncludeCategory)
                Query = Query.Include(c => c.StockCategory);

            return Query;
        }
    }
}
