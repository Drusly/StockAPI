using Karluna.DAL.Generic;
using Karluna.Entities.Entities;
using Karluna.Entities.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.DAL.Interface
{
    public interface IRepStockSubCategory : IBaseRepository<StockSubCategory>
    {
        IQueryable<StockSubCategory> GetStockSubCategory(ReqStockSubCategory req);
    }
}
