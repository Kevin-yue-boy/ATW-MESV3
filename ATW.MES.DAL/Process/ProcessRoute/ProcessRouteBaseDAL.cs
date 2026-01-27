using ATW.CommonBase.DataAccess.Common;
using ATW.MES.Model.DTOs.Process.ProcessRoute;
using ATW.MES.Model.Entitys.Process.ProcessRoute;
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
    public class ProcessRouteBaseDAL
    {

        #region Parameter

        private ISqlSugarClient DB { get; set; }
        private CacheUniteSqlSugarRepository CUSR { get; set; }

        private readonly IMapper IM;

        #endregion

        #region 构造函数

        public ProcessRouteBaseDAL(ISqlSugarClient sqlSugarClient, CacheUniteSqlSugarRepository cUSR, IMapper iM)
        {
            IM = iM;
            DB = sqlSugarClient.AsTenant().GetConnectionScope("MainDB");
            CUSR = cUSR;
        }

        #endregion

        #region 根据工艺路线名称查询基础工艺路线信息

        /// <summary>
        /// 根据工艺路线名称查询基础工艺路线信息
        /// </summary>
        /// <param name="processRouteName">工艺路线名称</param>
        /// <returns></returns>
        public async Task<ProcessRouteBaseResponse> GetByProcessRouteName(string processRouteName)
        {
            ProcessRouteBaseResponse processRouteBaseResponse = new ProcessRouteBaseResponse();
            var processRouteBase = await CUSR.GetFirstAsync<ProcessRouteBaseEntity>(it => it.ProcessRouteName == processRouteName);
            processRouteBaseResponse = IM.Map<ProcessRouteBaseResponse>(processRouteBase);
            return processRouteBaseResponse;
        }

        #endregion

        #region 根据工艺路线名称查询基础工艺路线信息

        /// <summary>
        /// 根据工艺路线名称查询基础工艺路线信息
        /// </summary>
        /// <param name="processRouteName">工艺路线名称</param>
        /// <returns></returns>
        public async Task<ProcessRouteBaseResponse> GetByProcessRouteGUID(Guid processRouteGUID)
        {
            ProcessRouteBaseResponse processRouteBaseResponse = new ProcessRouteBaseResponse();
            var processRouteBase = await CUSR.GetFirstAsync<ProcessRouteBaseEntity>(it => it.GUID == processRouteGUID);
            processRouteBaseResponse = IM.Map<ProcessRouteBaseResponse>(processRouteBase);
            return processRouteBaseResponse;
        }

        #endregion

    }
}
