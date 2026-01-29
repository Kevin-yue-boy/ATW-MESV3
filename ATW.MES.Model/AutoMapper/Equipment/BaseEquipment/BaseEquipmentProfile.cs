using ATW.MES.Model.DTOs.Equipment.BaseEquipment;
using ATW.MES.Model.DTOs.Process.ProcessRoute;
using ATW.MES.Model.Entitys.Equipment.BaseEquipment;
using ATW.MES.Model.Entitys.Process.ProcessRoute;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.AutoMapper.Equipment.BaseEquipment
{
    public class BaseEquipmentProfile : Profile
    {

        public BaseEquipmentProfile()
        {

            CreateMap<EquipmentCommunicateEntity, EquipmentCommunicateDTO>();
            CreateMap<EquipmentCommunicateDTO, EquipmentCommunicateEntity>();

        }
    }
}
