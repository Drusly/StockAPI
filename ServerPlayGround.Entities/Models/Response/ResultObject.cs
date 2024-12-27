using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Karluna.Entities.Enums.GeneralEnum;

namespace Karluna.Entities.Models.Response
{
    public class ResultObject<T> where T : class
    {
        public T Result { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ResultStatusEnum StatusCode { get; set; }
        public string Message { get; set; }
    }
}
