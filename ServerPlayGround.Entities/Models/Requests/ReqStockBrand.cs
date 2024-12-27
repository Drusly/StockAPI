using Karluna.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Entities.Models.Requests
{
    public class ReqStockBrand
    {
        public ReqStockBrand()
        {
            this.StockBrand = new StockBrand();
        }

        public StockBrand StockBrand {  get; set; } 
    }
}
