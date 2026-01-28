using ATW.CommonBase.DataAccess.Common;
using ATW.CommonBase.Model.DataAccess;
using ATW.MES.Model.DTOs.System.BaseData;
using ATW.MES.Model.Entitys.System.BaseData;
using AutoMapper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.DAL.System.BaseData
{
    public class BaseJobTypeDAL
    {

        #region Parameter

        private ISqlSugarClient DB { get; set; }
        private CacheUniteSqlSugarRepository CUSR { get; set; }
        private readonly IMapper IM;

        #endregion

        #region 构造函数

        public BaseJobTypeDAL(ISqlSugarClient sqlSugarClient, CacheUniteSqlSugarRepository cUSR, IMapper iM)
        {
            IM = iM;
            DB = sqlSugarClient.AsTenant().GetConnectionScope("MainDB");
            CUSR = cUSR;
        }

        #endregion

        #region 查找工作类型信息

        /// <summary>
        /// 查找工作类型信息
        /// </summary>
        /// <returns>工作类型信息列表</returns>
        public async Task<List<BaseJobTypeResponse>> Get()
        {
            var baseJobTypeEntitys = await CUSR.GetListAsync<BaseJobTypeEntity>();
            return IM.Map<List<BaseJobTypeResponse>>(baseJobTypeEntitys);
        }

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询工作类型信息
        /// </summary>
        /// <param name="pagingQueryRequest">查询条件实体</param>
        /// <returns>分页后的工作类型信息列表</returns>
        public async Task<List<BaseJobTypeResponse>> PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
            var baseJobTypeEntitys = await CUSR.PagingQueryAsync<BaseJobTypeEntity>(pagingQueryRequest);
            return IM.Map<List<BaseJobTypeResponse>>(baseJobTypeEntitys);
        }

        #endregion

        #region 添加工作类型信息

        /// <summary>
        /// 添加工作类型信息
        /// </summary>
        /// <param name="baseJobTypeResponse">工作类型信息请求实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Insert(BaseJobTypeResponse baseJobTypeResponse)
        {
            try
            {
                var baseJobTypeEntity = IM.Map<BaseJobTypeEntity>(baseJobTypeResponse);
                return await DB.Insertable(baseJobTypeEntity).ExecuteCommandAsync();
            }
            finally
            {
                // 新增后清空缓存，保证后续查询数据最新
                CUSR.RemoveList<BaseJobTypeEntity>();
            }
        }

        #endregion

        #region 编辑工作类型信息

        /// <summary>
        /// 编辑工作类型信息
        /// </summary>
        /// <param name="baseJobTypeResponse">工作类型信息请求实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Edit(BaseJobTypeResponse baseJobTypeResponse)
        {
            try
            {
                var baseJobTypeEntity = IM.Map<BaseJobTypeEntity>(baseJobTypeResponse);
                return await DB.Updateable(baseJobTypeEntity)
                    .Where(it => it.GUID == baseJobTypeResponse.GUID)
                    .ExecuteCommandAsync();
            }
            finally
            {
                // 编辑后清空缓存，保证后续查询数据最新
                CUSR.RemoveList<BaseJobTypeEntity>();
            }
        }

        #endregion

        #region 删除工作类型信息

        /// <summary>
        /// 删除工作类型信息
        /// </summary>
        /// <param name="baseJobTypeResponse">工作类型信息请求实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Delete(BaseJobTypeResponse baseJobTypeResponse)
        {
            try
            {
                var baseJobTypeEntity = IM.Map<BaseJobTypeEntity>(baseJobTypeResponse);
                return await DB.Deleteable<BaseJobTypeEntity>()
                    .Where(it => it.GUID == baseJobTypeEntity.GUID)
                    .ExecuteCommandAsync();
            }
            finally
            {
                // 删除后清空缓存，保证后续查询数据最新
                CUSR.RemoveList<BaseJobTypeEntity>();
            }
        }

        #endregion

    }
}
