using Karluna.DAL.Generic;
using Karluna.DAL.Interface;
using Karluna.Data.DbContext;
using Karluna.Entities.Entities;
using Karluna.Entities.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.DAL.Repositories
{
    public class RepStockBrand : BaseRepository<StockBrand>, IRepStockBrand
    {
        public RepStockBrand(KtsDbContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<StockBrand> GetStockBrand(ReqStockBrand req) 
        {
            var Query = KtsDbContext.Set<StockBrand>().AsQueryable();

            if (req.StockBrand.Id > 0)
                Query = Query.Where(c => c.Id == req.StockBrand.Id);

            //if (string.IsNullOrEmpty(req.StockBrand.Name) == false)
            //    Query = Query.Where(c => c.Name == req.StockBrand.Name);

            return Query;
        }
    }
}
