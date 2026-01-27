using ATW.CommonBase.Model;
using ATW.CommonBase.Model.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.DataProcessing.DataConverter
{
    public static class EntityConverter
    {

        #region 打印实体类属性的Description和值

        /// <summary>
        /// 打印实体类属性的Description和值（格式：Description:变量值;）
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="entity">实体实例</param>
        /// <returns>拼接好的格式化字符串（也可直接打印）</returns>
        public static string PrintLogByEntityDescription<T>(this T entity)
        {
            if (entity == null)
            {
                throw null;
            }

            // 获取实体类型的所有公共属性
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (properties.Length == 0)
            {
                throw null;
            }

            // 拼接格式化字符串
            string result = string.Empty;
            foreach (PropertyInfo prop in properties)
            {
                // 1. 获取Description特性值
                DescriptionAttribute? descAttr = prop.GetCustomAttribute<DescriptionAttribute>();
                string description = descAttr?.Description ?? prop.Name; // 无特性则用属性名

                // 2. 获取属性值（处理null值）
                object? value = prop.GetValue(entity) ?? "null";
                if (!string.IsNullOrWhiteSpace(description)
                    &&!string.IsNullOrWhiteSpace(value.ToString()))
                {
                    if (prop.GetValue(entity).GetType()==typeof(DateTime))
                    {
                        // 3. 按格式拼接
                        string formatStr = $"{description}:{Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss:fff")}; ";
                        result += formatStr;
                    }
                    else
                    {
                        // 3. 按格式拼接
                        string formatStr = $"{description}:{value}; ";
                        result += formatStr;
                    }
                    
                }
                
            }

            return result;
        }

        #endregion

    }
}
