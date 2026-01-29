using ATW.CommonBase.DataAccess.Common;
using ATW.CommonBase.DataAccess.Redis;
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

namespace ATW.MES.DAL.Process.ProcessRoute
{
    public class ProcessRouteDAL
    {

        #region Parameter

        private ISqlSugarClient DB { get; set; }
        private CacheUniteSqlSugarRepository CUSR { get; set; }

        private readonly IMapper IM;

        #endregion

        #region 构造函数

        public ProcessRouteDAL(ISqlSugarClient sqlSugarClient, CacheUniteSqlSugarRepository cUSR, IMapper iM)
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
        public async Task<List<ProcessRouteDTO>> GetByProcessRouteGUID(Guid processRouteGUID)
        {
            List<ProcessRouteDTO> processRouteDTOs = new List<ProcessRouteDTO>();
            var processRoutes = await CUSR.GetListAsync<ProcessRouteEntity>(it => it.ProcessRouteGUID == processRouteGUID);
            processRouteDTOs = IM.Map<List<ProcessRouteDTO>>(processRoutes);
            if (processRouteDTOs!=null&& processRouteDTOs.Count>0)
            {
                var baseProcessEntitys = await CUSR.GetListAsync<BaseProcessEntity>();
                processRouteDTOs.ForEach(it => {
                    it.ProcessName = baseProcessEntitys?.Find(ix => { return ix.GUID == it.BaseProcessGUID; }).ProcessName;
                    it.ProcessCode = baseProcessEntitys?.Find(ix => { return ix.GUID == it.BaseProcessGUID; }).ProcessCode;
                });
            }
            return processRouteDTOs;
        }

        #endregion

    }
}
