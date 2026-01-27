using ATW.CommonBase.Global;
using ATW.CommonBase.Method.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.DAL.Global
{
    public  class IOCServices_DAL
    {

        /// <summary>
        /// IOC容器-注册服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appsettings"></param>
        public static void ConfigureServices(ServiceCollection services, IConfigurationRoot appsettings)
        {

            DIServiceExtensions.AddSingletonByEntityTag(services, Assembly.GetExecutingAssembly(), "DAL");

            #region 注入项目使用基础服务

            //注入项目使用基础服务
            services.AddSingleton<ProjectService_DAL>();

            #endregion

        }

    }
}
