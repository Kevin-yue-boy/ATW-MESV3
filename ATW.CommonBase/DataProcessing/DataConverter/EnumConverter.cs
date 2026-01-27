using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.DataProcessing.DataConverter
{
    public static class EnumConverter
    {

        #region 解析枚举的描述

        /// <summary>
        /// 解析枚举的描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EnumToDesc(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            return Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute att ? att.Description : value.ToString();
        }

        #endregion

    }
}
