using ATW.MES.Model.DTOs.Process.ProcessRoute;
using ATW.MES.Model.DTOs.System.Log;
using ATW.MES.Model.Entitys.Process.ProcessRoute;
using ATW.MES.Model.Entitys.System.Log;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.AutoMapper.System.Log
{
    public class LogProfile : Profile
    {

        public LogProfile()
        {
            CreateMap<UserOperateLogEntity, UserOperateLogDTO>();
            CreateMap<UserOperateLogDTO, UserOperateLogEntity>();

        }

    }

}
