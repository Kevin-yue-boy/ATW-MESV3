using ATW.MES.Model.Entitys.Equipment.BaseEquipment;
using ATW.MES.Model.Entitys.Process.ProcessRoute;
using ATW.MES.Model.Entitys.System.BaseData;
using ATW.MES.Model.Test;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.DAL.Global
{
    public class CreateDataBaseDAL
    {

        ISqlSugarClient Db_MES { get; set; }

        public CreateDataBaseDAL(ISqlSugarClient sqlSugarClient)
        {
            Db_MES = sqlSugarClient.AsTenant().GetConnectionScope("MainDB");
        }

        public void StartCreateMESDataBaseTable()
        {

            #region 工艺管理

            #region 工艺路线

            Db_MES.CodeFirst.InitTables(typeof(ProcessRouteBaseEntity));
            Db_MES.CodeFirst.InitTables(typeof(ProcessRouteEntity));

            #endregion

            #endregion

            #region 设备

            #region 设备基础模块

            Db_MES.CodeFirst.InitTables(typeof(EquipmentCommunicateEntity));

            #endregion

            #endregion

            #region 系统管理

            #region 基础数据

            Db_MES.CodeFirst.InitTables(typeof(BaseProcessEntity));
            Db_MES.CodeFirst.InitTables(typeof(BaseProductTypeEntity));
            Db_MES.CodeFirst.InitTables(typeof(BasePLCInterfaceBaseEntity));
            Db_MES.CodeFirst.InitTables(typeof(BaseUnitEntity));
            Db_MES.CodeFirst.InitTables(typeof(BaseWorkTypeEntity));
            Db_MES.CodeFirst.InitTables(typeof(BaseToolTypeEntity));

            #endregion

            #endregion

        }

    }
}
