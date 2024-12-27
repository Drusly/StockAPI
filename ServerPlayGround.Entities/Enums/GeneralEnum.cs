using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Entities.Enums
{
    public class GeneralEnum
    {
        public enum ResultStatusEnum
        {
            Succeed,
            Error,
            RecordCannotDuplicate,
            UserAlreadyExist,
            UserMustHaveRole
        }
    }
}
