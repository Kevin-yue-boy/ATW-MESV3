using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.DTOs.System.Log
{
    public class UserOperateLogDTO
    {

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIP { get; set; } = "127.0.0.1";

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; } = "sys";

        /// <summary>
        /// 权限名称
        /// </summary>
        public string AuthorityName { get; set; } = "sys";

        /// <summary>
        /// 位置
        /// </summary>
        public string PageName { get; set; } = "Global";

        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperateLogType { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 记录
        /// </summary>
        public string Log { get; set; }

        /// <summary>
        /// 耗时
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        /// 底层数据
        /// </summary>
        public string UnderlyingData { get; set; }

    }
}
