using ATW.CommonBase.DataAccess.Common;
using ATW.CommonBase.DataAccess.ElasticSearch;
using ATW.CommonBase.DataProcessing.DataConverter;
using ATW.CommonBase.DataProcessing.DataExpand;
using ATW.CommonBase.Model.DataAccess;
using ATW.MES.Model.DTOs.Equipment.BaseEquipment;
using ATW.MES.Model.DTOs.System.Log;
using ATW.MES.Model.Entitys.Equipment.BaseEquipment;
using ATW.MES.Model.Entitys.System.Log;
using AutoMapper;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.DAL.System.Log
{
    public class UserOperateLogDAL
    {

        #region Parameter

        private ElasticSearchRepository ES { get; set; }
        private readonly IMapper IM;

        #endregion

        #region 构造函数

        public UserOperateLogDAL(ElasticSearchRepository eS, IMapper iM)
        {
            IM = iM;
            ES = eS;
        }

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pagingQueryRequest">查询条件实体</param>
        /// <returns></returns>
        public async Task<List<UserOperateLogDTO>> PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest
            , string field
            , string value
            , DateTime start
            , DateTime end)
        {

            #region 生成indices

            var indices_date = DateConverter.GetDaysBetweenDates(start, end);
            if (indices_date == null || indices_date.Count < 1) { return null; }
            var indices_date_exists = new List<string>();
            var indices_like =await ES.GetIndexsAsync("operatelog_*");
            for (int i = 0; i < indices_date.Count(); i++)
            {
                if (indices_like.Exists(it => it.Split('_')[1] == indices_date[i]))
                {
                    indices_date_exists.Add($"operatelog_{indices_date[i]}");
                }
            }
            var indices = indices_date_exists.ToArray();

            #endregion

            #region 查询数据

            List<UserOperateLogEntity> userOperateLogEntitys = new List<UserOperateLogEntity>();

            if (!string.IsNullOrWhiteSpace(field))
            {
                #region 条件查询

                userOperateLogEntitys = (await ES.ESClient.SearchAsync<UserOperateLogEntity>(s => s
                .Indices(indices)
               .Query(q => q
                .Fuzzy(f => f
                    .Field(StringTypeConverter.FirstCharToLower(field)) // 要模糊匹配的字段 //首字母小写
                    .Value(value) // 匹配的关键词
                    .PrefixLength(0)
                    )
                )
                //分页
                .From((pagingQueryRequest.PageIndex - 1) * pagingQueryRequest.MaxPageCount)
                .Size(pagingQueryRequest.MaxPageCount)
                //排序
                .Sort(c => c.Field("createTime", SortOrder.Desc))//首字母小写
                )).Documents.ToList();

                #endregion
            }
            else
            {
                #region 查询所有

                userOperateLogEntitys = (await ES.ESClient.SearchAsync<UserOperateLogEntity>(s => s
                .Indices(indices)
                .Query(q => q.MatchAll())
                //分页
                .From((pagingQueryRequest.PageIndex - 1) * pagingQueryRequest.MaxPageCount)
                .Size(pagingQueryRequest.MaxPageCount)
                //排序
                .Sort(c => c.Field("createTime", SortOrder.Desc))//首字母小写
                )).Documents.ToList();

                #endregion
            }

            //调试用 勿删
            //var byteArray = models.ApiCallDetails.RequestBodyInBytes;
            //string utf8Str = Encoding.UTF8.GetString(byteArray);

            #endregion

            #region 查询数据数量 并计算页面数量

            if (!string.IsNullOrWhiteSpace(field))
            {
                #region 条件查询

                pagingQueryRequest.DataCount = (await ES.ESClient.CountAsync<object>(
                c => c.Indices(indices)
                .Query(q => q
                 .Fuzzy(f => f
                 .Field(StringTypeConverter.FirstCharToLower(field)) // 要模糊匹配的字段 //首字母小写
                 .Value(value) // 匹配的关键词
                 ))
                )).Count;

                #endregion
            }
            else
            {
                #region 查询所有

                pagingQueryRequest.DataCount = (await ES.ESClient.CountAsync<object>(
                c => c.Indices(indices)
                )).Count;

                #endregion
            }

            
            pagingQueryRequest.PageCount = pagingQueryRequest.DataCount % pagingQueryRequest.MaxPageCount > 0 ? Convert.ToInt32(pagingQueryRequest.DataCount / pagingQueryRequest.MaxPageCount) + 1 : Convert.ToInt32(pagingQueryRequest.DataCount / pagingQueryRequest.MaxPageCount);
            pagingQueryRequest.ViewPageIndex = $"{pagingQueryRequest.PageIndex}/{pagingQueryRequest.PageCount}";

            #endregion

            return IM.Map<List<UserOperateLogDTO>>(userOperateLogEntitys);

        }

        #endregion

    }
}
