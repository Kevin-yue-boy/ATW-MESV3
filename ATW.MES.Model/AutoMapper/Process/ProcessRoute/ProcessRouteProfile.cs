using ATW.MES.Model.DTOs.Process.ProcessRoute;
using ATW.MES.Model.Entitys.Process.ProcessRoute;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.AutoMapper.Process.ProcessRoute
{
    public class ProcessRouteProfile: Profile
    {

        public ProcessRouteProfile()
        {
            CreateMap<ProcessRouteEntity, ProcessRouteResponse>()
                .ForMember(d => d.ProcessName, opt =>
                {
                    opt.MapFrom(s => s.BaseProcessGUID);
                });

            CreateMap<ProcessRouteBaseEntity, ProcessRouteBaseResponse>();


        }

    }
}
