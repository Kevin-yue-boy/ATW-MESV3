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

            CreateMap<BaseProcessEntity, BaseProcessDTO>();
            CreateMap<BaseProcessDTO, BaseProcessEntity>();

            CreateMap<BaseProductTypeEntity, BaseProductTypeDTO>();
            CreateMap<BaseProductTypeDTO, BaseProductTypeEntity>();

            CreateMap<BaseUnitEntity, BaseUnitDTO>();
            CreateMap<BaseUnitDTO, BaseUnitEntity>();

            CreateMap<BaseWorkTypeEntity, BaseWorkTypeDTO>();
            CreateMap<BaseWorkTypeDTO, BaseWorkTypeEntity>();

            CreateMap<BaseToolTypeEntity, BaseToolTypeDTO>()
                .ForMember(d => d.WorkTypeName, opt =>
                {
                    opt.MapFrom(s => s.WorkTypeGUID);
                });
            CreateMap<BaseToolTypeDTO, BaseToolTypeEntity>();

        }

    }

}
