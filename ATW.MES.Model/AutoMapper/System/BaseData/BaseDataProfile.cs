using ATW.MES.Model.DTOs.Process.ProcessRoute;
using ATW.MES.Model.DTOs.System.BaseData;
using ATW.MES.Model.Entitys.Process.ProcessRoute;
using ATW.MES.Model.Entitys.System.BaseData;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.AutoMapper.System.BaseData
{
    public class BaseDataProfile : Profile
    {

        public BaseDataProfile()
        {
            CreateMap<BaseProcessEntity, BaseProcessResponse>();
            CreateMap<BaseProcessResponse, BaseProcessEntity>();

            CreateMap<BaseProductTypeEntity, BaseProductTypeResponse>();
            CreateMap<BaseProductTypeResponse, BaseProductTypeEntity>();

            CreateMap<BaseUnitEntity, BaseUnitResponse>();
            CreateMap<BaseUnitResponse, BaseUnitEntity>();

        }

    }

}
