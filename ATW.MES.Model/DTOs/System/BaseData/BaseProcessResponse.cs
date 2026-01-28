using ATW.CommonBase.Model.Log;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.DTOs.System.BaseData
{
    public class BaseProcessResponse
    {

        public int Id { get; set; }

        public Guid GUID { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "工序名称", IsNullable = false)]
        public string ProcessName { get; set; }

        /// <summary>
        /// 工序编码
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "工序编码", IsNullable = false)]
        public string ProcessCode { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "说明", IsNullable = true)]
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
