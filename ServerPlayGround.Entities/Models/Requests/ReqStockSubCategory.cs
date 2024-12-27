using Karluna.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Entities.Models.Requests
{
    public class ReqStockSubCategory
    {
        public ReqStockSubCategory()
        {
            this.StockSubCategory = new StockSubCategory();
        }

        public StockSubCategory StockSubCategory { get; set; }
        public bool IncludeCategory { get; set; }
        public int CategoryId {  get; set; }
    }
}
