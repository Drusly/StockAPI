using Karluna.DAL.Generic;
using Karluna.DAL.Interface;
using Karluna.DAL.Repositories;
using Karluna.Entities.Entities;
using Karluna.Entities.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.DAL.UnitOfWork
{
    public interface IWorker
    {
        IRepStockProduct StockProduct { get; }
        IRepStockBrand StockBrand { get; }
        IRepStockSubCategory StockSubCategory { get; }
        IRepStockCategory StockCategory { get; }
        IRepStockProductVersion StockProductVersion { get; }
        IRepUser User { get; }
        IRepUserRole UserRole { get; }

        Task SaveChanges();
    }
}
