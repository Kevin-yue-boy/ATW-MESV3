using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Method.CustomException
{
    public class CheckException: Exception
    {

        public CheckException() { }

        public CheckException(string message) : base(message) { }

        public CheckException(string message, Exception inner) : base(message, inner) { }

    }


}
