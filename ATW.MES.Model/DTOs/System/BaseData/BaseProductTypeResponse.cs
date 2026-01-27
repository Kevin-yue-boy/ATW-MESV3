using ATW.CommonBase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.DTOs.System.BaseData
{
    public class BaseProductTypeResponse
    {

        public int Id { get; set; }

        public Guid GUID { get; set; }

        /// <summary>
        /// 产品类型名称
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "产品类型名称", IsNullable = false)]
        public string ProductTypeName { get; set; }

        /// <summary>
        /// 产品类型名称
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "产品类型名称", IsNullable = false)]
        public string ProductTypeCode { get; set; }

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
