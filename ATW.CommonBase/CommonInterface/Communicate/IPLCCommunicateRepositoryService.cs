using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.CommonInterface.Communicate
{
    public interface IPLCCommunicateRepositoryService
    {


        public void SetConnect(ICommunicatePLC iPLC, Guid gUID);

        public ICommunicatePLC GetConnect(Guid gUID);

    }
}
