using ATW.CommonBase.DataAccess.Common;
using ATW.CommonBase.Model.DataAccess;
using ATW.MES.Model.DTOs.System.BaseData;
using ATW.MES.Model.Entitys.System.BaseData;
using AutoMapper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.DAL.System.BaseData
{
    public class BaseProductTypeDAL : CacheUniteSqlSugarContext<BaseProductTypeDTO, BaseProductTypeEntity>
    {

        #region Parameter



        #endregion

        #region 构造函数

        public BaseProductTypeDAL(ISqlSugarClient sqlSugarClient, CacheUniteSqlSugarRepository cUSR, IMapper iM)
        {
            IM = iM;
            DB = sqlSugarClient.AsTenant().GetConnectionScope("MainDB");
            CUSR = cUSR;
        }

        #endregion

    }
}
