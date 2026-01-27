using ATW.MES.BLL.Equipment.BaseEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATW.MES.SIS.PLC.Global
{
    public class LoadPLCFlowSIS
    {

        #region Parameter

        /// <summary>
        /// 
        /// </summary>
        private EquipmentCommunicateBLL EquComBLL { get; set; }

        #endregion



        public LoadPLCFlowSIS(EquipmentCommunicateBLL equComBLL)
        {
            this.EquComBLL = equComBLL;
        }

        

    }
}
