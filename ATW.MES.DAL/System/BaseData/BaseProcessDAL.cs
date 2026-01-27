using ATW.CommonBase.DataAccess.Common;
using ATW.CommonBase.Model.DataAccess;
using ATW.MES.Model.DTOs.Process.ProcessRoute;
using ATW.MES.Model.DTOs.System.BaseData;
using ATW.MES.Model.Entitys.Process.ProcessRoute;
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
    public class BaseProcessDAL
    {

        #region Parameter

        private ISqlSugarClient DB { get; set; }
        private CacheUniteSqlSugarRepository CUSR { get; set; }
        private readonly IMapper IM;

        #endregion

        #region 构造函数

        public BaseProcessDAL(ISqlSugarClient sqlSugarClient, CacheUniteSqlSugarRepository cUSR, IMapper iM)
        {
            IM = iM;
            // 沿用示例中的数据库连接名称 "MainDB"，请根据实际情况调整
            DB = sqlSugarClient.AsTenant().GetConnectionScope("MainDB");
            CUSR = cUSR;
        }

        #endregion

        #region 查找工序信息

        /// <summary>
        /// 查找工序信息
        /// </summary>
        /// <returns>工序信息列表</returns>
        public async Task<List<BaseProcessResponse>> Get()
        {
            var baseProcessEntitys = await CUSR.GetListAsync<BaseProcessEntity>();
            return IM.Map<List<BaseProcessResponse>>(baseProcessEntitys);
        }

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询工序信息
        /// </summary>
        /// <param name="pagingQueryRequest">查询条件实体</param>
        /// <returns>分页后的工序信息列表</returns>
        public async Task<List<BaseProcessResponse>> PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
            var baseProcessEntitys = await CUSR.PagingQueryAsync<BaseProcessEntity>(pagingQueryRequest);
            return IM.Map<List<BaseProcessResponse>>(baseProcessEntitys);
        }

        #endregion

        #region 添加工序信息

        /// <summary>
        /// 添加工序信息
        /// </summary>
        /// <param name="baseProcessResponse">工序信息请求实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Insert(BaseProcessResponse baseProcessResponse)
        {
            try
            {
                var baseProcessEntity = IM.Map<BaseProcessEntity>(baseProcessResponse);
                return await DB.Insertable(baseProcessEntity).ExecuteCommandAsync();
            }
            finally
            {
                // 新增后清空缓存，保证后续查询数据最新
                CUSR.RemoveList<BaseProcessEntity>();
            }
        }

        #endregion

        #region 编辑工序信息

        /// <summary>
        /// 编辑工序信息
        /// </summary>
        /// <param name="baseProcessResponse">工序信息请求实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Edit(BaseProcessResponse baseProcessResponse)
        {
            try
            {
                var baseProcessEntity = IM.Map<BaseProcessEntity>(baseProcessResponse);
                // 以GUID作为更新条件（和示例保持一致）
                return await DB.Updateable(baseProcessEntity)
                    .Where(it => it.GUID == baseProcessResponse.GUID)
                    .ExecuteCommandAsync();
            }
            finally
            {
                // 编辑后清空缓存，保证后续查询数据最新
                CUSR.RemoveList<BaseProcessEntity>();
            }
        }

        #endregion

        #region 删除工序信息

        /// <summary>
        /// 删除工序信息
        /// </summary>
        /// <param name="baseProcessResponse">工序信息请求实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Delete(BaseProcessResponse baseProcessResponse)
        {
            try
            {
                var baseProcessEntity = IM.Map<BaseProcessEntity>(baseProcessResponse);
                // 以GUID作为删除条件（和示例保持一致）
                return await DB.Deleteable<BaseProcessEntity>()
                    .Where(it => it.GUID == baseProcessEntity.GUID)
                    .ExecuteCommandAsync();
            }
            finally
            {
                // 删除后清空缓存，保证后续查询数据最新
                CUSR.RemoveList<BaseProcessEntity>();
            }
        }

        #endregion

       

    }
}
