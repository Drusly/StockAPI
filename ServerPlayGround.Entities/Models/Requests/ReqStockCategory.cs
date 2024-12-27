using Karluna.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Entities.Models.Requests
{
    public class ReqStockCategory
    {
        public ReqStockCategory() 
        { 
            this.StockCategory = new StockCategory();
        }

        public StockCategory StockCategory { get; set; }
        public bool GetForCode {  get; set; }
    }
}
