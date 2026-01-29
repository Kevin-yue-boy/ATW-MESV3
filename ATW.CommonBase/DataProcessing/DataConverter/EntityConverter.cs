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

        #region 根据属性名字符串获取实体类实例的属性值

        /// <summary>
        /// 根据属性名字符串获取实体类实例的属性值
        /// </summary>
        /// <param name="entityInstance">实体类实例（获取静态属性时可传null）</param>
        /// <param name="propertyName">要获取的属性名称（区分大小写，需与实体类属性名完全一致）</param>
        /// <returns>属性对应的实际值（object类型，可按需强转）</returns>
        /// <exception cref="ArgumentNullException">实体实例/属性名为空时抛出</exception>
        /// <exception cref="ArgumentException">属性不存在时抛出</exception>
        public static object GetEntityPropertyValue(object entityInstance, string propertyName)
        {
            // 1. 校验输入参数
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName), "属性名称不能为空或空白");
            }
            // 获取实例类型（静态属性时instance为null，需直接传类型）
            Type entityType = entityInstance?.GetType() ?? throw new ArgumentNullException(nameof(entityInstance),
                "获取非静态属性时，实体实例不能为null；获取静态属性请使用重载方法");

            // 2. 查找属性（支持public的实例/静态属性）
            PropertyInfo propertyInfo = entityType.GetProperty(
                propertyName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            // 3. 校验属性是否存在
            if (propertyInfo == null)
            {
                throw new ArgumentException($"实体类【{entityType.Name}】中未找到属性【{propertyName}】", nameof(propertyName));
            }

            // 4. 获取属性值（静态属性时instance传null）
            object propertyValue = propertyInfo.GetValue(entityInstance);

            // 5. 返回属性值（值类型会自动装箱，null值也会正常返回）
            return propertyValue;
        }

        #endregion


    }
}
