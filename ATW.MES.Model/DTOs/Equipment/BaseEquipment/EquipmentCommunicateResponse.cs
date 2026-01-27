using ATW.CommonBase.Model.Log;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.DTOs.Equipment.BaseEquipment
{
    public class EquipmentCommunicateResponse
    {
        public int Id { get; set; }

        public Guid GUID { get; set; }

        /// <summary>
        /// 通讯名称
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "通讯名称",IsNullable =false)]
        public string CommunicateName { get; set; }

        /// <summary>
        /// 通讯编码
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "通讯编码", IsNullable = false)]
        public string CommunicateCode { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "启用", IsNullable = false)]
        public bool Enable { get; set; }

        /// <summary>
        /// 通讯类型
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "通讯类型", IsNullable = false)]
        public string CommunicateType { get; set; }

        /// <summary>
        /// 连接字符串Json
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "连接字符串Json", IsNullable = false)]
        public string ConnectAddress { get; set; }

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
        [EntityDataCheckModel(ColumnDescription = "备注", IsNullable = true)]
        public string Remark { get; set; }

    }
}
