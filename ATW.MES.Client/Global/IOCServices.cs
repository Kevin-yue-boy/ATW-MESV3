using ATW.CommonBase.CommonInterface.DataAccess;
using ATW.CommonBase.DataAccess.Common;
using ATW.CommonBase.DataAccess.Redis;
using ATW.MES.BLL.Global;
using ATW.MES.DAL.Global;
using ATW.MES.DAL.Process.ProcessRoute;
using ATW.MES.Model.Global;
using ATW.MES.Model.Models.AppSettings;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using StackExchange.Redis;
using System.Data;
using System.Reflection;

namespace ATW.MES.Client.Global
{
    public class IOCServices
    {

        #region IOC容器-注册服务

        public static IServiceProvider ConfigureServices()
        {

            #region 配置文件 AppSettings.json

            var appsettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true).Build();

            #endregion

            var services = new ServiceCollection();

            #region 注册服务-引用项目

            //CommonBase
            IOCServices_CommonBase.ConfigureServices(services, appsettings);
            //Model
            IOCServices_Model.ConfigureServices(services, appsettings);
            //DAL
            IOCServices_DAL.ConfigureServices(services, appsettings);
            //BLL
            IOCServices_BLL.ConfigureServices(services, appsettings);

            #endregion

            #region 全局服务

            //配置文件
            services.Configure<AppSettings>(appsettings);

            
            services.AddSingleton<MESClientServices>();

            #endregion

            return services.BuildServiceProvider();

        }

        #endregion

    }


}
