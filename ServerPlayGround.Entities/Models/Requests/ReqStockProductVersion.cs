using Karluna.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Entities.Models.Requests
{
    public class ReqStockProductVersion
    {
        public ReqStockProductVersion() 
        {
            this.StockProductVersion = new StockProductVersion();
        }

        public StockProductVersion StockProductVersion { get; set; }
    }
}
