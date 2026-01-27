using ATW.CommonBase.DataAccess.Common;
using ATW.CommonBase.Model;
using ATW.CommonBase.Model.DataAccess;
using ATW.MES.Model.DTOs.Equipment.BaseEquipment;
using ATW.MES.Model.DTOs.Process.ProcessRoute;
using ATW.MES.Model.Entitys.Equipment.BaseEquipment;
using ATW.MES.Model.Entitys.Process.ProcessRoute;
using AutoMapper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.DAL.Equipment.BaseEquipment
{
    public class EquipmentCommunicateDAL
    {

        #region Parameter

        private ISqlSugarClient DB { get; set; }
        private CacheUniteSqlSugarRepository CUSR { get; set; }
        private readonly IMapper IM;

        #endregion

        #region 构造函数

        public EquipmentCommunicateDAL(ISqlSugarClient sqlSugarClient, CacheUniteSqlSugarRepository cUSR, IMapper iM)
        {
            IM = iM;
            DB = sqlSugarClient.AsTenant().GetConnectionScope("MainDB");
            CUSR = cUSR;
        }

        #endregion

        #region 查找设备通讯信息
        
        /// <summary>
        /// 查找设备通讯信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<EquipmentCommunicateResponse>> Get()
        {
            var equComEntitys = await CUSR.GetListAsync<EquipmentCommunicateEntity>();
            return IM.Map<List<EquipmentCommunicateResponse>>(equComEntitys);
        }

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pagingQueryRequest">查询条件实体</param>
        /// <returns></returns>
        public async Task<List<EquipmentCommunicateResponse>> PagingQueryAsync(PagingQueryRequestModel pagingQueryRequest)
        {
            var equComEntitys = await CUSR.PagingQueryAsync<EquipmentCommunicateEntity>(pagingQueryRequest);
            return IM.Map<List<EquipmentCommunicateResponse>>(equComEntitys);
        }

        #endregion

        #region 添加通讯信息

        /// <summary>
        /// 添加通讯信息
        /// </summary>
        /// <param name="equComResponse"></param>
        /// <returns></returns>
        public async Task<int> Insert(EquipmentCommunicateResponse equComResponse)
        {
            try
            {
                var equComEntity = IM.Map<EquipmentCommunicateEntity>(equComResponse);
                return await DB.Insertable(equComEntity).ExecuteCommandAsync();
            }
            finally
            {
                //清空缓存数据
                CUSR.RemoveList<EquipmentCommunicateEntity>();
            }
        }

        #endregion

        #region 编辑通讯信息

        /// <summary>
        /// 编辑通讯信息
        /// </summary>
        /// <param name="equComResponse"></param>
        /// <returns></returns>
        public async Task<int> Edit(EquipmentCommunicateResponse equComResponse)
        {
            try
            {
                var equComEntity = IM.Map<EquipmentCommunicateEntity>(equComResponse);
                return await DB.Updateable(equComEntity)
                    .Where(it => it.GUID == equComResponse.GUID)
                    .ExecuteCommandAsync();
            }
            finally
            {
                //清空缓存数据
                CUSR.RemoveList<EquipmentCommunicateEntity>();
            }
        }

        #endregion

        #region 删除通讯信息

        /// <summary>
        /// 删除通讯信息
        /// </summary>
        /// <param name="equComResponse"></param>
        /// <returns></returns>
        public async Task<int> Delete(EquipmentCommunicateResponse equComResponse)
        {
            try
            {
                var equComEntity = IM.Map<EquipmentCommunicateEntity>(equComResponse);
                return await DB.Deleteable<EquipmentCommunicateEntity>()
                    .Where(it => it.GUID == equComEntity.GUID)
                    .ExecuteCommandAsync();
            }
            finally
            {
                //清空缓存数据
                CUSR.RemoveList<EquipmentCommunicateEntity>();
            }
        }

        #endregion

    }
}
