using ATW.CommonBase.Model.Log;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.DTOs.System.BaseData
{
    public class BaseWorkTypeDTO
    {

        public int Id { get; set; }

        public Guid GUID { get; set; }

        /// <summary>
        /// 工作类型名称
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "工作类型名称", IsNullable = false)]
        public string WorkTypeName { get; set; }

        /// <summary>
        /// 工作类型编码
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "工作类型编码", IsNullable = false)]
        public string WorkTypeCode { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "启用", IsNullable = false)]
        public bool Enable { get; set; }

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
