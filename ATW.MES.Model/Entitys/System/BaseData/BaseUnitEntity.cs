using ATW.CommonBase.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.Entitys.System.BaseData
{
    [SugarTable("Base_Unit")]
    [CacheStorage(EnableYN = true)]
    public class BaseUnitEntity
    {

        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "GUID")]
        public Guid GUID { get; set; }

        /// <summary>
        /// 单位类型
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "单位类型", Length = 200)]
        public string UnitType { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "单位名称", Length = 200)]
        public string UnitName { get; set; }

        /// <summary>
        /// 换算符
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "换算符")]
        public long UnitConversion { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "说明", ColumnDataType = "NVARCHAR(max)")]
        public string Explain { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>        
        [SugarColumn(IsNullable = false, ColumnDescription = "创建时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>        
        [SugarColumn(IsNullable = false, ColumnDescription = "更新时间")]
        public DateTime LastEditTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>        
        [SugarColumn(IsNullable = true, ColumnDescription = "备注", ColumnDataType = "NVARCHAR(max)")]
        public string Remark { get; set; }


    }
}
