using ATW.CommonBase.DataAccess.ElasticSearch;
using ATW.CommonBase.Global;
using ATW.CommonBase.Model.Log;
using CommunityToolkit.Mvvm.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Services.Log
{
    public class LogESService
    {

        #region Parameter

        /// <summary>
        /// 临时存储日志
        /// </summary>
        private List<LogESModel> LogModels = new List<LogESModel>();

        private CancellationTokenSource? Cts;

        private object obj = new object();

        /// <summary>
        /// 
        /// </summary>
        private bool LoadESRepositoryYN { get; set; } = false;
        ElasticSearchRepository ES{ get; set; }
        

        #endregion

        #region 加载

        public LogESService()
        {
            StartService();
        }

        #endregion

        #region 写入日志

        public void WriteAsync(string index, object value)
        {
            Task.Run(() => {
                lock (obj)
                {
                    //缓存一万条默认ES数据库未启用
                    if (LogModels.Count>10000)
                    {
                        LogModels = null;
                    }
                    if (LogModels!=null)
                    {
                        LogModels.Add(new LogESModel()
                        {
                            Index = index,
                            Value = value
                        });
                    }
                }
            });
        }

        #endregion

        #region 启动服务

        /// <summary>
        /// 启动服务
        /// </summary>
        public void StartService()
        {
            Cts = new CancellationTokenSource();
            Task.Run(() =>
            {
                while (!Cts.Token.IsCancellationRequested)
                {
                    lock (obj)
                    {
                        try
                        {

                            #region 注入ES

                            if (!LoadESRepositoryYN&& GlobalModel.AppLoadYN)
                            {
                                LoadESRepositoryYN = true;
                                ES= Ioc.Default.GetRequiredService<ElasticSearchRepository>();
                            }

                            #endregion

                            #region 日志按天存入ES

                            if (LoadESRepositoryYN && LogModels!=null && LogModels.Count() > 0)
                            {
                                var logModels = LogModels.GroupBy(it=>it.Index).ToList();
                                for (int i = 0; i < logModels.Count(); i++)
                                {
                                    var _logModels = LogModels.FindAll(it => { return it.Index == logModels[i].Key.ToString(); });
                                    var values = new List<object>();
                                    for (int j = 0; j < _logModels.Count;j++)
                                    {
                                        values.Add(_logModels[i].Value);
                                    }
                                    var result = ES.BulkWrite(logModels[i].Key.ToString(), values);
                                    LogModels.RemoveAll(it => it.Index == logModels[i].Key.ToString());
                                }
                            }

                            #endregion

                        }
                        catch (Exception)
                        {
                        }
                    }
                    System.Threading.Thread.Sleep(10);
                }
            });
        }

        #endregion

        #region 关闭服务 

        public void StopService()
        {
            if (Cts != null)
            {
                Cts.Cancel();
                Cts = null;
            }
        }

        #endregion

    }
}
