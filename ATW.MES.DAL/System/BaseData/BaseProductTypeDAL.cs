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
    public class BaseProductTypeDAL
    {

        #region Parameter

        private ISqlSugarClient DB { get; set; }
        private CacheUniteSqlSugarRepository CUSR { get; set; }
        private readonly IMapper IM;

        #endregion

        #region 构造函数

        public BaseProductTypeDAL(ISqlSugarClient sqlSugarClient, CacheUniteSqlSugarRepository cUSR, IMapper iM)
        {
            IM = iM;
            DB = sqlSugarClient.AsTenant().GetConnectionScope("MainDB");
            CUSR = cUSR;
        }

        #endregion

        #region 查找产品类型信息

        /// <summary>
        /// 查找产品类型信息
        /// </summary>
        /// <returns>产品类型信息列表</returns>
        public async Task<List<BaseProductTypeResponse>> Get()
        {
            var baseProductTypeEntitys = await CUSR.GetListAsync<BaseProductTypeEntity>();
            return IM.Map<List<BaseProductTypeResponse>>(baseProductTypeEntitys);
        }

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询产品类型信息
        /// </summary>
        /// <param name="pagingQueryRequest">查询条件实体</param>
        /// <returns>分页后的产品类型信息列表</returns>
        public async Task<List<BaseProductTypeResponse>> PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
            var baseProductTypeEntitys = await CUSR.PagingQueryAsync<BaseProductTypeEntity>(pagingQueryRequest);
            return IM.Map<List<BaseProductTypeResponse>>(baseProductTypeEntitys);
        }

        #endregion

        #region 添加产品类型信息

        /// <summary>
        /// 添加产品类型信息
        /// </summary>
        /// <param name="baseProductTypeResponse">产品类型信息请求实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Insert(BaseProductTypeResponse baseProductTypeResponse)
        {
            try
            {
                var baseProductTypeEntity = IM.Map<BaseProductTypeEntity>(baseProductTypeResponse);
                return await DB.Insertable(baseProductTypeEntity).ExecuteCommandAsync();
            }
            finally
            {
                // 新增后清空缓存，保证后续查询数据最新
                CUSR.RemoveList<BaseProductTypeEntity>();
            }
        }

        #endregion

        #region 编辑产品类型信息

        /// <summary>
        /// 编辑产品类型信息
        /// </summary>
        /// <param name="baseProductTypeResponse">产品类型信息请求实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Edit(BaseProductTypeResponse baseProductTypeResponse)
        {
            try
            {
                var baseProductTypeEntity = IM.Map<BaseProductTypeEntity>(baseProductTypeResponse);
                return await DB.Updateable(baseProductTypeEntity)
                    .Where(it => it.GUID == baseProductTypeResponse.GUID)
                    .ExecuteCommandAsync();
            }
            finally
            {
                // 编辑后清空缓存，保证后续查询数据最新
                CUSR.RemoveList<BaseProductTypeEntity>();
            }
        }

        #endregion

        #region 删除产品类型信息

        /// <summary>
        /// 删除产品类型信息
        /// </summary>
        /// <param name="baseProductTypeResponse">产品类型信息请求实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Delete(BaseProductTypeResponse baseProductTypeResponse)
        {
            try
            {
                var baseProductTypeEntity = IM.Map<BaseProductTypeEntity>(baseProductTypeResponse);
                return await DB.Deleteable<BaseProductTypeEntity>()
                    .Where(it => it.GUID == baseProductTypeEntity.GUID)
                    .ExecuteCommandAsync();
            }
            finally
            {
                // 删除后清空缓存，保证后续查询数据最新
                CUSR.RemoveList<BaseProductTypeEntity>();
            }
        }

        #endregion

    }
}
