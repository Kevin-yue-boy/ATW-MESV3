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
    public class BaseUnitDAL
    {

        #region Parameter

        private ISqlSugarClient DB { get; set; }
        private CacheUniteSqlSugarRepository CUSR { get; set; }
        private readonly IMapper IM;

        #endregion

        #region 构造函数

        public BaseUnitDAL(ISqlSugarClient sqlSugarClient, CacheUniteSqlSugarRepository cUSR, IMapper iM)
        {
            IM = iM;
            DB = sqlSugarClient.AsTenant().GetConnectionScope("MainDB");
            CUSR = cUSR;
        }

        #endregion

        #region 查找单位信息

        /// <summary>
        /// 查找单位信息
        /// </summary>
        /// <returns>单位信息列表</returns>
        public async Task<List<BaseUnitResponse>> Get()
        {
            var baseUnitEntitys = await CUSR.GetListAsync<BaseUnitEntity>();
            return IM.Map<List<BaseUnitResponse>>(baseUnitEntitys);
        }

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询单位信息
        /// </summary>
        /// <param name="pagingQueryRequest">查询条件实体</param>
        /// <returns>分页后的单位信息列表</returns>
        public async Task<List<BaseUnitResponse>> PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
            var baseUnitEntitys = await CUSR.PagingQueryAsync<BaseUnitEntity>(pagingQueryRequest);
            return IM.Map<List<BaseUnitResponse>>(baseUnitEntitys);
        }

        #endregion

        #region 添加单位信息

        /// <summary>
        /// 添加单位信息
        /// </summary>
        /// <param name="baseUnitResponse">单位信息请求实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Insert(BaseUnitResponse baseUnitResponse)
        {
            try
            {
                var baseUnitEntity = IM.Map<BaseUnitEntity>(baseUnitResponse);
                return await DB.Insertable(baseUnitEntity).ExecuteCommandAsync();
            }
            finally
            {
                // 新增后清空缓存，保证后续查询数据最新
                CUSR.RemoveList<BaseUnitEntity>();
            }
        }

        #endregion

        #region 编辑单位信息

        /// <summary>
        /// 编辑单位信息
        /// </summary>
        /// <param name="baseUnitResponse">单位信息请求实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Edit(BaseUnitResponse baseUnitResponse)
        {
            try
            {
                var baseUnitEntity = IM.Map<BaseUnitEntity>(baseUnitResponse);
                return await DB.Updateable(baseUnitEntity)
                    .Where(it => it.GUID == baseUnitResponse.GUID)
                    .ExecuteCommandAsync();
            }
            finally
            {
                // 编辑后清空缓存，保证后续查询数据最新
                CUSR.RemoveList<BaseUnitEntity>();
            }
        }

        #endregion

        #region 删除单位信息

        /// <summary>
        /// 删除单位信息
        /// </summary>
        /// <param name="baseUnitResponse">单位信息请求实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Delete(BaseUnitResponse baseUnitResponse)
        {
            try
            {
                var baseUnitEntity = IM.Map<BaseUnitEntity>(baseUnitResponse);
                return await DB.Deleteable<BaseUnitEntity>()
                    .Where(it => it.GUID == baseUnitEntity.GUID)
                    .ExecuteCommandAsync();
            }
            finally
            {
                // 删除后清空缓存，保证后续查询数据最新
                CUSR.RemoveList<BaseUnitEntity>();
            }
        }

        #endregion

    }
}
