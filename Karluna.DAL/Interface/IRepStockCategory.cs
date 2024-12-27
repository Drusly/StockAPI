using Karluna.DAL.Generic;
using Karluna.DAL.Repositories;
using Karluna.Entities.Entities;
using Karluna.Entities.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.DAL.Interface
{
    public interface IRepStockCategory : IBaseRepository<StockCategory>
    {
        IQueryable<StockCategory> GetStockCategory(ReqStockCategory req);
    }
}
