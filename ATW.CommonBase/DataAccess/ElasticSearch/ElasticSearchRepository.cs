using ATW.CommonBase.Model.Log;
using Elastic.Clients.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.DataAccess.ElasticSearch
{
    public class ElasticSearchRepository
    {

        #region Parameter

        public readonly ElasticsearchClient ESClient;

        #endregion

        #region 构造函数

        public ElasticSearchRepository(ElasticsearchClient elasticClient)
        {
            this.ESClient = elasticClient;
        }

        #endregion

        #region 删除索引

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <returns></returns>
        public async Task DeleteIndexAsync(string index)
        {
            await ESClient.Indices.DeleteAsync(index);
        }

        #endregion

        #region 获取模糊匹配索引

        /// <summary>
        /// 获取模糊匹配索引
        /// </summary>
        /// <param name="index">*_*</param>
        /// <returns></returns>
        public async Task<List<string>> GetIndexsAsync(string index)
        {
            var getIndexResponse = await ESClient.Indices.GetAsync(index);
            if (getIndexResponse.IsSuccess())
            {
                return getIndexResponse.Indices.Keys.ToList();
            }
            return null;
        }

        #endregion

        #region 单条写入

        /// <summary>
        /// 单条写入
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> WriteAsync(string index, object value)
        {
            try
            {
                var response = await ESClient.IndexAsync(value, i => i.Index(index));
                return response.IsSuccess();
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        #endregion

        #region 批量异步写入

        /// <summary>
        /// 批量写入
        /// </summary>
        /// <param name="index"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public async Task<bool> BulkWriteAsync(string index, List<object> values)
        {
            if (values == null || values.Count == 0)
                return false;
            var response = await ESClient.BulkAsync(b => b
               .Index(index) // 目标索引（小写）
               .IndexMany(values) // 批量添加索引操作
               );
            return response.IsSuccess();
        }

        #endregion

        #region 批量写入

        /// <summary>
        /// 批量写入
        /// </summary>
        /// <param name="index"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool BulkWrite(string index, List<object> values)
        {
            if (values == null || values.Count == 0)
                return false;
            var response = ESClient.Bulk(b => b
               .Index(index) // 目标索引（小写）
               .IndexMany(values) // 批量添加索引操作
               );
            return response.IsSuccess();
        }

        #endregion

        #region 查询示例

        ///// <summary>
        ///// 查询
        ///// </summary>
        ///// <param name="index"></param>
        ///// <returns></returns>
        //public async Task<List<OperateLogModel>> Get(string index, int from, int size)
        //{
        //    var result = await ESClient.SearchAsync<OperateLogModel>(s => s
        //    .Indices(index)
        //    .Query(q => q.MatchAll())
        //    //分页
        //    .From(from)
        //    .Size(size)
        //    //排序
        //    // .Sort(c => c.Field("age", SortDirection.desc))
        //    );
        //    return result.Documents.ToList();
        //}

        #endregion

        #region 查找索引对应数据数量

        /// <summary>
        /// 查找索引对应数据数量
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<long> GetQtyByIndex(string index)
        {
            var countResponse = await ESClient.CountAsync<object>(c => c.Indices(index));
            long liveDocumentCount = countResponse.Count; // 仅存活文档（未删除）
            return liveDocumentCount;
        }

        #endregion

        #region 查找索引对应数据数量 多索引

        /// <summary>
        /// 查找索引对应数据数量 多索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<long> GetQtyByIndex(string[] indexs)
        {
            var countResponse = await ESClient.CountAsync<object>(c => c.Indices(indexs));
            long liveDocumentCount = countResponse.Count; // 仅存活文档（未删除）
            return liveDocumentCount;
        }

        #endregion

    }

}
