using ATW.CommonBase.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.Services.Log
{
    public class LogTxtService
    {

        #region Parameter

        /// <summary>
        /// 日志文件地址
        /// </summary>
        public string FilePathBase { get; set; } = "D:\\Logs";

        /// <summary>
        /// 日志类型文件名称
        /// </summary>
        public string TypeFileName { get; set; } = "Common";

        /// <summary>
        /// 临时存储日志
        /// </summary>
        private List<LogTxtModel> LogModels = new List<LogTxtModel>();

        private CancellationTokenSource? Cts;

        private object obj = new object();

        #endregion

        #region 加载

        public LogTxtService(string filePath, string typeFileName)
        {
            FilePathBase = filePath;
            TypeFileName = typeFileName;
            StartService();
        }

        #endregion

        #region 写入日志

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="value"></param>
        public void WriteAsync(string value, string secondaryFileName = "")
        {
            Task.Run(() => {
                lock (obj)
                {
                    LogModels.Add(new LogTxtModel()
                    {
                        FilePath = FilePathBase
                        + $"\\{TypeFileName}"
                        + (string.IsNullOrWhiteSpace(secondaryFileName) ? "" : $"\\{secondaryFileName}")
                        + $"\\{DateTime.Now.ToString("yyyyMM")}\\{DateTime.Now.ToString("yyyy-MM-dd")}.log",
                        Msg = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff} {value}"
                    });
                }
            });
        }

        #endregion

        #region 写入日志-实体

        /// <summary>
        /// 写入日志-实体 根据Description特性打印
        /// </summary>
        /// <param name="value"></param>
        public void WriteEntityAsync(string value, string secondaryFileName = "")
        {
            Task.Run(() => {
                lock (obj)
                {
                    LogModels.Add(new LogTxtModel()
                    {
                        FilePath = FilePathBase
                        + $"\\{TypeFileName}"
                        + (string.IsNullOrWhiteSpace(secondaryFileName) ? "" : $"\\{secondaryFileName}")
                        + $"\\{DateTime.Now.ToString("yyyyMM")}\\{DateTime.Now.ToString("yyyy-MM-dd")}.log",
                        Msg = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")} {value}"
                    });
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
                            if (LogModels.Count() > 0)
                            {
                                var filePaths = LogModels.Select(item => (item.FilePath)).Distinct().ToList();
                                for (int i = 0; i < filePaths.Count(); i++)
                                {
                                    if (string.IsNullOrEmpty(filePaths[i].Trim()))
                                        throw new Exception("文件路径不能为空！");
                                    if (!Directory.Exists(filePaths[i].Substring(0, filePaths[i].LastIndexOf('\\'))))
                                        Directory.CreateDirectory(filePaths[i].Substring(0, filePaths[i].LastIndexOf('\\')));
                                    using (FileStream fs = new FileStream(filePaths[i], FileMode.Append))
                                    {
                                        StreamWriter sw = new StreamWriter(fs);
                                        var logModels = LogModels.FindAll(it => { return it.FilePath == filePaths[i]; });
                                        for (int j = 0; j < logModels.Count(); j++)
                                        {
                                            sw.Write($"{logModels[j].Msg}{Environment.NewLine}");
                                        }
                                        LogModels.RemoveAll(it => it.FilePath == filePaths[i]);
                                        sw.Flush();
                                    }
                                }

                                string _FilePath = FilePathBase + $"\\{TypeFileName}\\{DateTime.Now.ToString("yyyyMM")}\\{DateTime.Now.ToString("yyyy-MM-dd")}.log";

                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    System.Threading.Thread.Sleep(1);
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
