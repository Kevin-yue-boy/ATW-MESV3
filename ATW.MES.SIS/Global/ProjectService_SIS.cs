using ATW.CommonBase.Method.Log;
using ATW.CommonBase.Model.Enum;
using ATW.CommonBase.Model.Log;
using ATW.MES.BLL.Equipment.BaseEquipment;
using ATW.MES.DAL.Equipment.BaseEquipment;
using ATW.MES.DAL.Global;
using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.SIS.Global
{
    //System Integration Service
    public class ProjectService_SIS
    {

        #region Parameter

        private EquipmentCommunicateBLL EquComBLL { get; set; }

        #endregion

        #region 构造函数

        public ProjectService_SIS(EquipmentCommunicateBLL equComBLL)
        {
            EquComBLL = equComBLL;
        }

        #endregion

        public async Task StartServicesAsync()
        {
            //加载通讯
            await EquComBLL.LoadALLCommunicateAsync();
            Logger.OperateLog("加载所有通讯完成！");

        }



    }
}
