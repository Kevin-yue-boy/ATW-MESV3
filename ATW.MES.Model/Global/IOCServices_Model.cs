
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.Global
{
    public class IOCServices_Model
    {

        /// <summary>
        /// IOC容器-注册服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appsettings"></param>
        public static void ConfigureServices(ServiceCollection services, IConfigurationRoot appsettings)
        {

            //注入AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

        }

    }
}
