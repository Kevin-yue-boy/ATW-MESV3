using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.DTOs.Process.ProcessRoute
{
    public class ProcessRouteResponse
    {

        public int Id { get; set; }

        public Guid GUID { get; set; }

        /// <summary>
        /// 基础工序编码
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 基础工序名称
        /// </summary>
        public string ProcessCode { get; set; }

        /// <summary>
        /// 基础工序GUID
        /// </summary>
        public Guid BaseProcessGUID { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "排序序号")]
        public int SortNo { get; set; }

        /// <summary>
        /// 入站校验
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "入站校验")]
        public bool CheckIn { get; set; }

        /// <summary>
        /// 出站校验
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "出站校验")]
        public bool CheckOut { get; set; }

        /// <summary>
        /// 厂级MES入站校验
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "厂级MES入站校验")]
        public bool CheckInFactoryMES { get; set; }

        /// <summary>
        /// 厂级MES出站校验
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "厂级MES出站校验")]
        public bool CheckOutFactoryMES { get; set; }

        /// <summary>
        /// 启用工序
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "启用工序")]
        public bool EnableProcess { get; set; }

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
        public string Remark { get; set; }

    }
}
