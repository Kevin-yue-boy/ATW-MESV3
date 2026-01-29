using ATW.CommonBase.Model;
using ATW.MES.DAL.Process.ProcessRoute;
using ATW.MES.Model.DTOs.Process.ProcessRoute;
using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.BLL.Process.ProcessRoute
{
    public class ProcessRouteBLL
    {

        /// <summary>
        /// 根据工艺路线名称查询工艺路线信息
        /// </summary>
        /// <param name="processRouteName"></param>
        /// <returns></returns>
        public async Task<List<ProcessRouteDTO>> GetByProcessRouteName(string processRouteName, ResponseModel responseModel)
        {
            responseModel.Result = true;
            var processRouteBaseResponse = await Ioc.Default.GetRequiredService<ProcessRouteBaseDAL>()
                .GetByProcessRouteName(processRouteName);
            if (processRouteBaseResponse == null) 
            {
                responseModel.Result = false;
                responseModel.Msg = $"根据工艺路线名称:{processRouteName}未查询到相关工艺路线数据!";
                return null;
            }
            var values = await GetByProcessRouteGUID(processRouteBaseResponse.GUID, responseModel);
            return values;
        }




        /// <summary>
        /// 根据工艺路线GUID查询工艺路线信息
        /// </summary>
        /// <param name="processRouteGUID"></param>
        /// <returns></returns>
        public async Task<List<ProcessRouteDTO>> GetByProcessRouteGUID
            (Guid processRouteGUID, ResponseModel responseModel)
        {
            responseModel.Result = true;
            var processRouteBaseResponse = await Ioc.Default.GetRequiredService<ProcessRouteBaseDAL>()
               .GetByProcessRouteGUID(processRouteGUID);
            if (processRouteBaseResponse == null) if (processRouteBaseResponse == null)
                {
                    responseModel.Result = false;
                    responseModel.Msg = $"根据工艺路线GUID:{processRouteGUID.ToString()}未查询到相关工艺路线数据!";
                    return null;
                }
            return await Ioc.Default.GetRequiredService<ProcessRouteDAL>()
                .GetByProcessRouteGUID(processRouteGUID);
        }

    }
}
