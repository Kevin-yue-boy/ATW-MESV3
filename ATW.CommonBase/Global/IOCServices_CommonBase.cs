using ATW.CommonBase.CommonInterface.DataAccess;
using ATW.CommonBase.CommonInterface.View;
using ATW.CommonBase.DataAccess.Common;
using ATW.CommonBase.DataAccess.ElasticSearch;
using ATW.CommonBase.DataAccess.Redis;
using ATW.CommonBase.Services.Communicate;
using ATW.CommonBase.Services.View;
using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Global
{
    public class IOCServices_CommonBase
    {

        /// <summary>
        /// IOC容器-注册服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appsettings"></param>
        public static void ConfigureServices(ServiceCollection services, IConfigurationRoot appsettings)
        {

            #region 数据库

            #region SqlSugar连接

            services.AddSingleton<ISqlSugarClient>(s =>
            {
                SqlSugarClient sqlSugar = new SqlSugarClient(
                    new List<ConnectionConfig>()
                    {
                        new ConnectionConfig()
                        {
                            ConfigId="MainDB" ,
                            DbType= SqlSugar.DbType.SqlServer,
                            ConnectionString=appsettings.GetSection("MainDB_ConnStr").Value,
                            IsAutoCloseConnection=true
                        }
                    },
                    db =>
                    {
                        //每次上下文都会执行
                        db.Aop.OnLogExecuting = (sql, pars) =>
                        {
                        };
                    });
                return sqlSugar;
            });

            #endregion

            #region Redis注入托管

            if (appsettings.GetSection("Redis_EnableYN").Value== "True")
            {
                services.AddSingleton<IConnectionMultiplexer>(sp =>
                {
                    return ConnectionMultiplexer.Connect(appsettings.GetSection("Redis_ConnStr").Value);
                });
                services.AddSingleton<RedisCacheRepository>();
                services.AddSingleton<ICacheRepository, RedisCacheRepository>();
                services.AddSingleton<CacheUniteSqlSugarRepository>();
            }

            #endregion

            #region ES数据库

            if (appsettings.GetSection("ES_EnableYN").Value == "True")
            {
                // 配置ES客户端设置
                var esSettings = new ElasticsearchClientSettings
                    (new Uri(appsettings.GetSection("ES_Url").Value))
                    .RequestTimeout(TimeSpan.FromSeconds(Convert.ToInt32(appsettings.GetSection("ES_RequestTimeout").Value))).EnableDebugMode();
                // 账号密码认证（可选）

                //注册 ES 客户端为单例（推荐单例，减少连接开销）
                services.AddSingleton<ElasticsearchClient>
                    (_ => new ElasticsearchClient(esSettings));

                // 注册为单例（ES客户端线程安全，单例最优）
                services.AddSingleton<ElasticSearchRepository>();
            }


            #endregion

            #endregion

            #region HttpClient 厂级MES交互


            //// 配置HttpClientFactory
            //services.AddHttpClient("Common1", client =>
            //{
            //    client.Timeout = TimeSpan.FromSeconds(10);
            //});
            //// 配置HttpClientFactory
            //services.AddSingleton<TestHttpClientIOCServices>();

            //services.AddSingleton<ICommunicateHttpClient, HttpClient_RestShap>();

            #endregion

            #region 注入项目使用基础服务

            //注入项目使用基础服务
            services.AddSingleton<ProjectService_CommonBase>();

            //PLC连接仓库
            services.AddSingleton<IPLCCommunicateRepositoryService, PLCCommunicateRepositoryService>();

            //用户控件仓库
            services.AddSingleton<IUserControlRepositoryService, UserControlRepositoryService>();

            #endregion

        }

    }
}
