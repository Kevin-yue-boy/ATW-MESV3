using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Method.DI
{
    public static class DIServiceExtensions
    {

        /// <summary>
        /// 扩展方法增加过滤条件注入容器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly">程序集</param>
        /// <param name="entityTag"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddSingletonByEntityTag(ServiceCollection services, Assembly assembly, string entityTag)
        {

            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (string.IsNullOrWhiteSpace(entityTag) || entityTag.Length < 1)
                throw new ArgumentNullException("AddSingletonByEntityTag:条件注入容器识别条件为空！");
            // 收集要扫描的程序集
            var assemblies = new List<Assembly> { assembly };

            // 查找所有类名包含"xx"的非抽象类
            var dalTypes = assemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract
                && (
                t.Name.Length > entityTag.Length &&
                t.Name.Substring((t.Name.Length - entityTag.Length), entityTag.Length) == entityTag)
                && !t.Name.Contains("IOCServices", StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (dalTypes != null && dalTypes.Count > 0)
            {
                foreach (var type in dalTypes)
                {
                    services.AddSingleton(type);
                }
            }
        }

        /// <summary>
        /// 扩展方法增加过滤条件注入容器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly">程序集</param>
        /// <param name="entityTag"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddSingletonByEntityTag(ServiceCollection services, string assemblyString, string entityTag)
        {
            AddSingletonByEntityTag(services, Assembly.Load(assemblyString), entityTag);
        }


    }
}
