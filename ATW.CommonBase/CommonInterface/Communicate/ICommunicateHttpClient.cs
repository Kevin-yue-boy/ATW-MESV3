using ATW.CommonBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.CommonInterface.Communicate
{
    public interface ICommunicateHttpClient
    {

        /// <summary>
        /// 发送Http请求-POST-Json+log
        /// </summary>
        /// <typeparam name="TResponse">返回实体数据</typeparam>
        /// <typeparam name="TRequest">请求实体数据</typeparam>
        /// <param name="url">url</param>
        /// <param name="requestData">请求数据</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="logFileName">日志文件名,null则不记录日志</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<TResponse?> SendPostJsonAsync<TResponse, TRequest>(
            string url,
            TRequest requestData,
            int timeout = 5000,
            string logFileName = "",
            CancellationToken ct = default)
            where TResponse : class, new();



    }
}
