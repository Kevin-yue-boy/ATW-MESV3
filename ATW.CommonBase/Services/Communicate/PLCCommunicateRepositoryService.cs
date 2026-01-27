
using ATW.CommonBase.Model.Communicate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Services.Communicate
{
    public class PLCCommunicateRepositoryService: IPLCCommunicateRepositoryService
    {

        #region Parameter

        private List<PLCConnectModel> PLCConnects { get; set; }

        private object obj = new object();

        #endregion

        #region 构造函数

        public PLCCommunicateRepositoryService()
        {
            PLCConnects = new List<PLCConnectModel>();
        }

        #endregion

        #region 添加PLC通讯连接

        /// <summary>
        /// 添加PLC通讯连接
        /// </summary>
        /// <param name="iPLC"></param>
        /// <param name="gUID"></param>
        public void SetConnect(ICommunicatePLC iPLC, Guid gUID)
        {
            lock (obj)
            {
                if (PLCConnects != null)
                {
                    var PLCConnect = PLCConnects.Find(it => { return it.GUID == gUID; });
                    if (PLCConnect != null)
                    {
                        PLCConnect.IPLC = iPLC;
                    }
                    else
                    {
                        PLCConnects.Add(new PLCConnectModel()
                        {
                            IPLC = iPLC,
                            GUID = gUID
                        });
                    }
                }
            }
        }

        #endregion

        #region 获取PLC通讯连接

        /// <summary>
        /// 获取PLC通讯连接
        /// </summary>
        /// <param name="gUID"></param>
        /// <returns></returns>
        public ICommunicatePLC GetConnect(Guid gUID)
        {
            return PLCConnects.Find(it => { return it.GUID == gUID; })?.IPLC;
        }

        #endregion

    }
}
