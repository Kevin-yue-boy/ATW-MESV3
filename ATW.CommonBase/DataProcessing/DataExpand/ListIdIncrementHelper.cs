using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.DataProcessing.DataExpand
{
    /// <summary>
    /// 泛型List Id自增赋值工具类
    /// </summary>
    public static class ListIdIncrementHelper
    {
        /// <summary>
        /// 给泛型List<T>中的实体Id属性从1开始自增赋值
        /// </summary>
        /// <typeparam name="T">实体类型（需包含int类型的Id属性）</typeparam>
        /// <param name="list">需要赋值的泛型List集合</param>
        /// <exception cref="ArgumentException">实体无Id属性或Id属性不可赋值时抛出</exception>
        public static void AssignIncrementalId<T>(this List<T> list)
        {
            // 空集合直接返回，避免无效操作
            if (list == null || list.Count == 0)
                return;

            // 1. 通过反射获取T类型的Id属性
            PropertyInfo idProperty = typeof(T).GetProperty("Id",
                BindingFlags.Public | BindingFlags.Instance);

            // 校验Id属性是否存在
            if (idProperty == null)
            {
                throw new ArgumentException($"类型 {typeof(T).Name} 未找到公共的Id属性");
            }

            // 校验Id属性是否可写
            if (!idProperty.CanWrite)
            {
                throw new ArgumentException($"类型 {typeof(T).Name} 的Id属性不可赋值（set访问器缺失）");
            }

            // 2. 核心逻辑：遍历集合，从1开始给Id赋值
            for (int i = 0; i < list.Count; i++)
            {
                T item = list[i];
                if (item != null) // 处理空元素
                {
                    idProperty.SetValue(item, i + 1);
                }
            }
        }

    }
}
