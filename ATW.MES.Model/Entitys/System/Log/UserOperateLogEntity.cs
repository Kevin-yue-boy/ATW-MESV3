using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.Entitys.System.Log
{
    public class UserOperateLogEntity
    {

        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        [Description("客户端IP")]
        public string ClientIP { get; set; } = "127.0.0.1";

        /// <summary>
        /// 用户名称
        /// </summary>
        [Description("用户")]
        public string UserName { get; set; } = "sys";

        /// <summary>
        /// 权限名称
        /// </summary>
        [Description("权限")]
        public string AuthorityName { get; set; } = "sys";

        /// <summary>
        /// 位置
        /// </summary>
        [Description("位置")]
        public string PageName { get; set; } = "Global";

        /// <summary>
        /// 操作类型
        /// </summary>
        [Description("操作类型")]
        public string OperateLogType { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        [Description("结果")]
        public string Result { get; set; }

        /// <summary>
        /// 记录
        /// </summary>
        [Description("记录")]
        public string Log { get; set; }

        /// <summary>
        /// 耗时
        /// </summary>
        [Description("耗时(ms)")]
        public long Time { get; set; }

        /// <summary>
        /// 底层数据
        /// </summary>
        [Description("底层数据")]
        public string UnderlyingData { get; set; }

    }
}
