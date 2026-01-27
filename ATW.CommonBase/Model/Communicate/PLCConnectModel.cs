using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Model.Communicate
{
    public class PLCConnectModel
    {

        public Guid GUID { get; set; }

        public ICommunicatePLC IPLC { get; set; }

    }
}
