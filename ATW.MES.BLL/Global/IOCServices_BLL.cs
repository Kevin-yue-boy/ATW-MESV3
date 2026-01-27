
using ATW.CommonBase.Method.DI;
using ATW.MES.BLL.Equipment.BaseEquipment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.BLL.Global
{
    public class IOCServices_BLL
    {

        /// <summary>
        /// IOC容器-注册服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appsettings"></param>
        public static void ConfigureServices(ServiceCollection services, IConfigurationRoot appsettings)
        {
            DIServiceExtensions.AddSingletonByEntityTag(services, Assembly.GetExecutingAssembly(), "BLL");
        }

    }
}
