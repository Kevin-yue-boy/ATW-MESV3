using ATW.CommonBase.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Model.Log
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = true)]
    public class EntityDataCheckModel : Attribute
    {

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNullable { get; set; } = false;

        /// <summary>
        /// 校验数据类型
        /// </summary>
        public bool IsDateType { get; set; } = true;

        /// <summary>
        /// 中文备注
        /// </summary>
        public string ColumnDescription { get; set; }

    }
}
