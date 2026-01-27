using ATW.CommonBase.Model.Log;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.DTOs.System.BaseData
{
    public class BaseUnitResponse
    {

        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "GUID")]
        public Guid GUID { get; set; }

        /// <summary>
        /// 单位类型
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "单位类型", IsNullable = false)]
        public string UnitType { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "单位名称", IsNullable = false)]
        public string UnitName { get; set; }

        /// <summary>
        /// 换算符
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "换算符", IsNullable = false)]
        public long UnitConversion { get; set; }

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
