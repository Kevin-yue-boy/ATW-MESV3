using ATW.CommonBase.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.Entitys.Equipment.BaseEquipment
{
    [SugarTable("Equipment_Communicate")]
    [CacheStorage(EnableYN = true)]
    public class EquipmentCommunicateEntity
    {

        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "GUID")]
        public Guid GUID { get; set; }

        /// <summary>
        /// 通讯名称
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "通讯名称", Length = 200)]
        public string CommunicateName { get; set; }

        /// <summary>
        /// 通讯编码
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "通讯编码", Length = 200)]
        public string CommunicateCode { get; set; }

        /// <summary>
        /// 通讯类型
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "通讯类型", Length = 200)]
        public string CommunicateType { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "启用")]
        public bool Enable { get; set; }

        /// <summary>
        /// 连接字符串Json
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "连接字符串Json", Length = 500)]
        public string ConnectAddress { get; set; }

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
