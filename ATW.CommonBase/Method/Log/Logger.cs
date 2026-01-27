using ATW.CommonBase.Global;
using ATW.CommonBase.Model.Enum;
using ATW.CommonBase.Model.Log;
using ATW.CommonBase.Services.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Method.Log
{
    public static class Logger
    {

        #region Parameter

        public static string FilePath { get; set; } = "D:\\Logs";

        private static LogTxtService Operate_LogTxtService { get; set; }

        #endregion

        #region 启动服务

        public static void StartServices()
        {
            Operate_LogTxtService = new LogTxtService(FilePath, "System");
            Logger.OperateLog($"系统日志服务加载完成!");
        }

        #endregion

        #region 操作日志

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="value"></param>
        public static void OperateLog(string value)
        {
            Operate_LogTxtService.WriteEntityAsync(value);
        }

        #endregion


    }
}
