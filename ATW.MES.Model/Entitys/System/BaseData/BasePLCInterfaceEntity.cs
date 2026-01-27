using ATW.CommonBase.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.Entitys.System.BaseData
{
    [SugarTable("Base_PLCInterface")]
    [CacheStorage(EnableYN = true)]
    public class BasePLCInterfaceEntity
    {

        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "GUID")]
        public Guid GUID { get; set; }

        /// <summary>
        /// PLC接口基础信息GUID
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "PLC接口基础信息GUID")]
        public Guid PLCInterFaceBaseGUID { get; set; }

        /// <summary>
        /// 工序编码
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "工序编码", Length = 200)]
        public string ProcessCode { get; set; }

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
