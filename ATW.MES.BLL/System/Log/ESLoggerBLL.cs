using ATW.CommonBase.DataProcessing.DataConverter;
using ATW.CommonBase.DataProcessing.Serializer;
using ATW.CommonBase.Global;
using ATW.CommonBase.Method.Log;
using ATW.CommonBase.Model.Enum;
using ATW.CommonBase.Model.Log;
using ATW.CommonBase.Services.Log;
using ATW.MES.Model.Entitys.System.Log;
using ATW.MES.Model.Models.System.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.BLL.System.Log
{
    public class ESLoggerBLL
    {

        #region Parameter

        private LogESService UserOperate_LogESService { get; set; }

        #endregion

        #region 加载

        public ESLoggerBLL()
        {
            UserOperate_LogESService = new LogESService();
        }

        #endregion

        #region 用户操作日志记录


        public void OperateLog(string value, object? entity, EnumOperateLogType operateLogType
            , bool result = true
            , long time = 0
            , string underlyingData = ""
            , string clientIP = "127.0.0.1"
            , string userName = ""
            , string authorityName = ""
            , string pageName = "")
        {
            underlyingData = $"Entity:\r{Serializer_JsonNet.ObjectToJson(entity)}";
            OperateLog(value, operateLogType
                , result
                , time
                , underlyingData
                , clientIP
                , userName
                , authorityName
                , pageName);
        }

        public  void OperateLog(string value, object entity, object entity_Old, EnumOperateLogType operateLogType
            , bool result = true
            , long time = 0
            , string underlyingData = ""
            , string clientIP = "127.0.0.1"
            , string userName = ""
            , string authorityName = ""
            , string pageName = "")
        {
            underlyingData = $"Entity:\r{Serializer_JsonNet.ObjectToJson(entity)}" +
                             $"\r  Entity_Old:\r" +
                             $"{Serializer_JsonNet.ObjectToJson(entity_Old)}";
            OperateLog(value, operateLogType
                , result
                , time
                , underlyingData
                , clientIP
                , userName
                , authorityName
                , pageName);
        }

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="value"></param>
        public void OperateLog(string value, EnumOperateLogType operateLogType
            , bool result = true
            , long time = 0
            , string underlyingData = ""
            , string clientIP = "127.0.0.1"
            , string userName = ""
            , string authorityName = ""
            , string pageName = "")
        {

            var userOperateLog = new UserOperateLogEntity();
            userOperateLog.UserName = string.IsNullOrWhiteSpace(userName) ? GlobalModel.UserName : userName;
            userOperateLog.AuthorityName = string.IsNullOrWhiteSpace(authorityName) ? GlobalModel.AuthorityName : authorityName;
            userOperateLog.PageName = string.IsNullOrWhiteSpace(pageName) ? GlobalModel.PageName : pageName;
            userOperateLog.OperateLogType = operateLogType.EnumToDesc();
            userOperateLog.Result = result ? "成功" : "失败";
            userOperateLog.Time = time;
            userOperateLog.Log = value.Replace("\r","");
            userOperateLog.ClientIP = clientIP;
            userOperateLog.UnderlyingData = underlyingData;
            userOperateLog.CreateTime = DateTime.Now;
            UserOperate_LogESService.WriteAsync($"operatelog_{DateTime.Now.ToString("yyyyMMdd")}", userOperateLog);
            
            var txtuserOperateLog = DeepCopy.JsonDeepCopy(userOperateLog);
            txtuserOperateLog.UnderlyingData = "";
            var log_txt= EntityConverter.PrintLogByEntityDescription(txtuserOperateLog);
            Logger.OperateLog(log_txt);
        }


        #endregion



    }
}
