using ATW.CommonBase.DataAccess.Common;
using ATW.CommonBase.Model.DataAccess;
using ATW.MES.Model.DTOs.Process.ProcessRoute;
using ATW.MES.Model.DTOs.System.BaseData;
using ATW.MES.Model.Entitys.Process.ProcessRoute;
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

    public class BaseProcessDAL : CacheUniteSqlSugarContext<BaseProcessDTO, BaseProcessEntity>
    {

        #region Parameter



        #endregion

        #region 构造函数

        public BaseProcessDAL(ISqlSugarClient sqlSugarClient, CacheUniteSqlSugarRepository cUSR, IMapper iM)
        {
            IM = iM;
            // 沿用示例中的数据库连接名称 "MainDB"，请根据实际情况调整
            DB = sqlSugarClient.AsTenant().GetConnectionScope("MainDB");
            CUSR = cUSR;
        }

        #endregion

    }
}
