using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Entities.Enums
{
    public class StockEnum
    {
        public enum DomainName
        {
            Electronical,
            Mechanical,
            Others
        }

        public enum MaterialStatus
        {
            FinishedMaterial,
            SemiFinishedMaterial,
            RawMaterial
        }
    }
}
