using ATW.CommonBase.Model.Log;
using SqlSugar;

namespace ATW.MES.Model.DTOs.Process.Product
{
    public class ProductResponse
    {

        public int Id { get; set; }

        public Guid GUID { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "产品名称", IsNullable = false)]
        public string ProductName { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        [EntityDataCheckModel(ColumnDescription = "产品编码", IsNullable = false)]
        public string ProductCode { get; set; }

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
