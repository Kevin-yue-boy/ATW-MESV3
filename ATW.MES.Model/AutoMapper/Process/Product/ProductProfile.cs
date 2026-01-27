using ATW.MES.Model.DTOs.Process.Product;
using ATW.MES.Model.DTOs.System.BaseData;
using ATW.MES.Model.Entitys.Process.Product;
using ATW.MES.Model.Entitys.System.BaseData;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.Model.AutoMapper.Process.Product
{
    public class ProductProfile : Profile
    {

        public ProductProfile()
        {

            CreateMap<ProductEntity, ProductResponse>();
            CreateMap<ProductResponse, ProductEntity>();

        }


    }
}
