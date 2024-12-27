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
    public class RepStockProduct : BaseRepository<StockProduct>, IRepStockProduct
    {
        public RepStockProduct(KtsDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<StockProduct> GetStockProducts(ReqStockProduct req)
        {
            var Query = KtsDbContext.Set<StockProduct>().AsQueryable();

            if (req.StockProduct.Id > 0)
                Query = Query.Where(c => c.Id == req.StockProduct.Id);

            if(string.IsNullOrEmpty(req.StockProduct.Code) == false) //req.StockProduct.Code != null && !req.StockProduct.Code.Equals("")
                Query = Query.Where(c => c.Code == req.StockProduct.Code);

            if (req.SubCategoryId > 0)
                Query = Query.Where(c => c.SubCategoryId == req.SubCategoryId);
            
            if (req.CategoryId > 0)
                Query = Query.Where(c => c.CategoryId == req.CategoryId);

            if (req.BrandId > 0)
                Query = Query.Where(c => c.BrandId == req.BrandId);

            if (req.IncludeBrand ?? false)
                Query = Query.Include(c => c.StockBrand);

            if (req.IncludeSubCategory ?? false)
                Query = Query.Include(c => c.StockSubCategory);

            if (req.IncludeCategory ?? false)
                Query = Query.Include(c => c.StockCategory);

            return Query;
        }
    }
}
