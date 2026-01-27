using ATW.CommonBase.Model;
using RestSharp;
using System.Diagnostics;
using System.Security.Policy;

namespace ATW.CommonBase.Communicate.HTTP
{
    public class HttpClient_RestShap : ICommunicateHttpClient
    {

        #region 发送Http请求-POST-Json-RestShap

        /// <summary>
        /// 发送Http请求-POST-Json-RestShap
        /// </summary>
        /// <typeparam name="TResponse">返回实体数据</typeparam>
        /// <typeparam name="TRequest">请求实体数据</typeparam>
        /// <param name="url">url</param>
        /// <param name="requestData">请求数据</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="logFileName">日志文件名,null则不记录日志</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<TResponse?> SendPostJsonAsync<TResponse, TRequest>(
            string url,
            TRequest requestData,
            int timeout = 5000,
            string logFileName = "",
            CancellationToken ct = default)
            where TResponse : class, new()
        {

            #region Parameter

            var sw = Stopwatch.StartNew();
            string errMsg = "";
            string? responseMsg = "";

            #endregion

            try
            {
                var httpClient = new RestClient(url);
                var restRequest = new RestRequest();
                restRequest.AddHeader("Content-Type", "application/json; charset=utf-8");
                var requestMsg = System.Text.Json.JsonSerializer.Serialize(requestData, SerializerGlobal.jsonOption_DatetimeConverter);
                restRequest.AddJsonBody(requestMsg);
                RestResponse responsePost = await httpClient.ExecutePostAsync(restRequest, ct);
                if (!responsePost.IsSuccessful)
                {
                    throw new HttpRequestException(
                       $"HTTP请求失败: {responsePost.StatusCode}\n响应内容: {responsePost.Content}");
                }
                responseMsg = responsePost.Content;
                return System.Text.Json.JsonSerializer.Deserialize<TResponse>(responsePost.Content);
            }
            catch (Exception ex)
            {
                errMsg =ex.ToString();
                throw;
            }
            finally
            {
                sw.Stop();
                if (!string.IsNullOrWhiteSpace(logFileName))
                {
                    //Logger.HttpLog($"\rRequest({url}):{System.Text.Json.JsonSerializer.Serialize(requestData, SerializerGlobal.jsonOption_DatetimeConverter)}" +
                    //$"\rResponse({url}):{responseMsg}" +
                    //$"{(string.IsNullOrWhiteSpace(errMsg) ? "" : $"\r异常报错:{errMsg}")}" +
                    //$"\r耗时统计=>处理数据(超时时间:{timeout}):{sw.ElapsedMilliseconds}", logFileName);
                }
            }

        }

        #endregion

    }
}
