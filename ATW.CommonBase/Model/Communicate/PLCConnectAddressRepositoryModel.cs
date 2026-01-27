using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Model.Communicate
{
    public class PLCConnectAddressRepositoryModel
    {
        public string IP { get; set; }

        public int Port { get; set; }

        /// <summary>
        /// MES自己判定与PLC交互心跳地址 使用UINT数据类型
        /// </summary>
        public string HeartAddress { get; set; }

    }
}
