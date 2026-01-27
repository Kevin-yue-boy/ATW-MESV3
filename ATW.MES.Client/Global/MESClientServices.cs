using ATW.CommonBase.CommonInterface.View;
using ATW.CommonBase.DataAccess.Redis;
using ATW.CommonBase.Method.Log;
using ATW.CommonBase.Model.Enum;
using ATW.CommonBase.Model.Log;
using ATW.MES.DAL.Global;
using ATW.MES.Model.Models.AppSettings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Client.Global
{
    public class MESClientServices
    {

        public void StartServicesAsync()
        {

            #region 从IOC取出配置文件appsettings

            var appSettings = Ioc.Default.GetRequiredService<IOptionsMonitor<AppSettings>>().CurrentValue;

            #endregion

            #region 启动项目使用基础服务

            Ioc.Default.GetRequiredService<ProjectService_CommonBase>()
                .StartServices();
            Logger.OperateLog($"CommonBase服务加载完成!");
            Ioc.Default.GetRequiredService<ProjectService_DAL>()
                .StartServices();
            Logger.OperateLog($"DAL服务加载完成!");
            Ioc.Default.GetRequiredService<IUserControlRepositoryService>()
                .AddUserControl(Assembly.GetExecutingAssembly());
            Logger.OperateLog($"用户控件服务加载完成!");

            #endregion
        }

    }
}
