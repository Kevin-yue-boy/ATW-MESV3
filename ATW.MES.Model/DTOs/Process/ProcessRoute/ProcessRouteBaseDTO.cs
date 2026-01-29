using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.DTOs.Process.ProcessRoute
{
    public class ProcessRouteBaseDTO
    {
        public int Id { get; set; }

        public Guid GUID { get; set; }

        /// <summary>
        /// 工艺路线名称
        /// </summary>
        public string ProcessRouteName { get; set; }

        /// <summary>
        /// 工艺路线编码
        /// </summary>
        public string ProcessRouteCode { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>        
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>        
        public DateTime LastEditTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>        
        public string Remark { get; set; }

    }
}
