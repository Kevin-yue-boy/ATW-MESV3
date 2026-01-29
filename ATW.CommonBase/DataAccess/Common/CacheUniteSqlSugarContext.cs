using ATW.CommonBase.Model.DataAccess;
using AutoMapper;
using NPOI.HSSF.Util;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.CommonBase.DataAccess.Common
{
    public class CacheUniteSqlSugarContext<DTO, Entity> where DTO : class, new() where Entity : class, new()
    {

        #region parameter

        protected ISqlSugarClient DB { get; set; }
        protected CacheUniteSqlSugarRepository CUSR { get; set; }
        protected IMapper IM;

        #endregion

        #region 添加

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dTo">请求实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Insert(DTO dTo)
        {
            try
            {
                var entity = IM.Map<Entity>(dTo);
                return await DB.Insertable(entity).ExecuteCommandAsync();
            }
            finally
            {
                // 新增后清空缓存，保证后续查询数据最新
                CUSR.RemoveList<Entity>();
            }
        }

        #endregion

        #region 编辑

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dTO">请求实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Edit(DTO dTO)
        {
            try
            {
                var entity = IM.Map<Entity>(dTO);
                return await DB.Updateable(entity)
                    .Where($"GUID='{EntityConverter.GetEntityPropertyValue(dTO, "GUID")}'")
                    .ExecuteCommandAsync();
            }
            finally
            {
                // 编辑后清空缓存，保证后续查询数据最新
                CUSR.RemoveList<Entity>();
            }
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dTO">请求实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> Delete(DTO dTO)
        {
            try
            {
                var entity = IM.Map<Entity>(dTO);
                return await DB.Deleteable<Entity>()
                    .Where($"GUID='{EntityConverter.GetEntityPropertyValue(dTO,"GUID")}'")
                    .ExecuteCommandAsync();
            }
            finally
            {
                // 删除后清空缓存，保证后续查询数据最新
                CUSR.RemoveList<Entity>();
            }
        }

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询工序信息
        /// </summary>
        /// <param name="pagingQueryRequest">查询条件实体</param>
        /// <returns>分页后的工序信息列表</returns>
        public async Task<List<DTO>> PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
            var entitys = await CUSR.PagingQueryAsync<Entity>(pagingQueryRequest);
            return IM.Map<List<DTO>>(entitys);
        }

        #endregion

        #region 查找工作类型信息

        /// <summary>
        /// 查找工作类型信息
        /// </summary>
        /// <returns>工作类型信息列表</returns>
        public async Task<List<DTO>> Get()
        {
            var baseJobTypeEntitys = await CUSR.GetListAsync<Entity>();
            return IM.Map<List<DTO>>(baseJobTypeEntitys);
        }

        #endregion

    }
}
