using ATW.CommonBase.DataAccess.Common;
using ATW.CommonBase.Model;
using ATW.MES.Model.DTOs.Process.ProcessRoute;
using ATW.MES.Model.Entitys.Process.ProcessRoute;
using ATW.MES.Model.Entitys.System.BaseData;
using AutoMapper;
using CommunityToolkit.Mvvm.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Services.Process.ProcessRoute
{
    public class ProcessRouteService
    {


        #region Parameter

        private ISqlSugarClient DB { get; set; }
        private CacheUniteSqlSugarRepository CUSR { get; set; }

        private readonly IMapper IM;

        #endregion

        #region 构造函数

        public ProcessRouteService(ISqlSugarClient sqlSugarClient, CacheUniteSqlSugarRepository cUSR, IMapper iM)
        {
            IM = iM;
            DB = sqlSugarClient.AsTenant().GetConnectionScope("MainDB");
            CUSR = cUSR;
        }

        #endregion

        #region 根据工艺路线GUID查询工艺路线信息

        /// <summary>
        /// 根据工艺路线GUID查询工艺路线信息
        /// </summary>
        /// <param name="processRouteGUID"></param>
        /// <returns></returns>
        public async Task<List<ProcessRouteResponse>> GetByProcessRouteGUID(Guid processRouteGUID, ResponesModel responesModel)
        {
            responesModel.Result = true;
            var processRouteBaseEntity = await CUSR.GetFirstAsync<ProcessRouteBaseEntity>(it => it.GUID == processRouteGUID);
            if (processRouteBaseEntity == null) if (processRouteBaseEntity == null)
                {
                    responesModel.Result = false;
                    responesModel.Msg = $"根据工艺路线GUID:{processRouteGUID.ToString()}未查询到相关工艺路线数据!";
                    return null;
                }
            List<ProcessRouteResponse> processRouteResponses = new List<ProcessRouteResponse>();
            var processRoutes = await CUSR.GetListAsync<ProcessRouteEntity>(it => it.ProcessRouteGUID == processRouteGUID);
            processRouteResponses = IM.Map<List<ProcessRouteResponse>>(processRoutes);
            if (processRouteResponses != null && processRouteResponses.Count > 0)
            {
                var baseProcessEntitys = await CUSR.GetListAsync<BaseProcessEntity>();
                processRouteResponses.ForEach(it => {
                    it.ProcessName = baseProcessEntitys?.Find(ix => { return ix.GUID == it.BaseProcessGUID; }).ProcessName;
                    it.ProcessCode = baseProcessEntitys?.Find(ix => { return ix.GUID == it.BaseProcessGUID; }).ProcessCode;
                });
            }
            return processRouteResponses;
        }

        #endregion

        #region 根据工艺路线GUID查询工艺路线信息

        /// <summary>
        /// 根据工艺路线GUID查询工艺路线信息
        /// </summary>
        /// <param name="processRouteGUID"></param>
        /// <returns></returns>
        public async Task<List<ProcessRouteResponse>> GetByProcessRouteGUID(Guid processRouteGUID)
        {
            List<ProcessRouteResponse> processRouteResponses = new List<ProcessRouteResponse>();
            var processRoutes = await CUSR.GetListAsync<ProcessRouteEntity>(it => it.ProcessRouteGUID == processRouteGUID);
            processRouteResponses = IM.Map<List<ProcessRouteResponse>>(processRoutes);
            if (processRouteResponses != null && processRouteResponses.Count > 0)
            {
                var baseProcessEntitys = await CUSR.GetListAsync<BaseProcessEntity>();
                processRouteResponses.ForEach(it => {
                    it.ProcessName = baseProcessEntitys?.Find(ix => { return ix.GUID == it.BaseProcessGUID; }).ProcessName;
                    it.ProcessCode = baseProcessEntitys?.Find(ix => { return ix.GUID == it.BaseProcessGUID; }).ProcessCode;
                });
            }
            return processRouteResponses;
        }

        #endregion

    }
}
